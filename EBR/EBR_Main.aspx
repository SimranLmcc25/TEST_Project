<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EBR_Main.aspx.cs" Inherits="EBR_EBR_Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <%-- <script type="text/javascript"  src="../Scripts/jquery-1.8.3.min.js"></script>--%>
    <%-- <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/jquery-1.8.3.min.js") %>'></script>--%>
    <link href="../Style/Style_Service.css" rel="Stylesheet" type="text/css" media="screen" />
    <%--<script src="../Scripts/highcharts.js" type="text/javascript"></script>
    <script src="../Scripts/accessibility.js" type="text/javascript"></script>
    <script src="../Scripts/export-data.js" type="text/javascript"></script>
    <script src="../Scripts/exporting.js" type="text/javascript"></script>--%>


    <script src="https://code.highcharts.com/highcharts.js" type="text/javascript"></script>
    <script src="https://code.highcharts.com/modules/exporting.js" type="text/javascript"></script>
    <script src="https://code.highcharts.com/modules/export-data.js" type="text/javascript"></script>
    <script src="https://code.highcharts.com/modules/accessibility.js" type="text/javascript"></script>

    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function() {
            var data = <%= GetChartData() %>;
            renderChart(data);
        });

        function renderChart(data) {
            Highcharts.chart('container', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: 'IRM Record Count'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.y}</b>'
                },
                accessibility: {
                    point: {
                        valueSuffix: ''
                    }
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.y}'
                        }
                    }
                },
                series: [{
                    name: 'Records',
                    colorByPoint: true,
                    data: data
                }]
            });
        } 

    </script>
    <style type="text/css">
        .style2
        {
            width: 759px;
        }
        .style4
        {
            width: 588px;
        }
        
        .divc
        {
            width: 100%;
            overflow-x: auto; /* Allow horizontal scrolling if needed */
            display: flex;
            justify-content: center; /* Center the flex-container horizontally */
            padding: 20px; /* Add some padding to the container */
            box-sizing: border-box; /* Include padding in the container's width */
            margin-top: -31px;
        }
        .flex-container
        {
            display: flex;
            flex-wrap: nowrap;
            gap: 20px;
            justify-content: flex-start;
        }
        .flex-container .box
        {
            background-color: #ffffff;
            width: 134px;
            height: 100px;
            display: flex;
            flex-direction: column;
            justify-content: center;
            border: 1px solid #e6edf1;
            border-radius: 5px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            transition: transform 0.3s;
            text-align: center;
            position: relative; /* Positioning context for absolute elements */
        }
        .flex-container .box:hover
        {
            transform: translateY(-5px);
        }
        .textcl
        {
            color: #666;
            font-size: 14px;
            margin: 0;
            position: absolute;
            top: 10px;
            left: 16px;
        }
        .value
        {
            color: #333;
            font-size: 24px;
            font-weight: bold;
            margin: 0;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }
        
        
        .classwidth
        {
            width: 290px;
        }
        .clsdt
        {
            margin-left: 148px;
            text-align: left;
            margin-top: 12px;
            border-bottom: 4px solid #FFAC1C;
            width: 100px;
        }
    </style>
    <style type="text/css">
        .card2
        {
            width: 554px;
            height: 361px;
            background-color: #f8f8ff;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: rgba(0, 0, 0, 0.1) 0px 0px 5px 0px, rgba(0, 0, 0, 0.1) 0px 0px 1px 0px;
            transition: all 0.6s ease;
            margin-left: 19px;
            margin-bottom: 136px;
            float: right;
            margin-top: -18px;
            height: 338px;
        }
        
        .card2:hover
        {
            box-shadow: rgba(255, 172, 28, 0.199) -10px 10px, rgba(255, 172, 28, 0.19) -20px 20px;
        }
        
        .card2 .img
        {
            height: 40%;
            background: url("https://cdn.pixabay.com/photo/2015/07/28/20/55/tools-864983_1280.jpg");
            background-size: cover;
        }
        
        .card2 .content
        {
            display: flex;
            flex-direction: column;
            padding: 20px;
            font-weight: 300;
        }
        
        .card2 .content h3
        {
            width: 200px;
            font-weight: 300;
            font-size: 25px;
            padding-bottom: 5px;
            border-bottom: 4px solid #FFAC1C;
            color: #150652;
            margin-left: 156px;
            margin-top: 5px;
        }
        
        .lblclass
        {
            width: 509px;
            font-size: 17px;
            margin-top: -8px;
            margin-bottom: 5px;
            text-align: left;
            margin-left: 6px;
        }
        .lblcss
        {
            font-size: 22px;
            padding-bottom: 5px;
            border-bottom: 4px solid #FFAC1C;
            color: #150652;
        }
        
        .card2 .content a
        {
            list-style-type: none;
            text-decoration: none;
        }
        
        .card2 .content button
        {
            font-family: 'Londrina Solid' , cursive;
            font-weight: 100;
            font-size: 20px;
            color: #FFAC1C;
            width: 100%;
            height: 45px;
            text-align: center;
            display: flex;
            align-items: center;
            justify-content: center;
            text-decoration: none;
            border: none;
            border-radius: 5px;
            background-color: #ffac1c26;
            transition: 0.2s ease-in-out;
        }
        
        .card2 .content button:hover
        {
            background-color: #ffac1c3f;
            cursor: pointer;
        }
        .tabletd
        {
            padding: 4px;
        }
        
        .divclass
        {
            width: 504px;
            margin-top: -157px;
            height: 328px;
        }
    </style>
    <style type="text/css">
        table
        {
            font-family: arial, sans-serif;
            width: 100%;
            font-size: 15px;
            border-collapse: collapse;
        }
        
        table thead th
        {
            color: #ffffff;
            background: #004976;
            padding: 15px;
            border: 1px solid #ebebec;
            font-weight: 500;
        }
        
        table tr
        {
            background: #ffffff;
        }
        
        table tr td
        {
            border: 1px solid #e6edf1;
            padding: 15px;
        }
        
        @media only screen and (max-width: 640px)
        {
            table thead
            {
                display: none;
            }
        
            table tbody
            {
                display: block;
            }
        
            table tbody tr td
            {
                display: flex;
                align-items: center;
                padding: 0;
            }
        
            table tbody tr
            {
                margin-bottom: 20px;
                display: block;
                border: 1px solid #004976;
                background: #f9f9f9;
                overflow: hidden;
            }
        
            table tbody td::before
            {
                content: attr(data-label);
                font-weight: bold;
                width: 30%;
                display: flex;
                background: #004976;
                color: #fff;
                padding: 15px;
                margin-inline-end: 10px;
            }
        }
        
        
        
    </style>
