<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="WebForm1.aspx.cs" Inherits="Test_Rama.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #Text1 {
            height: 17px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        APP SERVER TESTS<br />
        <br />
    
        IP:<input id="ip" type="text" runat="server" /><br />
        Puerto:<input id="puerto" type="text" runat="server" /><br />
        DB:<input id="database" type="text" runat="server" /><br />
        <select id="select" name="D1" runat="server">
            <option>Mongo</option>
            <option>SQL</option>
            <option>OPT</option>
        </select><br />
        Usuario:<input id="usu" type="text" runat="server" /><br />
        Contraseña:<input id="pw" type="text" runat="server" /><br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <br />
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
