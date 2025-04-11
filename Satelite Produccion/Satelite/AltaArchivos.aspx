<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AltaArchivos.aspx.cs" Inherits="Satelite.AltaArchivos" %>
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

              <div id="page_wrapper" runat="server"><!-- /#page-wrapper -->
                <div class="row">
                  <div class="col-lg-12">
                    <h3 id="H3Titulo" runat="server" visible="true"> Relación de Archivos documentales <i class="fa fa-long-arrow-right"></i> <small> “Crea, vincula o desvincula Tablas y columnas o campos relacionales”  </small><small id="SMKey" style="font-weight: bold;" runat="server"></small></h3>
                    <h3 id="H3Desarrollo" runat="server"  style="color:red;" visible="false">(DESARROLLO) --> Relación de Archivos documentales <i class="fa fa-long-arrow-right"></i> <small> “Crea, vincula o desvincula Tablas y columnas o campos relacionales”  </small></h3>
                  </div>
                </div><!-- /.row -->

        <div class="windowmessaje" visible="false" runat="server" id="windowmessaje">
            <div class="contenedormessaje">
                <div class="content-text">
                <asp:Label runat="server"  ID="Lbmensaje" BorderStyle="None" border="0" Width="100%" Text=""/>
               <%-- <a href="#">Leer más.</a>--%>

                </div>
                <%--<div class="content-buttons"><a href="#" id="close-button">Aceptar</a></div>--%>
                <br />
                <br />
                 <div class="row" id="Asume" visible="true" runat="server">
                      <div class="col-lg-3">
                    </div>
                     <div class="col-lg-6">
                        <asp:Button runat="server" ID="btnasume" Visible="true" tooltip="Aceptar" CssClass="btn btn-success btn-block" Width="100%"  Text="Aceptar" OnClick="checkOk_Click"/>
                      </div>
                      <div class="col-lg-3">
                      </div>
                 </div>
                 <div class="row" id="cuestion" visible="false" runat="server">
                     <div class="col-lg-3">
                     </div>
                    <div class="col-lg-6">
                        <asp:Button runat="server" ID="BtnAcepta" Visible="true" tooltip="Aceptar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Aceptar" OnClick="checkSi_Click"/>                                                    
                        </div>
                    <div class="col-lg-6">
                        <asp:Button runat="server" ID="BTnNoAcepta" Visible="true" tooltip="Cancelar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Cerrar" OnClick="checkNo_Click"/>                 
                    </div>
                     <div class="col-lg-3">
                    </div>
                </div>
            </div>
        </div>

