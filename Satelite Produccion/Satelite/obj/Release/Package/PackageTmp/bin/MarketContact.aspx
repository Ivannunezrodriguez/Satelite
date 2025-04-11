<%@ Page Language="C#" MasterPageFile="~/MarketplaceParent.Master" AutoEventWireup="true" CodeBehind="MarketContact.aspx.cs" Inherits="Satelite.MarketplaceContact" %>
    <asp:Content ID="Home" ContentPlaceHolderID="Main" runat="server">
        <link rel="stylesheet" href="./css/marketcontact.css"/>    
        <main class="main">
            <!--==================== HOME ====================-->
            <section class="home" id="home">
                <div class="home__container container grid">
                    <div class="home__img-bg" style="display: flex; align-items: center; justify-content: center">
                        <img src="../images/Logo_Rio_Eresma.png" id="img" runat="server" alt="" class="home__img"/>
                    </div>
    
                    <!-- REDES SOLCIALES DEL  -->
                    <div class="home__social">
                        <a href="https://www.facebook.com/" target="_blank" class="home__social-link">
                            Facebook
                        </a>
                        <a href="https://es.linkedin.com/company/rioeresma" target="_blank" class="home__social-link">
                            Linkedin
                        </a>
                        <a href="https://www.instagram.com/" target="_blank" class="home__social-link">
                            Instagram
                        </a>
                    </div>
    
                    <!-- DESCRIPCION DEL PRODUCTO -->
                    <div class="home__data">
                        <!-- TITULO DEL PRODUCTO -->
                        <div class="footer__content">
                            <h3 class="footer__title">Central de Rio Eresma</h3>

                            <ul class="footer__list">
                                <li>C/ navalmanzano 22</li>
                                <li>Navalmanzano, 63003 (SEGOVIA)</li>
                                <li>9XX XX XX XX</li>
                                <li>6XX XX XX XX</li>
                                <li>9:00h a 14:00h y de 17:00h a 19:00h (De lunies a jueves)</li>
                            </ul>
                        </div>

                    </div>
                </div>

                <div class="home__container container grid">
                    <div class="home__img-bg" style="display: flex; align-items: center; justify-content: center">
                        <img src="../images/Logo_Rio_Eresma.png" id="img" alt="" class="home__img"/>
                    </div>
    
                    <!-- REDES SOLCIALES DEL  -->
                    <div class="home__social">
                        <a href="https://www.facebook.com/" target="_blank" class="home__social-link">
                            Facebook
                        </a>
                        <a href="https://es.linkedin.com/company/Rioeresma" target="_blank" class="home__social-link">
                            Linkedin
                        </a>
                        <a href="https://www.instagram.com/" target="_blank" class="home__social-link">
                            Instagram
                        </a>
                    </div>
    
                    <!-- DESCRIPCION DEL PRODUCTO -->
                    <div class="home__data">
                        <!-- TITULO DEL PRODUCTO -->
                        <div class="footer__content">
                            <h3 class="footer__title">Almacen de Mudrian</h3>

                            <ul class="footer__list">
                                <li>C/ Mudrian 2</li>
                                <li>Mudrian, 63000, (SEGOVIA)</li>
                                <li>9XX XX XX XX</li>
                                <li>6XX XX XX XX</li>
                                <li>9:00h a 14:00h y de 17:00h a 19:00h (De lunies a jueves)</li>
                            </ul>
                        </div>

                    </div>
                </div>
                <div class="home__container container grid" id="formcontact">
                   <div class="home__data">
                        <!-- TITULO DEL PRODUCTO -->
                        <h3 class="footer__title">Envianos un mensaje</h3>
                        

                         <form runat="server">
                             <table>
                                 <tr>
                                     <td>
                                         
                                    </td>
                                    <td>
                                        <a href="mailto:direccion@rioeresma.es">info@rioeresma.es</a>
                                    </td>
                                </tr>
                                <tr>
                                     <td>
                                         Nombre y apellidos:<asp:RequiredFieldValidator CssClass="recuired" ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxName" ErrorMessage="Este campo es obligatorio">*</asp:RequiredFieldValidator>
                                    </td>
                                     <td>
                                         <asp:TextBox visible="true" style="text-align:center; font-weight:bold; font-size:18px; background-color:white;" AutoPostBack="true"  runat="server" Width="100%" tooltip="Introduzca su Nombre"  ID="TxName" Text=""  Font-Bold="True" />
                                         
                                    </td>
                                </tr>
                                <tr>
                                     <td>
                                         Email:<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxMail" ErrorMessage="Este campo es obligatorio">*</asp:RequiredFieldValidator>
                                    </td>
                                     <td>
                                         <asp:TextBox visible="true" style="text-align:center; font-weight:bold; font-size:18px; background-color:white;" AutoPostBack="true"  runat="server" Width="100%" tooltip="Introduzca su Nombre"  ID="TxMail" Text=""  Font-Bold="True" />
                                         
                                    </td>
                                </tr>
                                <tr>
                                     <td>
                                          Telefono:<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxTelefono" ErrorMessage="Este campo es obligatorio">*</asp:RequiredFieldValidator>
                                    </td>
                                     <td>
                                         <asp:TextBox visible="true" style="text-align:center; font-weight:bold; font-size:18px; background-color:white;" AutoPostBack="true"  runat="server" Width="100%" tooltip="Introduzca su Nombre"  ID="TxTelefono" Text=""  Font-Bold="True" />
                                         
                                    </td>
                                </tr>
                                <tr>
                                     <td>
                                         Mensaje:<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxMensaje" ErrorMessage="Este campo es obligatorio">*</asp:RequiredFieldValidator>
                                    </td>
                                     <td>
                                         <asp:TextBox visible="true" style="text-align:center; font-weight:bold; font-size:18px; background-color:white;" AutoPostBack="true"  runat="server" Width="100%" tooltip="Introduzca su Nombre"  ID="TxMensaje" Text=""  Font-Bold="True" />
                                         
                                    </td>
                                </tr>
                                 <tr>
                                     <td>
                                         <asp:CheckBox ID="chck" runat="server" CssClass="elementor-field elementor-size-sm  elementor-acceptance-field" />
                                        <label style="font-size:18px;">He leído y acepto la <a class="especial" href="privacidad.html">política de privacidad</a> </label>
                                    </td>
                                     <td>
                                       
                                    </td>
                                </tr>
                             </table>

                            <button id="Button3" type="button"  visible="true" class="btn" style="width:140px;height:60px;" runat="server" onserverClick="Enviar_Click">
                                <i style="font-size:16px;" class="fa fa-lock"></i> Enviar

                            </button>
                         </form>
                    </div>
                </div>
            </section>
         </main>
       </asp:Content>
