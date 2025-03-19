<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTR_Cummulative_Acc_View.aspx.cs"
    Inherits="TRF_CTR_Cummulative_Acc_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC - CTR System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function val_MonthYear() {
            var txtmontYear = document.getElementById('txtmontYear');
           // alert(1);
            if (txtmontYear.value == '__/____') {

                alert('Enter Month Year');
                txtmontYear.focus();
                return false;
            }

            if (parseFloat(txtmontYear.value.substring(0, 2)) > '12' || parseFloat(txtmontYear.value.substring(0, 2)) <= '00') {

                alert("Invalid Month");
                txtmontYear.focus();
                return false;
            }
            return true;
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
    <form id="frmEmployeeMaster" runat="server" autocomplete="on">
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
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" colspan="2">
                            <span class="pageLabel"><strong>Cummulative Total Data Entry View</strong></span>
                        </td>
                        <td align="right" width="25%">
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Record"
                                OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <span class="elementLabel">Branch : </span>
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="dropdownList" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                AutoPostBack="true" TabIndex="1">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp; <span class="elementLabel">Month/Year : </span>
                            <asp:TextBox ID="txtmontYear" runat="server" Width="50px" CssClass="textBox" OnTextChanged="txtmontYear_TextChanged" AutoPostBack="true" TabIndex="2"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdApplicationDate" runat="server" ClearMaskOnLostFocus="false"
                             CultureDateFormat="MY"  Enabled="true" ErrorTooltipEnabled="True" Mask="99/9999"
                                MaskType="None" TargetControlID="txtmontYear">
                            </ajaxToolkit:MaskedEditExtender>

                          
                        </td>
                        <td align="right" valign="top" colspan="2" nowrap>
                            <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                Class="textBox" MaxLength="40" Width="180px" TabIndex="3"></asp:TextBox>
                            <asp:Button ID="Button1" runat="server" Text="Go" CssClass="buttonDefault" TabIndex="4"
                                ToolTip="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr id="rowGrid" runat="server">
                        <td colspan="3">
                            <asp:GridView ID="GridViewCummTotalEntry" runat="server" AutoGenerateColumns="false"
                                Width="100%" AllowPaging="true" OnRowCommand="GridViewCummTotalEntry_RowCommand"
                                OnRowDataBound="GridViewCummTotalEntry_RowDataBound">
                                <PagerSettings Visible="false" />
                                <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                <AlternatingRowStyle Wrap="false" HorizontalAlign="left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridAlternateItem" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Account Number" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAcNo" runat="server" Text='<%# Eval("AcNo") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Name of Account" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cummulative Credit" HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-HorizontalAlign="right" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCumCredit" runat="server" Text='<%# Eval("CumCredit") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cummulative Deposit" HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-HorizontalAlign="right" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCumDeposit" runat="server" Text='<%# Eval("CumDeposit") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cummulative Withdraw" HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-HorizontalAlign="right" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCumWithdraw" runat="server" Text='<%# Eval("CumWithdraw") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Report Type" HeaderStyle-HorizontalAlign="right"
                                        ItemStyle-HorizontalAlign="right" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReportType" runat="server" Text='<%# Eval("TransType") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Delete" ItemStyle-HorizontalAlign="Center" 
                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("AcNo")+";"+Eval("TransType") %>' CommandName="RemoveRecord"
                                                Text="Delete" ToolTip="Delete Record" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="rowPager" runat="server">
                        <td align="center" style="width: 100%" valign="top" colspan="3" class="gridHeader">
                            <table cellspacing="0" cellpadding="2" width="100%" border="0" class="gridHeader">
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
