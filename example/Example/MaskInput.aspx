<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaskInput.aspx.cs" Inherits="Example.MaskInput" %>

<%@ Register TagPrefix="CustomMaskInputInPage" Namespace="WebFormHtml5DataList.MaskInput" Assembly= "WebFormHtml5DataList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="js/jquery-3.5.1.min.js"></script>
    <script src="js/jquery.mask-1.4.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Decimal(5,0)
            <br />
            <CustomMaskInputInPage:DecimalTextBoxControl ID="DecimalTextBox1" runat="server" IntPart="5"></CustomMaskInputInPage:DecimalTextBoxControl>
            <br />
            ROCDate
            <br />
            <CustomMaskInputInPage:ROCDateTextBoxControl ID="ROCDateTextBox1" runat="server" ></CustomMaskInputInPage:ROCDateTextBoxControl>
            <br />
            ROCMonth
            <br />
            <CustomMaskInputInPage:ROCMonthTextBoxControl ID="ROCMonthTextBox1" runat="server" ></CustomMaskInputInPage:ROCMonthTextBoxControl>
            <br />
            English + digit only
            <br />
            <CustomMaskInputInPage:AnyCharTextBoxControl ID="AnyCharTextBox1" runat="server" UTF8="false" MaxLength="20" ></CustomMaskInputInPage:AnyCharTextBoxControl>

        </div>
    </form>
</body>
</html>
