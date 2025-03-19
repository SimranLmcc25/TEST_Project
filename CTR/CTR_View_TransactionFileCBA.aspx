<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTR_View_TransactionFileCBA.aspx.cs"
    Inherits="CTR_CTR_View_TransactionFileCBA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="header1" runat="server">
    <title>LMCC-CTR System</title>
    <link id="Link1" runat="server" rel="Shortcut Icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="Stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" language="javascript">
        function TransID() {
            var txtID = document.getElementById('txtID');

            if (txtID.value == "") {
                txtID.value = 1;
            }
            var TransLen = txtID.value.length;
            for (i = TransLen; i < 4; i++) {
                txtID.value = "0" + txtID.value;
            }
        }
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
        function checkYear() {

            var d = new Date();
            var docYear = document.getElementById('txtyear');
            var docYearLen = docYear.value.length;

            if (docYearLen > 3) {

                if (parseFloat(docYear.value) > 1990 && parseFloat(docYear.value) < 2050) {
                    return false;
                }
                else
                    docYear.value = d.getFullYear();
            }

            else
                docYear.value = d.getFullYear();
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
                    <table width="100%" cellpadding="0" border="0">
                        <tr>
                            <td align="left" colspan="2" valign="bottom">
                                <span class="pageLabel" style="font-weight: bold">CBA Transaction File Data Entry</span>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnAdd" runat="server" CssClass="buttonDefault" Text="Add New" 
                                    onclick="btnAdd_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="3">
                        <hr />
                          </td>
                        </tr>
                        <tr>
                            <td align="right" width="5%">
                                <span class="elementLabel">Branch : </span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlbranch" runat="server" CssClass="dropdownList" 
                                    Width="100px" onselectedindexchanged="ddlbranch_SelectedIndexChanged" AutoPostBack=true>
                                </asp:DropDownList>
                            </td>
                            <td align="right" style="width: 50%;" valign="top">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px" TabIndex="3"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" OnClick="btnSearch_Click" TabIndex="4" />
                            </td>
                        </tr>
                        <tr>
                        <td align=right nowrap>
                        <span class=elementLabel>Transaction ID : </span></td>
                        <td align=left colspan=2>
                        <asp:TextBox ID=txtCTR runat=server CssClass=textBox Text="CTR" Enabled=false Width=40px></asp:TextBox>
                        <asp:TextBox ID=txtID runat=server CssClass=textBox Width=40px MaxLength=4 onblur="TransID();"></asp:TextBox>
                        <asp:TextBox ID=txtbranch runat=server CssClass=textBox Width=40px MaxLength=5 Enabled=false></asp:TextBox>
                        <asp:TextBox ID=txtyear runat=server CssClass=textBox Width=40px MaxLength=4 
                                ontextchanged="txtyear_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                            <td align="left" colspan=3>
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                         <tr id="rowGrid" runat="server">
                            <td align="left" style="width: 100%" valign="top" colspan=3>
                                <asp:GridView ID="GridViewTransactionFile" runat="server" AutoGenerateColumns="False"
                                    Width="100%" AllowPaging="True" 
                                    onrowdatabound="GridViewTransactionFile_RowDataBound" 
                                    onrowcommand="GridViewTransactionFile_RowCommand" >
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                     <asp:TemplateField HeaderText="Ref No" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Eval("RefNo") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transaction Date" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransDate") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transaction ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransID" runat="server" Text='<%# Eval("TransID") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Account Number" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAcNo" runat="server" Text='<%# Eval("AcNo") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name of Account" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="30%" ItemStyle-Width="30%" HeaderStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNameofAccount" runat="server" Text='<%# Eval("Name") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center"  HeaderText="Delete"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("RefNo")+";"+Eval("TransID")+";"+Eval("AcNo") %>'
                                                    CommandName="RemoveRecord" Text="Delete" ToolTip="Delete Record" CssClass="deleteButton" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="rowPager" runat="server">
                            <td align="center" style="width: 100%" valign="top" class="gridHeader" 
                                colspan=3>
                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" style="width: 25%">
                                                &nbsp;Records per page:&nbsp;
                                                <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" 
                                                    AutoPostBack="true" 
                                                    onselectedindexchanged="ddlrecordperpage_SelectedIndexChanged">
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
    <script language=javascript type="text/javascript">
        window.onload = function () {
            TransID();
        }
    </script>
</body>
</html>
