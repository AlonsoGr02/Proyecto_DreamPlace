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


    <!--jQuery-->
    <script src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha384-tsQFqpEReu7ZLhBV2VZlAu7zcOV+rXbYlF2cqB8txI/8aZajjp4Bqd+V6D5IgvKT" crossorigin="anonymous"></script>

    <!--Normalize CSS-->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/normalize/8.0.0/normalize.min.css" rel="stylesheet" type="text/css"/>

    <!--Flex Photo Gallery CSS-->
    <link rel="stylesheet" href="dist/css/jquery.flex-photo-gallery.css"/>

    <!--Flex Photo Gallery JS-->
    <script src="dist/js/jquery.flex-photo-gallery.js"></script>

    <%---------------------------%>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css"/>



    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>


    <%--    Navbar--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />

    <link href="../Estilos/Css_Inicio2.css" rel="stylesheet" />
    <link href="../Estilos/Css_Inicio3.css" rel="stylesheet" />
    <link href="../Estilos/Css_Resena.css" rel="stylesheet" />
    <link href="../Estilos/footer.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" integrity="sha512-xjMY0gIb5Jjt/6ZcPlV16JdA85z3PkjGm9Uo8Mp+YOuLOxM5Ygv4dxX4STl3MgIbRYlpuT0D+qsMxZRdcti81A==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link href="../Estilos/imagenes.css" rel="stylesheet" />
    <link href="../Estilos/calendario.css" rel="stylesheet" />
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
            margin-left: 80%; /* Centra el botón horizontalmente */
            transition: background-color 0.3s ease; /* Transición suave del color de fondo */
        }

            .btn-favorito:hover {
                background-color: red; /* Cambia el color de fondo al pasar el ratón por encima */
            }

        .container_reserva {
            max-width: 90%; /* Cambiado a 90% para mayor adaptabilidad */
            margin: 5vw auto; /* Utilizando unidades de viewport para el margen */
            padding: 5vw; /* Utilizando unidades de viewport para el padding */
            box-sizing: border-box;
            background-color: #fff;
            border: 2px solid black;
        }

        /* Media Query para pantallas más pequeñas */
        @media (max-width: 768px) {
            .container_reserva {
                max-width: 95%; /* Cambio en el ancho máximo para pantallas más pequeñas */
                margin: 3vw auto; /* Cambio en el margen para pantallas más pequeñas */
                padding: 3vw; /* Cambio en el padding para pantallas más pequeñas */
            }
        }



        .container_img {
            position: relative;
            z-index: 0; /* Puedes intentar cambiar el z-index para el carrusel */
            width: 320px;
            margin: 100px auto 0 auto;
            perspective: 1000px;
        }

        .carousel__face img {
            position: relative;
            z-index: 2; /* Asegura que las imágenes estén por encima del resto */
        }

        /* Asegúrate de que los otros elementos tengan un z-index menor */
        .mini-cardDescription,
        .mini-card {
            position: relative;
            z-index: 1; /* Puede ser menor que el de las imágenes */
            height: 1000px;
        }


        .carousel {
            position: absolute;
            width: 100%;
            height: 100%;
            transform-style: preserve-3d;
            animation: rotate360 30s infinite forwards linear;
        }

        .carousel__face {
            position: absolute;
            width: 300px;
            height: 187px;
            top: 20px;
            left: 10px;
            right: 10px;
            background-size: cover;
            box-shadow: inset 0 0 0 2000px rgba(0,0,0,0.5);
            display: flex;
        }

        span {
            margin: auto;
            font-size: 2rem;
        }


        .carousel__face:nth-child(1) {
            transform: rotateY( 0deg) translateZ(600px);
        }

        .carousel__face:nth-child(2) {
            transform: rotateY( 40deg) translateZ(600px);
        }

        .carousel__face:nth-child(3) {
            transform: rotateY( 80deg) translateZ(600px);
        }

        .carousel__face:nth-child(4) {
            transform: rotateY(120deg) translateZ(600px);
        }

        .carousel__face:nth-child(5) {
            transform: rotateY(160deg) translateZ(600px);
        }

        .carousel__face:nth-child(6) {
            transform: rotateY(200deg) translateZ(600px);
        }

        .carousel__face:nth-child(7) {
            transform: rotateY(240deg) translateZ(600px);
        }

        .carousel__face:nth-child(8) {
            transform: rotateY(280deg) translateZ(600px);
        }

        .carousel__face:nth-child(9) {
            transform: rotateY(320deg) translateZ(600px);
        }

        @keyframes rotate360 {
            from {
                transform: rotateY(0deg);
            }

            to {
                transform: rotateY(-360deg);
            }
        }

        .servicio-container {
            display: flex;
            flex-wrap: wrap;
            justify-content: flex-start;
        }

        .servicio-card {
            width: calc(5% - 10px); /* Calcula el ancho con margen entre las tarjetas */
            margin: 10px;
            padding: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            text-align: center;
        }

            .servicio-card img {
                max-width: 100%;
                height: auto;
                margin-bottom: 10px;
            }

            .servicio-card h4 {
                margin-bottom: 10px;
            }

        .info-container {
            display: flex;
            flex-direction: column;
        }

        .info-item {
            display: block;
            margin-bottom: 10px;
        }

        /* Estilos para el contenedor de las restricciones */
        .restriccion-item {
            display: flex;
            align-items: center;
            border-bottom: 1px solid #ccc;
            margin-bottom: 10px;
        }

            /* Estilos para el icono de información */
            .restriccion-item i {
                font-size: 20px;
                margin-right: 0.5px;
                color: #007BFF; /* Color del icono, puedes cambiarlo según tu diseño */
            }

            /* Estilos para la descripción de la restricción */
            .restriccion-item span {
                font-size: 25px;
                line-height: 2;
                color: #333; /* Color del texto, puedes cambiarlo según tu diseño */
            }
    </style>
