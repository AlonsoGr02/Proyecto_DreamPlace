<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManteAlojamiento.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.ManteAlojamiento" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Mantenimiento</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link href="../Estilos/Css_ManteA.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar">
            <a href="CuentaAnfitrion.aspx?Correo=<%= Session["Correo"] %>">
                <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo" class="logo" />
            </a>
            <div class="navbar-links">
                <a href="#"><i class="fa-solid fa-user" style="color: #000000; font-size: 30px;"></i></a>
            </div>
        </nav>
        <div class="container">
            <div class="information">
                <div class="flex-container">
                    <a href="CuentaAnfitrion.aspx?Correo=<%= Session["Correo"] %>">Cuenta</a>
                    <asp:Label ID="Label1" runat="server">  >  </asp:Label>
                    <asp:Label ID="Labelseparador" runat="server" Text="Mantenimiento de tu Inmueble "></asp:Label>
                    <hr />
                </div>
                <h1>Mantenimiento de tu Inmueble</h1>
            </div>

            <br />
            <asp:DropDownList ID="ddlAlojamientos" runat="server"></asp:DropDownList><br />
            <hr />
            <div class="container-info">
                <h2>Caracteristicas de tu Inmueble</h2>
                <asp:Label ID="Label2" runat="server" Text="Nombre:"></asp:Label><br />
                <asp:TextBox ID="txtNombre" runat="server" AutoCompleteType="Disabled"></asp:TextBox><br />

                <asp:Label ID="Label3" runat="server" Text="Descripción:"></asp:Label><br />
                <asp:TextBox ID="txtDescripcion" runat="server" AutoCompleteType="Disabled"></asp:TextBox><br />

                <asp:Label ID="Label4" runat="server" Text="Cantidad de Personas:"></asp:Label><br />
                <asp:TextBox ID="txtCantidadP" runat="server" AutoCompleteType="Disabled"></asp:TextBox><br />

                <asp:Label ID="Label5" runat="server" Text="Cantidad de Dormitorios:"></asp:Label><br />
                <asp:TextBox ID="txtCantidadD" runat="server" AutoCompleteType="Disabled"></asp:TextBox><br />

                <asp:Label ID="Label6" runat="server" Text="Cantidad de Baños:"></asp:Label><br />
                <asp:TextBox ID="txtCantidadB" runat="server" AutoCompleteType="Disabled"></asp:TextBox><br />

                <asp:Label ID="Label7" runat="server" Text="Cantidad de Camas:"></asp:Label><br />
                <asp:TextBox ID="txtCantiCamas" runat="server" AutoCompleteType="Disabled"></asp:TextBox><br />

                <asp:Label ID="Label8" runat="server" Text="Categoria:"></asp:Label><br />
                <asp:DropDownList ID="ddlCategoria" runat="server"></asp:DropDownList><br />

                <asp:Label ID="Label9" runat="server" Text="Estado:"></asp:Label><br />
                <asp:DropDownList ID="ddlEstado" runat="server"></asp:DropDownList><br />

                <asp:Label ID="Label10" runat="server" Text="Descripción del Estado :"></asp:Label><br />
                <asp:TextBox ID="txtDescripcionEstado" runat="server" AutoCompleteType="Disabled"></asp:TextBox><br />
            </div>
            <div class="container-image">
                <asp:FileUpload ID="FileUpload1" runat="server" /><br />
                <asp:Image ID="Image1" runat="server" AlternateText="imagen 1" CssClass="imagen-estilo" /><br />
                <asp:Image ID="Image2" runat="server" AlternateText="imagen 2" CssClass="imagen-estilo" /><br />
                <asp:Image ID="Image3" runat="server" AlternateText="imagen 3" CssClass="imagen-estilo" /><br />
                <asp:Image ID="Image4" runat="server" AlternateText="imagen 4" CssClass="imagen-estilo" /><br />
                <asp:Image ID="Image5" runat="server" AlternateText="imagen 5" CssClass="imagen-estilo" /><br />

            </div>

            <br />
            <br />
            <hr />
            <br />
            <asp:Button ID="btnGuardar" class="fa-solid" runat="server" Text="Guardar" />

        </div>
    </form>
</body>
</html>

