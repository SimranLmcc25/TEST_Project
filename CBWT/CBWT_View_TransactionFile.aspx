<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CBWT_View_TransactionFile.aspx.cs"
    Inherits="CBWT_View_TransactionFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="header1" runat="server">
    <title>LMCC- CBWT System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script type="text/javascript" language="javascript">
        function val_MonthYear() {
            var txtYearMonth = document.getElementById('txtYearMonth');

            if (txtYearMonth.value == '__/____') {
                alert('Enter Month Year');
                txtYearMonth.focus();
                return false;
            }


            if (parseFloat(txtYearMonth.value.substring(0, 2)) > 12) {

                alert("Invalid Month");
                txtYearMonth.focus();
                return false;
            }
            return true;
        }
        function isValidDate(controlID, CName) {
            var obj = controlID;
            var today = new Date();

            if (controlID.value != "__/____") {

                var month = obj.value.split("/")[0];
                var year = obj.value.split("/")[1];

                if (month == "__") {
                    month = today.getMonth() + 1;
                }
                if (year == "____") {
                    year = today.getFullYear();
                }
                var dt = new Date(year, month - 1);

                if ((dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {

                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" valign="bottom" colspan="2">
                                <span class="pageLabel" style="font-weight: bold">Transaction File Data Entry View</span>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnAdd" runat="server" CssClass="buttonDefault" Text="Add New" OnClick="btnAdd_Click"
                                    TabIndex="5" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="3">
                                <hr />
                            </td>
                        </tr>
                        <tr align="right">
                            <td align="right" width="10%">
                                <span class="elementLabel">Ref Code</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlRefNo" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                    Width="80px" OnSelectedIndexChanged="ddlRefNo_SelectedIndexChanged" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td align="right" style="width: 50%;" valign="top">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px" TabIndex="3"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" OnClick="btnSearch_Click" TabIndex="4" />
                            </td>
                        </tr>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">Month Year : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtYearMonth" runat="server" AutoPostBack="true" CssClass="textBox"
                                    OnTextChanged="txtYearMonth_TextChanged" Width="50px" TabIndex="2"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdApplicationDate" runat="server" ClearMaskOnLostFocus="false"
                                    CultureDateFormat="MY" Enabled="true" ErrorTooltipEnabled="True" Mask="99/9999"
                                    MaskType="None" TargetControlID="txtYearMonth">
                                </ajaxToolkit:MaskedEditExtender>
                            </td>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                </td>
                            </tr>
                        </tr>
                    </table>
                    <table cellspacing="0" border="0" width="100%">
                        <tr id="rowGrid" runat="server">
                            <td align="left" style="width: 100%" valign="top">
                                <asp:GridView ID="GridViewBankCodeList" runat="server" AutoGenerateColumns="False"
                                    Width="100%" AllowPaging="True" OnRowCommand="GridViewBankCodeList_RowCommand"
                                    OnRowDataBound="GridViewBankCodeList_RowDataBound">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SRNO") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transaction Ref No" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransactionRefNo" runat="server" Text='<%# Eval("TRANS_REF_NO") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Transaction Date" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TRAN_DATE") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remitter A/C No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerACNo" runat="server" Text='<%# Eval("RemitterID") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remitter Name" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%" ItemStyle-Width="30%"
                                            HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("RemitterName") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Beneficary A/C No" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                            HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBeneID" runat="server" Text='<%# Eval("BeneficiaryID") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Beneficiary Name" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="40%" ItemStyle-Width="40%"
                                            HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBeneName" runat="server" Text='<%# Eval("BeneName") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  HeaderText="Delete"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("RefNo")+";"+Eval("YEARMONTH")+";"+Eval("SRNO") %>'
                                                    CommandName="RemoveRecord" Text="Delete" ToolTip="Delete Record" CssClass="deleteButton" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
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
