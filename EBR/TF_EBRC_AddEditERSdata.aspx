
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EBRC_AddEditERSdata.aspx.cs"
    Inherits="EBR_TF_EBRC_AddEditERSdata" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="Shortcut Icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Scripts/Validations.js"></script>
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen"/>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <script language="javascript" type="text/javascript">
        
        function checkSysDate(controlID) {

            var obj = controlID;

            if (controlID.value != "__/__/____") {

                var day = obj.value.split("/")[0];

                var month = obj.value.split("/")[1];
                var year = obj.value.split("/")[2];


                var dt = new Date(year, month - 1, day);
                var today = new Date();

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                    alert("Invalid Date");
                    controlID.focus();
                    return false;
                }
            }
        }


        function validate_dateRange() {
            var str1 = document.getElementById("txtDocumentDate").value;
            var str2 = document.getElementById("txtShippingBillDate").value;
          
            var dt1 = parseInt(str1.substring(0, 2), 10);
            var mon1 = parseInt(str1.substring(3, 5), 10) - 1;
            var yr1 = parseInt(str1.substring(6, 10), 10);

            var dt2 = parseInt(str2.substring(0, 2), 10);
            var mon2 = parseInt(str2.substring(3, 5), 10) - 1;
            var yr2 = parseInt(str2.substring(6, 10), 10);

          
            var DocDate = new Date(yr1, mon1, dt1);
            var ShippingDate = new Date(yr2, mon2, dt2);
            
            if ((document.getElementById("txtDocumentDate").value != '__/__/____') && (document.getElementById("txtShippingBillDate").value != '__/__/____')) {
                if (ShippingDate < DocDate)  {
                }
                else {

                    if ((document.getElementById("txtDocumentDate").value != '') && (document.getElementById("txtShippingBillDate").value != '')) {
                        alert('Shippment Date should be less than Realization Date.');

                        document.getElementById("txtShippingBillDate").value = '__/__/____';

                        document.getElementById("txtShippingBillDate").focus();

                        return true;
                    }
                    return true;
                }
            }
            return true;
        }

        function isValidDate(controlID, CName) {



            checkSysDate(controlID);
            var obj = controlID;
            if (controlID.value != "__/__/____") {

                var day = obj.value.split("/")[0];
                var month = obj.value.split("/")[1];
                var year = obj.value.split("/")[2];
                var today = new Date();

                if (day == "__") {
                    day = today.getDay();
                }
                if (month == "__") {
                    month = today.getMonth() + 1;
                }
                if (year == "____") {
                    year = today.getFullYear();
                }
                var dt = new Date(year, month - 1, day);

                //              if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {

                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
                validate_dateRange();
            }
        }
        function DoBlurtextbox(fld) {
            fld.className = 'textBox';
        }

        function DoBlurtextBoxRight(fld) {
            fld.className = 'textBoxRight';
        }
        function DoBlurdropdown(fld) {
            fld.className = 'dropdownList';
        }

        function DoFocus(fld) {
            fld.className = 'focus';
        }

        function DotextBoxRight(fld) {
            fld.className = 'textBoxRightfocus';
        }

        function AddLeadingZero(currentField) {

            //Check if the value length hasn't reach its max length yet
            if (currentField.value.length != currentField.maxLength) {
                //Add leading zero(s) in front of the value
                var numToAdd = currentField.maxLength - currentField.value.length;
                var value = "";
                for (var i = 0; i < numToAdd; i++) {
                    value += "0";
                }
                currentField.value = value + currentField.value;
            }
        }

    </script>
    <script language="javascript" type="text/javascript">


        function formatBillNumber() {
            var billNumber = document.getElementById('txtBillNumber');
            var spaces = '';
            if (trimAll(billNumber.value) != '') {
                var number = billNumber.value;
                var len = 6 - number.length;
                if (len > 0) {
                    for (var i = 0; i < len; i++) {
                      //  spaces += '0';
                    }
                }
            }
            // billNumber.value = spaces + billNumber.value;

            billNumber.value = billNumber.value;
        }

        function AddCoomaToTextboxes() {


            if (document.getElementById('txtFCAmount').value != '') {
                document.getElementById('txtFCAmount').value = (Math.round(document.getElementById('txtFCAmount').value * 100) / 100);
            }


            var txtvaluetxtFCAmount = trimAll(document.getElementById('txtFCAmount').value);
            if (txtvaluetxtFCAmount == '')
                txtvaluetxtFCAmount = 0;
            else
                document.getElementById('txtFCAmount').value = txtvaluetxtFCAmount;

            var txtvaluetxtINRAmount = trimAll(document.getElementById('txtINRAmount').value);
            if (txtvaluetxtINRAmount == '')
                txtvaluetxtINRAmount = 0;
            else
                document.getElementById('txtINRAmount').value = txtvaluetxtINRAmount;

            var txtvaluetxtRealisedAmount = trimAll(document.getElementById('txtRealisedAmount').value);
            if (txtvaluetxtRealisedAmount == '')
                txtvaluetxtRealisedAmount = 0;
            else
                document.getElementById('txtRealisedAmount').value = txtvaluetxtRealisedAmount;

        }

        //start
        function numeric(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && ((charCode >= 48 && charCode <= 57) || charCode == 46))
                return true;
            else {
                //                alert('Please Enter Numeric values.');
                return false;
            }
        }


        //end

        function validate_date(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 110 && charCode != 111 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

        function validateSave() {
            var expAlpha = /^[a-zA-Z0-9]+$/i;
            var expNumeric = /^[-+]?[0-9]+$/;


            var documentNumber = document.getElementById('txtDocumentNumber');
            if (trimAll(documentNumber.value) == '') {
                try {
                    alert('Enter Document No.');
                    documentNumber.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Document No.');
                    return false;
                }
            }

            var documentDate = document.getElementById('txtDocumentDate');
            if (trimAll(documentDate.value) == '') {
                try {
                    alert('Enter Realization Date.');
                    documentDate.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Realization Date.');
                    return false;
                }
            }

            var currency = document.getElementById('dropDownListCurrency');
            if (currency.selectedIndex == -1 || currency.selectedIndex == 0) {
                try {
                    alert('Select Currency.');
                    currency.focus();
                    return false;
                }
                catch (err) {
                    alert('Select Currency.');
                    return false;
                }
            }

//            simran start
            var exportscategory = document.getElementById('ddlCategoryOfExports');
            if (exportscategory.selectedIndex == -1 || exportscategory.selectedIndex == 0) {
                try {
                    alert('Select Category Of exports.');
                    exportscategory.focus();
                    return false;
                }
                catch (err) {
                    alert('Select Category Of exports.');
                    return false;
                }
            }

            var Forfeighting = document.getElementById('ddlForfeighting');
            if (Forfeighting.selectedIndex == -1 || Forfeighting.selectedIndex == 0) {
                try {
                    alert('Select Forfeiting.');
                    Forfeighting.focus();
                    return false;
                }
                catch (err) {
                    alert('Select Forfeiting.');
                    return false;
                }
            }

            var Factoring = document.getElementById('ddlFactoring');
            if (Factoring.selectedIndex == -1 || Factoring.selectedIndex == 0) {
                try {
                    alert('Select Factoring.');
                    Factoring.focus();
                    return false;
                }
                catch (err) {
                    alert('Select Factoring.');
                    return false;
                }
            }

            var vostro = document.getElementById('ddlVastro');
            if (vostro.selectedIndex == -1 || vostro.selectedIndex == 0) {
                try {
                    alert('Select Vostro.');
                    vostro.focus();
                    return false;
                }
                catch (err) {
                    alert('Select Vostro.');
                    return false;
                }
            }

            var categoryofexports = document.getElementById('ddlCategoryOfExports');
            var saccode = document.getElementById('txtSACcode');
            if (categoryofexports.selectedIndex == 2 && trimAll(saccode.value) == '') {
                try {
                    alert('Please Enter SAC Code For Service Export.');
                    saccode.focus();
                    return false;
                }
                catch (err) {
                    alert('Please Enter SAC Code For Service Export.');
                    return false;
                }
            }
//            simran end






            var fcAmount = document.getElementById('txtFCAmount');
            if (trimAll(fcAmount.value) == '') {
                try {
                    alert('Enter Amount in FC.');
                    fcAmount.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Amount in FC.');
                    return false;
                }
            }
            var realisedAmount = document.getElementById('txtRealisedAmount');
            if (trimAll(realisedAmount.value) == '') {
                try {
                    alert('Enter Realised Amount.');
                    realisedAmount.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Realised Amount.');
                    return false;
                }
            }


            var exchangeRate = document.getElementById('txtExchangeRate');
            if (trimAll(exchangeRate.value) == '' || exchangeRate.value == 0) {
                var answer = confirm('Exchange Rate is Blank or Zero. Do you want to Proceed ?')
                if (answer) {
                    
                }
                else
                    if (trimAll(exchangeRate.value) == '' || exchangeRate.value == 0) {
                           exchangeRate.focus();
                           return false;
                     }
            }
            var custId = document.getElementById('txtCustomerID');
            if (trimAll(custId.value) == '') {
                try {
                    alert('Select Customer.');
                    custId.focus();
                    return false;
                }
                catch (err) {
                    alert('Select Customer.');
                    custId.focus();
                    return false;
                }

            }

            var IECode = document.getElementById('txtIECode');
            if (IECode.value == "") {
                alert('Update IE Code in Customer Master. IE Code Can not be blank ');
                custId.focus();
                return false;
            }

            var portCode = document.getElementById('dropDownListPortCode');
            if (portCode.selectedIndex == -1 || portCode.selectedIndex == 0) {
                try {
                    alert('Select Port Code.');
                    portCode.focus();
                    return false;
                }
                catch (err) {
                    alert('Select Port Code.');
                    return false;
                }
            }

            var billPrefix = document.getElementById('txtBillNumberPrefix');
            if (trimAll(billPrefix.value) == '') {
                try {
                    alert('Enter Bill No Prefix.');
                    billPrefix.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Bill No Prefix.');
                    billPrefix.focus();
                    return false;
                }
            }


            var shippingBillNo = document.getElementById('txtShippingBillNumber');
            if (trimAll(shippingBillNo.value) == '') {
                try {
                    alert('Enter Shipping Bill No.');
                    shippingBillNo.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Shipping Bill No.');
                    shippingBillNo.focus();
                    return false;
                }
            }

            var shippingBillDate = document.getElementById('txtShippingBillDate');
            if (trimAll(shippingBillDate.value) == '') {
                try {
                    alert('Enter Shipping Bill Date.');
                    shippingBillDate.focus();
                    return false;
                }
                catch (err) {
                    alert('Enter Shipping Bill Date.');
                    return false;
                }
            }


            var billNumberPrefix = document.getElementById('txtBillNumberPrefix');
            var billNumber = document.getElementById('txtBillNumber');
            var tempShipping = trimAll(billNumberPrefix.value);

            if (tempShipping != '') {
                if (tempShipping.toUpperCase() != 'N' && tempShipping.toUpperCase() != 'D' && tempShipping.toUpperCase() != 'P' && tempShipping.toUpperCase() != 'C' && tempShipping.toUpperCase() != 'M' && tempShipping.toUpperCase() != 'A' && tempShipping.toUpperCase()!='E') {
                    alert("Bill No. Prefix can only be 'N', 'D', 'P', 'C', 'M', 'E' or 'A'.");
                    try {
                        billNumberPrefix.focus();
                        return false;
                    }
                    catch (err) {
                        return false;
                    }
                }

                if (trimAll(billNumber.value) == '') {
                    alert('Enter Bill No.');
                    try {
                        billNumber.focus();
                        return false;
                    }
                    catch (err) {
                        return false;
                    }
                }
            }

            if (trimAll(billNumber.value) != '') {
                if (expAlpha.test(trimAll(billNumber.value)) == false) {
                    alert('Only Alphanumeric value is allowed.');
                    try {
                        billNumber.focus();
                        return false;
                    }
                    catch (err) {
                        return false;
                    }
                }
                if (trimAll(tempShipping) == '') {
                    alert('Enter Bill No. Prefix');
                    try {
                        billNumberPrefix.focus();
                        return false;
                    }
                    catch (err) {
                        return false;
                    }
                }
            }

            if (document.getElementById('txtRealisation').value == 'P') {
//                var answer = confirm('Bill is Part Realised.Do you want to Proceed')
//                if (answer) {
//                    var xa = 0;
//                }
                //                else {
                alert("This bill is Part Realized, Data will not be Saved");

                    document.getElementById('txtRealisation').focus();
                    return false;
//                }
            }
            return true;
        }


        function calculateINR() {


            var fcAmount = document.getElementById('txtFCAmount').value;
            var exchangeRate = document.getElementById('txtExchangeRate').value;
            var INRAmount = '';
            if (fcAmount == '' || fcAmount == "0") {
                alert('Enter Amount in FC.');
                try {
                    document.getElementById('txtFCAmount').focus();
                    return false;
                }
                catch (err) {
                    return false;
                }
            }
            else {
                if (exchangeRate == '' || exchangeRate == "0") {
//                    alert('Enter Exchange Rate.');
//                    try {
//                        document.getElementById('txtExchangeRate').focus();
//                        return false;
//                    }
//                    catch (err) {
//                        return false;
                    //                    }
                    document.getElementById('txtINRAmount').value = 0;
                }
                else {
                    var realisedAmt = document.getElementById('txtRealisedAmount').value;
                    INRAmount = realisedAmt * exchangeRate;

                    document.getElementById('txtINRAmount').value = formatAmt(INRAmount);

                }
            }

        }

      
        function changeDate() {
            __doPostBack('btnChangeDate', '');
        }



        function setfocus() {
            document.getElementById('txtDocumentNumber').focus();
        }

        function toUpper_Case() {
            document.getElementById('txtRealisation').value = (document.getElementById('txtRealisation').value).toUpperCase();
            document.getElementById('txtBillNumberPrefix').value = (document.getElementById('txtBillNumberPrefix').value).toUpperCase();
        }

        function validateAmt() {

            var fcAmount = document.getElementById('txtFCAmount').value;
            if (fcAmount == 0 || fcAmount == '') {
                document.getElementById('txtFCAmount').focus();
                return false;
            }
            else {
                var f = (Math.round(fcAmount * 100) / 100);
                document.getElementById('txtFCAmount').value = formatAmt(f);
            }

//          start  Added by simran (23-05-2023)
            var freightval = document.getElementById('txtFreightValue').value;
            if (freightval == '') {
                document.getElementById('txtFreightValue').value = '';
                return false;
            }
            else {
                var f = (Math.round(freightval * 100) / 100);
                document.getElementById('txtFreightValue').value = formatAmt(f);
            }

            var InsuranceValue = document.getElementById('txtInsuranceValue').value;
            if (InsuranceValue == '') {
                document.getElementById('txtInsuranceValue').value = '';
                return false;
            }
            else {
                var f = (Math.round(InsuranceValue * 100) / 100);
                document.getElementById('txtInsuranceValue').value = formatAmt(f);
            }

            var CommissionValue = document.getElementById('txtCommissionValue').value;
            if (CommissionValue == '') {
                document.getElementById('txtCommissionValue').value = '';
                return false;
            }
            else {
                var f = (Math.round(CommissionValue * 100) / 100);
                document.getElementById('txtCommissionValue').value = formatAmt(f);
            }
//            Simran end


        }


        function validateRealisedAmt() {
            var fcAmount = document.getElementById('txtFCAmount').value;

            if (fcAmount != '') {
                var realisedAmount = document.getElementById('txtRealisedAmount').value;
                if (realisedAmount == 0 || realisedAmount == '') {
                    alert('Enter Realised Amount');
                    document.getElementById('txtRealisedAmount').focus();
                    return false;
                }
                else {
                    if ((Math.round(realisedAmount * 100) / 100) <= (Math.round(fcAmount * 100) / 100)) {
                        var f = (Math.round(realisedAmount * 100) / 100);
                        document.getElementById('txtRealisedAmount').value = formatAmt(f);
                        if ((Math.round(realisedAmount * 100) / 100) < (Math.round(fcAmount * 100) / 100)) {
                            document.getElementById('txtRealisation').value = 'P';
                            document.getElementById('lblFullPartRealisation').innerText = "Part Realisation";
                            document.getElementById('txtRealisation').focus();
                        }
                        else {
                            document.getElementById('txtRealisation').value = 'F';
                            document.getElementById('lblFullPartRealisation').innerText = "Full Realisation";
                        }
                    }
                    else {
                        alert('Enter valid Realised Amount.');
                        document.getElementById('txtRealisedAmount').value = '';
                        document.getElementById('txtRealisedAmount').focus();
                        return false;
                    }
                }
            }
            else {
                alert('Enter FC Amount.');
                 return false;
            }
        }

        function onlyFP(evnt) {

            var charCode = (evnt.which) ? evnt.which : event.keyCode;
             if (charCode == 70 || charCode == 80 || charCode == 8 || charCode == 9 || charCode == 46 || charCode == 37 || charCode == 39) {
                if (charCode == 70) {
                    document.getElementById('lblFullPartRealisation').innerText = "Full Realisation";
                    document.getElementById('txtRealisation').value = '';
                   var answer = confirm('Bill is Fully Realised.')
                   if (answer) {
                      document.getElementById('txtRealisation').value = 'F';
                    }
                    else {

                        document.getElementById('txtRealisation').value = 'P';
                        document.getElementById('lblFullPartRealisation').innerText = "Part Realisation";
                        document.getElementById('txtExchangeRate').focus();
                        return false;
                    }

                }
                if (charCode == 80)
                    document.getElementById('lblFullPartRealisation').innerText = "Part Realisation";
                return true;
            }
            else {
                return false;
            }

        }
              
    </script>

   <%-- portcode script--%>
    <script language="javascript" type="text/javascript">


        //All helps function


        // Help for Currency

        function OpenCurrencyList(hNo) {
            open_popup('../TF_CurrencyLookup1.aspx?pc=' + hNo, 450, 350, 'CurrencyList');
            return false;
        }
        function selectCurrency(selectedID, hNo) {
            var id = selectedID;
            document.getElementById('hdnCurrencyHelpNo').value = hNo;
            document.getElementById('hdnCurId').value = id;
            document.getElementById('btnCurr').click();

        }
        function changeCurrDesc() {


            var dropDownListCurrency = document.getElementById('dropDownListCurrency');
            var txtCurrencyDescription = document.getElementById('txtCurrencyDescription');
            if (dropDownListCurrency.value != "0") {
                txtCurrencyDescription.innerHTML = dropDownListCurrency.value;

            }
            else
                txtCurrencyDescription.innerHTML = "";

            // dropDownListCurrency.focus();

            return true;
        }


        // Help for portcode
//        function PortHelp() {

//            open_popup('../RRETURN/RET_HelpPortCode.aspx', 400, 400, "DocFile");
//            return false;
//        }


        function PortHelp() {

            var portCode = document.getElementById('txtPortCode').value;

            open_popup('../TF_PortCodeLookup.aspx?PortID=' + portCode, 400, 400, "DocFile");
            return false;
        }


        function selectPort(Uname) {

            // document.getElementById('dropDownListPortCode').value = Uname;
            document.getElementById('hdnPortCode').value = Uname;
            document.getElementById('dropDownListPortCode').focus();
            document.getElementById('btnPortCode').click();
        }

        function changePortCodeDesc() {

            var dropDownListPortCode = document.getElementById('dropDownListPortCode');

            var txtPortCode = document.getElementById('txtPortCode');

            if (txtPortCode.value != "0")
                txtPortCode.innerHTML = dropDownListPortCode.value;
            else
                txtPortCode.innerHTML = "";

            if (txtPortCode.innerHTML == "0")
                txtPortCode.innerHTML = "";


            return true;
        }

        //help for Customer Code

        function OpenCustomerCodeList(e) {
            var keycode;
            var txtIntermediaryBank;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                var txtCustAcNo = document.getElementById('txtCustomerID');
                open_popup('../TF_CustomerLookUp.aspx?CustID=' + txtCustAcNo.value, 450, 450, 'CustomerCodeList');
                return false;
            }
        }
        function selectCustomer(selectedID) {

            var id = selectedID;
            document.getElementById('hdnCustomerCode').value = id;
            document.getElementById('btnCustomerCode').click();
        }


        //Overseas Bank Help

        function OpenOverseasBankList(e) {
            var keycode;

            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                var txtOverseasBankID = document.getElementById('txtBankID');
                open_popup('../EBR/EXP_OverseasBankLookup.aspx?hNo=1&bankID=' + txtOverseasBankID.value, 450, 650, 'OverseasBankList');
                return false;
            }
        }
        function selectOverseasBank(selectedID) {
            var id = selectedID;
            document.getElementById('hdnOverseasId').value = id;
            document.getElementById('btnOverseasBank').click();
        }


        // help for Copy SrNO information 

        //help for Customer Code

        function OpenCopySRNoList(e) {

            var custAcNO = document.getElementById('txtCustomerID');
            if (custAcNO.value == '') {
                alert('Enter Customer ID');
                custAcNO.focus();
                return false;
            }
            var keycode;
            var txtIntermediaryBank;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                var txtCustAcNo = document.getElementById('txtCopyFromDocNo');
                var txtBranchName = document.getElementById('txtBranchName');
                open_popup('Help_EBRC_CopyFrom.aspx?txtCopyFrom=' + txtCustAcNo.value + '&BranchName=' + txtBranchName.value + '&custACNo=' + custAcNO.value, 550, 845, 'CustomerCodeList');
                return false;
            }
        }
        function selectCopySrNo(selectedID) {

            var id = selectedID;
            document.getElementById('hdnCopySRNo').value = id;
            document.getElementById('btnhdnCopySRNo').click();
        }





        //function for fill discription after post bak
        function toogleDisplayHelp() {


            changeCurrDesc();
            changePortCodeDesc();

            return true;
        }


        

       
    </script>
