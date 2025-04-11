<%@ Page Language="C#" MasterPageFile="~/MarketplaceParent.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Satelite.Marketplace" %>
    <asp:Content ID="Home" ContentPlaceHolderID="Main" runat="server">
        <main class="main">
            <!--==================== HOME ====================-->
            <section class="home" id="home">
                <div class="home__container container grid">
                    <div class="home__img-bg" style="display: flex; align-items: center; justify-content: center">
                        <a href="" runat="server" id="eimg">
                            <img src="../images/Logo_Rio_Eresma.png" runat="server" id="img" alt="" class="home__img"/>
                        </a>
                    </div>
    
                    <!-- REDES SOLCIALES DEL  -->
                    <div class="home__social">
                        <a href="https://www.facebook.com/" target="_blank" class="home__social-link">
                            Facebook
                        </a>
                        <a href="https://es.linkedin.com/company/Viveros-Rioeresma" target="_blank" class="home__social-link">
                            Linkedin
                        </a>
                        <a href="https://www.instagram.com/" target="_blank" class="home__social-link">
                            Instagram
                        </a>
                    </div>
    
                    <!-- DESCRIPCION DEL PRODUCTO -->
                    <div class="home__data">
                        <!-- TITULO DEL PRODUCTO -->
                        <h1 class="home__title"> 
                            <label id="prueba" runat="server"> </label>
                        </h1>
                        <!--<p class="home__description">
                             <label id="descp" runat="server"> </label>
                        </p>-->
                        <span class="home__price" id="price" runat="server"></span>

                        <div class="home__btns">
                            <!--a href="#" class="button button--gray button--small">
                                Discover
                            </a-->

                            <!--button class="button home__button">ADD TO CART</!--button-->
                        </div>
                    </div>
                </div>
            </section>
         </main>
       </asp:Content>