</head>

<body>
    <contenttemplate>
        <form id="form1" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                <ContentTemplate>
                    <asp:ScriptManager ID="scriptManager1" runat="server" />

                    <div class="prueba">
                        <header style="background-color: #BDE038;">
                            <div class="logo">
                                <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo" />
                            </div>

                            <div id="topnav" class="topnav-container" runat="server">

                                <asp:Label ID="Label6" runat="server" Text=" " Style="color: black;"></asp:Label>
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
                                    <li><a href="PrincipalHuesped.aspx?Correo=<%= Session["Correo"] %>">Volver</a></li>
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

                        <div class="container_reserva">

                            <div class="hotel-details">
                                <asp:Button ID="btnFavorritos" runat="server" Text="Agregar Favoritos" CssClass="btn-favorito" OnClick="btnFavorritos_Click" />
                                <div class="hotel-title">
                                    <asp:Label class="hotel" ID="lblNombre" runat="server" Text=""></asp:Label>
                                </div>
                                <br />
                                <hr />
                                <div>
                                    <asp:Label class="hotel-description" ID="Label9" runat="server" Text="Ubicacion:">
                                        <i class="fas fa-map-marker-alt"></i> <!-- Ícono de ubikcación -->
                                    </asp:Label>

                                    <asp:Label class="hotel-description" ID="LabelUbicacion" runat="server" Text="Ubicacion:"></asp:Label>
                                    <asp:Label ID="lblubucacion" runat="server" Visible="false"></asp:Label>

                                </div>

                                <br />
                                <br />
                                <br />

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

                                <div class="container_img">
                                    <div class="carousel">
                                        <!-- Repeater para las imágenes -->
                                        <asp:Repeater ID="imageRepeater" runat="server">
                                            <ItemTemplate>
                                                <div class="carousel__face">
                                                    <img src='<%# "data:image/png;base64," + Convert.ToBase64String(((ImagenModel)Container.DataItem).ImagenData) %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />

                                <div class="sticky-box">
                                    <!-- Contenido de la caja -->
                                    <label class="hotel-title" id="Labelr">Reservar inmueble</label>
                                    <div>
                                        <div>
                                            <p class="text-dark">Cantidad de huéspedes</p>
                                        </div>

                                        <div class="input-group w-auto justify-content-end align-items-center">
                                            <asp:Button ID="btnMenosAdultos" class="button-minus border rounded-circle icon-shape icon-sm mx-1" data-field="adultos" runat="server" Text="-" OnClientClick="return RestarAdultos();" />
                                            <asp:TextBox ID="txtcantidadAdultos" class="comment-modal" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnMasAdultos" class="button-plus border rounded-circle icon-shape icon-sm" data-field="adultos" runat="server" Text="+" OnClientClick="return SumarAdultos();" />
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <div class="d-flex justify-content-between">
                                        </div>
                                    </div>
                                    <div>
                                        <label for="fechaLlegada">Fecha de llegada:</label>
                                        <asp:TextBox ID="txtfechaLlegada" runat="server" type="date" class="comment-modal"></asp:TextBox><br />

                                        <br />
                                        <label for="fechaSalida">Fecha de salida:</label>
                                        <asp:TextBox ID="txtfechaSalida" runat="server" type="date" class="comment-modal"></asp:TextBox><br />

                                    </div>

                                    <asp:Button ID="ReservarButton" runat="server" Text="Confirmar" OnClick="ReservarButton_Click" class="send-button" />

                                </div>


                                <div>
                                    <asp:Label ID="lbldescripcion" runat="server" Text="El Panoramic Hotel es un moderno y elegante hotel de 4 estrellas, asomado al mar, ideal para unas vacaciones románticas y de gran encanto, en el mágico escenario de Taormina y del Mar de Sicilia."></asp:Label>
                                </div>

                                    <br />
                                    <div class="mini-card">
                                        <div>
                                            <asp:Label class="hotel-description" ID="lblPrecioS" runat="server" Text="Precio por noche:₡ "></asp:Label>
                                            <asp:Label class="hotel-description" ID="lblPrecio" runat="server" Text=""></asp:Label><br />
                                            <asp:Label class="hotel-description" ID="lblDescuentoTotal" runat="server" Text=""></asp:Label>


                                <br />

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



                                <br />


                                <div>
                                    <asp:Label class="hotel-title" ID="Label1" runat="server" Text="Amenidades:"></asp:Label>
                                </div>

                                <div class="servicio-container">
                                    <asp:Repeater ID="rptAmenities" runat="server">
                                        <ItemTemplate>
                                            <div class="servicio-card">
                                                <asp:Image ID="imgIcono" runat="server" ImageUrl='<%# ObtenerImagen((byte[])Eval("Icono")) %>' />
                                                <%# Eval("Nombre") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <label class="hotel-title" id="Label7">Fechas disponibles:</label>
                                <br />



                                <div class="container_calendario">
                                    <div class="calendario">
                                        <asp:Calendar ID="calendar" runat="server" CssClass="calendar" OnDayRender="calendar_DayRender"></asp:Calendar>
                                    </div>
                                </div>


                                <script>
                                    $(function () {
                                        $("#datepicker").datepicker();
                                    });

                                </script>


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
                                    <br />
                                <br />
                                <br />
                                <br />
                                <div>
                                    <asp:Label class="hotel-title" ID="Label10" runat="server" Text="Restricciones del espacio y políticas:"></asp:Label>
                                </div>
                                <hr />
                                <asp:Repeater ID="rptRestricciones" runat="server">
                                    <ItemTemplate>
                                        <div class="restriccion-item">
                                            <i class="fa fa-info-circle"></i>
                                            <span><%# Eval("Descrip_Restricciones") %></span>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
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
