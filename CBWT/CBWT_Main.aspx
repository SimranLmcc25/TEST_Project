<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CBWT_Main.aspx.cs" Inherits="CBWT_Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    <%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC- CBWT System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon"/>
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico"/>
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <div style="white-space: nowrap;">
            <center>
                <uc1:menu ID="Menu1" runat="server" />
           </center>
        </div>
    </form>
</body>
</html>
