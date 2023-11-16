<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mensajes.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.Mensajes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Mensajes</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link rel="stylesheet" href="../Estilos/Css_Mensajes.css" type="text/css" />


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
                    <asp:Label ID="Label1" runat="server"> > </asp:Label>
                    <asp:Label ID="Labelseparador" runat="server" Text="Mensajes "></asp:Label>
                </div>
                <h1>Mensajes</h1>
            </div>
            <br />
            <div class="chat-container">
                <div class="user-list">
                    <h2>Anfitriones</h2>
                    <asp:DropDownList ID="ddlAnfitriones" runat="server"></asp:DropDownList>
                </div>
                <div class="chat-window">
                    <div class="chat-header">
                        <h2>Chat</h2>
                    </div>
                    <div class="chat-messages">
                        <asp:Repeater ID="rptMensajes" runat="server">
                            <ItemTemplate>
                                <div class="mensaje-item">
                                    <strong><%# Eval("Fecha", "{0:yyyy-MM-dd HH:mm}  >>>  ") %></strong>
                                    <%# Eval("Mensaje") %>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>
                    <div class="chat-input">
                        <asp:TextBox ID="txtMensaje" runat="server" placeholder="Escribe un mensaje..." AutoCompleteType="Disabled"></asp:TextBox>
                        <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
