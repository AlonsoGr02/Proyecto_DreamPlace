<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DenunciasAnf.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.DenunciasAnf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Denuncias</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" integrity="sha512-..." />
    <link rel="StyleSheet" href="../Estilos/Css_Notificaciones.css" type="text/css" />
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
                    <asp:Label ID="Labelseparador" runat="server" Text="Denuncias "></asp:Label>
                </div>
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

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Denuncias">
                            <ItemTemplate>
                                <div class="tarjeta" style="background-color: #8A9A5B; padding: 10px; margin-bottom: 10px; border-radius: 5px; width: 80%;">
                                    <h3 style="color: white;">Su inmueble <%# Eval("Nombre") %> ha recibido una denuncia sobre <%# Eval("Denuncia") %></h3>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:Repeater ID="rptDenuncias" runat="server">
                    <ItemTemplate>
                        <div class="tarjeta" style="background-color: #8A9A5B; padding: 10px; margin-bottom: 10px; border-radius: 5px; width: 80%; margin: 10px auto;">
                            <h3 style="color: white;">Su inmueble "<%# DataBinder.Eval(Container.DataItem, "NombreInmueble") %>" ha recibido una denuncia sobre "<%# DataBinder.Eval(Container.DataItem, "ContenidoDenuncia") %>"</h3>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>

</body>
</html>
