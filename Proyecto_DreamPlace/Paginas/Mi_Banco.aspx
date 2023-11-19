<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mi_Banco.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.Mi_Banco" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Mi Banco</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link rel="StyleSheet" href="../Estilos/Css_MiBanco.css" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar">
            <a href="PrincipalHuesped.aspx">
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
                    <asp:Label ID="Labelseparador" runat="server" Text="Mi Banco "></asp:Label>
                </div>
                <br />
                <h1>Mi Banco</h1>
            </div>
            <hr />
  
            <asp:Label ID="lblCedula" runat="server" Text="Cédula:"></asp:Label><br />
            <asp:TextBox ID="txtCedula" runat="server" ReadOnly></asp:TextBox>
            <br />

            <asp:Label ID="Label2" runat="server" Text="Nombre del Usuario:"></asp:Label><br />
            <asp:TextBox ID="txtNombre" runat="server" ReadOnly></asp:TextBox>
            <br />

             
            <asp:Label ID="Label3" runat="server" Text="Número de Tarjeta:"></asp:Label><br />
            <asp:TextBox ID="txtNTarjeta" runat="server" ReadOnly></asp:TextBox>
            <br />

            <asp:Label ID="Label4" runat="server" Text="Monto Total:"></asp:Label><br />
            <asp:TextBox ID="txtMontoTotal" runat="server" ReadOnly></asp:TextBox>
            <br />
            <hr />
            <asp:Button ID="btnDepositar" runat="server" Text="Depositar  Fondos" CssClass="fa-solid" OnClick="btnDepositar_Click" />

        </div>
    </form>
</body>
</html>