<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Perfil_Huesped.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.Perfil_Huesped" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Perfil Huésped</title>
    <link rel="StyleSheet" href="../Estilos/Css_PerfilHuesped.css" type="text/css" />
    <link href="../Estilos/footer.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
     <style>
        body{
            background-color: white;
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
                <a href="#"><i class="fa-solid fa-user" style=" color:#000000; font-size:30px;"></i></a>
            </div>
        </nav>
        <div class="container">
            <div class="information">
                <div class="flex-container">
                    <a href="Cuenta.aspx?Correo=<%= Session["Correo"] %>">Cuenta</a>
                    <asp:Label ID="Label1" runat="server">  >  </asp:Label>
                    <asp:Label ID="Labelseparador" runat="server" Text="Información Personal "></asp:Label>
                </div>
                <h1>Información Personal</h1>
            </div>
            <br />

            <asp:Label ID="lblCedula" runat="server" Text="Cédula:"></asp:Label><br />
            <asp:TextBox ID="txtCedula" runat="server" ReadOnly></asp:TextBox>
            <br />


            <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label><br />
            <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            <br />


            <asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label><br />
            <asp:TextBox ID="txtApellidos" runat="server"></asp:TextBox>
            <br />

            <asp:Label ID="Label3" runat="server" Text="Fecha de Nacimiento:"></asp:Label><br />
            <asp:TextBox ID="txtFechaNac" runat="server" type="date" CssClass="date-input" ReadOnly></asp:TextBox>
            <br />

            <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:"></asp:Label><br />
            <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
            <br />


            <asp:Label ID="lblCorreo" runat="server" Text="Correo"></asp:Label><br />
            <asp:TextBox ID="txtCorreo" runat="server" ReadOnly></asp:TextBox>
            <br />


            <asp:Label ID="lblClave" runat="server" Text="Tu último código de verificación:"></asp:Label><br />
            <asp:TextBox ID="txtClave" runat="server" ReadOnly></asp:TextBox>
            <br />

            <asp:Label ID="Label2" runat="server" Text="Rol de Usuario:"></asp:Label><br />
            <asp:TextBox ID="txtRol" runat="server" ReadOnly></asp:TextBox>
            <br />
            <asp:Button ID="btnGuardar" runat="server" class="fa-solid" Text="Guardar" OnClick="btnGuardar_Click" /><br />
                <i class="fa-solid fa-floppy-disk" style=" color:#A3AB78; font-size:30px;" ></i>
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
