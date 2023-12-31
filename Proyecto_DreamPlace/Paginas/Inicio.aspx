﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>DreamPlace - Inicio</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick-theme.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600;700&display=swap" />

    <link href="../Estilos/footer.css" rel="stylesheet" />
    <link href="../Estilos/Css_Inicio.css" rel="stylesheet" />
    <link href="../Estilos/Css_Inicio2.css" rel="stylesheet" />
    <link href="../Estilos/Css_Inicio3.css" rel="stylesheet" />
    <link href="../Estilos/Tarjeta.css" rel="stylesheet" />
    <style>
        .topnav-container {
            background-color: #f2f2f2;
            border-radius: 25px;
            padding: 5px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            max-width: 750px;
            margin: 0 auto;
        }

        /* Estilo de los enlaces de navegación */
        .topnav a {
            color: black;
            text-decoration: none;
            padding: 5px 5px; /* Espacio alrededor de los enlaces */
            border-radius: 5px;
        }

            .topnav a:hover {
                background-color: #ddd;
                color: black;
            }

        /* Estilo de la barra de búsqueda */
        .topnav input[type="text"] {
            padding: 10px;
            border: none;
            border-radius: 10px;
            width: 340px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptManager1" runat="server" />

        <div class="prueba">
            <header style="background-color: #BDE038;">
                <div class="logo">
                    <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo" />
                </div>

                <div id="topnav" class="topnav-container" runat="server">
                    <div class="topnav">

                        <input type="text" id="txtBusqueda" placeholder="Buscar por nombre..." runat="server" />
                        <asp:Button ID="Button1" runat="server" Text="Filtrar" OnClick="Button1_Click" CssClass="searchButton" />
                         <asp:Button ID="Button2" runat="server" Text="Reestablecer Filtros" CssClass="searchButton" OnClick="Button2_Click" />
                    </div>
                </div>

                <div class="icon-container" id="menu-trigger">
                    <div class="menu-icon">
                        <i class="fa-solid fa-bars"></i>
                    </div>
                    <div class="avatar">
                        <div class="avatar-box">
                            <img src="../img/user.png" alt="Avatar" />
                        </div>
                    </div>
                </div>
                <div id="user-menu" class="user-menu">
                    <ul>
                        <li><a href="Registrarme.aspx">Registrarse</a></li>
                        <li><a href="Login.aspx">Iniciar Sesión</a></li>

                    </ul>
                </div>
            </header>
            <br />
            <asp:UpdatePanel ID="updatePanel2" runat="server">
                <ContentTemplate>
                    <div id="Div1" class="categorias-carousel" runat="server">
                        <div class="categorias">
                            <asp:Repeater ID="repeaterCategorias" runat="server">
                                <ItemTemplate>
                                    <div class="categoria" onclick='<%# Eval("IdCategoria", "SeleccionarCategoria(this, {0})") %>'>
                                        <img src='<%# Eval("ImagenBase64") %>' alt='<%# Eval("Categoria") %>' />
                                        <span><%# Eval("Categoria") %></span>
                                        <asp:Label ID="lblIdCategoria" runat="server" Text='<%# Eval("IdCategoria") %>' Visible="false"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <asp:HiddenField ID="HiddenFieldIdCategoria" runat="server" ClientIDMode="Static" />
                    </div>
                    <script type="text/javascript">
                        var tarjetaSeleccionada = null;
                        var colorOriginal = null;

                        function SeleccionarCategoria(elemento, idCategoria) {
                            if (tarjetaSeleccionada !== null) {
                                tarjetaSeleccionada.style.border = colorOriginal;
                            }

                            if (tarjetaSeleccionada !== elemento) {
                                colorOriginal = elemento.style.border;
                                elemento.style.border = '2px solid blue';
                                tarjetaSeleccionada = elemento;
                                document.getElementById('<%= HiddenFieldIdCategoria.ClientID %>').value = idCategoria;
                            } else {
                                elemento.style.border = colorOriginal;
                                tarjetaSeleccionada = null;
                                document.getElementById('<%= HiddenFieldIdCategoria.ClientID %>').value = '';
                            }
                        }
                    </script>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="contenedorTarjetasFiltradas" runat="server" class="contenedor-tarjetas">
            <!-- Las tarjetas generadas dinámicamente se agregarán aquí -->
        </div>

        <div id="contenedorTarjetas" runat="server" class="contenedor-tarjetas">
            <!-- Las tarjetas generadas dinámicamente se agregarán aquí -->
        </div>

        <!-- scripts para el carrusel de imagenes funcione -->
        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick.min.js"></script>
        <script>
            $(document).ready(function () {
                $('.carousel').slick({
                    slidesToShow: 1,
                    slidesToScroll: 1,
                    prevArrow: '<button class="slick-prev" aria-label="Previous" type="button">&#9664;</button>', /* Flecha izquierda */
                    nextArrow: '<button class="slick-next" aria-label="Next" type="button">&#9654;</button>' /* Flecha derecha */
                });

                // Agrega un evento de clic al icono de usuario
                document.getElementById('menu-trigger').addEventListener('click', function () {
                    // Obtén el menú desplegable
                    var userMenu = document.getElementById('user-menu');

                    // Toggle (alternar) la clase 'active' para mostrar u ocultar el menú
                    userMenu.classList.toggle('active');
                });
            });
        </script>
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
