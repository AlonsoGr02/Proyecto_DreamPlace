<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.Favoritos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Favoritos</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick-theme.min.css" />
    <link rel="StyleSheet" href="../Estilos/Css_Favoritos.css" type="text/css" />
    <link href="../Estilos/footer.css" rel="stylesheet" />
     <style>
        body{
            background-color: white;
        }
    </style>
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
                    <asp:Label ID="Labelseparador" runat="server" Text="Favoritos "></asp:Label>
                </div>
                <h1>Favoritos</h1>
            </div>
            <div id="container" runat="server" class="contenedor-tarjetas">
                <!-- Aquí se agregarán dinámicamente las tarjetas de inmuebles -->
            </div>
            <br />

        </div>
        <div id="Eliminardiv" runat="server">
            <asp:Label ID="lblIdElminar" runat="server" Text=""></asp:Label>
            <asp:Button ID="btnEliminarFav" runat="server" Text="Eliminar Favorito" OnClick="btnEliminarFav_Click" />
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