</head>
<body style="background-color: #ebedf0;">
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div style="white-space: nowrap;">
        <uc2:Menu ID="Menu1" runat="server" />
        <asp:Literal ID="lt" runat="server"></asp:Literal>
    </div>
    <table>
        <tr style="background: #ebedf0;">
            <td align="left">
                <span class="elementLabel" style="font-size: 12px; color: Black">Financial Year :
                </span>
                <asp:TextBox ID="txtYearMonth" runat="server" AutoPostBack="true" CssClass="textBox"
                    Width="35px" TabIndex="1" MaxLength="4" OnTextChanged="txtYearMonth_TextChanged"
                    Visible="false"></asp:TextBox>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="dropdownList"
                    TabIndex="2" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Button ID="bthGo" runat="server" Text="Go" TabIndex="2" Visible="false" OnClick="bthGo_Click" />
                <span class="elementLabel" style="font-size: 12px">&nbsp;&nbsp; Select Month :</span>
                <span>
                    <asp:DropDownList ID="DDMonth" runat="server" AutoPostBack="true" CssClass="dropdownList"
                        OnSelectedIndexChanged="DDMonth_SelectedIndexChanged">
                        <asp:ListItem Text="January" Value="01" />
                        <asp:ListItem Text="Febuary" Value="02" />
                        <asp:ListItem Text="March" Value="03" />
                        <asp:ListItem Text="April" Value="04" />
                        <asp:ListItem Text="May" Value="05" />
                        <asp:ListItem Text="June" Value="06" />
                        <asp:ListItem Text="July" Value="07" />
                        <asp:ListItem Text="August" Value="08" />
                        <asp:ListItem Text="September" Value="09" />
                        <asp:ListItem Text="October" Value="10" />
                        <asp:ListItem Text="November" Value="11" />
                        <asp:ListItem Text="December" Value="12" />
                    </asp:DropDownList>
                </span>
            </td>
        </tr>
  
    <%-- Data Container--%>
 
    </table>
   
    <table>
      <tr style="background: #ebedf0;">
            <td>
                <div class="divc">
                    <div class="flex-container">
                        <div class="box">
                            <p class="textcl">
                                IRM Uploaded</p>
                            <p class="value">
                                <asp:Label ID="lblIRMUpload" runat="server" Text=""></asp:Label></p>
                        </div>
                        <div class="box">
                            <p class="textcl">
                                IRM Approved</p>
                            <p class="value">
                                <asp:Label ID="lblIRMAprrove" runat="server" Text=""></asp:Label></p>
                        </div>
                        <div class="box">
                            <p class="textcl">
                                IRM Processed</p>
                            <p class="value">
                                <asp:Label ID="lblIRMProcessed" runat="server" Text=""></asp:Label></p>
                        </div>
                        <div class="box">
                            <p class="textcl">
                                ORM Uploaded</p>
                            <p class="value">
                                <asp:Label ID="lblORMUploaded" runat="server" Text=""></asp:Label></p>
                        </div>
                        <div class="box">
                            <p class="textcl">
                                ORM Approved</p>
                            <p class="value">
                                <asp:Label ID="lblORMApproved" runat="server" Text=""></asp:Label></p>
                        </div>
                        <div class="box">
                            <p class="textcl">
                                ORM Processed</p>
                            <p class="value">
                                <asp:Label ID="lblORMProcessed" runat="server" Text=""></asp:Label></p>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
     <table>
     <tr>
            <%-- Api status--%>
            <td style="background-color: #ebedf0; width: 50%;">
                <div class="card2">
                 
                  
                    <div class="content">
                        <h3>
                            LMCC API Status</h3>
                        <div class="lblclass">
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" Font-Bold="true" Text="Service Name" ID="lblServicename"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" Text="Status" Font-Bold="true" ID="lblStatus"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" Text="Push IRM Service" ID="lblDGFTSERVICE"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDGFTSERVICEStatus"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" Text="Get IRM Service " ID="lblGetIRMStatus_Service"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblGetIRMStatus_ServiceStatus"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" Text="Push ORM Service" ID="lblDGFTORMAPI_PUSH"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDGFTORMAPI_PUSHStatus"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" Text="Get ORM Service" ID="lblGETORMStatus_Service"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDGETORMStatus_ServiceStatus"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                  
                </div>
            </td>
            <%-- Pie Chart--%>
            <td style="background-color: #ebedf0; width: 50%;">
                <div id="container" class="divclass">
                <ul id="dataList"></ul>
                </div>
            </td>
        </tr>
    
    </table>
    <asp:Label runat="server" ID="lblhostname" Visible="false" Text="LAPTOP-07S00PJ0"></asp:Label>
    <asp:Label ID="financialyr_lbl" runat="server" Style="display: none"></asp:Label>
    </form>
</body>
</html>
