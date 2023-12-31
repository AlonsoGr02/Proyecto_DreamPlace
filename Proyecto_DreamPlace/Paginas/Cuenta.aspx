﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cuenta.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.Cuenta" %>


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
    <link href="../Estilos/footer.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <header style="background-color: #BDE038;">
            <div class="logo">
                <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo" />
            </div>

            <div id="Div1" class="topnav-container" runat="server">
                <%--                        <div class="topnav">
                            <asp:Button ID="btnLFamosos" runat="server" Text="Lugares Famosos" CssClass="searchButton" />
                            <asp:Button ID="Button1" runat="server" Text="Filtrar" CssClass="searchButton" />
                            <input type="text" id="txtBusqueda" placeholder="Buscar por nombre..." runat="server" />

                        </div>--%>
                <asp:Label ID="lblRol" runat="server" Text="" Style="color: black;"></asp:Label>
                <asp:Label ID="Label1" runat="server" Text=" · " Style="color: black;"></asp:Label>
                <asp:Label ID="lblNombre" runat="server" Text="Nombre completo" Style="color: black;"></asp:Label>
                <asp:Label ID="lblApellido" runat="server" Text="Apellidos" Style="color: black;"></asp:Label>
                <asp:Label ID="lblCorreo" runat="server" Text="Correo" Visible="false"></asp:Label>
                <asp:Label ID="Label3" runat="server" Text=" · "></asp:Label>
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
                    <li><a href="PrincipalHuesped.aspx?Correo=<%= Session["Correo"] %>">Principal</a></li>
                    <li><a href="PoliticasServicio.aspx?Correo=<%= Session["Correo"] %>">Politicas de Servicio</a></li>
                    <li><a href="VerficarIdentidad.aspx?Correo=<%= Session["Correo"] %>">Verficación de Identidad</a></li>
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


                    <a href="Perfil_Huesped.aspx?Correo=<%= Session["Correo"] %>">Ir al Perfil</a>
                </div>
            </div>
            <br />



            <div class="card-container">
                <!-- Tarjeta de Perfil -->
                <div class="flip-card">
                    <a href="Perfil_Huesped.aspx?Correo=<%= Session["Correo"] %>" />
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

                <!-- Tarjeta de Info Reservas -->
                <div class="flip-card">
                    <a href="Info_Reservas.aspx?Correo=<%= Session["Correo"] %>" />
                    <div class="flip-card-inner">
                        <div class="flip-card-front">
                            <i class="fa-solid fa-house" style="font-size: 150px; line-height: 200px;"></i>
                        </div>
                        <div class="flip-card-back">
                            <h2>Historial de Reservas</h2>
                            <p>Mira el Historial de reservas que haz hecho durante tu estadia en DreamPlace</p>
                        </div>
                    </div>
                </div>

                <!-- Tarjeta de Favoritos -->
                <div class="flip-card">
                    <a href="Favoritos.aspx?Correo=<%= Session["Correo"] %>">
                        <div class="flip-card-inner">
                            <div class="flip-card-front">
                                <i class="fa-solid fa-heart" style="font-size: 150px; line-height: 200px;"></i>
                            </div>
                            <div class="flip-card-back">
                                <h1>Favoritos</h1>
                                <p>Mira tu información del alojamiento que tanto deseaste para estas vacaciones</p>
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
                                <asp:Button ID="Button2" runat="server" Text="Cerrar" OnClientClick="CerrarModalEvaluacion()" />
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


                    function CerrarModalEvaluacion() {
                        var modal = document.getElementById("modalEvaluacion");
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
                        var url = 'Mi_Banco.aspx?correo=' + encodeURIComponent(correo);

                        // Redirige a la página Mi_Banco.aspx con el parámetro 'correo'
                        window.location.href = url;
                    }
                </script>

                <!-- Tarjeta de Notificaciones -->
                <div class="flip-card">
                    <a href="Notis.aspx?Correo=<%= Session["Correo"] %>" />
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
                <div class="flip-card">
                    <a href="Mensajes.aspx?Correo=<%= Session["Correo"] %>">
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
                <div class="flip-card" onclick="AbrirModalEvalucion(event)">

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



                <!-- Modal Evaluación -->
                <div id="modalEvaluacion" class="modal">
                    <div class="modal-content">
                        <div class="modal-content-inner">
                            <h2>Calificar Inmueble</h2>
                            <div class="container">
                                <asp:Repeater ID="repeaterInmuebles" runat="server">
                                    <HeaderTemplate>
                                        <table>
                                            <thead>
                                                <tr>
                                                    <th>Nombre Inmueble</th>
                                                    <th>Fecha de entrdas</th>
                                                    <th>Fecha de salida</th>
                                                    <th>Calificación</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("NombreInmueble") %></td>
                                            <td><%# Eval("FechaI") %></td>
                                            <td><%# Eval("FechaF") %></td>
                                            <%--  <td>
                                                <%# Eval("IdCedula") + " " + Eval("ApellidoHuesped") %>
                                            </td>--%>
                                            <td>
                                                <label class="star-checkbox" style="display: flex; align-items: center;">
                                                    <asp:CheckBox ID="chkStar1" runat="server" CssClass="star-checkbox" Text="1" OnCheckedChanged="chkStar_CheckedChanged" AutoPostBack="true" />
                                                    <asp:CheckBox ID="chkStar2" runat="server" CssClass="star-checkbox" Text="2" OnCheckedChanged="chkStar_CheckedChanged" AutoPostBack="true" />
                                                    <asp:CheckBox ID="chkStar3" runat="server" CssClass="star-checkbox" Text="3" OnCheckedChanged="chkStar_CheckedChanged" AutoPostBack="true" />
                                                    <asp:CheckBox ID="chkStar4" runat="server" CssClass="star-checkbox" Text="4" OnCheckedChanged="chkStar_CheckedChanged" AutoPostBack="true" />
                                                    <asp:CheckBox ID="chkStar5" runat="server" CssClass="star-checkbox" Text="5" OnCheckedChanged="chkStar_CheckedChanged" AutoPostBack="true" />
                                                </label>
                                            </td>
                                            <td style="display: none;">
                                                <asp:Label ID="LabelIdCedula" runat="server" Text='<%# Eval("IdCedula") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblIdInmueble" runat="server" Text='<%# Eval("IdInmueble") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="LabelIdReserva" runat="server" Text='<%# Eval("IdReserva") %>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Button ID="btnEvaluarAnfitrion" runat="server" Text="Puntuar Anfitrión" AutoPostBack="true" OnClick="btnEnviarCalificacion_Click" />
                                <asp:Button ID="Button1" runat="server" Text="Cerrar" OnClientClick="CerrarModalEvaluacion()" />
                            </div>
                        </div>
                    </div>
                </div>

                <script>
                    function AbrirModalEvalucion(event) {
                        event.stopPropagation(); // Detener la propagación del clic para evitar cerrar el modal
                        var modal = document.getElementById("modalEvaluacion");
                        modal.style.display = "block";
                    }

                    document.getElementById("btnEvaluarAnfitrion").onclick = function () {
                        LimpiarModalEvaluacion(); // Limpia el modal al hacer clic en el botón de evaluar
                        var modal = document.getElementById("modalEvaluacion");
                        modal.style.display = "none";
                    }

                    var checkboxes = document.querySelectorAll('.star-checkbox input[type="checkbox"]');
                    checkboxes.forEach(function (checkbox) {
                        checkbox.onclick = function (event) {
                            event.stopPropagation();
                        };
                    });


                    function CerrarModalEvaluacion() {
                        LimpiarModalEvaluacion(); // Limpia el modal al cerrarlo
                        var modal = document.getElementById("modalEvaluacion");
                        modal.style.display = "none";
                    }

                    window.onclick = function (event) {
                        var modal = document.getElementById("modalEvaluacion");
                        var clickedElement = event.target;

                        var isOutsideModal = !modal.contains(clickedElement);

                        if (isOutsideModal) {
                            modal.style.display = "none";
                        }
                    }

                    function LimpiarModalEvaluacion() {
                        var repeater = document.getElementById("<%= repeaterInmuebles.ClientID %>");
                        repeater.innerHTML = ''; // Limpiar el contenido del repeater
                    }


                </script>

                <!-- Modal de calificacion exitosa -->
                <div id="MostrarModalExito" class="modal" style="display: none; justify-content: center; align-items: center;">
                    <div class="modal-content" style="text-align: center;">
                        <h2 style="font-size: 2em;">¡Calificación Exitosa!</h2>
                        <div style="font-size: 1.2em; margin-bottom: 10px;">Tu calificación se realizó con exito!</div>
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
                <div id="MostrarModalErrorCalificacion" class="modal" style="display: none; justify-content: center; align-items: center;">
                    <div class="modal-content" style="text-align: center;">
                        <h2 style="font-size: 2em;">¡Error!</h2>
                        <div style="font-size: 1.2em; margin-bottom: 10px;">Error al calificar</div>
                        <div>
                            <i class="fa fa-times-circle" style="color: red; font-size: 3em;"></i>
                        </div>
                    </div>
                </div>

                <script>
                    function AbrirModalErrorCalificacion() {
                        var modal = document.getElementById("MostrarModalErrorCalificacion");
                        modal.style.display = "flex"; // Mostrar el modal
                        setTimeout(function () {
                            CerrarModalSaldoInsuficiente();
                        }, 6000);
                    }

                    function CerrarModalSaldoInsuficiente() {
                        var modal = document.getElementById("MostrarModalErrorCalificacion");
                        modal.style.display = "none"; // Ocultar el modal
                    }
                </script>


                <!-- Tarjeta de Denuncias -->
                <div class="flip-card">
                    <a href="Denuncias.aspx?Correo=<%= Session["Correo"] %>" />
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

