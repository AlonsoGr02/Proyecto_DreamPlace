<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrincipalHuesped.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.PrincipalHuesped" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>DreamPlace - Húesped</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick-theme.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600;700&display=swap" />

    <link href="../Estilos/Css_Inicio.css" rel="stylesheet" />
    <link href="../Estilos/Css_Inicio2.css" rel="stylesheet" />
    <link href="../Estilos/Css_Inicio3.css" rel="stylesheet" />
    <link href="../Estilos/Tarjeta.css" rel="stylesheet" />
    <link href="../Estilos/footer.css" rel="stylesheet" />
    <link href="../Estilos/Css_Modal.css" rel="stylesheet" />


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
                        <asp:Button ID="btnLFamosos" runat="server" Text="Lugares Famosos" OnClick="btnLFamosos_Click" CssClass="searchButton" />
                        <asp:Button ID="Button1" runat="server" Text="Filtrar" OnClick="Button1_Click" CssClass="searchButton" />
                        <input type="text" id="txtBusqueda" placeholder="Buscar por nombre..." runat="server" />

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
                        <li><a href="Cuenta.aspx?Correo=<%= Session["Correo"] %>">Cuenta</a></li>
                        <li><a href="Favoritos.aspx?Correo=<%= Session["Correo"] %>">Favoritos</a></li>
                        <li><a href="#" onclick="openModalMovimientos()">Notificaciones</a></li>
                        <li><a href="PoliticasServicio.aspx?Correo=<%= Session["Correo"] %>">Politicas de Servicio</a></li>
                        <li><a href="Inicio.aspx">Cerrar Sesión</a></li>
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

                     <div id="myModalMov" class="modal">
                        <div class="modal-content">
                            <span class="close" onclick="closeModalMovimintos()">&times;</span>
                            <h2>Notificaciones</h2>
                            <hr />
                            <div style="margin: 0 auto;">
                                <asp:GridView ID="gvNotificaciones" runat="server" Style="margin: 0 auto;" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#BDE038" Font-Bold="True" ForeColor="Black" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E3EAEB" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                </asp:GridView>
                                <br />
                                <br />

                            </div>
                        </div>
                    <script>
                        // Función para abrir el modal
                        function openModalMovimientos() {
                            document.getElementById('myModalMov').style.display = 'block';
                            return false; // Evita que la página se recargue
                        }

                        // Función para cerrar el modal
                        function closeModalMovimintos() {
                            document.getElementById('myModalMov').style.display = 'none';
                        }

                        // Cierra el modal si se hace clic fuera de él
                        window.onclick = function (event) {
                            if (event.target === document.getElementById('myModalMov')) {
                                closeModalMovimintos();
                            }
                        };
                    </script>


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
