<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reserva.aspx.cs" Inherits="Proyecto_DreamPlace.Reserva" %>

<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Import Namespace="CapaNegocio" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Hotel Reservation</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.0/css/all.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">



    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>


    <%--    Navbar--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />

    <link href="../Estilos/Css_Inicio2.css" rel="stylesheet" />
    <link href="../Estilos/Css_Inicio3.css" rel="stylesheet" />
    <link href="../Estilos/Css_Resena.css" rel="stylesheet" />
    <link href="../Estilos/footer.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" integrity="sha512-xjMY0gIb5Jjt/6ZcPlV16JdA85z3PkjGm9Uo8Mp+YOuLOxM5Ygv4dxX4STl3MgIbRYlpuT0D+qsMxZRdcti81A==" crossorigin="anonymous" referrerpolicy="no-referrer" />



    <link href="Estilos/navbar.css" rel="stylesheet" />
    <link href="../Estilos/Reserva.css" rel="stylesheet" />
    <style>
        .btn-favorito {
            background-color: #e74c3c; /* Color de fondo rojo */
            color: #fff; /* Color del texto */
            border: 1px solid #c0392b; /* Borde del botón */
            padding: 10px 20px; /* Espaciado interno */
            font-size: 16px; /* Tamaño del texto */
            cursor: pointer; /* Cambia el cursor al pasar el ratón por encima */
            border-radius: 5px; /* Bordes redondeados */
            display: block; /* Hace que el botón sea un bloque para centrarlo */
            margin: 0 auto; /* Centra el botón horizontalmente */
            transition: background-color 0.3s ease; /* Transición suave del color de fondo */
        }

            .btn-favorito:hover {
                background-color: #c0392b; /* Cambia el color de fondo al pasar el ratón por encima */
            }




    </style>
</head>

