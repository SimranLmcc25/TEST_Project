<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EBRC_ORM_Checker.aspx.cs" Inherits="EBR_TF_EBRC_ORM_Checker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
   <%-- <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../Scripts/InitEndRequest.js" type="text/javascript"></script>--%>

 <%# "<link href='" + ResolveClientUrl("~/Style/style_new.css") + "' rel='stylesheet' type='text/css' media='screen' />"%>
<%# "<link href='" + ResolveClientUrl("~/Style/Style.css") + "' rel='stylesheet' type='text/css' media='screen' />"%>
<%# "<script src='" + ResolveClientUrl("~/Scripts/Enable_Disable_Opener.js") + "' type='text/javascript'></script>" %>
<%# "<script src='" + ResolveClientUrl("~/Scripts/InitEndRequest.js") + "' type='text/javascript'></script>"%>

    <script type="text/javascript" language="javascript">
        function validateSearch() {
            var _txtvalue = document.getElementById('txtSearch').value;
            _txtvalue = _txtvalue.replace(/'&lt;'/, "");
            _txtvalue = _txtvalue.replace(/'&gt;'/, "");
            if (_txtvalue.indexOf('<!') != -1 || _txtvalue.indexOf('>!') != -1 || _txtvalue.indexOf('!') != -1 || _txtvalue.indexOf('<') != -1 || _txtvalue.indexOf('>') != -1 || _txtvalue.indexOf('|') != -1) {
                alert('!, |, <, and > are not allowed.');
                document.getElementById('txtSearch').value = _txtvalue;
                return false;
            }
            else
                return true;
        }

        function submitForm(event) {
            if (event.keyCode == '13') {
                if (validateSearch() == true)
                    __doPostBack('btnSearch', '');
                else
                    return false;
            }
        }

        function validate() {

            var ddlBranch = document.getElementById('ddlBranch');
            if (ddlBranch.value == "0") {
                alert('Enter Branch Name');
                ddlBranch.focus();
                return false;
            }
        }  
    </script>
    <script type="text/javascript">
         // Select/Deselect checkboxes based on header checkbox
         function SelectheaderCheckboxes(headerchk) {
             debugger
             var gvcheck = document.getElementById('GridViewReturnData');
             var i;
             //Condition to check header checkbox selected or not if that is true checked all checkboxes
             if (headerchk.checked) {
                 for (i = 0; i < gvcheck.rows.length; i++) {
                     var inputs = gvcheck.rows[i].getElementsByTagName('input');
                     inputs[0].checked = true;
                 }
             }
             //if condition fails uncheck all checkboxes in gridview
             else {
                 for (i = 0; i < gvcheck.rows.length; i++) {
                     var inputs = gvcheck.rows[i].getElementsByTagName('input');
                     inputs[0].checked = false;
                 }
             }
         }
         //function to check header checkbox based on child checkboxes condition
         function Selectchildcheckboxes(header) {
             var ck = header;
             var count = 0;
             var gvcheck = document.getElementById('GridViewReturnData');
             var headerchk = document.getElementById(header);
             var rowcount = gvcheck.rows.length;
             //By using this for loop we will count how many checkboxes has checked
             for (i = 1; i < gvcheck.rows.length; i++) {
                 var inputs = gvcheck.rows[i].getElementsByTagName('input');
                 if (inputs[0].checked) {
                     count++;
                 }
             }
             //Condition to check all the checkboxes selected or not
             if (count == rowcount - 1) {
                 headerchk.checked = true;
             }
             else {
                 headerchk.checked = false;
             }
         }
