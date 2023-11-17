<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>DreamPlace - Inicio</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.8.1/slick-theme.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600;700&display=swap"/>

    <link href="../Estilos/Css_Inicio.css" rel="stylesheet"  />
    <link href="../Estilos/Css_Inicio2.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptManager1" runat="server" />
      <%-- Hola sofi--%>

        <header style="background-color: #BDE038;">
            <div class="logo">
                <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo"/>
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
                        <img src="../img/user.png" alt="Avatar"/>
                    </div>
                </div>
            </div>
            <div id="user-menu" class="user-menu">
                <ul>
                    <li><a href="Registrarme.aspx">Registrarse</a></li>
                    <li><a href="Login.aspx">Iniciar Sesión</a></li>
                    
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
        <asp:UpdatePanel ID="updatePanel2" runat="server">
            <ContentTemplate>
                <div id="Div1" class="categorias-carousel" runat="server">
                    <div class="categorias" onclick='<%# Eval("IdCategoria", "SeleccionarCategoria({0})") %>'>
                        <asp:Repeater ID="repeaterCategorias" runat="server">
                            <ItemTemplate>
                                <div class="categoria" onclick='<%# Eval("IdCategoria", "SeleccionarCategoria(this,{0})") %>'>
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

      <%--  <asp:Repeater ID="repeater1" runat="server" OnItemCommand="RepeaterCategorias_ItemCommand">
            <!-- ... (resto del código) ... -->
        </asp:Repeater>--%>

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
            });
        </script>

        <script>
            function enviarDatos(event) {
                // Prevenir la recarga de la página
                event.preventDefault();

                // Obtener los valores de los campos del formulario
                var idCedula = document.getElementById('register-id-cedula').value;
                var correo = document.getElementById('register-username').value;
                var nombre = document.getElementById('register-nombre').value;
                var apellidos = document.getElementById('register-apellidos').value;
                var fechaNac = document.getElementById('register-fecha-nac').value;
                var telefono = document.getElementById('register-telefono').value;
                var tipoUser = document.getElementById('register-tipoUsuario').value;
                // Puedes agregar más campos según sea necesario

                // Enviar datos al servidor usando AJAX
                var xhr = new XMLHttpRequest();
                xhr.open('POST', 'Principal.aspx/GuardarDatos', true);
                xhr.setRequestHeader('Content-Type', 'application/json;charset=UTF-8');

                // Convertir datos a formato JSON
                var datos = {
                    idCedula: idCedula,
                    correo: correo,
                    nombre: nombre,
                    apellidos: apellidos,
                    fechaNac: fechaNac,
                    telefono: telefono,
                    tipoUser: tipoUser
                    // Puedes agregar más campos según sea necesario
                };

                xhr.onload = function () {
                    if (xhr.status === 200) {
                        console.log('Datos enviados correctamente');
                    } else {
                        console.error('Error al enviar datos');
                    }
                };

                // Convertir datos a formato JSON antes de enviar
                xhr.send(JSON.stringify(datos));
            }
        </script>
    </form>
</body>
</html>