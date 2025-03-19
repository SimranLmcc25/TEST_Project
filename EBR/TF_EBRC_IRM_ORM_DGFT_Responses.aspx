<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EBRC_IRM_ORM_DGFT_Responses.aspx.cs" Inherits="EBR_TF_EBRC_IRM_ORM_DGFT_Responses" %>

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
    <script src="../Scripts/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <link href="../Scripts/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Scripts/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <script src="../Scripts/MyJquery1.js" language="javascript" type="text/javascript"></script>
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
     <script language="javascript" type="text/javascript">
         function openIrmOrmNo(e, hNo) {
             var keycode;
             if (keycode == 113 || e == 'mouseClick') {
                 var txttype = document.getElementById('ddltype');
                 open_popup('../TF_EBRC_IRMORM_StatusLookUp.aspx?hNo=' + hNo + '&Type=' + txttype.value, 500, 500, 'number');
                 return false;
             }
             return true;
         }

         function selectNumber(id, hNo) {
             var number = document.getElementById('txtnumber');
             if (hNo == '1') {
                 number.value = id;
                 __doPostBack('number', '');
                 return true;
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
        <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
        <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
        <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
            <ProgressTemplate>
                <div id="progressBackgroundMain" class="progressBackground">
                    <div id="processMessage" class="progressimageposition">
                        <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table width="100%" cellpadding="1">
                    <tr>
                        <td align="left">
                            <span class="pageLabel"><strong>EBRC IRM/ORM DGFT Responses</strong></span>
                        </td>
                    </tr>
                   <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                            </td>
                        </tr>
                    <tr>
                        <td style="text-align: left">
                           <span class="mandatoryField">*</span> <span class="elementLabel">Select Type : </span>
                            <asp:DropDownList runat="server" ID="ddltype" CssClass="dropdownList" 
                                AutoPostBack="true" onselectedindexchanged="ddltype_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">IRM</asp:ListItem>
                                <asp:ListItem Value="2">ORM</asp:ListItem>
                            </asp:DropDownList>
                             &nbsp;  <span class="mandatoryField">*</span> <span class="elementLabel">Approval Date :</span>
                           <asp:TextBox ID="txtfromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtval"
                                                Width="70" TabIndex="2" AutoPostBack="true" 
                                ontextchanged="txtfromDate_TextChanged"></asp:TextBox>
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
                                &nbsp;&nbsp;
                                <span class="mandatoryField">*</span> <span class="elementLabel">IRM/ORM Status :</span>
                            <asp:DropDownList ID="ddlirmormstatus" runat="server" AutoPostBack="true" 
                                CssClass="dropdownList" Width="124px" 
                                onselectedindexchanged="ddlirmormstatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Fresh</asp:ListItem>
                                <asp:ListItem Value="2">Amended</asp:ListItem>
                                <asp:ListItem Value="3">Cancelled</asp:ListItem>
                            </asp:DropDownList>
   
                            
                        </td>

                     </tr>
                     <tr>
                      <td style="text-align: left">
                      <asp:RadioButton ID="rdb_All" Text="All" AutoPostBack="true" GroupName="Status"
                                    runat="server" Font-Bold="true" Visible="true" Checked="true" 
                                oncheckedchanged="rdb_All_CheckedChanged"/>
                                &nbsp;
                                 <asp:RadioButton ID="rdb_Processed" Text="Processed" AutoPostBack="true" GroupName="Status"
                                    runat="server" Font-Bold="true" Visible="true" ForeColor="yellowgreen" 
                                oncheckedchanged="rdb_Processed_CheckedChanged"/>
                                &nbsp;
                                 <asp:RadioButton ID="rdb_Errored" Text="Errored" AutoPostBack="true" GroupName="Status"
                                    runat="server" Font-Bold="true" ForeColor="red" Visible="true" 
                                oncheckedchanged="rdb_Errored_CheckedChanged" />
                     </td>
                     </tr> 
                   
                        <tr>
                            <td align="left" style="width: 50%;" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label></td>
                        </tr>
                         <%----GridView Start---%>
                        <tr id="rowGrid" runat="server">
                            <td align="left" style="width: 100%" valign="top">
                                <asp:GridView ID="GridViewReturnData" runat="server" AutoGenerateColumns="False"
                                    Width="100%" AllowPaging="True">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                    

                                        <asp:TemplateField HeaderText="BankUniqueTxtID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbluniquetxid" runat="server" Text='<%#Eval("BankUniqueTransactionId")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblirmormNumber" runat="server" Text='<%#Eval("irmormNumber")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                             <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblirmormIssueDate" runat="server" Text='<%#Eval("irmormIssueDate")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" 
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblirmormstatus" runat="server" CssClass="elementLabel" 
                                                        Text='<%# Eval("Status_") %>'></asp:Label>
                                                </ItemTemplate>
                                                   <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                            </asp:TemplateField>

                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" 
                                                HeaderText="API_Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAPI_Status" runat="server" CssClass="elementLabel" 
                                                        Text='<%# Eval("[API_Status]") %>'></asp:Label>
                                                </ItemTemplate>
                                               <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                            </asp:TemplateField>

                                          <asp:TemplateField HeaderText="ackStatus" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblackStatus" runat="server" Text='<%#Eval("ackStatus")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="errorCode" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblerrorCode" runat="server" Text='<%#Eval("errorCode")%>' CssClass="elementLabel">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                        </asp:TemplateField>
                                   
                                            <asp:TemplateField HeaderText="errorDetails" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblerrorDetails" runat="server" Text='<%#Eval("errorDetails")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                            <ItemStyle HorizontalAlign="Center" />
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
    </div>
    </form>
</body>
</html>