</head>
<body onload="EndRequest();setfocus();" onunload="closeWindows();">
    <form id="formrdata" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
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
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCopy" />
                    <asp:PostBackTrigger ControlID="btnCustomerCode" />
                    <asp:PostBackTrigger ControlID="btnOverseasBank" />
                    <asp:PostBackTrigger ControlID="btnhdnCopySRNo" />
                  
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td>
                                <input type="hidden" id="hdnCurrencyHelpNo" runat="server" />
                                <input type="hidden" id="hdnCurId" runat="server" />
                                <asp:Button ID="btnCurr" Style="display: none;" runat="server" OnClick="btnCurr_Click" />
                                <input type="hidden" id="hdnPortCode" runat="server" />
                                <asp:Button ID="btnPortCode" Style="display: none;" runat="server" OnClick="btnPortCode_Click" />
                                <input type="hidden" id="hdnCustId" runat="server" />
                                <asp:Button ID="btnOverseasBank" Style="display: none;" runat="server" OnClick="btnOverseasBank_Click" />
                                <input type="hidden" id="hdnCustomerCode" runat="server" />
                                <asp:Button ID="btnCustomerCode" Style="display: none;" runat="server" OnClick="btnCustomerCode_Click" />

                                 <input type="hidden" id="hdnCopySRNo" runat="server" />
                                <asp:Button ID="btnhdnCopySRNo" Style="display: none;" runat="server" OnClick="btnCopy_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">EBRC Data Entry</span>
                            </td>
                            <td align="right" style="width: 50%">
                             <asp:Label runat="server" ID="lblSupervisormsg" Text="Role : Supervisor - ONLY VIEW THE DATA  " 
                                    style="color:red" Visible="false" ></asp:Label>&nbsp;

                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click"
                                    ToolTip="Back" TabIndex="16" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 80%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                        <tr>
                            <td align="right" valign="top" style="width: 9%;" nowrap>
                                <span class="mandatoryField">* </span><span class="elementLabel">Branch. :</span>
                            </td>
                            <td align="left" style="width: 20%;" nowrap>
                                &nbsp;<asp:TextBox ID="txtBranchName" runat="server" CssClass="textBox" MaxLength="15"
                                    Enabled="false" Width="100px"></asp:TextBox>&nbsp;
                            </td>
                            <td align="right" valign="top" style="width: 05%;" nowrap>
                                <asp:Label ID="lblCopyFrom" runat="server" CssClass="elementLabel" Text="Copy From Sr No.:"
                                    Width="100px" Visible="false"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;
                                <asp:TextBox ID="txtCopyFromDocNo" Width="50px" runat="server" CssClass="textBox"
                                    TabIndex="115" Visible="false" MaxLength="20" OnTextChanged="txtCopyFromDocNo_TextChanged"
                                    AutoPostBack="true"  onkeydown="OpenCopySRNoList(this)"></asp:TextBox><asp:Button ID="btnDocNoListtoCopy" runat="server"
                                        CssClass="btnHelp_enabled" TabIndex="-1" Visible="false" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCopy" runat="server" Text="Copy" CssClass="buttonCopy" Width="40px"
                                    ToolTip="Copy" TabIndex="116" Visible="false" OnClick="btnCopy_Click1" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" style="width: 9%;" nowrap>
                                <span class="mandatoryField">* </span><span class="elementLabel">Serial No. :</span>
                            </td>
                            <td align="left" style="width: 20%;" nowrap>
                                &nbsp;<asp:TextBox ID="txtSerialNumber" runat="server" CssClass="textBox" MaxLength="4"
                                    Enabled="false" Width="70px"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="mandatoryField">* </span><span class="elementLabel">Document No. :</span>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtDocumentNumber" runat="server" CssClass="textBox" MaxLength="20"
                                    Width="200px" TabIndex="1"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" style="width: 5%;" nowrap>
                                <span class="mandatoryField">* </span><span class="elementLabel">Realization Date :</span>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtDocumentDate" runat="server" CssClass="textBox" MaxLength="10"
                                    ValidationGroup="dtVal" Width="70px" TabIndex="2"></asp:TextBox>
                                <asp:Button ID="btncalendar_DocumentDate" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtDocumentDate" InputDirection="RightToLeft"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left"
                                    PromptCharacter="_">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:CalendarExtender ID="calendarDocumentDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDocumentDate" PopupButtonID="btncalendar_DocumentDate">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                    ValidationGroup="dtVal" ControlToValidate="txtDocumentDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                </ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="mandatoryField">* </span><span class="elementLabel">Currency :</span>
                            </td>
                            <td align="left" nowrap>
                                &nbsp;<asp:DropDownList ID="dropDownListCurrency" CssClass="dropdownList" TabIndex="3"
                                    runat="server">
                                </asp:DropDownList>
                                <asp:Button ID="btncurrList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                <asp:Label ID="txtCurrencyDescription" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 5%;" nowrap>
                                <span class="mandatoryField">* </span><span class="elementLabel">Amount in FC :</span>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtFCAmount" runat="server" CssClass="textBoxRight" MaxLength="15"
                                    TabIndex="4" Width="120px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 9%;" nowrap>
                                <span class="mandatoryField">*</span> <span class="elementLabel">Realised Amount :</span>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtRealisedAmount" runat="server" CssClass="textBoxRight"
                                    TabIndex="5" MaxLength="15" Width="120px"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Realisation :</span>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtRealisation" runat="server" CssClass="textBox" MaxLength="1"
                                    Width="15px" TabIndex="5"></asp:TextBox>
                                <asp:Label ID="lblFullPartRealisation" runat="server" CssClass="elementLabel" Width="250px"
                                    OnLoad="setLabel">Full (F) / Part (P)</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 9%;" nowrap>
                                <span class="elementLabel">Exchange Rate :</span>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtExchangeRate" runat="server" CssClass="textBoxRight" MaxLength="15"
                                    TabIndex="6" Width="90px"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Amount in INR :</span>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtINRAmount" runat="server" CssClass="textBoxRight" MaxLength="15"
                                    TabIndex="6" Width="140px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 9%;" nowrap>
                                <span class="mandatoryField">* </span><span class="elementLabel">Customer ID :</span>
                            </td>
                            <td align="left" nowrap>
                                &nbsp;<asp:TextBox ID="txtCustomerID" runat="server" CssClass="textBox" MaxLength="40"
                                    TabIndex="7" Width="70px" onkeydown="OpenCustomerCodeList(this)" AutoPostBack="True"
                                    OnTextChanged="txtCustomerID_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnCustList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                <asp:Label ID="lblCustomerName" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                            </td>
                            <td align="right" style="width: 9%;" nowrap>
                                <span class="elementLabel">IE Code :</span>
                            </td>
                            <td align="left" nowrap>
                                &nbsp;<asp:TextBox ID="txtIECode" runat="server" CssClass="textBoxRight" MaxLength="20"
                                    TabIndex="6" Width="80px" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 5%;" nowrap>
                                <span class="elementLabel">Overseas Bank ID :</span>
                            </td>
                            <td align="left" nowrap>
                                &nbsp;<asp:TextBox ID="txtBankID" runat="server" CssClass="textBox" MaxLength="40"
                                    TabIndex="8" Width="70px" onkeydown="OpenOverseasBankList(this)" AutoPostBack="True"
                                    OnTextChanged="txtBankID_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnBankList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                <asp:Label ID="lblBankName" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" width="100%">
                               <%-- <fieldset align="left" style="padding: 3px; border: 0px solid gray; width: 70%;"
                                    bgcolor="#8f8a85">--%>
                                   <%-- <legend align="center" style="text-align: center;"><span class="mandatoryField">* Export--%>
                                       <%-- Details</span>--%>
                                        <table border="0" cellpadding="0" cellspacing="0" width="60%" align="left" bgcolor="#c0c0c0">
                                            <tr>
                                                <td align="right" nowrap style="width: 9%; border-top: 1px solid black; border-left: 1px solid black">
                                                </td>
                                                <td align="Right" nowrap style="width: 20%; border-top: 1px solid black;">
                                                     <legend align="center" style="text-align: center;"> 
                                                     <span class="elementLabel" style="text-align :center; color: Black; "><strong>EXPORT 
                                                         DETAILS</strong> </span></legend>
                                                </td>
                                                <td align="left" nowrap style="width: 5%; border-top: 1px solid black;">
                                                </td>
                                                <td style="border-right: 1px solid black; border-top: 1px solid black;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" nowrap style="width: 10%; border-left: 1px solid black">
                                                   <span class="mandatoryField">* </span>  <span class="elementLabel" style ="color: Black;">Port Code :</span>
                                                </td>
                                                <td align="left" nowrap>
                                                    &nbsp;<asp:DropDownList ID="dropDownListPortCode" runat="server" CssClass="dropdownList"
                                                        TabIndex="9">
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnPortCodeList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                    <asp:Label ID="txtPortCode" runat="server" CssClass="elementLabel" style ="color: Black;" Width="200px"></asp:Label>
                                                </td>
                                                <td align="right" nowrap style="width: 5%;">
                                                   <span class="mandatoryField">* </span>  <span class="elementLabel" style ="color: Black;">Bill No. :</span>
                                                </td>
                                                <td align="left" style="border-right: 1px solid black;">
                                                 <%--   &nbsp; <asp:TextBox ID="txtBillNumberPrefix" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="10" ToolTip="[ N/D/P/C/M/E/A ]" Width="15px"></asp:TextBox>--%>
                                                        &nbsp;<asp:TextBox ID="txtBillNumber"
                                                        runat="server" CssClass="textBox" MaxLength="16" TabIndex="11" Width="88px"></asp:TextBox>

                                                    <span class="elementLabel" style ="color: Black;">[N/D/P/C/M/E/A]</span>                                                 </td>
                                                <tr>
                                                    <td align="right" nowrap style="width: 10%; border-left: 1px solid black; border-bottom: 1px solid black;">
                                                        &nbsp;<span class="mandatoryField">* </span>  <span class="elementLabel" style ="color: Black;">Shipping Bill No. :</span>
                                                    </td>
                                                    <td align="left" nowrap style="border-bottom: 1px solid black;">
                                                        &nbsp;<asp:TextBox ID="txtShippingBillNumber" runat="server" CssClass="textBox" MaxLength="7"
                                                            TabIndex="12" Width="60px"></asp:TextBox>
                                                    </td>
                                                    <td align="right" nowrap style="width: 5%; border-bottom: 1px solid black;">
                                                        <span class="mandatoryField">* </span> <span class="elementLabel" style ="color: Black;">Shipping Bill Date :</span>
                                                    </td>
                                                    <td align="left" nowrap style="width: 45%; border-bottom: 1px solid black; border-right: 1px solid black;">
                                                     &nbsp;&nbsp;<asp:TextBox ID="txtShippingBillDate" runat="server" CssClass="textBox" MaxLength="10"
                                                            TabIndex="13" ValidationGroup="dtVal" Width="70px"></asp:TextBox>
                                                        <asp:Button ID="btncalendar_ShippingBillDate" runat="server" CssClass="btncalendar_enabled"
                                                            TabIndex="-1" />
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                            CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" InputDirection="RightToLeft"
                                                            Mask="99/99/9999" MaskType="Date" PromptCharacter="_" TargetControlID="txtShippingBillDate">
                                                        </ajaxToolkit:MaskedEditExtender>
                                                        <ajaxToolkit:CalendarExtender ID="calendarShippingBillDate" runat="server" Format="dd/MM/yyyy"
                                                            PopupButtonID="btncalendar_ShippingBillDate" PopupPosition="TopRight" TargetControlID="txtShippingBillDate">
                                                        </ajaxToolkit:CalendarExtender>
                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                            ControlToValidate="txtShippingBillDate" EmptyValueBlurredText="*" EmptyValueMessage="Enter Date Value"
                                                            InvalidValueBlurredMessage="Date is invalid" ValidationGroup="dtVal">
                                                        </ajaxToolkit:MaskedEditValidator>
                                                    </td>
                                                </tr>
                                        </table>
                                <%--</fieldset>--%>
                            </td>
                        </tr>


                            <%--Suchitra Start--%>
                        <tr>
                            <td align="right" valign="top" style="width: 9%;" nowrap>
                                </span><span class="elementLabel">Freight Value :</span>
                            </td>
                            <td align="left" style="width: 20%;" nowrap>
                                &nbsp;<asp:TextBox ID="txtFreightValue" runat="server" CssClass="textBoxRight" MaxLength="15"
                                    TabIndex="1" Width="140px"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="mandatoryField">* <span class="elementLabel">Category Of Exports :</span>
                            </td>
                            <td align="left">
                                &nbsp;<asp:DropDownList ID="ddlCategoryOfExports" CssClass="dropdownList" AutoPostBack="true"
                                    runat="server" TabIndex="1">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">G</asp:ListItem>
                                    <asp:ListItem Value="2">S</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;<span class="elementLabel">[G : Goods and S : Service]</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" style="width: 9%;" nowrap>
                                </span><span class="elementLabel">Insurance Value :</span>
                            </td>
                            <td align="left" style="width: 20%;" nowrap>
                                &nbsp;<asp:TextBox ID="txtInsuranceValue" runat="server" CssClass="textBoxRight" MaxLength="15"
                                    TabIndex="1" Width="140px"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">Commission Value :</span>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtCommissionValue" runat="server" CssClass="textBoxRight" MaxLength="15"
                                    TabIndex="1" Width="140px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" style="width: 9%;" nowrap>
                                </span><span class="elementLabel">SAC Code :</span>
                            </td>
                            <td align="left" style="width: 20%;" nowrap>
                                &nbsp;<asp:TextBox ID="txtSACcode" runat="server" CssClass="textBoxRight" MaxLength="06"
                                    TabIndex="1" Width="90px"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="mandatoryField">* <span class="elementLabel">Forfeiting :</span>
                            </td>
                            <td align="left">
                                 &nbsp;<asp:DropDownList ID="ddlForfeighting" CssClass="dropdownList" AutoPostBack="true"
                                    runat="server" TabIndex="1">
                                     <asp:ListItem Value="0">Select</asp:ListItem>
                                     <asp:ListItem Value="1">Y</asp:ListItem>
                                     <asp:ListItem Value="2">N</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                                <span class="mandatoryField">* <span class="elementLabel">Factoring :</span>
                                &nbsp;<asp:DropDownList ID="ddlFactoring" CssClass="dropdownList" AutoPostBack="true"
                                    runat="server" TabIndex="1">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                     <asp:ListItem Value="1">Y</asp:ListItem>
                                     <asp:ListItem Value="2">N</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" style="width: 9%;" nowrap>
                                </span><span class="elementLabel">Vostro :</span>
                            </td>
                            <td align="left" style="width: 20%;" nowrap>
                                &nbsp;<asp:DropDownList ID="ddlVastro" CssClass="dropdownList" AutoPostBack="true"
                                    runat="server" TabIndex="1">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                     <asp:ListItem Value="1">Y</asp:ListItem>
                                     <asp:ListItem Value="2">N</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">RMT City :</span>
                            </td>
                            <td align="left">
                               &nbsp;<asp:TextBox ID="txtRMTCity" runat="server" CssClass="textBox" MaxLength="50"
                                    TabIndex="1" Width="140px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" style="width: 9%;" wrap>
                                <span class="elementLabel">RMT Bank :</span>
                            </td>
                            <td align="left" style="width: 20%;" nowrap>
                                &nbsp;<asp:TextBox ID="txtRMTBank" runat="server" CssClass="textBox" MaxLength="50"
                                    TabIndex="1" Width="260px"></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">RMT Country :</span>
                            </td>
                            <td align="left">
                               &nbsp;<asp:TextBox ID="txtRMTCountry" runat="server" CssClass="textBox" MaxLength="50"
                                    TabIndex="1" Width="140px"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <%--Simran End--%>


                        <tr>
                            <td align="right">
                            </td>
                            <td align="right" style="padding-top: 10px; padding-bottom: 10px">
                                &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                    OnClick="btnSave_Click" ToolTip="Save" TabIndex="14" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                    OnClick="btnCancel_Click" ToolTip="Cancel" TabIndex="15" />
                                <input type="hidden" runat="server" id="hdnFromDate" />
                                <input type="hidden" runat="server" id="hdnToDate" />
                                <%-- These section to handle log  --%>
                                <input type="hidden" runat="server" id="hdnCurrency" />
                                <input type="hidden" runat="server" id="hdnFCAmt" />
                                <input type="hidden" runat="server" id="hdnINRAmt" />
                                <input type="hidden" runat="server" id="hdnExtRate" />
                                <input type="hidden" runat="server" id="hdnRealisedAmt" />
                                <input type="hidden" runat="server" id="hdnCustomerId" />
                                <input type="hidden" runat="server" id="hdnOverseasId" />
                                <%-- End of These section to handle log  --%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <hr />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        window.onload = function () {
            toogleDisplayHelp();

            var txtvaluetxtFCAmount = document.getElementById('txtFCAmount');
            if (txtvaluetxtFCAmount.value == '')
                txtvaluetxtFCAmount.value = 0;
            else
                document.getElementById('txtFCAmount').value = parseFloat(txtvaluetxtFCAmount.value).toFixed(2); 
       
            var txtvaluetxtINRAmount = document.getElementById('txtINRAmount');
            if (txtvaluetxtINRAmount.value == '')
                txtvaluetxtINRAmount.value = 0;
            else
                document.getElementById('txtINRAmount').value = parseFloat(txtvaluetxtINRAmount.value).toFixed(2);

            var txtvaluetxtRealisedAmount = document.getElementById('txtRealisedAmount');
            if (txtvaluetxtRealisedAmount.value == '')
                txtvaluetxtRealisedAmount.value = 0;
            else
                document.getElementById('txtRealisedAmount').value = parseFloat(txtvaluetxtRealisedAmount.value).toFixed(2);


            //simran start 23-05-2023
            var txtvaluetxtFreightValue = document.getElementById('txtFreightValue');
            if (txtvaluetxtFreightValue.value == '')
                txtvaluetxtFreightValue.value = '';
            else
                document.getElementById('txtFreightValue').value = parseFloat(txtvaluetxtFreightValue.value).toFixed(2);

            var txtvaluetxtInsuranceValue = document.getElementById('txtInsuranceValue');
            if (txtvaluetxtInsuranceValue.value == '')
                txtvaluetxtInsuranceValue.value = '';
            else
                document.getElementById('txtInsuranceValue').value = parseFloat(txtvaluetxtInsuranceValue.value).toFixed(2);

            var txtvaluetxtCommissionValue = document.getElementById('txtCommissionValue');
            if (txtvaluetxtCommissionValue.value == '')
                txtvaluetxtCommissionValue.value = '';
            else
                document.getElementById('txtCommissionValue').value = parseFloat(txtvaluetxtCommissionValue.value).toFixed(2);

            //simran end

        }
    </script>
</body>
</html>
