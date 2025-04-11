<%@ Page Title="" Language="C#" MasterPageFile="~/MarketplaceParent.Master" AutoEventWireup="true" CodeBehind="MarketplaceProducts.aspx.cs" Inherits="Satelite.MarketplaceProducts" %>

<asp:Content ID="CAMPOSCULTIVO" ContentPlaceHolderID="Main" runat="server">
    <!--==================== PRODUCTOS ====================-->
<form runat="server">
    <section class="products section container" id="products>
        <div class="products__container grid">
            <h2 class="section__title" id="group" runat="server">
                          Grupo
                    </h2>
                <br/><br/>
            <asp:Repeater ID="RepeaterSubgrup" runat="server" OnItemDataBound="RepeaterSubgrup_ItemDataBound">
                <ItemTemplate>
                    <!-- cabecera del producto (tipo 1 del subgrupo) -->
                    <div class="story__container grid">
                        <div class="story__data">
                            <h2 class="section__title story__section-title">
                                <%# Eval("Subgrupo") %>
                            </h2>
    
                            <h1 class="story__title">
                                <%# Eval("Nombre") %>
                            </h1>
    
                            <p class="story__description">
                                <%# Eval("Descp") %>
                            </p>
    
                            <a href="#" class="button button--small">Comprar</a>
                        </div>

                        <div class="story__images">
                            <a href='<%# "MarketProduct.aspx?product=" + Eval("Idp") %>'>
                                <img src="<%# Eval("Url") %>" alt="" class="story__img"  style="top:3rem !important"/>
                            </a>
                            <div class="story__square"></div>
                        </div>
                    </div>
                    <br/><br/><br/><br/>
                    <h2 class="section__title">
                          Detalle del Producto
                    </h2>

                    <!-- Repeater de productos -->
                    <div class="products__container grid">
                        <asp:Repeater ID="RepeaterProductos" runat="server">
                            <ItemTemplate> 
                                <article class="products__card">
                                    <a href='<%# "MarketProduct.aspx?product=" + Eval("Id") %>'>
                                        <img src="<%# Eval("Url") %>" alt="" class="products__img"/>
                                    </a>
                                    <h3 class="products__title"><%# Eval("Nombre") %></h3>
                                    <span class="products__price"><%# Eval("Precio").ToString() != "0" ? Eval("Precio") + " €" : "" %> </span>
                                    <button runat="server" class="products__button" id="btdetalle" onServerClick="sendCesta" data-id='<%# Eval("Id") %>'>  
                                        <i class='fa-id-card'></i>
                                    </button>
                                    <button runat="server" class="products__button" id="btn" onServerClick="sendCesta" data-id='<%# Eval("Id") %>'>  
                                        <i class='bx bx-shopping-bag'></i>
                                    </button>
                                </article>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>
</form>
</asp:Content>