</script>
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            var grid = document.getElementById('GridViewReturnData');
            var checkBoxes = grid.getElementsByTagName("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            var rowCount = 0;
            var rowCount1 = 0;
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked == false) {
                    rowCount++;
                }
                else {
                    rowCount1++;
                }
            }

            if (rowCount1 != 0) {
                if (rowCount1 == 1) {
                    if (confirm("Do you want to approve ORM transaction?")) {
                        confirm_value.value = "Yes";
                    } else {
                        confirm_value.value = "No";
                    }
                    document.forms[0].appendChild(confirm_value);
                }

                if (rowCount1 != 1) {
                    if (confirm("Do you want to approve bulk ORM transaction?")) {
                        confirm_value.value = "Yes";
                    } else {
                        confirm_value.value = "No";
                    }
                    document.forms[0].appendChild(confirm_value);
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
           <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
            <ProgressTemplate>
                <div id="progressBackgroundMain" class="progressBackground">
                    <div id="processMessage" class="progressimageposition">
                    <%--    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />--%>
                    <img src='<%=ResolveClientUrl("~/Images/ajax-loader.gif") %>' style="border: 0px"
                        alt="" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <Triggers>
                    <%-- <asp:PostBackTrigger ControlID="btnAdd" />--%>
                    <%-- <asp:PostBackTrigger ControlID="btnLog" />--%>
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel" style="font-weight: bold">EBRC ORM Data Entry View-Checker</span>
                                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                              &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                               &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                                    <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="150px"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" onclick="btnSearch_Click"  />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                            </td>
                        </tr>

                          <tr align="right" id="trid">
                           <td align="left" style="width: 50%;" valign="top">
                                 <span class="mandatoryField">*</span><span class="elementLabel">From Upload Date :</span>
                                  <asp:TextBox ID="txtfromDate" runat="server" CssClass="textBox" MaxLength="10" 
                                     TabIndex="2" ValidationGroup="dtval" Width="70"></asp:TextBox>
                                 <asp:Button ID="btncalendar_FromDate" runat="server" 
                                     CssClass="btncalendar_enabled" TabIndex="-1" />
                                
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
                                           
                                            &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Upload Date :</span>
                                           
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10"
                                                Width="70" TabIndex="3"></asp:TextBox>
                                            <asp:Button ID="btncalendar_ToDate" runat="server" 
                                                CssClass="btncalendar_enabled" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtToDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="btncalendar_ToDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="mdfdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtToDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                                             <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Search Records"
                                                ToolTip="Search Records" TabIndex="7" onclick="btnSave_Click" />
                                          <%--  <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="false" />--%>
                                      
                                       <span class="elementLabel">ORM Status :</span>
                                      &nbsp;<asp:DropDownList ID="ddlORMstatus" CssClass="dropdownList" AutoPostBack="true"
                                    runat="server" TabIndex="1" onselectedindexchanged="ddlORMstatus_SelectedIndexChanged" 
                                    >
                                     <asp:ListItem Value="0">-Select-</asp:ListItem>
                                     <asp:ListItem Value="1">Fresh</asp:ListItem>
                                     <asp:ListItem Value="2">Amended</asp:ListItem>
                                     <asp:ListItem Value="3">Cancelled</asp:ListItem>
                                </asp:DropDownList>
                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                            </td> 
                           
                        </tr>
                          <tr align="right">
                            <td width="10%" align="left">
                                &nbsp;
                            <asp:RadioButton ID="rdb_Pending" runat="server" Text="Pending" AutoPostBack="true"
                                    GroupName="Status" Font-Bold="true"
                                    Checked="true" oncheckedchanged="rdb_Pending_CheckedChanged" />
                                <asp:RadioButton ID="rdb_Approved" Text="Approved" AutoPostBack="true" GroupName="Status"
                                    runat="server" Font-Bold="true"
                                    ForeColor="GreenYellow" oncheckedchanged="rdb_Approved_CheckedChanged" />
                                     <asp:RadioButton ID="rdb_Reject" runat="server" Text="Rejected" AutoPostBack="true" 
                                    GroupName="Status" ForeColor="Tomato"  Font-Bold="true" oncheckedchanged="rdb_Reject_CheckedChanged" />
                                    <span> (By DGFT)</span>
                                   
                                         <%--      <asp:Button ID="btnapprove" runat="server" CssClass="buttonDefault" 
                                         Text="Approve" onclick="btnapprove_Click"/>--%>
                                    </td>
                              
                        </tr>
                          <tr>
                        <td align="right">
                          <asp:Button ID="btnapprove" runat="server" CssClass="buttonDefault" 
                                         Text="Approve" onclick="btnapprove_Click"/>
                        </td>
                        </tr>
                         
                        <tr>
                            <td align="left" style="width: 50%;" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </tr>
                         <tr align="right">
                        <%-- <td align="right" style="width: 50%;" valign="top">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" onclick="btnSearch_Click"  />
                            </td>  --%>
                        </tr>
              
                        <%----GridView Start---%>
                        <tr id="rowGrid" runat="server">
                            <td align="left" style="width: 100%" valign="top">
                                <asp:GridView ID="GridViewReturnData" runat="server" AutoGenerateColumns="False"
                                    Width="100%" AllowPaging="True" OnRowDataBound="GridViewReturnData_RowDataBound"
                                    OnRowCommand="GridViewReturnData_RowCommand" 
                                   >
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                 
                                  <asp:TemplateField HeaderText="BankUniqueTransactionId" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBankUniqueTransactionId" runat="server" Text='<%#Eval("BankUniqueTransactionId")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                             <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ORMNumber" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblORMNumber" runat="server" Text='<%#Eval("ORMNumber")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                             <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ORMissueDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblORMissueDate" runat="server" Text='<%#Eval("ORMissueDate")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="PaymentDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaymentdate" runat="server" Text='<%#Eval("Paymentdate")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ORNFCC" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblORNFCC" runat="server" Text='<%#Eval("OrnFCC")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ORNFCCAmount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblORNFCCAmount" runat="server" Text='<%#Eval("OrnFCCAmount")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                        </asp:TemplateField>                
                                         <asp:TemplateField HeaderText="ORMStatus" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblORMStatus" runat="server" Text='<%#Eval("ORMStatus")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="API Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAPI_Status" runat="server" Text='<%#Eval("[API_Status]")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DGFT Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDgftSts" runat="server" Text='<%#Eval("[DGFT_Status]")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="1%" 
                                                 ItemStyle-HorizontalAlign="center" ItemStyle-Width="1%">
                                                 <HeaderTemplate> 
                                                     <asp:CheckBox runat="server" ID="chkall" AutoPostBack="true" 
                                                            onclick="javascript:SelectheaderCheckboxes(this)" 
                                                            oncheckedchanged="chkall_CheckedChanged"/>
                                                 </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chkselect" AutoPostBack="true" CommandArgument="View profile" />
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                              
                                         <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" runat="server" ToolTip="Delete Record" CommandName="RemoveRecord"
                                                    CommandArgument='<%#Eval("ORMNumber")%>' Text="Delete" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <%----GridView End---%>
                       <tr id="rowPager" runat="server">
                            <td align="center" style="width: 100%" valign="top" class="gridHeader">
                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" style="width: 25%">
                                                &nbsp;Records per page:&nbsp;
                                                <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 50%" valign="top">
                                                <asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click" />
                                                <asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Previous" OnClick="btnnavpre_Click" />
                                                <asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click" />
                                                <asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click" />
                                            </td>
                                            <td align="right" style="width: 25%;">
                                                &nbsp;<asp:Label ID="lblpageno" runat="server"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblrecordno" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
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
