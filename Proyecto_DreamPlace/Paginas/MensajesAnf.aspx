﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MensajesAnf.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.MensajesAnf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Mensajes</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link rel="stylesheet" href="../Estilos/Css_Mensajes.css" type="text/css" />
    <link href="../Estilos/footer.css" rel="stylesheet" />
    <style>
        /* Estilos generales */
   
        body{
            background-color: white;
        }
  
        .mensaje-item {
            margin-bottom: 10px;
        }

        /* Estilos para mensajes a la derecha */
        .mensaje-derecha {
            text-align: right;
        }

        .mensaje-izquierda {
            text-align: left;
        }

        .mensaje-derecha .mensaje {
            background-color: #dcf8c6; /* Color de fondo para mensajes a la derecha */
            padding: 8px;
            border-radius: 8px;
            display: inline-block;
            margin-bottom: 3px;
        }

        .mensaje-derecha .fecha {
            font-size: 0.75em; /* Tamaño de fuente más pequeño para la fecha */
            color: #888; /* Color de la fecha */
            text-align: right;
        }

        /* Estilos para mensajes a la izquierda */
        .mensaje-izquierda .mensaje {
            background-color: #d3cdcd; /* Color de fondo para mensajes a la izquierda */
            padding: 8px;
            border-radius: 8px;
            display: inline-block;
            margin-bottom: 3px;
        }

        .mensaje-izquierda .fecha {
            font-size: 0.75em; /* Tamaño de fuente más pequeño para la fecha */
            color: #888; /* Color de la fecha */
            text-align: left;
        }

        /*.list-group {
            height: 800px;*/ /* Altura máxima */
            /*overflow-y: auto;*/ /* Añade desplazamiento vertical si es necesario */
            /*border: 1px solid #ced4da;*/ /* Borde de la lista */
            /*border-radius: 5px;*/ /* Bordes redondeados */
        /*}*/

        .list-group-item {
            display: block; /* Muestra cada elemento en una línea */
            width: 350px;
            padding: 10px 15px; /* Espaciado interno */
            margin-bottom: -1px; /* Elimina el espacio entre elementos */
            background-color: #fff; /* Color de fondo de los elementos */
            border: 1px solid rgba(0, 0, 0, 0.125); /* Borde de los elementos */
            border-radius: 0; /* Bordes sin redondear */
        }

            .list-group-item:hover {
                background-color: #f5f5f5; /* Cambia el color de fondo al pasar el mouse */
                cursor: pointer; /* Cambia el cursor al pasar sobre los elementos */
            }
    </style>
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
                    <h2>Huéspedes</h2>
                    <br />
                    <div>
                        <asp:ListBox ID="lstHuespedes" runat="server" CssClass="list-group-item" AutoPostBack="true" OnSelectedIndexChanged="lstHuespedes_SelectedIndexChanged"></asp:ListBox>
                    </div>
                    <br />
                </div>
                <div class="chat-window">
                    <div class="chat-header">
                        <h2>Chat</h2>
                    </div>
                    <div class="chat-messages">
                        <asp:Repeater ID="rptMensajes" runat="server">
                            <ItemTemplate>
                                <div class="mensaje-item">
                                    <asp:Panel ID="panelMensaje" runat="server" CssClass='<%# ObtenerClaseMensaje(Eval("IdCedula").ToString()) %>'>
                                        <%-- Mensaje --%>
                                        <div class="mensaje">
                                            <%# Eval("Mensaje") %>
                                        </div>
                                        <%-- Fecha --%>
                                        <div class="fecha">
                                            <small><%# Eval("Fecha", "{0:yyyy-MM-dd HH:mm}") %></small>
                                        </div>
                                    </asp:Panel>
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
