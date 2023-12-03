<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Iniciar Sesión</title>

    <link href="../Estilos/Css_Login1.css" rel="stylesheet" />
    <link href="../Estilos/Css_Login2.css" rel="stylesheet" />
    <link href="../Estilos/footer.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar">
            <a href="Inicio.aspx">
                <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo" class="logo" />
            </a>
            <div class="navbar-links">
                <a href="#"><i class="fa-solid fa-user" style="color: #000000; font-size: 30px;"></i></a>
            </div>
        </nav>
        <div class="container">
            <div class="information">
                <div class="flex-container">
                    <a href="Inicio.aspx">Inicio</a>
                    <asp:Label ID="Label1" runat="server">  >  </asp:Label>
                    <asp:Label ID="Labelseparador" runat="server" Text="Iniciar Sesión "></asp:Label>
                </div>
                <hr />
                <h1>Iniciar Sesión</h1>
            </div>
            <br />

            <div id="paso1" runat="server">
                <asp:TextBox ID="txtcorreo" runat="server" placeholder="Correo Electrónico"></asp:TextBox><br />
                <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" placeholder="Contraseña" CssClass="txtContra"></asp:TextBox><br />
                <asp:Button ID="btnSoliCodigo" runat="server" Text="Validar" CssClass="botonSubir" OnClick="btnSoliCodigo_Click" /><br />
                <br />
            </div>

            <div id="paso2" runat="server">
                <asp:TextBox ID="txtcodigoVerificion" runat="server" placeholder="Codigo de verificación" AutoCompleteType="Disabled"></asp:TextBox><br />
                <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" CssClass="botonSubir" OnClick="btnLogin_Click" /><br />
            </div>
            <asp:Label ID="lblRespu" runat="server" Text=""></asp:Label>
        </div>

    </form>
    
</body>
</html>
