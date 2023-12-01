<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginMiBanco.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.LoginMiBanco" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Mi Banco - Login</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link href="../Estilos/Css_LoginBanco.css" rel="stylesheet" />
    <link href="../Estilos/Css_LoginMiBanco2.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <header style="background-color: #003554;">
            <div class="logo">
                <img src="../img/Mi_Banco.jpg" alt="Logo" />
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
        <div class="container">
            <div class="information">
                <hr />
                <h1>Iniciar Sesión</h1>
            </div>
            <br />
            <%--<asp:TextBox ID="txtcorreo" runat="server" placeholder="Correo Electrónico"></asp:TextBox><br />
            <asp:Button ID="btnSoliCodigo" runat="server" Text="Solicitar Codigo" CssClass="botonSubir" OnClick="btnSoliCodigo_Click" /><br />
            <br />

            <asp:TextBox ID="txtcodigoVerificion" runat="server" placeholder="Codigo de verificación" AutoCompleteType="Disabled"></asp:TextBox><br />

            <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" CssClass="botonSubir" OnClick="btnLogin_Click" /><br />
            <asp:Label ID="lblRespu" runat="server" Text=""></asp:Label>--%>
             <div id="paso1" runat="server">
                <asp:TextBox ID="txtcorreo" runat="server" placeholder="Correo Electrónico"></asp:TextBox><br />
                 <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" placeholder="Contraseña" CssClass="txtPasswordBanco"></asp:TextBox><br />
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
