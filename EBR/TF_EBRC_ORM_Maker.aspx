<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EBRC_ORM_Maker.aspx.cs" Inherits="EBR_TF_EBRC_ORM_Maker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-EBRC SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
<%--    <script src="../Scripts/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <link href="../Scripts/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Scripts/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <script src="../Scripts/MyJquery1.js" language="javascript" type="text/javascript"></script>
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>--%>
    <script type="text/javascript" language="javascript">

        function confirmmessage() {
            var filename = document.getElementById('FileUpload1').value;
            var ddlBranch = document.getElementById('ddlBranch').value;
            var branchname = $("#ddlBranch option:selected").text();
            if (filename == '') {
                Alert('Please Select File.')
                return false;
            }
            if (branchname == '---Select---') {
                Alert('Select Branch')
                return false;
            }

        }  

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div id="dialog" class="AlertJqueryHide">
        <p id="Paragraph">
        </p>
    </div>
  <%--  <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>--%>
 <asp:Literal runat="server" ID="jsScript1"></asp:Literal>
<asp:Literal runat="server" ID="jsScript2"></asp:Literal>
<asp:Literal runat="server" ID="cssLink"></asp:Literal>
<asp:Literal runat="server" ID="jsScript3"></asp:Literal>
<asp:Literal runat="server" ID="jsScript4"></asp:Literal>
<asp:Literal runat="server" ID="jsScript5"></asp:Literal>

    <div>
        <center>
               <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanelMain">
                <ProgressTemplate>
                    <div id="progressBackgroundMain" class="progressBackground">
                        <div id="processMessage" class="progressimageposition">
                            <img src="../Images/ajax-loader.gif" style="border: 0px" alt="please wait..." />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </center>
    </div>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <asp:Label ID="pgLabel" runat="server" CssClass="pageLabel" Style="font-weight: bold">EBRC ORM Data Entry View-Maker</asp:Label>
                            </td>
                        
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                            </td>
                        </tr>
                      <%--  <tr>
                            <td align="left" style="width: 100%; height: 21px;" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>--%>
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
                                                ToolTip="Search Records" TabIndex="7" 
                                     onclick="btnSave_Click"   />
                                         <%--   <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
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

                              
                            </td> 
                        </tr>
                         <tr align="right">
                         <%--   <td align="left" style="width: 50%;" valign="top">
                                &nbsp;<span class="elementLabel">Branch :</span> &nbsp;
                               
                            </td>--%>
                            <td align="right" style="width: 50%;" valign="top">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" onclick="btnSearch_Click"  />
                            </td>
                        </tr>
                         <tr>
                            <td align="left" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                      
                        <!-------------------GridView start------------------->
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
                                                <asp:Label ID="lblBankUniqueTransactionIdr" runat="server" Text='<%#Eval("BankUniqueTransactionId")%>' CssClass="elementLabel"></asp:Label>
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
                                          
                                           <%--  <asp:TemplateField HeaderText="Rejecetd By" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRejectedByUser" runat="server" Text='<%#Eval("RejectedByUser")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Rejecetd On" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRejected_Date" runat="server" Text='<%#Eval("Rejected_Date")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>--%>
                                                       
                                         <asp:TemplateField HeaderText="ORMStatus" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblORMStatus" runat="server" Text='<%#Eval("ORMStatus")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" Visible="false" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <!-------------------GridView End------------------->
                                  <tr id="rowPager" runat="server">
                            <td align="center" style="width: 100%" valign="top" class="gridHeader">
                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" style="width: 25%">
                                                &nbsp;Records per page:&nbsp;
                                                <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true">
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
