<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CuentaAnfitrion.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.CuentaAnfitrion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Cuenta</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.0/css/all.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>    
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="StyleSheet" href="../Estilos/Css_Cuenta.css" type="text/css" />
    <link href="../Estilos/Modal.css" rel="stylesheet" type="text/css" />
    <link href="../Estilos/Css_Registro.css" rel="stylesheet" />
    <link href="../Estilos/Css_Inicio2.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
         <header style="background-color: #BDE038;">
            <div class="logo">
                <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo">
            </div>
            

            <div id="topnav" class="topnav-container" runat="server">
                <div class="topnav">
                    <a class="active" href="#home">Destacados</a>
                    <a href="#about">Lugares Famosos</a>
                    <input type="text" placeholder="Buscar ..."/>
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
                <h1>Cuenta</h1>
                <div class="flex-container">
                    <asp:Label ID="Label2" runat="server" Text=" · "></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text=" · "></asp:Label>
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre completo" ></asp:Label>
                    <asp:Label ID="lblApellido" runat="server" Text="Apellidos" ></asp:Label>
                    <asp:Label ID="Labelseparador" runat="server" Text=", "></asp:Label>
                    <asp:Label ID="lblCorreo" runat="server" Text="Correo"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text=" · "></asp:Label>
                    <asp:Label ID="Labelseparrador2" runat="server" Text=" · "></asp:Label>
                    <a href="Perfil_Anfitrion.aspx?Correo=<%= Session["Correo"] %>">Ir al Perfil</a>
                </div>
            </div>
            <br />



            <div class="card-container">
                <!-- Tarjeta de Perfil -->
                <div class="flip-card">
                    <a href="Perfil_Anfitrion.aspx?Correo=<%= Session["Correo"] %>" />
                    <div class="flip-card-inner">
                        <div class="flip-card-front">
                            <i class="fa-solid fa-user" style="font-size: 150px; line-height: 200px;"></i>
                        </div>
                        <div class="flip-card-back">
                            <h1>Mi Perfil</h1>
                            <p>Mira tu información personal y verifica si tus datos estan correctos</p>
                        </div>
                    </div>
                </div>

                 <!-- Tarjeta de mante alojamiento -->
                 <div class="flip-card">
                    <a href="PublicarAnuncio.aspx?Correo=<%= Session["Correo"] %>">
                        <div class="flip-card-inner">
                            <div class="flip-card-front">
                                <i class="fa-solid fa-image" style="font-size: 150px; line-height: 200px;"></i>
                            </div>
                            <div class="flip-card-back">
                                <h2>Publicar Anuncio</h2>
                                <p>Crea un alojamiento para tus huéspedes</p>
                            </div>
                        </div>
                    </a>
                </div>
             

                <!-- Tarjeta de mante alojamiento -->
                 <div class="flip-card">
                    <a href="ManteAlojamiento.aspx?Correo=<%= Session["Correo"] %>">
                        <div class="flip-card-inner">
                            <div class="flip-card-front">
                                <i class="fa-solid fa-pen-to-square" style="font-size: 150px; line-height: 200px;"></i>
                            </div>
                            <div class="flip-card-back">
                                <h2>Mantenimiento Alojamiento</h2>
                                <p>Mira tu información del alojamiento</p>
                            </div>
                        </div>
                    </a>
                </div>
                <br />
               
                <!-- Tarjeta de Metodos de Pago -->
                <div class="flip-card" onclick="AbrirModal()">
                    <div class="flip-card-inner">
                        <div class="flip-card-front">
                            <i class="fa-solid fa-coins" style="font-size: 150px; line-height: 200px;"></i>
                        </div>
                        <div class="flip-card-back">
                            <h1>Metodos de Pago</h1>
                            <p>Mira tu información del alojamiento que tanto deseaste para estas vacaciones</p>
                        </div>
                    </div>
                </div>

                <%--ModalMetodosPago--%>
                <div id="modalMetodosPago" class="modal">
                    <div class="modal-content">
                        <span class="close" id="closeModalButton">&times;</span>
                        <!-- Contenido del modal -->
                        <div class="modal-content-inner">
                            <h2>Metodos de Pago</h2>
                            <div class="container">
                                <!-- Contenido del modal -->
                                <asp:Label ID="lblMCorreo" runat="server" Text="Correo"></asp:Label><br />

                                <asp:TextBox ID="txtCorreo" runat="server" placeholder="Cédula" ReadOnly></asp:TextBox><br />

                                <script>
                                    // Asigna el valor de la sesión al TextBox en el lado del cliente
                                    document.getElementById('<%= txtCorreo.ClientID %>').value = '<%= Session["Correo"] %>';
                                </script>

                                <asp:Label ID="lblNumeroDeTrajeta" runat="server" Text="Número de trajeta"></asp:Label><br />

                                <asp:TextBox ID="txtNumeroDeTrajeta" runat="server" placeholder="Número de trajeta"></asp:TextBox><br />

                                <asp:Label ID="lblFechaVencimiento" runat="server" Text="Fecha de Vencimiento"></asp:Label><br />

                                <asp:TextBox ID="txtFechaVencimiento" CssClass="comment-modal" runat="server" placeholder="Fecha de Vencimiento" OnClientClick="mostrarCalendarioentrada()"></asp:TextBox><br />

                                <asp:Label ID="lblcvv" runat="server" Text="CVV"></asp:Label><br />

                                <asp:TextBox ID="TXTCVV" runat="server" placeholder="CVV"></asp:TextBox><br />

                                <asp:Button ID="BtnAgregarTarjeta" runat="server" Text="Agregar Tarjeta" OnClick="BtnAgregarTarjeta_Click" />

                            </div>
                        </div>
                    </div>
                </div>

                <!-- Agrega este script al final de tu archivo HTML -->
                <script>
                    function AbrirModal() {
                        // Abre el modal al hacer clic en la tarjeta                       
                        var modal = document.getElementById("modalMetodosPago");
                        modal.style.display = "block";
                        // Puedes agregar más lógica aquí si es necesario
                    }

                    // Cierra el modal si se hace clic en la "x"
                    document.getElementById("closeModalButton").onclick = function () {
                        var modal = document.getElementById("modalMetodosPago");
                        modal.style.display = "none";
                    }

                    // Cierra el modal si se hace clic fuera del contenido del modal
                    window.onclick = function (event) {
                        var modal = document.getElementById("modalMetodosPago");
                        if (event.target == modal) {
                            modal.style.display = "none";
                        }
                    }
                </script>



                <script>
                    function mostrarCalendarioentrada() {
                        $("#fechaLlegada").datepicker({
                            dateFormat: 'yy-mm-dd',
                            onSelect: function (dateText, inst) {
                            }
                        });
                    }
                </script>

                <script>
                    $(document).ready(function () {
                        $("#<%=txtFechaVencimiento.ClientID %>").datepicker({
                            dateFormat: 'yy-mm-dd',
                            onSelect: function (dateText, inst) {
                                // Puedes agregar lógica adicional aquí si es necesario
                            }
                        });
                    });
                </script>



                <!-- Tarjeta de Mi Banco -->
                <div class="flip-card">
                    <a href="javascript:void(0);" onclick="redireccionarAMiBanco()" />
                    <div class="flip-card-inner">
                        <div class="flip-card-front">
                            <i class="fa-solid fa-landmark" style="font-size: 150px; line-height: 200px;"></i>
                        </div>
                        <div class="flip-card-back">
                            <h1>Mi Banco</h1>
                            <p>Mira tu información del alojamiento que tanto deseaste para estas vacaciones</p>
                        </div>
                    </div>
                </div>

                <script>
                    function redireccionarAMiBanco() {
                        // Obtén el valor de la sesión 'Correo' en el lado del cliente
                        var correo = '<%= Session["Correo"] %>';

                        // Construye la URL de redirección con el parámetro 'correo'
                        var url = 'Mi_BancoAnf.aspx?correo=' + encodeURIComponent(correo);

                        // Redirige a la página Mi_Banco.aspx con el parámetro 'correo'
                        window.location.href = url;
                    }
                </script>

                <!-- Tarjeta de Notificaciones -->
                <div class="flip-card">
                    <a href="NotificacionesAnf.aspx?Correo=<%= Session["Correo"] %>" />
                    <div class="flip-card-inner">
                        <div class="flip-card-front">
                            <i class="fa fa-bell" style="font-size: 150px; line-height: 200px;"></i>
                        </div>
                        <div class="flip-card-back">
                            <h2>Centro de Notificaciones</h2>
                            <p>Mira las novedades y alojamientos nuevos en DreamPlace</p>
                        </div>
                    </div>
                </div>
                <br />
                <!-- Tarjeta de Mensajes -->
                <!-- Tarjeta de Mensajes -->
                <div class="flip-card">
                    <a href="MensajesAnf.aspx?Correo=<%= Session["Correo"] %>">
                        <div class="flip-card-inner">
                            <div class="flip-card-front">
                                <i class="fa-solid fa-envelope" style="font-size: 150px; line-height: 200px;"></i>
                            </div>
                            <div class="flip-card-back">
                                <h1>Mensajes</h1>
                                <p>Conversa con tu afitrión</p>
                            </div>
                        </div>
                    </a>
                </div>

                <!-- Tarjeta de Evaluancion Experiencias -->
                <div class="flip-card">

                    <div class="flip-card-inner">
                        <div class="flip-card-front">
                            <i class="fa-solid fa-star" style="font-size: 150px; line-height: 200px;"></i>
                        </div>
                        <div class="flip-card-back">
                            <h1>Tu Experiencia</h1>
                            <p>Evalua tu experiencia segun el alojamiento donde te hospedaste </p>
                        </div>
                    </div>
                </div>

                <!-- Tarjeta de Denuncias -->
                <div class="flip-card">
                    <a href="DenunciasAnf.aspx?Correo=<%= Session["Correo"] %>" />
                    <div class="flip-card-inner">
                        <div class="flip-card-front">
                            <i class="fa-solid fa-circle-exclamation" style="font-size: 150px; line-height: 200px;"></i>
                        </div>
                        <div class="flip-card-back">
                            <h1>Denuncias</h1>
                            <p>Ten buena conducta en nuestra plataforma, eres un usuario valioso ❤ </p>
                        </div>
                    </div>
                </div>
            </div>




        </div>
    </form>
</body>
</html>
