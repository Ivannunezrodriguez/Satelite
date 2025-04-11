<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WordHtml.aspx.cs" Inherits="QRCode_Demo.WordHtml" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="btnUpload" runat="server" Text="submit" onserverClick="Upload" OnClick="Upload" />
        <hr />
        <div id = "dvWord" runat = "server"></div>
    </div>
    <br />
    </form>
</body>
</html>
