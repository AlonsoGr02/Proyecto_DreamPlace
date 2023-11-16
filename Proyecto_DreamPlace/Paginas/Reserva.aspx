<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reserva.aspx.cs" Inherits="Proyecto_DreamPlace.Reserva" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Hotel Reservation</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.0/css/all.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />



    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" integrity="sha512-xjMY0gIb5Jjt/6ZcPlV16JdA85z3PkjGm9Uo8Mp+YOuLOxM5Ygv4dxX4STl3MgIbRYlpuT0D+qsMxZRdcti81A==" crossorigin="anonymous" referrerpolicy="no-referrer" />



    <link href="Estilos/navbar.css" rel="stylesheet" />
    <link href="../Estilos/Reserva.css" rel="stylesheet" />
</head>

<body>
    <contenttemplate>
        <form id="form1" runat="server">
            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <nav class="navbar">
                        <a href="Inicio.aspx">
                            <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo" class="logo" />
                        </a>
                        <div class="navbar-links">
                            <a href="#"><i class="fa-solid fa-user" style="color: #000000; font-size: 30px;"></i></a>
                        </div>
                    </nav>

                    <div class="container">

                        <div class="hotel-details">
                            <div class="hotel-title">

                                <asp:Label class="hotel-title" ID="lblNombre" runat="server" Text="El Panoramic Hotel"></asp:Label>

                                <!-- Botón para abrir el modal -->
                                <asp:Button class="send-button" ID="btnAbrirmodalReserva" runat="server" Text="Reservar" OnClick="AbrirModal" />
                            </div>


                            <asp:Panel ID="imageGallery" runat="server" CssClass="image-gallery">
                                <div class="image-gallery">
                                    <!-- Las imágenes se agregarán dinámicamente aquí -->
                                </div>
                            </asp:Panel>


                        </div>

                        <div class="hotel-description">Descripción del hotel.</div>

                        <div>
                            <asp:Label ID="lbldescripcion" runat="server" Text="El Panoramic Hotel es un moderno y elegante hotel de 4 estrellas, asomado al mar, ideal para unas vacaciones románticas y de gran encanto, en el mágico escenario de Taormina y del Mar de Sicilia."></asp:Label>
                        </div>

                        <div>
                            <asp:Label class="hotel-description" ID="lblPrecioS" runat="server" Text="Precio por noche:₡ "></asp:Label>
                            <asp:Label class="hotel-description" ID="lblPrecio" runat="server" Text=""></asp:Label>

                        </div>



                        <div class="hotel-ratings">
                            <asp:Label class="fas fa-star" ID="lblcalificacion" runat="server" Text=" Puntuacion: 4.5"></asp:Label>
                        </div>
                        <br />

                        <div>
                            <asp:Label class="hotel-title" ID="lblofrecelugar" runat="server" Text="Lo que ofrece el lugar:"></asp:Label>
                        </div>
                        <br />

                        <!-- Mini Tarjeta 1 -->
                        <div class="mini-card">
                            <hr />
                            <i class="icon fas fa-users"></i>
                            <br />

                            <asp:Label ID="Label2" Text="Cantidad Personas:" runat="server"></asp:Label>
                            <asp:Label ID="lblCantidadPersonas" runat="server"></asp:Label><br />
                            <hr />

                            <i class="icon fas fa-bed"></i>
                            <br />
                            <asp:Label ID="Label3" Text="Cantidad Dormitorios:" runat="server"></asp:Label>
                            <asp:Label ID="lblCantidadDormitorios" runat="server"></asp:Label><br />
                            <hr />
                            <i class="icon fas fa-bath"></i>
                            <br />
                            <asp:Label ID="Label4" Text="Cantidad Baños:" runat="server"></asp:Label>
                            <asp:Label ID="lblCantidadBanos" runat="server"></asp:Label><br />
                            <hr />
                            <i class="icon fas fa-bed"></i>
                            <br />
                            <asp:Label ID="Label5" Text="Cantidad Camas:" runat="server"></asp:Label>
                            <asp:Label ID="lblCantidadCamas" runat="server"></asp:Label><br />
                            <hr />

                            <asp:Label ID="lblTipo" runat="server"></asp:Label><br />

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


                        <div class="comments">
                            <div class="comment-form">
                                <h2>Deja un comentario</h2>
                                <input class="comment-input" type="text" placeholder="Tu comentario">
                                <button class="send-button">Enviar</button>
                            </div>

                            <div class="comment-box">
                                <div class="comment-author">Usuario 1</div>
                                <div class="comment-date">6 de noviembre de 2023</div>
                                <div class="comment-text">
                                    Este hotel es increíble. Las habitaciones son espaciosas y limpias, y el personal es muy amable. ¡Recomiendo este lugar!
                                </div>
                            </div>



                            <div class="comment-box">
                                <div class="comment-author">Usuario 2</div>
                                <div class="comment-date">5 de noviembre de 2023</div>
                                <div class="comment-text">
                                    ¡Una experiencia increíble! La piscina es perfecta para relajarse y el restaurante ofrece comida deliciosa.
                                </div>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
</body>
</html>
