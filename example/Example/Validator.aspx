<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validator.aspx.cs" Inherits="Example.Validator" %>

<%@ Register TagPrefix="CustomValidatorInPage" Namespace="WebFormHtml5DataList.Validator" Assembly= "WebFormHtml5DataList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <CustomValidatorInPage:CustomRequiredValidator ID="CV1_Required" runat="server" ControlToValidate="TextBox1" Display="Dynamic" ></CustomValidatorInPage:CustomRequiredValidator>
            <CustomValidatorInPage:CustomRegexValidator ID="CV1_Regex" runat="server" ControlToValidate="TextBox1" DependentValidatorControl="CV1_Required" RegexFormat="^\d{1,3}$" isplay="Dynamic"></CustomValidatorInPage:CustomRegexValidator>
            <CustomValidatorInPage:CustomDependepntValidator ID="CV1_CV" runat="server" ControlToValidate="TextBox1" DependentValidatorControl="CV1_Required, CV1_Regex" OnServerValidate="CV1_CV_ServerValidate" isplay="Dynamic"></CustomValidatorInPage:CustomDependepntValidator>

            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"/>
        </div>
    </form>
</body>
</html>
