<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PoliticasServicio.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.PoliticasServicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Políticas de Servicio</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link href="../Estilos/Css_Inicio2.css" rel="stylesheet" />
    <link href="../Estilos/Css_PoliticasS.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptManager1" runat="server" />


        <header style="background-color: #BDE038;">
            <div class="logo">
                <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo" />
            </div>


            <div id="topnav" class="topnav-container" runat="server">
                <div class="topnav">
                    <a class="active" href="#home">Destacados</a>
                    <a href="#about">Lugares Famosos</a>
                    <input type="text" placeholder="Buscar ..." />
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
                    <li><a href="Notis.aspx?Correo=<%= Session["Correo"] %>">Notificaciones</a></li>
                    <li><a href="PoliticasServicio.aspx?Correo=<%= Session["Correo"] %>">Políticas de Servicio</a></li>
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
                <div class="flex-container">
                    <a href="PrincipalHuesped.aspx?Correo=<%= Session["Correo"] %>">Volver al Inicio</a>
                    <asp:Label ID="Label1" runat="server">  >  </asp:Label>
                    <asp:Label ID="Labelseparador" runat="server" Text="Políticas de Servicio "></asp:Label>
                </div>
                <h1>Políticas de Servicio</h1>
            </div>
            <br />

            <div>
                <h2>Política de Cancelación:</h2>
                <p>Los anfitriones pueden establecer sus propias políticas de cancelación, que varían en términos de reembolso dependiendo de cuándo se cancele la reserva...</p>
            </div>

            <div>
                <h2>Verificación de Identidad:</h2>
                <p>Airbnb alienta a los usuarios a verificar sus identidades mediante métodos como la verificación de identificación oficial y codigos de verificación por correo...</p>
            </div>

            <div>
                <h2>Normas de la Comunidad:</h2>
                <p>Los usuarios deben seguir las normas de la comunidad de DreamPlace, que incluyen el respeto mutuo, la prohibición de la discriminación y el comportamiento adecuado...</p>
            </div>

            <div>
                <h2>Política Antidiscriminatoria</h2>
                <p>DreamPlace tiene una política estricta contra la discriminación y prohíbe la discriminación basada en características como raza, color, religión, género, orientación sexual, etc.</p>
            </div>

            <div>
                <h2>Prohibición de Subarrendamiento no Autorizado </h2>
                <p>Los huéspedes no pueden subarrendar propiedades sin el consentimiento del anfitrión y no pueden reservar propiedades en nombre de otra persona sin su conocimiento.</p>
            </div>

            <div>
                <h2>Proceso de Resolución de Problemas </h2>
                <p>DreamPlace ofrece un proceso de resolución de problemas para ayudar a mediar en disputas entre anfitriones e invitados.</p>
            </div>

            <div>
                <h2>Seguridad y Protección </h2>
                <p>DreamPlace tiene medidas de seguridad y protección, incluyendo revisiones de perfiles, seguros para anfitriones e invitados, y sistemas de revisión y retroalimentación.</p>
            </div>

            <div>
                <h2>Política de No Fumar </h2>
                <p>Muchos anfitriones tienen una política de no fumar en sus propiedades, y los huéspedes deben respetar estas reglas.</p>
            </div>

            <div>
                <h2>Política de Pagos </h2>
                <p>DreamPlace facilita los pagos entre anfitriones e invitados y proporciona opciones para el reembolso en caso de cancelaciones o problemas.</p>
            </div>

        </div>
    </form>
</body>
</html>
