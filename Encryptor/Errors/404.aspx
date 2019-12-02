<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="Encryptor.Errors._404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body runat="server" >
<asp:content  runat="Server" ContentPlaceHolderID="head">
    <div class="container">
        <h1>Ошибка.</h1>
        <h2>При обработке запроса произошла ошибка.</h2>
        <form id="form1" runat="server">
            <p>
                <asp:Button runat="server" OnClick="Login_Click" Height="76px" Width="311px" Text="Вернуться" class="btn bth-secondary"/>
            </p>
        </form>
    </div>
</asp:content>

    
</body>
</html>
