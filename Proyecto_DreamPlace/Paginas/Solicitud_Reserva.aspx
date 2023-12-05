<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Solicitud_Reserva.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.Solicitud_Reserva" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reserva</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.0/css/all.min.css" />
    <link href="Estilos/reservas.css" rel="stylesheet" />
    <link href="Estilos/navbar.css" rel="stylesheet" />
    <link href="../Estilos/solicitudreserva.css" rel="stylesheet" />
    <link href="../Estilos/Modal.css" rel="stylesheet" />
    <link href="../Estilos/footer.css" rel="stylesheet" />
    <link href="../Estilos/Css_Inicio2.css" rel="stylesheet" />
    <link href="../Estilos/Css_Inicio3.css" rel="stylesheet" />

</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <header style="background-color: #BDE038;">
                    <div class="logo">
                        <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo" />
                    </div>

                    <div id="topnav" class="topnav-container" runat="server">
                        <%--                        <div class="topnav">
                            <asp:Button ID="btnLFamosos" runat="server" Text="Lugares Famosos" CssClass="searchButton" />
                            <asp:Button ID="Button1" runat="server" Text="Filtrar" CssClass="searchButton" />
                            <input type="text" id="txtBusqueda" placeholder="Buscar por nombre..." runat="server" />

                        </div>--%>
                        <asp:Label ID="lblNombre" runat="server" Text=" " Style="color: black;"></asp:Label>
                        <asp:Label ID="lblApellido" runat="server" Text=" " Style="color: black;"></asp:Label>
                        <asp:Label ID="lblRol" runat="server" Text="" Style="color: black;"></asp:Label>

                    </div>
                    <div class="user-information">
                        <asp:Label ID="Label8" runat="server" Text=" Huésped "></asp:Label>
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
                            <li><a href="Notis.aspx?Correo=<%= Session["Correo"] %>">Notificaciones</a></li>
                            <li><a href="PoliticasServicio.aspx?Correo=<%= Session["Correo"] %>">Politicas de Servicio</a></li>
                            <li><a href="Inicio.aspx">Cerrar Sesión</a></li>
                        </ul>
                    </div>


                    <script>
                        // Agrega un evento de clic al icono de usuario
                        document.getElementById('menu-trigger').addEventListener('click', function () {
                            // Obtén el menú desplegable
                            var userMenu = document.getElementById('user-menu');

                            // Toggle (alternar) la clase 'active' para mostrar u ocultar el menú
                            userMenu.classList.toggle('active');
                        });

                        // Obtén referencias al modal y al botón de cerrar del modal
                        var loginModal = document.getElementById('login-modal');
                        var confirmaModal = document.getElementById('confirma-modal');
                        var registerModal = document.getElementById('register-modal');
                        var confirmaModalR = document.getElementById('confirma-modalR');

                        var closeLoginModal = document.getElementById('close-login-modal');
                        var closeConfirmaModal = document.getElementById('confirma-login-modal');
                        var closeRegisterModal = document.getElementById('close-register-modal');
                        var closeConfirmaModalR = document.getElementById('confima-login-modalR');

                        // Agrega un evento de clic al enlace "Registrase" en el menú
                        document.querySelector('a[href="#login"]').addEventListener('click', function (e) {
                            e.preventDefault();
                            //loginModal.style.display = 'none'; // Muestra el modal al hacer clic
                            registerModal.style.display = 'flex'; // Muestra el modal de registro
                        });

                        // Agrega un evento de clic al enlace "Iniciar Sesión" en el menú
                        document.querySelector('a[href="#loginR"]').addEventListener('click', function (e) {
                            e.preventDefault();
                            loginModal.style.display = 'flex'; // Muestra el modal al hacer clic
                        });

                        // Agrega un evento de clic al botón de cerrar del modal login
                        closeLoginModal.addEventListener('click', function () {
                            loginModal.style.display = 'none';
                        });

                        // Agrega un evento de clic al botón de cerrar del modal Registrarse
                        closeRegisterModal.addEventListener('click', function () {
                            registerModal.style.display = 'none';
                        });

                        // Agrega un evento de clic al botón de Registrarse en el modal de registro
                        document.getElementById('register-form').addEventListener('submit', function (e) {
                            e.preventDefault();
                            enviarDatos(event);
                            registerModal.style.display = 'none';
                            confirmaModalR.style.display = 'flex';  // Muestra el modal de confirmación
                        });

                        // Cierra el modal Login si se hace clic fuera de él
                        window.addEventListener('click', function (event) {
                            if (event.target === loginModal || event.target === confirmaModal || event.target === registerModal || event.target === confirmaModalR) {
                                loginModal.style.display = 'none';
                                confirmaModal.style.display = 'none';
                                registerModal.style.display = 'none';
                                confirmaModalR.style.display = 'none';
                            }
                        });

                        var continueButton = loginModal.querySelector('button[type="submit"]');

                        // Agrega un evento de clic al botón de continuar en el Login modal
                        continueButton.addEventListener('click', function (e) {
                            e.preventDefault(); // Previene el comportamiento predeterminado del botón
                            loginModal.style.display = 'none'; // Oculta el primer modal

                            // Muestra el modal de validación
                            var confirmaModal = document.getElementById('confirma-modal');
                            confirmaModal.style.display = 'flex';
                        });

                        // Agrega un evento de clic al botón de cerrar del modal Confirmar
                        closeConfirmaModal.addEventListener('click', function () {
                            confirmaModal.style.display = 'none';
                        });

                    </script>

                </header>

                <div class="container">

                    <div class="information">

                        <hr />
                        <h1>Solicitud de Reserva</h1>
                    </div>
                    <br />

                    <asp:Label ID="lblCedula" runat="server" Text="Cédula"></asp:Label><br />

                    <asp:TextBox ID="txtCedula" runat="server" placeholder="Cédula" ReadOnly></asp:TextBox><br />


                    <asp:TextBox ID="txtNumeroTarjeta" runat="server" placeholder="Número de Tarjeta" Visible="false"></asp:TextBox><br />


                    <hr />


                    <!-- Caja de reserva al lado derecho -->
                    <div class="container">
                        <asp:Panel ID="imageGallery" runat="server" CssClass="image-gallery">
                            <div class="image-gallery">
                                <!-- Las imágenes se agregarán dinámicamente aquí -->
                            </div>
                        </asp:Panel>
                    </div>


                    <br />
                    <asp:Label ID="lblTuviaje" runat="server" Text="Tu viaje"></asp:Label><br />

                    <asp:TextBox ID="txtDestino" runat="server" placeholder="Destino" ReadOnly></asp:TextBox><br />

                    <asp:Label ID="lblfechaLlegada" runat="server" Text="Fecha de Llegada"></asp:Label><br />
                    <asp:TextBox ID="txtfechaLlegada" runat="server" placeholder="Fecha de Llegada" ReadOnly></asp:TextBox><br />

                    <asp:Label ID="lblfechaSalida" runat="server" Text="Fecha de Salida"></asp:Label><br />
                    <asp:TextBox ID="txtfechaSalida" runat="server" placeholder="Fecha de Salida" ReadOnly></asp:TextBox><br />

                    <asp:Label ID="lblHuespedes" runat="server" Text="Cantidad de huéspedes"></asp:Label><br />
                    <asp:TextBox ID="txttxtHuespedes" runat="server" placeholder="Cantidad de huéspedes" ReadOnly></asp:TextBox><br />


                    <asp:Label ID="Label2" runat="server" Text="Costo por noche:"></asp:Label>
                    <asp:Label ID="lblCostoxNoche" runat="server" Text=""></asp:Label><br />

                    <%--                    <asp:Label ID="Label3" runat="server" Text="Impuestos:"></asp:Label>
                    <asp:Label ID="lblImpuestos" runat="server" Text=""></asp:Label><br />--%>
                    <asp:Label ID="lblDescuento" runat="server" Text=""></asp:Label><br /><br />

                    <asp:Label ID="Label4" runat="server" Text="Total:"></asp:Label>
                    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label><br /><br />




                    <asp:Button ID="ConfirmarReservaButton" runat="server" Text="Confirmar Reserva" OnClick="ConfirmarReservaButton_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar Reserva" OnClick="btnCancelar_Click" />

                </div>
                </div>
                </div>

                <!-- Modal de reserva exitosa -->
                <div id="MostrarModalExito" class="modal" style="display: none; justify-content: center; align-items: center;">
                    <div class="modal-content" style="text-align: center;">
                        <h2 style="font-size: 2em;">¡Reserva Exitosa!</h2>
                        <div style="font-size: 1.2em; margin-bottom: 10px;">Tu reserva se ha realizado con éxito</div>
                        <div>
                            <i class="fa fa-check-circle" style="color: green; font-size: 3em;"></i>
                        </div>
                    </div>
                </div>

                <script>
                    function AbrirModalExito() {
                        var modal = document.getElementById("MostrarModalExito");
                        modal.style.display = "flex";
                        setTimeout(function () {
                            CerrarModalExito();
                        }, 6000);
                    }
                    function CerrarModalExito() {
                        var modal = document.getElementById("MostrarModalExito");
                        modal.style.display = "none";
                    }


                </script>


                <%--saldo insuficiente--%>

                <!-- Modal de saldo insuficiente -->
                <div id="MostrarModalSaldoInsuficiente" class="modal" style="display: none; justify-content: center; align-items: center;">
                    <div class="modal-content" style="text-align: center;">
                        <h2 style="font-size: 2em;">Saldo Insuficiente</h2>
                        <div style="font-size: 1.2em; margin-bottom: 10px;">No hay saldo suficiente en tu tarjeta</div>
                        <div>
                            <i class="fa fa-times-circle" style="color: red; font-size: 3em;"></i>
                        </div>
                    </div>
                </div>

                <script>
                    function AbrirModalSaldoInsuficiente() {
                        var modal = document.getElementById("MostrarModalSaldoInsuficiente");
                        modal.style.display = "flex"; // Mostrar el modal
                        setTimeout(function () {
                            CerrarModalSaldoInsuficiente();
                        }, 6000);
                    }

                    function CerrarModalSaldoInsuficiente() {
                        var modal = document.getElementById("MostrarModalSaldoInsuficiente");
                        modal.style.display = "none"; // Ocultar el modal
                    }
                </script>


            </ContentTemplate>
        </asp:UpdatePanel>
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