<body>
    <contenttemplate>
        <form id="form1" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                <ContentTemplate>
                    <asp:ScriptManager ID="scriptManager1" runat="server" />


                    <header style="background-color: #BDE038;">
                        <div class="logo">
                            <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo" />
                        </div>


                        <%--<div id="topnav" class="topnav-container" runat="server">
                            <div class="topnav">
                                <a class="active" href="#home">Destacados</a>
                                <a href="#about">Lugares Famosos</a>
                                <input type="text" placeholder="Buscar ..." />
                            </div>
                        </div>--%>
                        <div id="topnav" class="topnav-container" runat="server">
                            <div class="topnav">
                                <asp:Button ID="btnLFamosos" runat="server" Text="Lugares Famosos" CssClass="searchButton" />
                                <asp:Button ID="Button1" runat="server" Text="Filtrar" CssClass="searchButton" />
                                <input type="text" id="txtBusqueda" placeholder="Buscar por nombre..." runat="server" />
                            </div>
                        </div>


                        <asp:Label class="hotel-title" ID="lblMensajeError" runat="server" Text=""></asp:Label>


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
                                <li><a href="PrincipalHuesped.aspx?Correo=<%= Session["Correo"] %>">Cuenta</a></li>
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

                        <div class="hotel-details">
                            <div class="hotel-title">

                                <asp:Label class="hotel-title" ID="lblNombre" runat="server" Text="El Panoramic Hotel"></asp:Label>

                                <!-- Botón para abrir el modal -->
                                <asp:Button class="send-button" ID="btnAbrirmodalReserva" runat="server" Text="Reservar" OnClick="AbrirModal" />
                                <asp:Button ID="btnFavorritos" runat="server" Text="Agregar Favoritos" CssClass="btn-favorito" OnClick="btnFavorritos_Click" />
                            </div>

                            <%--Modal error reserva--%>
                            <div id="MostrarModalError" class="modal" style="display: none; justify-content: center; align-items: center;">
                                <div class="modal-content" style="text-align: center;">
                                    <h2 style="font-size: 2em;">Error</h2>
                                    <div style="font-size: 1.2em; margin-bottom: 10px;">La fecha seleccionada no está disponible</div>
                                    <div>
                                        <i class="fa fa-times-circle" style="color: red; font-size: 3em;"></i>
                                    </div>
                                </div>
                            </div>


                            <script>
                                function AbrirModalError() {
                                    var modal = document.getElementById("MostrarModalError");
                                    modal.style.display = "flex";
                                    setTimeout(function () {
                                        CerrarModalError();
                                    }, 6000);
                                }


                                function CerrarModalError() {
                                    var modal = document.getElementById("MostrarModalError");
                                    modal.style.display = "none";
                                }
                            </script>



                            <asp:Panel ID="imageGallery" runat="server" CssClass="image-galleryy">
                                <div class="container">
                                    <!-- Las imágenes se agregarán dinámicamente aquí -->
                                </div>
                            </asp:Panel>

                        </div>

                        <div class="mini-cardDescription">
                            <div>
                                <asp:Label class="hotel-title" ID="Label6" runat="server" Text="Descripción del hotel:"></asp:Label>
                            </div>



                            <div>
                                <asp:Label ID="lbldescripcion" runat="server" Text="El Panoramic Hotel es un moderno y elegante hotel de 4 estrellas, asomado al mar, ideal para unas vacaciones románticas y de gran encanto, en el mágico escenario de Taormina y del Mar de Sicilia."></asp:Label>
                            </div>
                        </div>

                        <br />
                        <div class="mini-card">
                            <div>
                                <asp:Label class="hotel-description" ID="lblPrecioS" runat="server" Text="Precio por noche:₡ "></asp:Label>
                                <asp:Label class="hotel-description" ID="lblPrecio" runat="server" Text=""></asp:Label>

                            </div>
                        </div>
                        <br />

                        <div class="hotel-ratings">
                            <asp:Label class="fas fa-star" ID="lblcalificacion" runat="server" Text=" Puntuacion: 4.5"></asp:Label>
                        </div>
                        <br />

                        <br />
                        <div>
                            <asp:Label class="hotel-title" ID="lblofrecelugar" runat="server" Text="Lo que ofrece el lugar:"></asp:Label>
                        </div>

                        <br />

                        <div class="mini-cardservicios">
                            <div class="info-container">
                                <div class="info-item">
                                    <i class="icon fas fa-users"></i>
                                    <span>
                                        <asp:Label ID="Label2" Text="Cantidad Personas:" runat="server"></asp:Label>
                                        <asp:Label ID="lblCantidadPersonas" runat="server"></asp:Label>
                                    </span>
                                </div>
                                <div class="info-item">
                                    <i class="icon fas fa-bed"></i>
                                    <span>
                                        <asp:Label ID="Label3" Text="Cantidad Dormitorios:" runat="server"></asp:Label>
                                        <asp:Label ID="lblCantidadDormitorios" runat="server"></asp:Label>
                                    </span>
                                </div>
                                <div class="info-item">
                                    <i class="icon fas fa-bath"></i>
                                    <span>
                                        <asp:Label ID="Label4" Text="Cantidad Baños:" runat="server"></asp:Label>
                                        <asp:Label ID="lblCantidadBanos" runat="server"></asp:Label>
                                    </span>
                                </div>
                                <div class="info-item">
                                    <i class="icon fas fa-bed"></i>
                                    <span>
                                        <asp:Label ID="Label5" Text="Cantidad Camas:" runat="server"></asp:Label>
                                        <asp:Label ID="lblCantidadCamas" runat="server"></asp:Label>
                                    </span>
                                </div>
                            </div>
                        </div>


                        <br />
                        <div class="cardDescription">

                            <div>
                                <asp:Label class="hotel-title" ID="Label1" runat="server" Text="Amenidades:"></asp:Label>
                            </div>


                            <div class="amenities">
                                <asp:Repeater ID="rptAmenities" runat="server">
                                    <ItemTemplate>
                                        <div class="amenity-card">
                                            <i class='<%# ObtenerIconClass((byte[])Eval("Icono"), Eval("Nombre").ToString()) %>'></i>
                                            <%# Eval("Nombre") %>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>


                            <div class="amenities">
                                <div class="amenity-card">
                                    <i class="fas fa-wifi amenity-icon"></i>
                                    Wi-Fi
                                </div>
                                <div class="amenity-card">
                                    <i class="fas fa-parking amenity-icon"></i>
                                    Aparcamiento
                                </div>
                                <div class="amenity-card">
                                    <i class="fas fa-swimming-pool amenity-icon"></i>
                                    Piscina
                                </div>
                                <div class="amenity-card">
                                    <i class="fas fa-utensils amenity-icon"></i>
                                    Restaurante
                                </div>
                            </div>

                        </div>
                        <div>
                            <label class="hotel-title" id="Label7">Fechas disponibles:</label>
                        </div>
                        <br />
                        <%--  <div class="month">
                            <ul>
                                <li class="prev">&#10094;</li>
                                <li class="next">&#10095;</li>
                                <li id="month-year"></li>
                            </ul>
                        </div>

                        <ul class="days" id="calendar-days">
                            <!-- Días del mes actual se añadirán aquí -->
                        </ul>

                        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
                        <script>
                            $(document).ready(function () {
                                let currentMonth;
                                let currentYear;
                                let fechasReservadas = <%= Newtonsoft.Json.JsonConvert.SerializeObject(CapaNegocio.ConexionBD.ObtenerFechasReservadas()) %>;

                                function getDaysInMonth(month, year) {
                                    return new Date(year, month + 1, 0).getDate();
                                }

                                function generateCalendar(month, year) {
                                    const weekdays = ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'];
                                    let daysHtml = '';
                                    const months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

                                    $('#month-year').text(months[month] + ' ' + year);

                                    for (let i = 0; i < 7; i++) {
                                        daysHtml += `<li>${weekdays[i]}</li>`;
                                    }

                                    const firstDay = new Date(year, month, 1).getDay();
                                    const daysInMonth = getDaysInMonth(month, year);

                                    let day = 1;

                                    for (let i = 0; i < 6; i++) {
                                        for (let j = 0; j < 7; j++) {
                                            if ((i === 0 && j < firstDay) || (day > daysInMonth)) {
                                                daysHtml += '<li></li>';
                                            } else {
                                                daysHtml += `<li>${day}</li>`;
                                                day++;
                                            }
                                        }
                                        if (day > daysInMonth) {
                                            break;
                                        }
                                    }

                                    $('#calendar-days').html(daysHtml);

                                    applyReservationClasses(); // Aplicar clases de reserva después de generar el calendario
                                }

                                function applyReservationClasses() {
                                    $('ul.days li').each(function () {
                                        const day = parseInt($(this).text(), 10);
                                        const currentDate = new Date(currentYear, currentMonth, day);
                                        const formattedCurrentDate = currentDate.toISOString().split('T')[0]; // Convertir a formato YYYY-MM-DD

                                        if (fechasReservadas.includes(formattedCurrentDate)) {
                                            $(this).css({
                                                'background-color': 'red',    // Color de fondo para fechas reservadas
                                                'color': 'white'              // Color del texto para fechas reservadas
                                            });
                                        } else {
                                            $(this).css({
                                                'background-color': 'green',  // Color de fondo para fechas no reservadas
                                                'color': 'white'              // Color del texto para fechas no reservadas
                                            });
                                        }
                                    });
                                }

                                function showPreviousMonth() {
                                    currentMonth--;
                                    if (currentMonth < 0) {
                                        currentMonth = 11;
                                        currentYear--;
                                    }
                                    generateCalendar(currentMonth, currentYear);
                                }

                                function showNextMonth() {
                                    currentMonth++;
                                    if (currentMonth > 11) {
                                        currentMonth = 0;
                                        currentYear++;
                                    }
                                    generateCalendar(currentMonth, currentYear);
                                }

                                $('.prev').on('click', showPreviousMonth);
                                $('.next').on('click', showNextMonth);

                                // Obtener el mes y año actual y generar el calendario
                                const today = new Date();
                                currentMonth = today.getMonth();
                                currentYear = today.getFullYear();
                                generateCalendar(currentMonth, currentYear);
                            });
                        </script>--%>



                        <asp:Calendar ID="calendar" runat="server" CssClass="customCalendar" OnDayRender="calendar_DayRender"></asp:Calendar>


                        <%--ModalReserva--%>

                        <div id="myModal" class="modal">
                            <div class="modal-content">
                                <span class="close" id="closeModalButton">&times;</span>
                                <!-- Contenido del modal -->

                                <div class="modal-content-inner">
                                    <h2>Reservar habitación</h2>
                                    <div class="container">
                                        <div class="row mb-8 justify-content-center">
                                            <div class="col-md-6 col-12">
                                                <div class="mb-4 border-bottom pb-2">
                                                </div>
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="d-flex justify-content-between">
                                                            <div>
                                                                <p class="text-dark">Cantidad de huéspedes</p>
                                                            </div>

                                                            <div class="input-group w-auto justify-content-end align-items-center">
                                                                <asp:Button ID="btnMenosAdultos" class="button-minus border rounded-circle icon-shape icon-sm mx-1" data-field="adultos" runat="server" Text="-" OnClientClick="return RestarAdultos();" />
                                                                <asp:TextBox ID="txtcantidadAdultos" class="comment-modal" runat="server"></asp:TextBox>
                                                                <asp:Button ID="btnMasAdultos" class="button-plus border rounded-circle icon-shape icon-sm" data-field="adultos" runat="server" Text="+" OnClientClick="return SumarAdultos();" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                        <div class="d-flex justify-content-between">
                                                            <%--                                                            <div>
                                                                <p class="text-dark">Cantidad de Niños</p>
                                                            </div>--%>

                                                            <%--                                                            <div class="input-group w-auto justify-content-end align-items-center">
                                                                <asp:Button ID="btnMenosNinos" runat="server" Text="-" OnClientClick="return RestarNinos();"></asp:Button>
                                                                <asp:TextBox ID="txtCantidadNinos" class="comment-modal" runat="server"></asp:TextBox>
                                                                <asp:Button ID="btnMasNinos" runat="server" Text="+" OnClientClick="return SumarNinos();" />
                                                            </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row mt-4">
                                                <div class="col-12">
                                                    <div class="d-flex justify-content-between">
                                                        <br />
                                                        <div>
                                                            <label for="fechaLlegada">Fecha de llegada:</label>
                                                            <asp:TextBox ID="txtfechaLlegada" runat="server" type="date" class="comment-modal"></asp:TextBox><br />

                                                            <br />
                                                            <label for="fechaSalida">Fecha de salida:</label>
                                                            <asp:TextBox ID="txtfechaSalida" runat="server" type="date" class="comment-modal"></asp:TextBox><br />

                                                        </div>

                                                        <asp:Button ID="ReservarButton" runat="server" Text="Confirmar" OnClick="ReservarButton_Click" class="send-button" />

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <script>
                            // Función para mostrar el calendario al hacer clic en fechaSalida
                            function mostrarCalendariosalida() {
                                $("#fechaSalida").datepicker({
                                    dateFormat: 'yy-mm-dd', // Puedes ajustar el formato de fecha
                                    onSelect: function (dateText, inst) {
                                    }
                                });
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
                            function AbrirModal() {
                                // Abre el modal al hacer clic en el botón
                                var modal = document.getElementById("myModal");
                                modal.style.display = "block";
                                // Puedes agregar más lógica aquí si es necesario
                            }

                            // Cierra el modal si se hace clic en la "x"
                            document.getElementById("closeModalButton").onclick = function () {
                                var modal = document.getElementById("myModal");
                                modal.style.display = "none";
                            }

                            // Cierra el modal si se hace clic fuera del contenido del modal
                            window.onclick = function (event) {
                                var modal = document.getElementById("myModal");
                                if (event.target == modal) {
                                    modal.style.display = "none";
                                }
                            }
                        </script>

                        <script>
                            function RestarAdultos() {
                                var txtCantidadNinos = document.getElementById('<%= txtcantidadAdultos.ClientID %>');

                                var cantidadNinos = parseInt(txtcantidadAdultos.value, 10) || 0;


                                if (cantidadNinos > 0) {
                                    cantidadNinos--;
                                    txtcantidadAdultos.value = cantidadNinos;
                                }

                                return false;
                            }
                        </script>

                        <script>
                            function SumarAdultos() {

                                var txtcantidadAdultos = document.getElementById('<%= txtcantidadAdultos.ClientID %>');


                                var cantidadNinos = parseInt(txtcantidadAdultos.value, 10) || 0;


                                cantidadNinos++;
                                txtcantidadAdultos.value = cantidadNinos;


                                return false;
                            }
                        </script>

                        <br />
                        <hr />
                        <br />
                        <div class="comentarios">
                            <h1>Reseñas</h1>
                            <asp:TextBox ID="txtComentario" runat="server" CssClass="input-comentarios" placeholder="Agrega un comentario"></asp:TextBox>
                            <asp:Button ID="btnCrearResena" runat="server" Text="Agregar Reseña" CssClass="btnResponder" OnClick="btnCrearResena_Click" EnableViewState="true" />
                            <asp:Repeater ID="ComentariosRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="comment">
                                        <p>
                                            <asp:Label ID="lblIdComentario" runat="server" Text='<%# Eval("IdComentario") %>' Visible="false" />
                                        </p>
                                        <p><%# Eval("IdCedula") %> - <%# Eval("Fecha") %></p>
                                        <p><%# Eval("ComentarioTexto") %></p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnCrearResena" />
                </Triggers>
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
