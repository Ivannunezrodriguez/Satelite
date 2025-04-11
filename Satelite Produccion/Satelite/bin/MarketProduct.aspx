<%@ Page Language="C#" MasterPageFile="~/MarketplaceParent.Master" AutoEventWireup="true" CodeBehind="MarketProduct.aspx.cs" Inherits="Satelite.MarketProduct" %>
    <asp:Content ID="Home" ContentPlaceHolderID="Main" runat="server">
        <link rel="stylesheet" href="./css/marketproduct.css"/>    
        <script type="text/javascript" src="./js/marketproduct.js"></script>
        <form runat="server">
            <!--==================== HOME ====================-->
            <section class="home" id="home">
                <div class="home__container container grid">
                    <div class="home__img-bg" style="display: flex; align-items: center; justify-content: center">
                         <asp:Repeater ID="RepeaterGaleria" runat="server">
                             <ItemTemplate>
                                 <input type="radio" name="navegacion" id="<%# Eval("Id") %>" <%# Eval("Check") %>>
                                 <img src="<%# Eval("Url") %>" class="home__img" alt="Galeria CSS 2"  />
                            </ItemTemplate>
                         </asp:Repeater>
                    </div>
    
                    <!-- DESCRIPCION DEL PRODUCTO -->
                    <div class="home__data">
                        <!-- TITULO DEL PRODUCTO -->
                        <h1 class="home__title"> 
                            <label id="titulo" runat="server"> </label>
                        </h1>
                        <span class="home__price" id="price" runat="server"></span>
                        <!-- COMPRAR -->
                        <div class="home__btns">
                            <button class="button home__button" runat="server" onServerClick="comprarProduct" id="comprar">
                                Comprar <i class='bx bx-shopping-bag'></i>
                            </button>
                            <button class="button home__button" type="button" runat="server" onclick="mostrar_info()" id="consultar" visible="false">
                                Consultar
                            </button>
                        </div>
                    </div>
                </div>
                <!-- DETALLES DEL PRODUCTO -->
                <div class=" container grid">
                    <!-- Descripcion -->
                    <div>
                        <h1>Descripcion</h1>
                        <p class="home__description">
                            <label id="desc" runat="server"> </label>
                        </p>
                    </div>
                    <!-- Especificaciones tecnicas -->
                    <div>
                        <h1>Especificaciones tecnicas</h1>
                        <p class="home__description">
                            <label id="esptec" runat="server"> </label>
                        </p>
                    </div>
                    <!--div>
                        <h1>Similares</h1>
                    </!--div-->
                </div>
            </section>
        </form>

        <!-- SOLICITUD PRECIO PRODUCTO -->
        <form id="div_info" class="div_pago div_info" action="javascript: sendmail()">
            <h2  style="font-size:28px;font-weight:bold; color: black; text-align:center;width:96%; background:var(--first-color); opacity: 0.91; border-radius: 26px 26px 26px 26px;">SOLICITAR INFORMACION</h2>
            <button id="Buttoncerrar" type="button" onclick="$('#div_info').css('display', 'none')" style=" text-align:right; position:absolute; top:10px; left:94%; border-style:none; background-color:transparent;" class="pull-right text-muted " >
                <i title="Cerrar ventana" class="fa fa-times fa-2x"></i>
            </button>

            <br />
            <br />
               <label style="font-size:14px;">Complete el Formulario con los datos necesarios para poder enviarle esta información a su cuenta de correo con copia a la nuestra, y poner en espera su tramitación de pedido.  </label>

            <br />
            <br />
            <div class="row" >
                <div class="col-sm-2"></div>
                <div class="col-sm-4">
                    <label style="font-size:18px;">Nombre y apellidos:  </label>
                </div>
                <div class="col-sm-4">
                    <input visible="true" style="text-align:center; font-weight:bold; font-size:18px; background-color:white;"   width="100%" placeholder=""  id="InfoNombre" required/>
                </div>
                <div class="col-sm-2"></div>
            </div>
            <div class="row" >
                <div class="col-sm-2"></div>
                <div class="col-sm-4">
                    <label style="font-size:18px;">Municipio:  </label>
                </div>
                <div class="col-sm-4">
                    <input visible="true" style="text-align:center; font-weight:bold; font-size:18px; background-color:white;"  width="100%" placeholder="Municipio"  id="InfoMunicipio" required/>
                </div>
                <div class="col-sm-2"></div>
            </div>
            <div class="row" >
                <div class="col-sm-2"></div>
                <div class="col-sm-4">
                    <label style="font-size:18px;">Provincia:  </label>
                </div>
                <div class="col-sm-4">
                    <input visible="true" style="text-align:center; font-weight:bold; font-size:18px; background-color:white;" width="100%" placeholder="Provincia"  id="InfoProvincia" required/>
                </div>
                <div class="col-sm-2"></div>
            </div>
            <div class="row" >
                <div class="col-sm-2"></div>
                <div class="col-sm-4">
                    <label style="font-size:18px;">Telefono:  </label>
                </div>
                <div class="col-sm-4">
                    <input type="tel" visible="true" style="text-align:center; font-weight:bold; font-size:18px; background-color:white;" width="100%" placeholder="91X XX XX XX"  id="InfoTelefono" required/>
                </div>
                <div class="col-sm-2"></div>
            </div>
            <div class="row" >
                <div class="col-sm-2"></div>
                <div class="col-sm-4">
                    <label style="font-size:18px;">Email:  </label>
                </div>
                <div class="col-sm-4">
                    <input type="email" visible="true" style="text-align:center; font-weight:bold; font-size:18px; background-color:white;" width="100%" placeholder="ejemplo@viveroseresma.com"  id="InfoMail" required/>
                </div>
                <div class="col-sm-2"></div>
            </div>

            
            <div class="row" >
                <div class="col-sm-5"></div>
                <div class="col-sm-2">
                    <button id="Btnenviar2" type="submit" class="btn" style="width:140px;height:60px;" ><i style="font-size:16px;" class="fa fa-lock"></i> Enviar</button>
                </div>
                <div class="col-sm-5"></div>
            </div>

        </form>

    </asp:Content>
