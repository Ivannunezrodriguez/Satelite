<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AltaCampos.aspx.cs" Inherits="Satelite.AltaCampos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
                  <%--aqui ponemos el javascript--%>
     <script src="<%=Page.ResolveUrl("js/jquery-1.11.0.js") %>"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="<%=Page.ResolveUrl("js/bootstrap.min.js") %>"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="<%=Page.ResolveUrl("js/plugins/metisMenu/metisMenu.min.js") %>"></script>

    <script src="<%=Page.ResolveUrl("js/sb-admin-2.js") %>"></script>

    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


      <div id="page_wrapper" runat="server" >
            <div class="row">
                <div class="panel panel-default">
                   <div class="panel-heading">
                           Campos disponibles
                     </div>
                </div>
                <div class="panel-body">
                   <div class="col-lg-3" >
                    </div>
                    <div class="col-lg-6" >
                        <div class="panel panel-default">

                       <div class="panel-body">

                            <div class="row">
                              <div class="col-lg-12" >
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <div class="row" style="text-align:center;">
                                                <div class="col-lg-2" >
                                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label14" Width="80%"   Text="Disponibles"> </asp:Label>
                                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="LbCampoCount" Width="10%"   Text=""> </asp:Label>
                                                </div>
                                                <div class="col-lg-3" >
                                                     <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="LbCampo1" Width="100%"   Text="Campo:"> </asp:Label>
                                                </div>
                                                <div class="col-lg-2" >
                                                    <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="LbId1" Width="100%"   Text="ID:"> </asp:Label>
                                                </div>
                                                <div class="col-lg-2" >
                                                    <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="LbTipo1" Width="100%"   Text="Tipo:"> </asp:Label>
                                                </div>
                                                <div class="col-lg-3" >
                                                    <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="LbSize1" Width="100%"   Text="Tamaño:"> </asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                       <div class="panel-body">
                                           <div class="row">
                                              <div class="col-lg-7" >
                                                <asp:ListBox ID="ListBox1" style="height:300px;width:100%" AutoPostBack="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                              </div>
                                                <div class="col-lg-3" >
                                                  <asp:ListBox ID="ListBox1Col" style="height:300px; width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBox1Col_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                              </div>
                                              <div class="col-lg-2" >
                                                  <asp:ListBox ID="ListBox1ID" style="height:300px; width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBox1ID_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                              </div>
                                          </div>
                                           <div class="row">
                                                <div class="col-lg-12" >
                                                    <asp:Label Font-Bold="true" type="text" runat="server" ID="LbID"  Text="Campo:"> </asp:Label>
                                                    <asp:DropDownList runat="server" ID="DrDenominacion" CssClass="form-control"  OnSelectedIndexChanged="DrDenominacion_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                </div>
                                            </div>
                                           <div class="row">
                                                <div class="col-lg-12" >
                                                    <asp:Label Font-Bold="true" Width="20%" type="text" runat="server" ID="Label1"  Text="Buscar:"> </asp:Label>
                                                    <asp:TextBox ID="TextBox1" Width="100%" CssClass="form-control" runat="server"></asp:TextBox> 
                                                </div>
                                            </div>
                                           <div class="row">
                                             <div class="col-lg-2" >
                                                    <asp:Button ID="Button5" Width="100%" Enabled="true" CssClass="btn btn-success btn-block"  runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                             </div>
                                               <div class="col-lg-8" >
                                                </div>
                                               <div class="col-lg-2" >
                                                   <asp:Button ID="Button1" Width="100%" Enabled="true" CssClass="btn btn-success btn-block"  runat="server" Text="limpiar" OnClick="btnLimpia_Click" />
                                                </div>
                                         </div>
                                      </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    <div class="col-lg-3" >
                    </div>
                    </div>
                    </div>
                 </div>
            </div>
          
          

            <div class="row">
             <div class="col-lg-6">
                <div class="panel panel-default">

                    <div class="panel-body">

                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="LblIdCampo"  Text="ID:"> </asp:Label>
                        <asp:TextBox CssClass="cajastextos" type="text" Width="100%" Height="35px" runat="server" ID="TxtCampoName" ></asp:TextBox>

                        <asp:Label CssClass="cajaslabel" runat="server" ID="LblTitulo"   Text="Descripción:"> </asp:Label>
                        <asp:TextBox CssClass="cajastextos" runat="server"  Width="100%" Height="35px" ID="Txttitulo"  ></asp:TextBox>


                        <asp:Label CssClass="cajaslabel" runat="server" ID="LblNombre"  Text="Nombre:"> </asp:Label>
                        <asp:TextBox runat="server" CssClass="cajastextos" Width="100%" Height="35px"  ID="txtNombre"  ></asp:TextBox>

                        <asp:Label CssClass="cajaslabel" runat="server" ID="LblTipo"   Text="Tipo Formato:"> </asp:Label>
                        <asp:DropDownList runat="server"  CssClass="form-control" Width="100%"  ID="DFormato"  OnSelectedIndexChanged="DFormato_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                        <asp:Label CssClass="cajaslabel" runat="server" ID="LbValorDef"  Text="Valor por defecto:"> </asp:Label>
                        <asp:TextBox runat="server" CssClass="cajastextos" Width="100%" Height="35px"  ID="TxtValorDefecto"  ></asp:TextBox>
                    </div>
                </div>
            </div>



                 <div class="col-lg-6">
                <div class="panel panel-default">

                    <div class="panel-body">
                        <asp:Label CssClass="cajaslabel" runat="server" ID="LblCapacidad" Text="Capacidad:"> </asp:Label>
                        <asp:TextBox runat="server"  CssClass="cajastextos" Width="100%" Height="35px"  ID="txtCapacidad"  ></asp:TextBox>

                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label5"  Text="Tabla de Validación:"> </asp:Label>
                        <asp:DropDownList runat="server" CssClass="form-control"  ID="DTvalidacion" OnSelectedIndexChanged="DTvalidacion_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />


 
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label7"  Text="Nivel seguridad:"> </asp:Label>
                        <asp:DropDownList runat="server"  CssClass="form-control"  ID="dlNivel"  OnSelectedIndexChanged="dlNivel_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                        <asp:Label CssClass="cajaslabel" runat="server" ID="LbEstado"  Text="Activo:"> </asp:Label>
                        <asp:DropDownList runat="server" CssClass="form-control"  ID="DEstado" OnSelectedIndexChanged="DEstado_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                    
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Lbfecha"  Text="Fecha Modificación:"> </asp:Label>
                        <asp:TextBox runat="server" CssClass="cajastextos" Width="100%" Height="35px"  ID="TxtFecha"  ></asp:TextBox>
                    </div>
                </div>
            </div>


        </div>


            <div class="row">
                <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                          
                            <div class="col-lg-1">
                            </div>
                        <div class="col-lg-2">

                            </div>
                            <div class="col-lg-2">
                                <asp:Button runat="server" ID="btnNuevo" Visible="true" CssClass="btn btn-success btn-block" Width="100%" Text="Nuevo" OnClick="btnNuevo_Click"/>
                                <asp:Button runat="server" ID="btnGuardar" Visible="false" CssClass="btn btn-info btn-block" Width="100%" Text="Guardar" OnClick="btnGuardar_Click"/>
                            </div>
                            <div class="col-lg-2">
                              </div>
                            <div class="col-lg-2">
                                <asp:Button runat="server" ID="btnEditar" Visible="true" CssClass="btn btn-success btn-block" Width="100%" Text="Editar" OnClick="btnEditar_Click"/>
                                <asp:Button runat="server" ID="btnCancelar" Visible="false" CssClass="btn btn-warning btn-block" Width="100%" Text="Cancelar" OnClick="btnCancelar_Click"/>
                            </div>
                            <div class="col-lg-2">

                            </div>
                        <div class="col-lg-1">
                            </div>
                    </div>
            </div> 
            </div>
            </div>
    </div>



</asp:Content>
