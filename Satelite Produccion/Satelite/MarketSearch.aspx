<%@ Page Title="" Language="C#" MasterPageFile="~/MarketplaceParent.Master" AutoEventWireup="true" CodeBehind="MarketSearch.aspx.cs" Inherits="Satelite.MarketSearch" %>

<asp:Content ID="CAMPOSCULTIVO" ContentPlaceHolderID="Main" runat="server">
    <!--==================== PRODUCTOS ====================-->
<form runat="server">
    <section class="products section container">
        <div class="products__container grid">
            <asp:Repeater ID="RepeaterShearch" runat="server">
                <ItemTemplate>
                    <article class="products__card">
                        <a href='<%# "MarketProduct.aspx?product=" + Eval("Id") %>'>
                            <img src="<%# Eval("Url") %>" alt="" class="products__img"/>
                        </a>
                        <h3 class="products__title"><%# Eval("Nombre") %></h3>
                        <span class="products__price"><%# Eval("Precio") %> €</span>
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
    </section>
</form>
</asp:Content>