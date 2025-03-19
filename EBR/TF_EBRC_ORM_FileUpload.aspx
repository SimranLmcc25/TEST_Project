<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EBRC_ORM_FileUpload.aspx.cs" Inherits="EBR_TF_EBRC_ORM_FileUpload" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-EBRC SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />

   <%-- <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
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

        function Validate(Result) {
            var ddlBranch = document.getElementById('ddlBranch').value;
            var branchname = $("#ddlBranch option:selected").text();
            $("#Paragraph").text(Result);
            $("#dialog").dialog({
                title: "Message From LMCC",
                width: 520,
                modal: true,
                closeOnEscape: true,
                dialogClass: "AlertJqueryDisplay",
                hide: { effect: "close", duration: 400 },
                buttons: [
                    {
                        text: "Ok",
                        icon: "ui-icon-heart",
                        click: function () {
                            $(this).dialog("close");

                            popup = window.open('EBRC_Rpt_Data_Validation.aspx?Branch=' + branchname, '', '_blank', 'height=600,  width=900,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100');

                            $("").focus();
                        }
                    }
                  ]
            });
            $('.ui-dialog :button').blur();
            return false;
        }


      

    </script>

      <script type="text/javascript">
          function CallConfirmBox() {

              $("[id$=btnExport]").click();

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
     <script language="javascript" type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/InitEndRequest.js") %>'></script>
    <script src='<%=ResolveClientUrl("~/Scripts/Enable_Disable_Opener.js") %>' type="text/javascript"></script>
    <div>
        <center>
            <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanelMain">
                <ProgressTemplate>
                    <div id="progressBackgroundMain" class="progressBackground">
                        <div id="processMessage" class="progressimageposition">
                           <%-- <img src="../Images/ajax-loader.gif" style="border: 0px" alt="please wait..." />--%>
                            <img src='<%=ResolveClientUrl("~/Images/ajax-loader.gif") %>' style="border: 0px"
                        alt="please wait..." />
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
                    <asp:PostBackTrigger ControlID="btnExport" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <asp:Label ID="pgLabel" runat="server" CssClass="pageLabel" Style="font-weight: bold">EBRC ORM File Upload</asp:Label>
                            </td>
                            <td align="right" style="width: 50%">
                                &nbsp;
                            </td>
                        </tr>
                           <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <asp:Label ID="lblmessage" Font-Bold="true" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                                <input type="hidden" id="hdnbranchcode" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr Style="display: none;" >
                                        <td align="right">
                                            <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" Style="display: none;" TabIndex="1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Select File : </span>
                                        </td>
                                        <td align="left">
                                            <asp:FileUpload ID="FileUpload1" runat="server" Width="500" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Input File :</span>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtInputFile" runat="server" CssClass="textBox" MaxLength="10" Width="413px"
                                                TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <span class="pageLabel"><strong>Do you want to upload now? </strong></span>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btnupload" CssClass="buttonDefault" Text="Upload" 
                                                runat="server" onclick="btnupload_Click"
                                              />  <%--OnClientClick="var b = confirmmessage(); if (b) ShowProgress(); return b"--%>
                                            <asp:Button ID="btnValidate" CssClass="buttonDefault" Text="Validate" 
                                                runat="server" onclick="btnValidate_Click"
                                                />
                                            <asp:Button ID="btnProcess" CssClass="buttonDefault" Text="Process" 
                                                runat="server" onclick="btnProcess_Click"
                                               />
                                                 <asp:Button ID="btnExport" runat="server" CssClass="buttonDefault" 
                                                Text="Export" Style="display: none;" onclick="btnExport_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblHint" runat="server" CssClass="pageLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr height="60px" valign="bottom">
                                        <td>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="labelMessage" runat="server" CssClass="pageLabel"></asp:Label>
                                            <asp:Label ID="lbltest" runat="server" CssClass="pageLabel"></asp:Label>
                                        </td>
                                    </tr>
                               
                                </table>
                                <br />
                            </td>
                        </tr>
                    </table>
                   
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
      <asp:Label runat="server" ID="lblLog" Visible="false"></asp:Label>
    </form>
</body>
</html>
