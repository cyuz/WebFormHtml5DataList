<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CusutomGridViewEncode.aspx.cs" Inherits="Example.CusutomGridViewEncode" %>

<%@ Register TagPrefix="CustomGridViewInPage" Namespace="WebFormHtml5DataList.GridView" Assembly= "WebFormHtml5DataList" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Val" HtmlEncode="false" HeaderText="two<br/>line"></asp:BoundField>
                    <CustomGridViewInPage:CustomHeaderEncodeBoundField DataField="Val" HeaderHtmlEncode="false" HeaderText="two<br/>line"></CustomGridViewInPage:CustomHeaderEncodeBoundField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
