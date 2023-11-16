<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublicarAnuncio.aspx.cs" Inherits="Proyecto_DreamPlace.Paginas.PublicarAnuncio" %>

<!DOCTYPE html>

<html>
<head>
    <title>Publicar Anuncio</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Montserrat&display=swap" />
    <link href="../Estilos/Css_PublicarA.css" rel="stylesheet" />
    <link href="../Estilos/Css_PublicarA2.css" rel="stylesheet" />
</head>
<body>
    <form runat="server">
        <asp:ScriptManager ID="scriptManager1" runat="server" />
        <img src="../img/DreamPlece Logo Lima.jpg" alt="Logo de la empresa" class="logo-empresa">

        <div class="contenedor" id="contenedorPrincipal" runat="server">
            <div class="informacion" id="informacion">
                <h3>Paso 1</h3>
                <h1>Describe tu espacio</h1>
                <p>En este paso, te preguntaremos qué tipo de propiedad tienes y si los huéspedes reservarán el alojamiento entero o solo una habitación. A continuación, indícanos la ubicación y cuántos huéspedes pueden quedarse.</p>
            </div>
            <div class="imagen" id="imagen">
                <img src="../img/paso1.png" alt="Imagen de la derecha" />
            </div>
        </div>

        <asp:UpdatePanel ID="updatePanel2" runat="server">
            <ContentTemplate>
                <div class="contenedor" id="contenedor2" runat="server">
                    <div class="titulo-contenedor">
                        <h3>¿Cuál de estas opciones describe mejor tu espacio?</h3>
                    </div>

                    <div class="categoria-contenedor">
                        <asp:Repeater ID="repeaterCategorias" runat="server">
                            <ItemTemplate>
                                <div class="categoria-tarjeta" onclick='<%# Eval("IdCategoria", "SeleccionarCategoria(this,{0})") %>'>
                                    <div class="categoria-info">
                                        <img src='<%# Eval("ImagenBase64") %>' alt='<%# Eval("Categoria") %>' data-idcategoria='<%# Eval("IdCategoria") %>' />
                                        <span><%# Eval("Categoria") %></span>
                                        <asp:Label ID="lblIdCategoria" runat="server" Text='<%# Eval("IdCategoria") %>' Visible='<%# false %>'></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <asp:HiddenField ID="HiddenFieldIdCategoria" runat="server" ClientIDMode="Static" />
                </div>
                <script type="text/javascript">
                    var tarjetaSeleccionada = null;

                    function SeleccionarCategoria(elemento, idCategoria) {
                        // Verifica si ya hay una tarjeta seleccionada
                        if (tarjetaSeleccionada !== null) {
                            // Restaura el color base de la tarjeta previamente seleccionada
                            tarjetaSeleccionada.style.border = '2px solid #556B2F';
                        }

                        // Verifica si la tarjeta actual no estaba seleccionada previamente
                        if (tarjetaSeleccionada !== elemento) {
                            // Cambia el color del borde de la tarjeta al hacer clic
                            elemento.style.border = '2px solid blue';
                            // Actualiza la tarjeta seleccionada y el valor del HiddenField con el idCategoria
                            tarjetaSeleccionada = elemento;
                            document.getElementById('<%= HiddenFieldIdCategoria.ClientID %>').value = idCategoria;
                        } else {
                            // Si ya estaba seleccionada, cambia al color base y borra el idCategoria
                            elemento.style.border = '2px solid #556B2F';
                            tarjetaSeleccionada = null;
                            document.getElementById('<%= HiddenFieldIdCategoria.ClientID %>').value = '';
                        }
                    }
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="contenedor" id="contenedor3" runat="server">
            <div class="titulo-contenedor">
                <h3>¿De qué tipo de alojamiento</h3>
                <h3>dispondrán los huéspedes?</h3>
            </div>
            <div class="contenedor-info">
                <div class="tarjeta-info" onclick="seleccionarTarjeta(this)">
                    <div class="icono">
                        <img src="../img/completa.jpg" />
                    </div>
                    <div class="info-tarjeta">
                        <h4>Un alojamiento entero</h4>
                    </div>
                </div>

                <div class="tarjeta-info" onclick="seleccionarTarjeta(this)">
                    <div class="icono">
                        <img src="../img/cuarto.png" />
                    </div>
                    <div class="info-tarjeta">
                        <h4>Una habitación</h4>
                    </div>
                </div>

                <div class="tarjeta-info" onclick="seleccionarTarjeta(this)">
                    <div class="icono">
                        <img src="../img/compartida.png" />
                    </div>
                    <div class="info-tarjeta">
                        <h4>Una habitación compartida</h4>
                    </div>
                </div>
                <asp:HiddenField ID="HiddenFieldTipoInmueble" runat="server" ClientIDMode="Static" />
            </div>
            <script type="text/javascript">
                var tarjetaSeleccionada = null;

                function seleccionarTarjeta(elemento) {
                    // Obtiene el elemento <h4> dentro de la tarjeta seleccionada
                    var h4Element = elemento.querySelector('h4');

                    // Obtiene el texto del <h4>
                    var tipoInmueble = h4Element.innerText;

                    // Verifica el color actual del borde
                    var colorActual = elemento.style.border;

                    // Si el color actual es diferente de azul, cambia a azul y restaura el color de la tarjeta previamente seleccionada
                    if (colorActual !== '2px solid blue') {
                        if (tarjetaSeleccionada !== null) {
                            tarjetaSeleccionada.style.border = '2px solid #A3AB78'; // Cambia al color base
                        }
                        elemento.style.border = '2px solid blue';
                        tarjetaSeleccionada = elemento;

                        // Asigna el tipoInmueble al HiddenField
                        document.getElementById('<%= HiddenFieldTipoInmueble.ClientID %>').value = tipoInmueble;
                    } else {
                        // Si ya estaba seleccionada, cambia al color base y borra el tipoInmueble
                        elemento.style.border = '2px solid #A3AB78';
                        tarjetaSeleccionada = null;
                        document.getElementById('<%= HiddenFieldTipoInmueble.ClientID %>').value = '';
                    }
                }
            </script>
        </div>

        <div class="contenedor" id="contenedor4" runat="server">
            <div class="titulo-contenedor">
                <h3>¿Dónde se encuentra tu espacio?</h3>
            </div>
            <div>
                <asp:TextBox ID="txtProvincia" runat="server" class="miTextBox" placeholder="Provincia"></asp:TextBox>
                <asp:TextBox ID="txtCanton" runat="server" class="miTextBox" placeholder="Cantón"></asp:TextBox>
                <asp:TextBox ID="txtDirecExcata" runat="server" class="miTextBox" placeholder="Dirección Exacta"></asp:TextBox>
            </div>
        </div>

        <div class="contenedor" id="contenedor5" runat="server">
            <div class="titulo-contenedor">
                <h3>Agrega algunos datos básicos sobre tu espacio.</h3>
                <h3>Después podrás agregar más información.</h3>
            </div>
            <div class="contenedor-info">
                <asp:TextBox ID="txtCantidadHuespedes" runat="server" class="miTextBox" placeholder="Húespedes"></asp:TextBox>
                <asp:TextBox ID="txtCantidadHabitaciones" runat="server" class="miTextBox" placeholder="Habitaciones"></asp:TextBox>
                <asp:TextBox ID="txtCantidadCamas" runat="server" class="miTextBox" placeholder="Camas"></asp:TextBox>
                <asp:TextBox ID="txtBanos" runat="server" class="miTextBox" placeholder="Baños"></asp:TextBox>
            </div>
        </div>

        <div class="contenedor" id="contenedorPaso2" runat="server">
            <div class="informacion" id="informacion2">
                <h3>Paso 2</h3>
                <h1>Elige lo mejor para que tu espacio destaque</h1>
                <p>En este espacio agregarás las comodidades que ofrece tu espacio, así como 5 fotos del lugar. Luego deberás crear un título y una descripción.</p>
            </div>

            <div class="imagen" id="imagen2">
                <img src="../img/paso2.png" alt="Imagen de la derecha" />
            </div>
        </div>

        <div class="contenedor" id="contenedor6" runat="server">
            <div class="titulo-contenedor">
                <h3>Cuéntale a los huéspedes todo lo que</h3>
                <h3>tu espacio tiene para ofrecerles</h3>
            </div>

            <div class="grupo-tarjetas">
                <asp:Repeater ID="repeaterServicios" runat="server">

                    <ItemTemplate>

                        <div class="tarjeta2" onclick="SeleccionarServicio(this, '<%# Eval("IdServiciosAlojamientos") %>')">
                            <div class="icono">
                                <img src='<%# Eval("ImagenBase64") %>' alt='Servicio Icon' />
                            </div>
                            <span><%# Eval("Nombre") %></span>
                            <asp:Label ID="lblIdServico" runat="server" Text='<%# Eval("IdServiciosAlojamientos") %>' Visible='<%# false %>'></asp:Label>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <asp:HiddenField ID="hfIdServicio1" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfIdServicio2" runat="server" ClientIDMode="Static" />

            <script type="text/javascript">
                var tarjetasSeleccionadas = [];

                function SeleccionarServicio(elemento, idServicio) {
                    // Verifica si la tarjeta actual ya estaba seleccionada previamente
                    var index = tarjetasSeleccionadas.indexOf(elemento);

                    if (index === -1) {
                        // Si no estaba seleccionada, verifica si ya hay dos tarjetas seleccionadas
                        if (tarjetasSeleccionadas.length < 2) {
                            // Cambia el color del borde de la tarjeta al hacer clic
                            elemento.style.border = '2px solid blue';
                            // Agrega la tarjeta a la lista de seleccionadas
                            tarjetasSeleccionadas.push({ elemento, idServicio });
                        }
                    } else {
                        // Si ya estaba seleccionada, cambia al color base y elimina la tarjeta de la lista de seleccionadas
                        elemento.style.border = '2px solid #A3AB78';
                        tarjetasSeleccionadas.splice(index, 1);
                    }

                    // Actualiza el valor de los HiddenFields con los IdServicio de las tarjetas seleccionadas
                    if (tarjetasSeleccionadas.length > 0) {
                        document.getElementById('<%= hfIdServicio1.ClientID %>').value = tarjetasSeleccionadas[0].idServicio;
                    } else {
                        document.getElementById('<%= hfIdServicio1.ClientID %>').value = '';
                    }

                    if (tarjetasSeleccionadas.length > 1) {
                        document.getElementById('<%= hfIdServicio2.ClientID %>').value = tarjetasSeleccionadas[1].idServicio;
                   } else {
                       document.getElementById('<%= hfIdServicio2.ClientID %>').value = '';
                    }
                }
            </script>
        </div>

        <div class="contenedor" id="contenedor7" runat="server">
            <div class="titulo-contenedor">
                <h3>Agrega algunas fotos de tu casa. Para empezar,</h3>
                <h3>necesitarás cinco fotos.</h3>
            </div>
            <div class="tarjeta-contenedor">
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="botonSubir" Multiple="true" />
                <asp:Button ID="Button1" runat="server" Text="Subir Fotos" CssClass="botonSubir" OnClick="Button1_Click" />
            </div>
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>

        </div>

        <div class="contenedor" id="contenedor8" runat="server">
            <div class="titulo-contenedor">
                <h3>Ahora, ponle un título a tu casa,</h3>
                <h3>los títulos cortos funcionan mejor.</h3>
                <h3>No te preocupes, puedes modificarlo más adelante.</h3>
            </div>
            <div>
                <asp:TextBox ID="txtTitulo" runat="server" class="miTextBox" placeholder="Titulo de tu casa"></asp:TextBox>
            </div>
        </div>

        <div class="contenedor" id="contenedor10" runat="server">
            <div class="titulo-contenedor">
                <h3>Escribe tu descripción</h3>
                <h3>Comparte lo que hace que tu espacio sea especial.</h3>
            </div>
            <div>
                <asp:TextBox ID="txtDescripcionI" runat="server" class="miTextBox" placeholder="Describe tu casa"></asp:TextBox>
            </div>
        </div>

        <div class="contenedor" id="contenedorPaso3" runat="server">
            <div class="informacion" id="informacion3">
                <h3>Paso 3</h3>
                <h1>Terminar y publicar</h1>
                <p>Este es el último paso. Elegirás las configuraciones de las reservaciones, establecerás el precio y publicarás tu anuncio.</p>
            </div>

            <div class="imagen" id="imagen3">
                <img src="../img/paso3.png" alt="Imagen de la derecha" />
            </div>
        </div>

        <div class="contenedor" id="contenedor12" runat="server">
            <div class="titulo-contenedor">
                <h3>Ahora establece el precio.</h3>
                <h3>Puedes cambiarlo cuando quieras.</h3>
            </div>

            <div class="tarjeta-container">
                <div>
                    <asp:TextBox ID="txtPrecioBase" runat="server" CssClass="miTextBox" placeholder="Precio por noche" oninput="calcularMontos()"></asp:TextBox>
                </div>
                <div class="price-card">
                    <div class="price-content">
                        <p class="price-pair">
                            <span class="price-label">Precio Base:</span>
                            <asp:Label ID="lblPrecioBase" runat="server" CssClass="price"> 0</asp:Label>
                        </p>

                        <p class="price-pair">
                            <span class="price-label">IVA:</span>
                            <asp:Label ID="lblTarifaServicio" runat="server" CssClass="price"> 0</asp:Label>
                        </p>

                        <p class="price-pair">
                            <span class="price-label">Precio para el Huésped:</span>
                            <asp:Label ID="lblPrecioHuesped" runat="server" CssClass="price"> 0</asp:Label>
                        </p>
                    </div>
                </div>

                <div class="price-card">
                    <div class="price-content">
                        <p class="price-pair">
                            <span class="price-label">Precio Total por Noche:</span>
                            <asp:Label ID="lblPrecioTotal" runat="server" CssClass="price">0</asp:Label>
                        </p>
                    </div>
                </div>
                <asp:HiddenField ID="hiddenPrecioBase" runat="server" />
                <asp:HiddenField ID="hiddenIva" runat="server" />
                <asp:HiddenField ID="hiddenPrecioTotal" runat="server" />
                

            </div>
            <script type="text/javascript">
                function calcularMontos() {
                    // Obtén el valor del TextBox
                    var precioBase = parseFloat(document.getElementById('<%= txtPrecioBase.ClientID %>').value) || 0;

                    // Calcula los montos
                    var iva = precioBase * 0.13; // Supongamos un 20% de IVA
                    var precioHuesped = precioBase + iva;
                    var precioTotal = precioHuesped;

                    // Actualiza los valores de los Labels
                    document.getElementById('<%= lblPrecioBase.ClientID %>').innerText = precioBase.toFixed(2);
                    document.getElementById('<%= lblTarifaServicio.ClientID %>').innerText = iva.toFixed(2);
                    document.getElementById('<%= lblPrecioHuesped.ClientID %>').innerText = precioHuesped.toFixed(2);
                    document.getElementById('<%= lblPrecioTotal.ClientID %>').innerText = precioTotal.toFixed(2);

                    // Almacena los valores en campos ocultos
                    document.getElementById('<%= hiddenPrecioBase.ClientID %>').value = precioBase;
                    document.getElementById('<%= hiddenIva.ClientID %>').value = iva;
                    document.getElementById('<%= hiddenPrecioTotal.ClientID %>').value = precioTotal;
                }
            </script>
        </div>

        <div class="contenedor" id="contenedor14" runat="server">
            <div class="contenedorVista">
                <h2 class="h2-v">Vista Previa Completa</h2>
                <img src="../img/completa.jpg" alt="Vista Previa" class="img-v" />

                <div class="informacion-dinamica-v">
                    <asp:Label ID="lblTituloAlojamiento" runat="server" Text="Titulo Inmueble" CssClass="label-v"></asp:Label>
                    <asp:Label ID="lblTipoAlojamiento" runat="server" Text="Tipo de Alojamiento: Un alojamiento completo" CssClass="label-v"></asp:Label>
                    <asp:Label ID="lblAnfitrion" runat="server" Text="Anfitrión: nombreAnfitrion" CssClass="label-v"></asp:Label>
                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción del Alojamiento" CssClass="label-v"></asp:Label>
                </div>

                <div class="tarjeta-comodidades-v">
                    <h3>Comodidades</h3>
                    <asp:Label ID="lblWifi" runat="server" Text="Wifi" CssClass="label-v"></asp:Label>
                    <asp:Label ID="lblTV" runat="server" Text="TV" CssClass="label-v"></asp:Label>
                </div>
            </div>
        </div>

        <div id="contenedor-botones">
            <asp:Button ID="btnPrev" CssClass="boton-anterior" runat="server" Text="Anterior" Visible="false" OnClick="btnPrev_Click" />
            <asp:Button ID="btnNext" CssClass="boton-siguiente" runat="server" Text="Siguiente" OnClick="btnNext_Click" />
        </div>
    </form>
</body>
</html>