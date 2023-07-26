<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Example.Default" %>

<%@ Register TagPrefix="CustomInPage" Namespace="WebFormHtml5DataList" Assembly= "WebFormHtml5DataList" %>

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
            <CustomInPage:HTML5DataListControlExtender ID="HTML5DataListControlExtenderFromDataSource" TargetControlID="TextBox1" runat="server">                
            </CustomInPage:HTML5DataListControlExtender>
            <br />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <CustomInWebConfig:HTML5DataListControlExtender ID="HTML5DataListControlExtenderListItem" TargetControlID="TextBox2" runat="server"/>
            <br />
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <CustomInWebConfig:HTML5DataListControlExtender ID="HTML5DataListControlExtenderListItemInPage" TargetControlID="TextBox3" runat="server">
                <asp:ListItem Value="123"></asp:ListItem>
                <asp:ListItem Value="456"></asp:ListItem>
            </CustomInWebConfig:HTML5DataListControlExtender>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Button" />
        </div>
    </form>
</body>
</html>
