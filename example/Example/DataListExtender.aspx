<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataListExtender.aspx.cs" Inherits="Example.DataListExtender" %>

<%@ Register TagPrefix="CustomDataListInPage" Namespace="WebFormHtml5DataList.Extender" Assembly= "WebFormHtml5DataList" %>

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
            <CustomDataListInPage:HTML5DataListControlExtender ID="HTML5DataListControlExtenderFromDataSource" TargetControlID="TextBox1" runat="server">                
            </CustomDataListInPage:HTML5DataListControlExtender>
            <br />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <CustomExtenderInWebConfig:HTML5DataListControlExtender ID="HTML5DataListControlExtenderListItem" TargetControlID="TextBox2" runat="server"/>
            <br />
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <CustomExtenderInWebConfig:HTML5DataListControlExtender ID="HTML5DataListControlExtenderListItemInPage" TargetControlID="TextBox3" runat="server">
                <asp:ListItem Value="123"></asp:ListItem>
                <asp:ListItem Value="456"></asp:ListItem>
            </CustomExtenderInWebConfig:HTML5DataListControlExtender>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Button" />
        </div>
    </form>
</body>
</html>
