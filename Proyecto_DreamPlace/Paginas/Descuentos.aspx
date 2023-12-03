<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Descuentos.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.Descuentos"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Descuentos</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link href="../Estilos/Css_Descuentos.css" rel="stylesheet" />
    <link href="../Estilos/footer.css" rel="stylesheet" />
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
                    <asp:Label ID="Labelseparador" runat="server" Text="Descuentos "></asp:Label>
                </div>
                <h1>Descuentos</h1>
            </div>
            <br />
            <asp:DropDownList ID="ddlAlojamientos" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAlojamientos_SelectedIndexChanged"></asp:DropDownList>
            <br />
            <hr />
            <div class="container-info">
                <h2>Inmueble</h2>
                <asp:Label ID="Label2" runat="server" Text="Nombre:"></asp:Label><br />
                <asp:TextBox ID="txtNombre" runat="server" AutoCompleteType="Disabled" ReadOnly></asp:TextBox><br />
                <br />

                <asp:Label ID="Label3" runat="server" Text="Descripción:"></asp:Label><br />
                <asp:TextBox ID="txtDescripcion" runat="server" AutoCompleteType="Disabled" ReadOnly></asp:TextBox><br />
                <br />
            </div>
            <div class="container-desc">
                <h2>Aplica un Descuento a tu Inmueble</h2>
                <asp:Label ID="Label8" runat="server" Text="Descuento:"></asp:Label><br />
                <asp:DropDownList ID="ddlDescuento" runat="server" OnSelectedIndexChanged="ddlDescuento_SelectedIndexChanged">
                    <asp:ListItem Text="0.00%" Value="0"></asp:ListItem>
                    <asp:ListItem Text="10.00%" Value="10"></asp:ListItem>
                    <asp:ListItem Text="15.00%" Value="15"></asp:ListItem>
                    <asp:ListItem Text="20.00%" Value="20"></asp:ListItem>
                    <asp:ListItem Text="30.00%" Value="30"></asp:ListItem>
                </asp:DropDownList><br /><br />


                <asp:Label ID="Label4" runat="server" Text="Total del Inmueble por Noche:"></asp:Label><br />
                <asp:TextBox ID="txtTotal" runat="server" AutoCompleteType="Disabled" ReadOnly></asp:TextBox><br />
                <br />

            </div>

            <br />
            <br />
            <hr />
            <br />

            <asp:Button ID="btnGuardar" class="fa-solid" runat="server" Text="Agregar Descuento" OnClick="btnGuardar_Click" />


            <asp:HiddenField ID="hdnDescuento" runat="server" />


            <script type="text/javascript">
                function actualizarDescuento() {
                    var ddlDescuento = document.getElementById('<%= ddlDescuento.ClientID %>');
                    var txtTotal = document.getElementById('<%= txtTotal.ClientID %>');

                    var descuentoSeleccionado = ddlDescuento.value;
                    var precioTotalActual = txtTotal.value;

                    // Llama al servidor para obtener el nuevo precio total
                    PageMethods.ObtenerNuevoTotal(descuentoSeleccionado, precioTotalActual, function (result) {
                        txtTotal.value = result;
                    });
                }
            </script>

        </div>
    </form>
</body>
</html>
