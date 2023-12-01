<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MensajesAnf.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.MensajesAnf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Mensajes</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link rel="stylesheet" href="../Estilos/Css_Mensajes.css" type="text/css" />
    <link href="../Estilos/footer.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar">
            <a href="CuentaAnfitrion.aspx?Correo=<%= Session["Correo"] %>">
                <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo" class="logo" />
            </a>
            <div class="navbar-links">
                <a href="#"><i class="fa-solid fa-user" style="color: #000000; font-size: 30px;"></i></a>
            </div>
        </nav>
        <div class="container">
            <div class="information">
                <div class="flex-container">
                    <a href="CuentaAnfitrion.aspx?Correo=<%= Session["Correo"] %>">Cuenta</a>
                    <asp:Label ID="Label1" runat="server"> > </asp:Label>
                    <asp:Label ID="Labelseparador" runat="server" Text="Mensajes "></asp:Label>
                </div>
                <h1>Mensajes</h1>
            </div>
            <br />
            <div class="chat-container">
                <div class="user-list">
                    <h2>Huespedes</h2>
                    <asp:DropDownList ID="ddlhuesped" runat="server"></asp:DropDownList>
                </div>
                <div class="chat-window">
                    <div class="chat-header">
                        <h2>Chat</h2>
                    </div>
                    <div class="chat-messages">
                        <asp:Repeater ID="rptMensajes" runat="server">
                            <ItemTemplate>
                                <div class="mensaje-item">
                                    <strong><%# Eval("Fecha", "{0:yyyy-MM-dd HH:mm} >>> ") %></strong>
                                    <%# Eval("Mensaje") %>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="chat-input">
                        <asp:TextBox ID="txtMensaje" runat="server" placeholder="Escribe un mensaje..." AutoCompleteType="Disabled"></asp:TextBox>
                        <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviarMensaje_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
    <footer>
        <div class="footer-content">
            <img src="../img/dreamplacefooter.jpg" alt="Logo de Pie de Página" />
            <div class="footer-info">
                <h1 class="footer-text">DreamPlace</h1>
                <p class="footer-contact">Contacto: dreamplace@gmail.com | Teléfono: +506 55495224</p>
                <p class="footer-copyright">&copy; 2023 DreamPlace. Todos los derechos reservados.</p>
            </div>
        </div>
    </footer>
</body>
</html>
