<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerficarIdentidad.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.VerficarIdentidad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Verificación Identidad</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link href="../Estilos/Css_VerificarIdentidad.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar">
            <a href="PrincipalHuesped.aspx?Correo=<%= Session["Correo"] %>">
                <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo" class="logo" />
            </a>
            <div class="navbar-links">
                <a href="#"><i class="fa-solid fa-user" style="color: #000000; font-size: 30px;"></i></a>
            </div>
        </nav>
        <div class="container">
            <div class="information">
                <div class="flex-container">
                    <a href="Cuenta.aspx?Correo=<%= Session["Correo"] %>">Cuenta</a>
                    <asp:Label ID="Label1" runat="server">  >  </asp:Label>
                    <asp:Label ID="Labelseparador" runat="server" Text="Verificación "></asp:Label>
                </div>
                <h1>Verificación de Identidad</h1>
                <h3>Por la seguridad de la plataforma y de sus usuarios.</h3>
            </div>
            <br />
            <div>
                <asp:Label ID="lblInfo" runat="server" Text="Adjunta la foto frontal de la cédula"></asp:Label><br />
                <asp:FileUpload ID="FileUploadFrontal" runat="server" CssClass="botonSubir" /><br />

                <hr />

                <asp:Label ID="lblInfoTrasera" runat="server" Text="Adjunta una foto visible de tu cara"></asp:Label><br />
                <asp:FileUpload ID="FileUploadTrasera" runat="server" CssClass="botonSubir" /><br />

                <br />
                <asp:Button ID="btnValidar" runat="server" Text="Verificar" CssClass="botonSubir" OnClick="btnValidar_Click" /><br />
                <asp:Label ID="lblRespu" runat="server" Text=""></asp:Label>
            </div>

        </div>
    </form>
</body>
</html>
