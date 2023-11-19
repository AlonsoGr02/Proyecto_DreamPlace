<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginMiBanco.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.LoginMiBanco" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Mi Banco - Login</title>
    <link href="../Estilos/Css_LoginBanco.css" rel="stylesheet" />
</head>
<body>

    <div id="login-form-wrap" style="margin-bottom: 100px;">
        <h1>Mi Banco</h1>
        <h2>Inicio de sesión</h2>
        <form runat="server" id="Form1">
            <p>
                <input type="text" id="idUsuario" name="idUsuario" placeholder="Correo Electrónico" style="width: 250px;" /><i class="validation"><span></span><span></span></i>
            </p>
            <p>
                <asp:Button ID="btnSoliCodigo" runat="server" Text="Solicitar Codigo" Style="width: 250px;" OnClick="btnSoliCodigo_Click" />
            </p>
            <p>
                <input type="password" id="password" name="password" placeholder="Codigo de verificación" style="width: 250px;" /><i class="validation"><span></span><span></span></i>
            </p>
            <p>
                <asp:Button ID="Conectar" type="submit" runat="server" Style="width: 250px;" Text="Iniciar sesion" OnClick="Conectar_Click" />
            </p>

        </form>
        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
    </div>

</body>
</html>
