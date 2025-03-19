<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CBWT_TransFileUpload_CSV.aspx.cs"
    Inherits="CBWT_TransFileUpload_CSV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
     <title>LMCC- CBWT System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script type="text/javascript" src="Scripts/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.uploadify.js"></script>
    <script type="text/javascript" language="javascript">
        function confirm_proceed() {
            
            var monthyr = document.getElementById('txtYearMonth');
            var fileinhouse = document.getElementById('fileinhouse');
            var ddlBranch = document.getElementById('ddlBranch');

            if (ddlBranch.value == '0') {
                alert('Please Select Ref No.');
                ddlBranch.focus();
                return false;
            }

            if (monthyr.value == '__/____' || monthyr.value.substring(0, 2) > 12) {
                alert('Enter valid Month/Year.');
                monthyr.focus();
                return false;
            }

            if (fileinhouse.value == '') {
                alert('Please Select File.');
                fileinhouse.focus();
                return false;
            }

            //====== to display update progress while uploading file
            if ($get('fileinhouse').value.length > 0) {
                $get('updateProgress').style.display = 'block';
            }

            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnupload">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/InitEndRequest.js"></script>
   <div> 
   
   <center>
   
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanelMain">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="Images/ajax-loader.gif" style="border: 0px" alt="please wait..."/>
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
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnupload" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <asp:Label ID="pgLabel" runat="server" CssClass="pageLabel">Transaction CSV File Upload</asp:Label>
                            </td>
                            <td align="right" style="width: 50%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Ref No. :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" runat="server" TabIndex="1"
                                                Width="80px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">For Month/Year :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtYearMonth" runat="server" MaxLength="7" Width="50px" CssClass="textBox"
                                                TabIndex="2"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" ClearMaskOnLostFocus="false"
                                                CultureDateFormat="MY" Enabled="true" ErrorTooltipEnabled="True" Mask="99/9999"
                                                MaskType="None" TargetControlID="txtYearMonth">
                                            </ajaxToolkit:MaskedEditExtender>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="right" style="width: 150px">
                                            <span class="elementLabel">Select File :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:FileUpload ID="fileinhouse" runat="server" contenteditable="false" Height="17px"
                                                Width="350" TabIndex="3" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <table border="1" bordercolor="red" cellpadding="2">
                                                <tr>
                                                    <td width="500px" align="center">
                                                        <asp:Label ID="lblupload" class="elementLabel" Style="font-size: small" runat="server">Do you want to upload NOW ?</asp:Label><br />
                                                        <asp:Button ID="btnupload" runat="server" Text="YES" OnClick="btnupload_Click" CssClass="buttonDefault"
                                                            TabIndex="6" OnClientClick="return confirm_proceed();" />
                                                        <asp:Button ID="btnNo" runat="server" Text="NO" CssClass="buttonDefault" TabIndex="5"
                                                            OnClick="btnNo_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr height="60px" valign="bottom">
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="labelMessage" runat="server" CssClass="pageLabel"></asp:Label>
                                            <asp:Label ID="lbltest" runat="server" CssClass="pageLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <%--   <div id="list" runat="server" align="left" style="position: relative; left: 50px;
                        width: 90%">
                    </div>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
<%--
<script type="text/javascript" language="javascript">
    $(window).load(
function () {
    $("#<%=fileinhouse.ClientID%>").fileUpload({
        'fileExt': '*.csv;'
    });
}
);

</script>--%>
