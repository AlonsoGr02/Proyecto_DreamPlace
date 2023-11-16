<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Solicitud_Reserva.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.Solicitud_Reserva" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.0/css/all.min.css" />
    <link href="Estilos/reservas.css" rel="stylesheet" />
    <link href="Estilos/navbar.css" rel="stylesheet" />
    <link href="../Estilos/solicitudreserva.css" rel="stylesheet" />

</head>

<body>
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
                    <!-- Título de la página -->
                    <h1>Solicitud de Reserva</h1>

                    <hr />


                    <div class="information">
                        <div class="flex-container">
                            <a href="Reserva.aspx">Reserva</a>
                            <asp:Label ID="Label1" runat="server">  >  </asp:Label>
                            <asp:Label ID="Labelseparador" runat="server" Text="Solicitud de Reserva "></asp:Label>
                        </div>
                        <hr />
                        <h1>Solicitud de Reserva</h1>
                    </div>
                    <br />

                    <asp:Label ID="lblCedula" runat="server" Text="Cédula"></asp:Label><br />

                    <asp:TextBox ID="txtCedula" runat="server" placeholder="Cédula" ReadOnly></asp:TextBox><br />

                    <asp:Label ID="lblNumeroTarjeta" runat="server" Text="Número de Tarjeta"></asp:Label><br />

                    <asp:TextBox ID="txtNumeroTarjeta" runat="server" placeholder="Número de Tarjeta" ReadOnly></asp:TextBox><br />


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

                    <asp:Label ID="Label4" runat="server" Text="Total:"></asp:Label>
                    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label><br />




                    <asp:Button ID="ConfirmarReservaButton" runat="server" Text="Confirmar Reserva" />

                </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>

</html>