<%--                <div runat="server" id="DvPreparado" visible="false"  style=" height:20%; width: 40%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
                    <div class="row" id="Div1" visible="true" style=" height:50%; width: 70%;" runat="server">
                        <div class="col-lg-1">
                        </div>
                        <div class="col-lg-10">
                            <button type="button" class="close" data-dismiss="alert">&times;</button> 
                            <i id="I2" runat="server" class="fa fa-exclamation-circle fa-2x"></i>
                            <asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="Lbmensaje" BorderStyle="None" border="0" Height="80%" Width="80%" Text=" El registro seleccionado se eliminará y las existencias volverán a Listas de Pedidos pendientes ¿Desea continuar?"  />
                        </div>
                        <div class="col-lg-1">
                        </div>
                        <br />
                        <br />
                    </div>
                    <div class="row" id="cuestion" style="height:20%;" visible="false" runat="server">
                        <div class="col-lg-6">
                            <asp:Button runat="server" ID="BtnAcepta" Visible="true" tooltip="Aceptar" CssClass="btn btn-success btn-block" Width="100%"  Text="Aceptar" OnClick="checkSi_Click"/>                                                    
                        </div>
                        <div class="col-lg-6">
                            <asp:Button runat="server" ID="BTnNoAcepta" Visible="true" tooltip="Cancelar" CssClass="btn btn-success btn-block" Width="100%"  Text="Cancelar" OnClick="checkNo_Click"/>                 
                        </div>
                    </div>
                     <div class="row" id="Asume" visible="false" runat="server">
                         <div class="col-lg-4">
                         </div>
                         <div class="col-lg-4">
                            <asp:Button runat="server" ID="Button9" Visible="true" tooltip="Aceptar" CssClass="btn btn-success btn-block" Width="100%"  Text="Aceptar" OnClick="checkSi_Click"/>                                                    
                         </div>
                         <div class="col-lg-4">
                          </div>
                     </div>
                </div>--%>

                     <%--Segundos Mensajes --%>
            <div runat="server" id="DivCuestion" visible="false"  style=" height:20%; width: 40%;z-index: 1999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155); overflow:auto;position: fixed;" class="alert alert-grey centrado">
                <%--<button type="button" class="close" data-dismiss="alert">&times;</button>--%>
                <div class="row" id="Div2" style=" height:50%; width: 50%;" visible="true" runat="server">
                    <div class="col-lg-1">
                     </div>
                    <div class="col-lg-10">
                        <i id="I3" runat="server" class="fa fa-exclamation-circle fa-2x"></i>
                        <asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="lbCuestion" BorderStyle="None" border="0" Width="100%" Text=" Se eliminarán los registros actuales con una nueva consulta ¿Desea continuar?"  />
                    </div>
                        <div class="col-lg-1">
                     </div>
                </div>
                <br />
                <br />
                <div class="row" id="Div4" visible="true" runat="server">
                    <div class="col-lg-4">
                     </div>
                    <div class="col-lg-4">
                    <asp:Button runat="server" ID="Button13" Visible="true" tooltip="Aceptar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Aceptar" OnClick="Aceptar_Click"/>                                                    
                    </div>
                    <div class="col-lg-4">
                    </div>
                </div>
            </div>



                <div class="row">
                  <div class="col-lg-5 col-md-6" >
                        <div class="panel panel-default" id="PanelCampos" visible="true" runat="server">
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
                          </div>
                        </div>
                        <div class="panel panel-default" id="PanelTabla" style="background-color: ivory" visible="false" runat="server">
                            <div class="panel-heading">
                                <div class="row" style="text-align:center;">
                                    <div class="col-lg-2" >
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label14T" Width="80%"   Text="En Tabla"> </asp:Label>
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="LbCampoCountT" Width="10%"   Text=""> </asp:Label>
                                    </div>
                                    <div class="col-lg-3" >
                                         <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="LbCampo1T" Width="100%"   Text="Campo:"> </asp:Label>
                                    </div>
                                    <div class="col-lg-2" >
                                        <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="LbId1T" Width="100%"   Text="ID:"> </asp:Label>
                                    </div>
                                    <div class="col-lg-2" >
                                        <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="LbTipo1T" Width="100%"   Text="Tipo:"> </asp:Label>
                                    </div>
                                    <div class="col-lg-3" >
                                        <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="LbSize1T" Width="100%"   Text="Tamaño:"> </asp:Label>
                                    </div>
                                </div>
                            </div>
                           <div class="panel-body">
                               <div class="row">
                                  <div class="col-lg-7" >
                                    <asp:ListBox ID="ListBox1T" style="height:270px;width:100%" AutoPostBack="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBox1T_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                    <div class="col-lg-3" >
                                      <asp:ListBox ID="ListBox1ColT" style="height:270px; width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBox1ColT_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                  <div class="col-lg-2" >
                                      <asp:ListBox ID="ListBox1IDT" style="height:270px; width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBox1IDT_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                              </div>
                              <div class="row">
                                 <div class="col-lg-7" >  
                                      <asp:Button ID="BtDeleteColum" Enabled="false" CssClass="btn btn-warning btn-block"  runat="server" Text="Eliminar columna fisicamente de la Tabla" OnClick="btnDeletecolum_Click" />
                                 </div>
                                 <div class="col-lg-5" >
                                      <asp:Button ID="BtAsigna" Enabled="false" CssClass="btn btn-success btn-block"  runat="server" Text="Asignar como Campos >>" OnClick="btnAsignacolum_Click" />
                                 </div>

                              </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-2 col-md-6" >
                            <div class="panel panel-default">
                                <div class="panel-heading" id="LbIDArchivo" runat="server">
                                    Relaciones 
                                </div>
                                <div class="panel-body">
                                    <%--<asp:Label  type="text" Width="100%" style="text-align:center; font-weight: bold;"  runat="server" ID="LbIDArchivo"  Text="Archivo:" > </asp:Label>--%>
                              <asp:Button ID="Button1" Enabled="false" CssClass="btn btn-success btn-block"  runat="server" Text="Asignar Campos >>" OnClick="btnPasarSeleccionados_Click" />
                              <asp:Button ID="Button2" Enabled="false" CssClass="btn btn-success btn-block" runat="server" Text="<< Desasignar Campos" OnClick="btnRegresarSeleccionados_Click" />
                              <%--<asp:Button ID="Button3"  CssClass="btn btn-success btn-block" runat="server" Text="Eliminar Campos" OnClick="btnEliminarSeleccionados_Click" />--%>

                              <asp:Button ID="Button4" Enabled="false" CssClass="btn btn-success btn-block" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                              <asp:Button ID="Button5" Enabled="true" CssClass="btn btn-success btn-block"  runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                    <br />
                              <asp:TextBox ID="TextBox1" Width="100%" CssClass="form-control" runat="server"></asp:TextBox>    
                                    <br />
                              <asp:Button ID="Button6" Enabled="false" CssClass="btn btn-success btn-block"   runat="server" Text="Subir" OnClick="Subir_Click" />
                              <asp:Button ID="Button7" Enabled="false" CssClass="btn btn-success btn-block" runat="server"  Text="Bajar" OnClick="Bajar_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-5 col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="row" style="text-align:center;">
                                    <div class="col-lg-1" >
                                    <asp:CheckBox ID="chkKey" ToolTip="Este Campo se utilizará como Key única." runat="server" OnCheckedChanged="sellectKey" AutoPostBack="true" />
                                    </div>
                                    <div class="col-lg-2" >
                                         <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label15" Width="80%"   Text="Utilizados"> </asp:Label>
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="LbUtilizados" Width="10%"   Text=""> </asp:Label>
                                     </div>
                                    <div class="col-lg-3" >
                                        <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="LbCampo2" Width="100%"   Text="Campo:"> </asp:Label>
                                    </div>
                                    <div class="col-lg-2" >
                                        <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="LbId2" Width="100%"   Text="ID:"> </asp:Label>
                                    </div>
                                    <div class="col-lg-1" >
                                        <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="LbTipo2" Width="100%"   Text="Tipo:"> </asp:Label>
                                    </div>
                                    <div class="col-lg-3" >
                                        <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="LbSize2" Width="100%"   Text="Tamaño:"> </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                  <div class="col-lg-7" >
                                    <asp:ListBox ID="ListBox2" style="height:300px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBox2_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                  <div class="col-lg-3" >
                                      <asp:ListBox ID="ListBox2Col" style="height:300px;width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBox2Col_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                   <div class="col-lg-2" >
                                      <asp:ListBox ID="ListBox2ID" style="height:300px;width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBox2ID_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                  <asp:ListBox ID="ListKeys" style="height:300px;width:10%" Visible="false" CssClass="form-control" runat="server"  SelectionMode="Multiple"></asp:ListBox>
                                </div>
                             </div>
                        </div>
                    </div>

                    <%--</div>--%>
                    </div >

                    <div class="row">
                         <div class="col-lg-12">
                               <div class="col-lg-2">
                                  <asp:Label  Font-Bold="true" type="text" runat="server" ID="Label10" Text="Todos los Archivos:"> </asp:Label>
                                </div>
                               <div class="col-lg-3">
                                   <asp:DropDownList runat="server" CssClass="form-control"   ID="DrArchivos"  OnSelectedIndexChanged="DrArchivos_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                </div>
                                 <div class="col-lg-1" style="text-align:left;">
                                     <asp:ImageButton ID="ibtDelete"  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteTabla_Click" ToolTip="Elimina el registro del Archivo Documental seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                     <asp:ImageButton ID="ImgTablaCampo" visible="true" runat="server" ImageUrl="~/Images/pnegro.png" OnClick="btnTablaCampo_Click" ToolTip="Muestra u oculta las Columnas de la Tabla creadas Fisicamente" Width="30px" Height="30px"></asp:ImageButton>
                                      <asp:ImageButton ID="ImgTablaCampoA" visible="false"  runat="server" ImageUrl="~/Images/pazul.png" OnClick="btnTablaCampo_Click" ToolTip="Oculta las Columnas de la Tabla creadas Fisicamente" Width="30px" Height="30px"></asp:ImageButton>
                                  </div>
                               <div class="col-lg-3">
                                   <asp:Label  Font-Bold="true" type="text" runat="server" ID="Label12"   Text="Campos asignados a este Archivo (Campo Clave ID):"> </asp:Label>
                               </div>
                               <div class="col-lg-3">
                                   <asp:DropDownList runat="server" CssClass="form-control" ID="DrCampoasig" OnSelectedIndexChanged="DrCampoasig_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                </div>
                            </div>
                    </div>

                   <div class="row">
                      <div class="col-lg-12">
                            <div class="col-lg-2">
                               <asp:Label  Font-Bold="true" type="text" runat="server" ID="Label19" Text="Filtro:"> </asp:Label>
                             </div>
                          
                            <div class="col-lg-3" >
                                 <asp:TextBox type="text" style="text-align:center;" Enabled="true" Width="100%" CssClass="form-control" runat="server" ID="Txtfiltrar"  Height="35px"  ></asp:TextBox>
                             </div>
                              <div class="col-lg-1" style="text-align:left;">
                                    <asp:ImageButton ID="ImgFiltro"  runat="server" ImageUrl="~/Images/filtro25x25.png" OnClick="btnFiltraTabla_Click" ToolTip="Filtro por el contenido del listado de Archivos" Width="20px" Height="20px"></asp:ImageButton>
                             </div>
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-3">
                             </div>
                         </div>
                 </div>

                     <br />


                    <%--Campos--%>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="LblIDArchivo" Width="100%"   Text="Identificador:"> </asp:Label>
                                    <asp:TextBox type="text" Enabled="false" CssClass="form-control" runat="server" ID="TxtNombre" Width="100%"  Height="35px"  ></asp:TextBox>
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="LblIdentificacion" Width="100%"   Text="Descripción:"> </asp:Label>
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtDescripcion" Width="100%"  ></asp:TextBox>
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label8"  Width="100%" Text="Nombre Tabla:"> </asp:Label>
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TablaName" Width="100%" Height="35px" ></asp:TextBox>
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label9"  Width="100%"  Text="Tabla Objetos:"> </asp:Label>
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TablaObj"  Width="100%"   Height="35px"></asp:TextBox>
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label1"  Width="100%"  Text="Conexión a Base de Datos:"> </asp:Label>
                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrConexion" Width="100%"  OnSelectedIndexChanged="dlConexion_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label3"  Width="100%"  Text="Número registros Documentos Electrónicos:"> </asp:Label>
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TextDocElec"  Width="100%"   Height="35px"></asp:TextBox>
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label11"  Width="100%"  Text="Ruta Documentos Electrónicos:"> </asp:Label>
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TextRuta"  Width="100%"   Height="35px"></asp:TextBox>
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label17"  Width="100%"  Text="Permitir registros duplicados:"> </asp:Label>
                                    <%--<asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TextDuplicado"  Width="100%"   Height="35px"></asp:TextBox>--%>
                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrDuplicado" Width="100%"  OnSelectedIndexChanged="DrDuplicado_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                    </div>
                            </div>
                         </div>
                           <div class="col-lg-6">
                            <div class="panel panel-default">

                                <div class="panel-body" runat="server" id="DivCampoDer">                        
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label6"  Width="95%" Text="Nivel seguridad:"> </asp:Label>
                                    <button id="BtTipo" visible="false" type="button" runat="server" style=" border-style:none; background-color:transparent;" class="text-muted "  onserverClick="BtTipo_Click"><i title="Introduzca consulta general con filtro o sin filtro según selección sobre la base de datos externa. Eliminará la anterior consulta guardada." style="margin-top:-10px;" class="fa fa-bars fa-2x"></i></button>
                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="dlNivel" Width="100%"  OnSelectedIndexChanged="dlNivel_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="LblResponsable" Width="100%"  Text="Dependiente de:"> </asp:Label>
                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="Djerarquia" Width="100%"  OnSelectedIndexChanged="Djerarquia_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />   
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label7" Width="100%"  Text="Estado:"> </asp:Label>
                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="dlEstado" Width="100%"  OnSelectedIndexChanged="dlEstado_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label5" Width="100%"   Text="Tipo de elemento:"> </asp:Label>
                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="Dtipo" Width="100%"  OnSelectedIndexChanged="Dtipo_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" /> 
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label2"  Width="100%"  Text="Número registros:"> </asp:Label>
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TextRegistro"  Width="100%"   Height="35px"></asp:TextBox>
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label13"  Width="100%"  Text="Unidades Documentos Electrónicos:"> </asp:Label>
                                    <asp:TextBox runat="server"  Enabled="false" CssClass="form-control" ID="Textunidad"  Width="100%"   Height="35px"></asp:TextBox>
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label4"  Width="100%"  Text="Ocupación en Disco:"> </asp:Label>
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TextHardDisc"  Width="100%"   Height="35px"></asp:TextBox>
                                </div>
                                <div class="panel-body" visible="false" runat="server" id="DivSQL">
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label16"  Width="90%" Text="Consulta SQL:"> </asp:Label>
                                    <button id="Button8" type="button" runat="server" style=" border-style:none; background-color:transparent;" class="text-muted "  onserverClick="Btconecta_Click"><i id="Iconexion" runat="server" title="Comprueba si es correcta la consulta general añadida a esta casilla de Texto." style="margin-top:-10px;" class="fa fa-exchange fa-2x"></i></button>
                                    <button id="Button3" type="button" runat="server" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtTipo_Click"><i title="Introduzca consulta general con filtro o sin filtro según selección sobre la base de datos externa. Eliminará la anterior consulta guardada." style="margin-top:-10px;" class="fa fa-bars fa-2x"></i></button>
                                    <asp:TextBox ID="CommentSQL" Width="100%" Height="100%"  TextMode="MultiLine" Columns="50" Rows="5" runat="server"/>
                                </div>
                                <div class="panel-body" visible="false" runat="server" id="DivReplica">
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label18"  Width="90%" Text="Consulta SQL:"> </asp:Label>
                                    <button id="Button12" type="button" runat="server" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="MigraRep_Click"><i title="Replica consulta general según selección sobre la base de datos externa en la base de datos de Sistema. Actualizará la anterior consulta guardada." style="margin-top:-10px;" class="fa fa-outdent fa-2x"></i></button>
                                    <button id="Button10" type="button" runat="server" style=" border-style:none; background-color:transparent;" class="text-muted "  onserverClick="BtconectaRep_Click"><i id="I1" runat="server" title="Comprueba si es correcta las consultas generales añadida a esta casillas de Texto, tanto Datos como Documentos." style="margin-top:-10px;" class="fa fa-exchange fa-2x"></i></button>
                                    <button id="Button11" type="button" runat="server" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtTipoRep_Click"><i title="Introduzca consulta general con filtro o sin filtro según selección sobre la base de datos externa. Una vez creadas las tablas solo actualiza el sistema." style="margin-top:-10px;" class="fa fa-bars fa-2x"></i></button>
                                    <asp:TextBox ID="CommentSQLRep" Width="100%" Height="100%"  TextMode="MultiLine" Columns="50" Rows="5" runat="server"/>
                                    <asp:TextBox ID="CommentSQLDoc" Width="100%" Height="100%"  TextMode="MultiLine" Columns="50" Rows="5" runat="server"/>
                                </div>
                            </div>
                         </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                                <div class="panel-body">
                          
                                        <div class="col-lg-1">
                                        </div>
                                    <div class="col-lg-2">
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Button runat="server" ID="btnGuardar" Visible="false" CssClass="btn btn-info btn-block" Width="100%" Text="Guardar" OnClick="btnGuardar_Click"/>
                                            <asp:Button runat="server" ID="btnNuevo" Visible="true" CssClass="btn btn-success btn-block" Width="100%"  Text="Nuevo" OnClick="btnNuevo_Click"/>
                                        </div>
                                        <div class="col-lg-2">
                                          </div>
                                        <div class="col-lg-2">
                                         <asp:Button runat="server" ID="btnEditar" Visible="true"  CssClass="btn btn-success btn-block"  Width="100%" Text="Editar" OnClick="btnEditar_Click"/>
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






</asp:Content>
