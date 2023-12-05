<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Info_Reservas.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.Info_Reservas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Datos de Reservas</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link href="../Estilos/Inf_Reservas.css" rel="stylesheet" />
    <link href="../Estilos/footer.css" rel="stylesheet" />
    <style>
        body{
            background-color: white;
        }
    </style>
</head>
    
<body>
    <form id="form1" runat="server">
        <nav class="navbar">
            <a href="Perfil_Huesped.aspx?Correo=<%= Session["Correo"] %>">
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
                    <asp:Label ID="Labelseparador" runat="server" Text="Historial de Reservas "></asp:Label>
                </div>
                <h1>Historial de Reservas</h1>
            </div>
            <br />
              <table class="table">
                <thead>
                    <tr>
                        <th>Número de reserva</th>
                        <th>Nombre Inmueble</th>
                        <th>Nombre del Propietario</th>
                        <th>Fecha de entrada</th>
                        <th>Fecha de salida</th>

                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptDenuncias" runat="server" OnItemCommand="rptDenuncias_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("IdReserva") %></td>
                                <td><%# Eval("NombreInmueble") %></td>
                                <td><%# Eval("NombrePropietario") + " " + Eval("ApellidoPropietario") %></td>
                                <td><%# Eval("FechaI") %></td>
                                <td><%# Eval("FechaF") %></td>
                         

                                <td style="display: none;">
                                    <asp:Label ID="LabelNombreInmueble" runat="server" Text='<%# Eval("NombreInmueble") %>' Visible="false"></asp:Label>

                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </form>
   
</body>
</html>