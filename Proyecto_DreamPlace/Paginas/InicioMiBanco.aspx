<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioMiBanco.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.InicioMiBanco" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Mi Banco</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.0/css/all.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;700&display=swap">
    <link href="../Estilos/Css_InicioMiBanco.css" rel="stylesheet" />
    <link href="../Estilos/Css_InicioMiBanco2.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <header style="background-color: #003554;">
            <div class="logo">
                <img src="../img/Mi_Banco.jpg" alt="Logo" />
            </div>


            <%--<div id="topnav" class="topnav-container" runat="server">
                <div class="topnav">
                    <a class="active" href="#home">Destacados</a>
                    <a href="#about">Lugares Famosos</a>
                    <input type="text" placeholder="Buscar ..."/>
                </div>
            </div>--%>

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
                    <li><a href="Inicio.aspx">Salir</a></li>
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
        <br />
        <div class="container">
            <div class="information">
                <div id="contenedor-info-dolar">
                <asp:Label ID="lblInfoDolar" runat="server" Text="Tipo de Cambio Dolar"></asp:Label>
                <hr />
                <div class="textbox-container">
                    <asp:TextBox ID="txtCompra" runat="server" ReadOnly></asp:TextBox>
                    <asp:TextBox ID="txtVenta" runat="server" ReadOnly></asp:TextBox>
                </div>
            </div>
                <h1>Mi Banco</h1>
            </div>

            <br />
            <asp:Label ID="lblCedula" runat="server" Text="Cédula:"></asp:Label><br />
            <asp:TextBox ID="txtCedula" runat="server" ReadOnly></asp:TextBox>
            <br />

            <asp:Label ID="Label1" runat="server" Text="Nombre del Usuario:"></asp:Label><br />
            <asp:TextBox ID="txtNombre" runat="server" ReadOnly></asp:TextBox>
            <br />


            <asp:Label ID="Label3" runat="server" Text="Número de Tarjeta:"></asp:Label><br />
            <asp:TextBox ID="txtNTarjeta" runat="server" ReadOnly></asp:TextBox>
            <br />

            <asp:TextBox ID="txtSaldoDisponible" runat="server" placeholder="Saldo Dsiponible" ReadOnly></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnAbrirModalDep" runat="server" Text="Depositar Fondos" OnClientClick="return openModalDeposito();" />

            <div id="myModalDeposito" class="modal">
                <div class="modal-content">
                    <span class="close" onclick="closeModalDeposito()">&times;</span>
                    <h2>Deposito de Dinero</h2>
                    <hr />
                    <asp:Label ID="Label2" runat="server" Text="Monto a Depositar:"></asp:Label><br />
                    <br />
                    <asp:TextBox ID="txtDeposito" runat="server" placeholder="Monto a Depositar"></asp:TextBox><br />
                    <br />

                    <asp:Button ID="btnDepositar" runat="server" Text="Depositar" CssClass="btn-guardar" />
                </div>
            </div>
            <script>
                // Función para abrir el modal
                function openModalDeposito() {
                    document.getElementById('myModalDeposito').style.display = 'block';
                    return false; // Evita que la página se recargue
                }

                // Función para cerrar el modal
                function closeModalDeposito() {
                    document.getElementById('myModalDeposito').style.display = 'none';
                }

                // Cierra el modal si se hace clic fuera de él
                window.onclick = function (event) {
                    if (event.target === document.getElementById('myModalDeposito')) {
                        closeModalDeposito();
                    }
                };
            </script>

            <asp:Button ID="btnMovimientos" runat="server" Text="Movimientos" OnClientClick="return openModalMovimientos();" />

            <div id="myModalMov" class="modal">
                <div class="modal-content">
                    <span class="close" onclick="closeModalMovimintos()">&times;</span>
                    <h2>Movimientos</h2>
                    <hr />

                    <br />
                    <br />

                    <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CssClass="btn-guardar" />
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
        </div>

    </form>
</body>
</html>
