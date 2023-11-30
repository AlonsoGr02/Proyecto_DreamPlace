<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registrarme.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.Registrarme" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registro</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;700&display=swap" />
    <link href="../Estilos/Css_Registro.css" rel="stylesheet" />
    <link href="../Estilos/Css_Login2.css" rel="stylesheet" />
    <link href="../Estilos/footer.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar">
            <a href="Inicio.aspx">
                <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo" class="logo" />
            </a>
            <div class="navbar-links">
                <a href="#"><i class="fa-solid fa-user" style="color: #000000; font-size: 30px;"></i></a>
            </div>
        </nav>
        <div class="container">
            <div class="information">
                <div class="flex-container">
                    <a href="Inicio.aspx">Inicio</a>
                    <asp:Label ID="Label1" runat="server">  >  </asp:Label>
                    <asp:Label ID="Labelseparador" runat="server" Text="Registro "></asp:Label>
                </div>
                <hr />
                <h1>Registro</h1>
            </div>
            <br />
            
            <asp:TextBox ID="registerCedula" runat="server" placeholder="Cédula" ></asp:TextBox><br />
            <asp:TextBox ID="registerUsername" runat="server" placeholder="Correo Electrónico" ></asp:TextBox><br />
            <asp:TextBox ID="registerNombre" runat="server" placeholder="Nombre" ></asp:TextBox><br />
            <asp:TextBox ID="registerApellidos" runat="server" placeholder="Apellidos" ></asp:TextBox><br />
            <asp:TextBox ID="registerFechaNac" runat="server" type="date" CssClass="date-input" ></asp:TextBox><br />
            <asp:TextBox ID="registerTelefono" runat="server" placeholder="Teléfono" ></asp:TextBox><br />
            <asp:TextBox ID="registerContrasena" runat="server" TextMode="Password" placeholder="Contraseña" CssClass="txtContra" ></asp:TextBox><br />
            <select id="seleccionarOp" runat="server">
                            <option value="1">Húesped</option>
                            <option value="2">Anfitrión</option>
                        </select>
            <br />
            <hr />
            <%--<asp:Label ID="lblInfo" runat="server" Text="Adjunta la foto frontal de la cédula"></asp:Label><br />
            <asp:FileUpload ID="FileUploadFrontal" runat="server" CssClass="botonSubir" /><br />

            <asp:Label ID="lblInfoTrasera" runat="server" Text="Adjunta la foto trasera de la cédula"></asp:Label><br />
            <asp:FileUpload ID="FileUploadTrasera" runat="server" CssClass="botonSubir" /><br />--%>

            <asp:Button ID="btnGuardar" runat="server" Text="Registrarme" OnClick="btnGuardar_Click" CssClass="botonSubir" /><br />
            <asp:Label ID="lblRespu" runat="server" Text=""></asp:Label>
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
