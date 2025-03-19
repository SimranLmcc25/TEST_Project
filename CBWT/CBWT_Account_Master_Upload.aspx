<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CBWT_Account_Master_Upload.aspx.cs"
    Inherits="CBWT_Account_Master_Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC- CBWT System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        
        function validateSave() {

            var AccountMasterfilelen = document.getElementById('FileAccountMaster').value.length;
            var strext = document.getElementById('FileAccountMaster').value.substring(AccountMasterfilelen - 4, AccountMasterfilelen);
            var Account_Master_filename = document.getElementById('FileAccountMaster').value.substring(AccountMasterfilelen - 17, AccountMasterfilelen);

            var Individualfilelen = document.getElementById('FileIndividualMaster').value.length;
            var Individualfileext = document.getElementById('FileIndividualMaster').value.substring(Individualfilelen - 4, Individualfilelen);
            var Individualfilename = document.getElementById('FileIndividualMaster').value.substring(Individualfilelen - 20, Individualfilelen);

            Individualfileext = Individualfileext.toLowerCase();

            var Legalfilelen = document.getElementById('FileLegalPerson').value.length;
            var Legalfileext = document.getElementById('FileLegalPerson').value.substring(Legalfilelen - 4, Legalfilelen);
            var Legalfilename = document.getElementById('FileLegalPerson').value.substring(Legalfilelen - 17, Legalfilelen);

            Legalfileext = Legalfileext.toLowerCase();


            if (document.getElementById('FileAccountMaster').value == "") {
                alert('Please Select Account Master file to upload.');
                try {
                    document.getElementById('FileAccountMaster').focus();
                    return false;
                }
                catch (err) {
                    return false;
                }
            }
            else {
                strext = strext.toLowerCase();
                if (strext != '.csv') {
                    alert('Invalid file format.');
                    return false;
                }
            }
            if (document.getElementById('FileIndividualMaster').value == '' && document.getElementById('FileLegalPerson').value == '') {
                alert('Please Select Individual or Legal File to upload.')
                return false;
            } else if (document.getElementById('FileIndividualMaster').value != '') {
                if (Individualfileext != '.csv') {
                    alert('Invalid File format of Individual Info File.');
                    document.getElementById('FileIndividualMaster').focus();
                    return false
                }
            }
            else if (document.getElementById('FileLegalPerson').value != '') {
                if (Legalfileext != '.csv') {
                    alert('Invalid File format of Legal Entity File.');
                    document.getElementById('FileLegalPerson').focus();
                    return false;
                }
            }

            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnupload">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="Images/ajax-loader.gif" style="border: 0px" alt="" />
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
                    <asp:PostBackTrigger ControlID="btnupload" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">Account Master Data Upload </span>
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
                                    <tr valign="top">
                                        <td align="right" style="width: 200px">
                                            <span class="elementLabel">Select Account Master File :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:FileUpload ID="FileAccountMaster" runat="server" contenteditable="false"
                                                Height="17px" Width="470" TabIndex="1" />
                                            <asp:Label ID="lblAccountMaster" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="right" style="width: 200px">
                                            <span class="elementLabel">Select Individual Master File :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:FileUpload ID="FileIndividualMaster" runat="server" contenteditable="false"
                                                Height="17px" Width="470" TabIndex="2" />
                                            <asp:Label ID="lblIndividualMaster" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="right" style="width: 200px">
                                            <span class="elementLabel">Select Legal Entity Master File :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:FileUpload ID="FileLegalPerson" runat="server" contenteditable="false"
                                                Height="17px" Width="470px" TabIndex="3" />
                                            <asp:Label ID="lblLegalFile" runat="server" CssClass="elementLabel"></asp:Label>
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
                                                        <span class="elementLabel" style="font-size: small">Do you want to upload NOW ?</span><br />
                                                        <asp:Button ID="btnupload" runat="server" Text="YES" OnClick="btnupload_Click" CssClass="buttonDefault"
                                                            TabIndex="6" />
                                                        <asp:Button ID="btnNo" runat="server" Text="NO" CssClass="buttonDefault" TabIndex="5"
                                                            OnClick="btnNo_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr valign="bottom">
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="labelMessage" runat="server" CssClass="pageLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <div id="list" runat="server" align="left" style="position: relative; left: 50px;
                        width: 90%">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
