﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Denuncias.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.Denuncias" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Denuncias</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link rel="StyleSheet" href="../Estilos/Css_Notificaciones.css" type="text/css" />
    <link href="../Estilos/footer.css" rel="stylesheet" />
    <link href="../Estilos/Modal.css" rel="stylesheet" />

    <style>
        body {
            background-color: white;
        }
        /* Estilos para el modal */
        .modal {
            display: none;
            position: fixed;
            z-index: 1;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgba(0,0,0,0.4);
        }

        .modal-content {
            background-color: #fefefe;
            margin: 10% auto;
            padding: 20px;
            border: 1px solid #888;
            width: 80%;
            max-width: 600px;
        }

        .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: black;
                text-decoration: none;
                cursor: pointer;
            }

        /* Estilos para las tarjetas */
        .denuncias-container {
            text-align: center;
        }

        .denuncia-card {
            display: inline-block;
            margin: 10px;
            padding: 10px;
            border: 1px solid #ccc;
            cursor: pointer;
        }

        /* Estilos para el campo de texto */
        textarea {
            width: 100%;
            margin: 10px 0;
            padding: 10px;
            border: 1px solid #ccc;
        }

        /* Estilos para el botón */
        button {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            border: none;
            cursor: pointer;
        }

            button:hover {
                background-color: #45a049;
            }


        /* Estilos para la tabla */
        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        th, td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }

        th {
            background-color: #f2f2f2;
        }

        /* Estilos para el botón Denunciar */
        .btnDenunciar {
            background-color: #ff0000;
            color: white;
            padding: 8px 16px;
            border: none;
            cursor: pointer;
            border-radius: 4px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 14px;
            margin: 4px 2px;
            transition-duration: 0.4s;
        }

            .btnDenunciar:hover {
                background-color: #8d0202;
                color: white;
            }

        /* Estilos para las tarjetas */
        .tarjetas-container {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-around;
            gap: 10px;
            margin-bottom: 20px;
        }

        .tarjeta {
            width: 200px;
            height: 100px;
            background-color: #f0f0f0;
            border: 1px solid #ccc;
            border-radius: 5px;
            display: flex;
            justify-content: center;
            align-items: center;
            cursor: pointer;
        }

            .tarjeta:hover {
                background-color: #e0e0e0;
            }

            .tarjeta.selected {
                background-color: #e09b9b; /* Cambia esto por el color que prefieras */
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar">
            <a href="PrincipalHuesped.aspx?Correo=<%= Session["Correo"] %>">
                <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo" class="logo" />
            </a>
            <div class="navbar-links">
                <a href="#"><i class="fa-solid fa-user" style="color: #000000; font-size: 30px;"></i></a>
            </div>
        </nav>
        <div class="container">
            <div class="information">
                <div class="flex-container">
                    <a href="Cuenta.aspx?Correo=<%= Session["Correo"] %>">Cuenta</a>
                    <asp:Label ID="Label1" runat="server">  >  </asp:Label>
                    <asp:Label ID="Labelseparador" runat="server" Text="Denuncias "></asp:Label>
                </div>
                                    <img src="../img/Denuncia.png" alt="Descripción de la imagen" style="width: 100px; height: auto; float: right;" />
                <h1>Denuncias</h1>
            </div>
            <br />
            <div style="margin: 0 auto;">
                <asp:GridView ID="gvDenuncias" runat="server" Style="margin: 0 auto;" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#BDE038" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
            </div>

            <table class="table">
                <thead>
                    <tr>
                        <th>Número de reserva</th>
                        <th>Nombre Inmueble</th>
                        <th>Nombre del Propietario</th>
                        <th>Fecha de entrada</th>
                        <th>Fecha de salida</th>
                        <th>Denunciar</th>

                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptDenuncias" runat="server" OnItemCommand="rptDenuncias_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("IdReserva") %></td>
                                <td><%# Eval("Nombre") %></td>
                                <td><%# Eval("NombrePropietario") + " " + Eval("ApellidoPropietario") %></td>
                                <td><%# Eval("FechaI") %></td>
                                <td><%# Eval("FechaF") %></td>
                                <td>
                                    <asp:Button ID="btnDenunciar" runat="server" Text="Denunciar" CommandName="Denunciar"
                                        CommandArgument='<%# Eval("IdReserva") %>' CssClass="btnDenunciar" OnClick="btnDenunciar_Click" />


                                    <br />
                                </td>

                                <td style="display: none;">
                                    <asp:Label ID="LabelNombreInmueble" runat="server" Text='<%# Eval("Nombre") %>' Visible="false"></asp:Label>

                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                </tbody>

            </table>
        </div>




        -
        <%--Modaldenuncia --%>
        <div id="Modaldenuncia" class="modal">
            <div class="modal-content">
                <span class="close" id="closeModalButton">&times;</span>
                <!-- Contenido del modal -->
                <div class="modal-content-inner">
                    <h2>Metodos de Pago</h2>
                    <div class="container">
                        <!-- Contenido del modal -->
                        <div class="tarjetas-container">
                            <div class="tarjeta" onclick="seleccionarDenuncia('Ruido excesivo', this)">Ruido excesivo</div>
                            <div class="tarjeta" onclick="seleccionarDenuncia('Limpieza deficiente', this)">Limpieza deficiente</div>
                            <div class="tarjeta" onclick="seleccionarDenuncia('Problemas de seguridad', this)">Problemas de seguridad</div>
                            <div class="tarjeta" onclick="seleccionarDenuncia('Mal servicio', this)">Mal servicio</div>
                            <div class="tarjeta" onclick="seleccionarDenuncia('Problemas con las instalaciones', this)">Problemas con las instalaciones</div>
                            <div class="tarjeta" onclick="seleccionarDenuncia('Mal funcionamiento de servicios', this)">Mal funcionamiento de servicios</div>
                        </div>
                        
                    <asp:Button ID="btnEnviarDenuncia" OnClick="btnEnviarDenuncia_Click" CssClass="btnDenunciar" runat="server" Text="Enviar Denuncia" />
                    <asp:Button ID="Button2" runat="server" CssClass="btnDenunciar" Text="Cerrar" OnClientClick="CerrarModalEvaluacion()" />

                    </div>

                    <input type="hidden" id="denunciaSeleccionada" runat="server" />
                    <input type="hidden" id="nombreInmuebleSeleccionado" runat="server" />

                    <!-- Resto de tu HTML -->

                </div>
            </div>
        </div>

        <script>
            function AbrirModal(idReserva) {
                var modal = document.getElementById("Modaldenuncia");
                modal.style.display = "block";
                document.getElementById('denunciaSeleccionada').value = idReserva;
            }

            function seleccionarDenuncia(texto, element) {
                document.getElementById('denunciaSeleccionada').value = texto;
                element.classList.add('selected');
                AbrirModal(event);
            }

            document.getElementById("closeModalButton").onclick = function () {
                var modal = document.getElementById("Modaldenuncia");
                modal.style.display = "none";
            }

            window.onclick = function (event) {
                var modal = document.getElementById("Modaldenuncia");
                if (event.target == modal) {
                    modal.style.display = "none";
                }
            }
        </script>
    </form>

</body>
</html>
