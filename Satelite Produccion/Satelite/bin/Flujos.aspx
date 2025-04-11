<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Flujos.aspx.cs" Inherits="Satelite.Flujos" %>
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



<%--        <div runat="server" id="DvPreparado" visible="false"  style=" width: 30%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
        <button type="button" class="close" data-dismiss="alert">&times;</button> 
        <i id="I2" runat="server" class="fa fa-exclamation-circle fa-2x"></i>
        <asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="Lbmensaje" BorderStyle="None" border="0" Width="100%" Text=" El registro seleccionado se eliminará y las existencias volverán a Listas de Pedidos pendientes ¿Desea continuar?"  />
        <div class="row" id="cuestion" visible="false" runat="server">
            <div class="col-lg-6">
                <asp:Button runat="server" ID="BtnAcepta" Visible="true" tooltip="Aceptar" CssClass="btn btn-success btn-block" Width="100%"  Text="Aceptar" OnClick="checkSi_Click"/>                                                    
            </div>
            <div class="col-lg-6">
                <asp:Button runat="server" ID="BTnNoAcepta" Visible="true" tooltip="Cancelar" CssClass="btn btn-success btn-block" Width="100%"  Text="Cancelar" OnClick="checkNo_Click"/>                 
            </div>
        </div>
            <div class="row" id="Asume" visible="false" runat="server">
                <asp:Button runat="server" ID="Button9" Visible="true" tooltip="Aceptar" CssClass="btn btn-success btn-block" Width="100%"  Text="Aceptar" OnClick="checkSi_Click"/>                                                    
        </div>
    </div>--%>

    <div id="PanelFlujos" visible="true" runat="server" class="panel-body">

              <div id="page_wrapper" runat="server"><!-- /#page-wrapper -->


                  <br />
                    <div class="row">
                            <div class="col-lg-2">
                            </div>
                            <div class="col-lg-1" style="text-align:right">
                                <asp:Label  Font-Bold="true" type="text" runat="server" ID="Label10" Text="Todos los Archivos:"> </asp:Label>
                            </div>
                            <div class="col-lg-4">
                                <asp:DropDownList runat="server" CssClass="form-control"  ID="DrArchivos"  OnSelectedIndexChanged="DrArchivos_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                            </div>
                             <div class="col-lg-5">
                            </div>
                        </div>
                        <br />

                        <div class="row">

                             <div class="col-lg-1" style="text-align:right">
                                   <asp:Label  Font-Bold="true" type="text" runat="server" ID="Label18" ToolTip="Flujos asignados al Archivo Documental seleccionado"   Text="Flujos:"> </asp:Label>
                            </div>
                            <div class="col-lg-2">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="DrFlujos" OnSelectedIndexChanged="DrFlujos_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-lg-3">
                                 <asp:ImageButton ID="ImageCopiaFlujo"  runat="server" ImageUrl="~/Images/copiar.png" OnClick="btnCopiaFlujo_Click" ToolTip="Copia el Flujo de Trabajo en pantalla y lo presenta pendiente de guardado" Width="30px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="IbtCreaFlujo"  runat="server" ImageUrl="~/Images/newdoc.png" OnClick="btnCreaFlujo_Click" ToolTip="Crear un nuevo  Flujo de Trabajo" Width="25px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ibtEditFlujo"  runat="server" ImageUrl="~/Images/editdoc.png" OnClick="btnEditFlujo_Click" ToolTip="Edita el Flujo de Trabajo del Archivo Documental seleccionado" Width="25px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ibtDeleteFlujo"  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteFlujo_Click" ToolTip="Elimina el Flujo de Trabajo del Archivo Documental seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="IbtAllFlujo"  runat="server" ImageUrl="~/Images/allwhite.png" OnClick="btnAllFlujo_Click" ToolTip="Muestra todos los Flujos de Trabajos en la lista de selección" Width="30px" Height="30px"></asp:ImageButton>
                                </div>

                            <div class="col-lg-1" style="text-align:right">
                                <asp:Label  Font-Bold="true" type="text" runat="server" ID="Label12"  ToolTip="Estados asignados al Flujo de trabajo seleccionado" Text="Estados:"> </asp:Label>
                            </div>
                            <div class="col-lg-2">
                                <asp:DropDownList runat="server" CssClass="form-control" ID="DrEstado" OnSelectedIndexChanged="DrEstado_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-lg-3" >
                                 <asp:ImageButton ID="ImageCopiaEstado"  runat="server" ImageUrl="~/Images/copiar.png" OnClick="btnCopiaEstado_Click" ToolTip="Crear un nuevo Estado" Width="30px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ibtCreaEstado"  runat="server" ImageUrl="~/Images/newdoc.png" OnClick="btnCreaEstado_Click" ToolTip="Crear un nuevo Estado" Width="25px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ibtEditEstado"  runat="server" ImageUrl="~/Images/editdoc.png" OnClick="btnEditEstado_Click" ToolTip="Edita el Estado seleccionado" Width="25px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ibtDeleteEstado"  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteEstado_Click" ToolTip="Elimina el Estado seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="IbtAllEstado"  runat="server" ImageUrl="~/Images/allwhite.png" OnClick="btnAllEstado_Click" ToolTip="Muestra todos los Estados de Flujos de Trabajos en la lista de selección" Width="30px" Height="30px"></asp:ImageButton>
                            </div>

                         </div>
                     </div>
                    <br />


                    <%--Campos--%>
                    <div class="row">
                        <div class="col-lg-6" >
                            <div class="panel panel-default" style="height:524px;">
                               <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-12" >
                                            <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label19" Width="80%" style="font-size:18px; font-weight: bold;"   Text="Flujo de Trabajo:"> </asp:Label>
                                        </div>
                                    </div>
                                </div>


                                
                                <div class="panel-body" runat="server" id="DivCampoIzq">
                                    <%--Flujos:--%>
                                    <div class="panel" style="height:400px;" visible="true" runat="server" id="DivPanelFlujo">
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="LblIDArchivo" Width="100%"   Text="Identificador:"> </asp:Label>
                                        <asp:TextBox type="text" Enabled="false" CssClass="form-control" runat="server" ID="TxtNombre" Width="100%"  Height="35px"  ></asp:TextBox>
                                        <asp:Label CssClass="cajaslabel" runat="server" ID="LblIdentificacion" Width="100%"   Text="Descripción:"> </asp:Label>
                                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtDescripcion" Width="100%"  ></asp:TextBox>
                                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label56"  Width="100%"  Text="Publicado en página:"> </asp:Label>
                                        <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrPagina" Width="100%"  OnSelectedIndexChanged="DrPagina_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label1"  Width="100%"  Text="Seleccione Archivo Documental:"> </asp:Label>
                                        <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrConexion" Width="100%"  OnSelectedIndexChanged="dlConexion_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label3" Width="96%"   Text="Archivos Documentales Asociados:"> </asp:Label>
                                         <asp:ImageButton ID="ImageButton1" style="top:20px; "  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteArDoc_Click" ToolTip="Elimina el Archivo Documental seleccionado de la lista" Width="15px" Height="15px"></asp:ImageButton>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                 <asp:ListBox ID="ListBoxArchivo" Enabled="false" style="height:140px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" ondblclick="ListBoxArchivo_DoubleClick"  SelectionMode="Multiple"></asp:ListBox>    
                                                 <asp:ListBox ID="ListBoxArchivoID" Visible="false" style="height:140px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" ondblclick="ListBoxArchivo_DoubleClick"  SelectionMode="Multiple"></asp:ListBox>    

                                            </div>

                                        </div>
                                        <div class="row" runat="server" Visible="false" ID="DivSQLProfile">
                                            <asp:Label CssClass="cajaslabel" runat="server" ID="Label16"  Width="90%" Text="Consulta SQL:"> </asp:Label>
                                            <asp:TextBox ID="TxtQueryProfile"  Width="100%" Height="100%"  TextMode="MultiLine" Columns="50" Rows="6" runat="server"/>
                                        </div>
                                        <br />
                                        <div class="row">
                                        
                                            <div class="col-lg-3">
                                                <asp:Button runat="server" Enabled="true" ID="Button17" Visible="true" CssClass="btn btn-info btn-block" Width="100%" Text="Perfil del Flujo" OnClick="btnProfiles_Click"/>
                                            </div>
                                            <div class="col-lg-1">
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:Button runat="server" Enabled="false" ID="BtguardaFlujo" Visible="true" CssClass="btn btn-success btn-block" Width="100%" Text="Guardar" OnClick="btnGuardarFlujo_Click"/>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:Button runat="server" Enabled="false" ID="BtCancelFlujo" Visible="true" CssClass="btn btn-warning btn-block" Width="100%" Text="Cancelar" OnClick="btnCancelarFlujo_Click"/>
                                            </div>
                                            <div class="col-lg-1">
                                            </div>
                                            <div class="col-lg-3">
                                            </div>

                                        </div>
                                    </div>
                                    <%--fin Flujos:--%>

                                    <%--Profiles:--%>
                                    <div class="panel" visible="false" style="height:400px;" runat="server" id="DivProfiles">
                                        <div class="panel" visible="true" runat="server" id="DvProfile">
                                             <div class="row">
                                                <div class="col-lg-6">
                                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label57" Width="100%"   Text="Identificador:"> </asp:Label>
                                                    <asp:TextBox type="text" Enabled="false" CssClass="form-control" runat="server" ID="TxtIdProfiles" Width="100%"  Height="35px"  ></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6">                                 
                                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label61" Width="100%"   Text="Archivo Documental:"> </asp:Label>
                                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrArchivoProfile" Width="100%"  OnSelectedIndexChanged="drProfilesAll_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6">                                            
                                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label62" Width="100%"   Text="Flujo de Trabajo:"> </asp:Label>
                                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrFlujoProfile" Width="100%"  OnSelectedIndexChanged="drProfilesAll_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                                </div>
                                                <div class="col-lg-6">                                      
                                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label63" Width="100%"   Text="Estado de Flujo de Trabajo:"> </asp:Label>
                                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrEstadoProfile" Width="100%"  OnSelectedIndexChanged="drProfilesAll_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6">                                            
                                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label64"  Width="100%"  Text="Publicado en página:"> </asp:Label>
                                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrPaginaProfile" Width="100%"  OnSelectedIndexChanged="drProfilesAll_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label65"  Width="100%"  Text="Procedimiento:"> </asp:Label>
                                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrProcedimientoProfile" Width="100%"  OnSelectedIndexChanged="drProfilesAll_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                                </div>
                                            </div>
                                           <div class="row">
                                                <div class="col-lg-6">
                                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label58" Width="100%"   Text="Ruta Documentos:"> </asp:Label>
                                                    <asp:TextBox type="text" Enabled="false" CssClass="form-control" runat="server" ID="TxtDocsProfile" Width="100%"  Height="35px"  ></asp:TextBox>
                                                </div>
                                                <div class="col-lg-6">
                                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label59" Width="100%"   Text="Ruta Directorios:"> </asp:Label>
                                                    <asp:TextBox type="text" Enabled="false" CssClass="form-control" runat="server" ID="TxtDirProfile" Width="100%"  Height="35px"  ></asp:TextBox>
                                                </div>
                                            </div>

                                            <asp:Label CssClass="cajaslabel" runat="server" ID="Label66"  Width="100%"  Text="Campo Filtro:"> </asp:Label>
                                            <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrcampoFiltro" Width="100%"  OnSelectedIndexChanged="drCampofiltroselección_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                                            <asp:Label CssClass="cajaslabel" runat="server" ID="Label67"  Width="100%"  Text="Campo Documentos:"> </asp:Label>
                                            <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="drCampofiltroseleccion" Width="100%"  OnSelectedIndexChanged="drCampofiltroselección_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                                             <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label68" Width="96%"   Text="Campos Filtro Condición:"> </asp:Label>
                                             <asp:ImageButton ID="ImageButton14" style="top:20px; "  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteFiltroProfile_Click" ToolTip="Elimina el Archivo Documental seleccionado de la lista" Width="15px" Height="15px"></asp:ImageButton>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                     <asp:ListBox ID="ListBox3" Enabled="false" style="height:60px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" ondblclick="ListBoxFiltroProfile_DoubleClick"  SelectionMode="Multiple"></asp:ListBox>    
                                                     <asp:ListBox ID="ListBox4" Visible="false" style="height:60px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" ondblclick="ListBoxFiltroProfile_DoubleClick"  SelectionMode="Multiple"></asp:ListBox>    

                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" runat="server" Visible="false" style="height:400px;" ID="DivSQL">
                                            <div class="col-lg-12">
                                                 <asp:Label CssClass="cajaslabel" runat="server" ID="Label94"  Width="90%" Text="Parametros:"> </asp:Label>
                                                <asp:TextBox ID="TxtParam"  Width="100%" Height="100%"  TextMode="MultiLine" Columns="1" Rows="1" runat="server"/>
                                                <asp:Label CssClass="cajaslabel" runat="server" ID="Label60"  Width="90%" Text="Consulta SQL:"> </asp:Label>
                                                <asp:TextBox ID="CommentSQL"  Width="100%" Height="100%"  TextMode="MultiLine" Columns="50" Rows="19" runat="server"/>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-2">
                                                    <asp:Button runat="server" Enabled="true" ID="Button18" Visible="true" CssClass="btn btn-info btn-block" Width="100%" Text="Condición" OnClick="btnCondicionProfile_Click"/>
                                            </div>
                                            <div class="col-lg-2">
                                                  <asp:ImageButton ID="ImageButton5"  runat="server" ImageUrl="~/Images/ejec.png" OnClick="btnEjecProceso_Click" ToolTip="Ejecuta la Consulta y si contiene parametros tambien se procesan." Width="30px" Height="30px"></asp:ImageButton>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:Button runat="server" Enabled="true" ID="BteditProfile" Visible="true" CssClass="btn btn-success btn-block" Width="100%" Text="Editar" OnClick="btnEditaProfile_Click"/>
                                                <asp:Button runat="server"  ID="BtGuardaProfile" Visible="false" CssClass="btn btn-success btn-block" Width="100%" Text="Guardar" OnClick="btnGuardarProfile_Click"/>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:Button runat="server"  ID="BtCancelaProfile" Visible="false" CssClass="btn btn-warning btn-block" Width="100%" Text="Cancelar" OnClick="btnCancelarProfile_Click"/>
                                            </div>
                                            <div class="col-lg-2">
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:Button runat="server" Enabled="true" ID="BtCerrarProfile" Visible="true" CssClass="btn btn-info btn-block" Width="100%" Text="Cerrar" OnClick="btnProfiles_Click"/>
                                            </div>
                                        </div>
                                       
                                    </div>
                                    <%--fin Profiles:--%>

                                  </div>
                            </div>
                         </div>

                        <%--Estado de Flujo de Trabajo:--%>
                        <div class="col-lg-6"  style="height:510px;">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-12" >
                                            <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label20" Width="80%" style="font-size:18px; font-weight: bold;"   Text="Estado de Flujo de Trabajo:"> </asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <%--Div General de la derecha--%>
                                <div class="panel-body" visible="true" runat="server" id="DivCampoDer">   
                                    <div class="panel" visible="true" style="height:400px;" runat="server" id="DivEstados">
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label2" Width="100%"   Text="Identificador:"> </asp:Label>
                                        <asp:TextBox type="text" Enabled="false" CssClass="form-control" runat="server" ID="TxtidEstado" Width="100%"  Height="35px"  ></asp:TextBox>
                                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label4" Width="100%"   Text="Descripción:"> </asp:Label>
                                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtEstado" Width="100%"  ></asp:TextBox>
                                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label5" Width="100%"   Text="Previo:"> </asp:Label>
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="Dratras" Width="100%"  OnSelectedIndexChanged="dratras_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                             </div>
                                            <div class="col-lg-1" >
                                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label11" Width="80%"   Text="Visible:"> </asp:Label>
                                            </div>
                                            <div class="col-lg-1">
                                                <asp:ImageButton ID="Imgatras" Visible="true"  runat="server" ImageUrl="~/Images/ok.png" OnClick="Imgatras_Click" ToolTip="Nuevo Flujo de Trabajo del Archivo Documental seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                                <asp:ImageButton ID="ImgatrasC" Visible="false" runat="server" ImageUrl="~/Images/cancel.png" OnClick="Imgatras_Click" ToolTip="Nuevo Flujo de Trabajo del Archivo Documental seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                            </div>
                                        </div>
                                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label6" Width="100%"   Text="Siguiente:"> </asp:Label>
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="Drsiguiente" Width="100%"  OnSelectedIndexChanged="drsiguiente_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                             </div>
                                            <div class="col-lg-1" >
                                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label21" Width="80%"   Text="Visible:"> </asp:Label>
                                            </div>
                                            <div class="col-lg-1">
                                                <asp:ImageButton ID="ImgNext" Visible="true"  runat="server" ImageUrl="~/Images/ok.png" OnClick="ImgNext_Click" ToolTip="Nuevo Flujo de Trabajo del Archivo Documental seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                                <asp:ImageButton ID="ImgNextC" Visible="false" runat="server" ImageUrl="~/Images/cancel.png" OnClick="ImgNext_Click" ToolTip="Nuevo Flujo de Trabajo del Archivo Documental seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                            </div>
                                        </div>
                                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label7" Width="100%"   Text="Alternativo:"> </asp:Label>
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="Dralternativo" Width="100%"  OnSelectedIndexChanged="dralternativo_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" /> 
                                            </div>
                                            <div class="col-lg-1" >
                                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label13" Width="80%"   Text="Visible:"> </asp:Label>
                                            </div>
                                            <div class="col-lg-1">
                                                <asp:ImageButton ID="ImgAlter" Visible="true" runat="server" ImageUrl="~/Images/ok.png" OnClick="ImgAlter_Click" ToolTip="Nuevo Flujo de Trabajo del Archivo Documental seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                                <asp:ImageButton ID="ImgAlterC" Visible="false" runat="server" ImageUrl="~/Images/cancel.png" OnClick="ImgAlter_Click" ToolTip="Nuevo Flujo de Trabajo del Archivo Documental seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                            </div>
                                        </div>
                                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label8" Width="100%"   Text="Final:"> </asp:Label>
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="Drfinal" Width="100%"  OnSelectedIndexChanged="drfinal_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" /> 
                                            </div>
                                            <div class="col-lg-1" >
                                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label17" Width="80%"   Text="Visible:"> </asp:Label>
                                            </div>
                                            <div class="col-lg-1">
                                                <asp:ImageButton ID="imgFin" Visible="true" runat="server" ImageUrl="~/Images/ok.png" OnClick="imgFin_Click" ToolTip="Nuevo Flujo de Trabajo del Archivo Documental seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                                <asp:ImageButton ID="imgFinC" Visible="false" runat="server" ImageUrl="~/Images/cancel.png" OnClick="imgFin_Click" ToolTip="Nuevo Flujo de Trabajo del Archivo Documental seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                            </div>
                                        </div>
                                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label9" Width="100%"   Text="Imagen del Estado:"> </asp:Label>
                                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtCondicion" Width="100%"  ></asp:TextBox>
                                         <br />
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <asp:Button runat="server" Enabled="true" ID="Button3" Visible="true" CssClass="btn btn-info btn-block" Width="100%" Text="Condición" OnClick="btnMuestraCondicion_Click"/>
                                            </div>
                                            <div class="col-lg-2">
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:Button runat="server" Enabled="false" ID="BtGuardaEstado" Visible="true" CssClass="btn btn-success btn-block" Width="100%" Text="Guardar" OnClick="btnGuardarEstado_Click"/>
                                             </div>
                                             <div class="col-lg-2">
                                                <asp:Button runat="server" Enabled="false" ID="BtCancelEstado" Visible="true" CssClass="btn btn-warning btn-block" Width="100%" Text="Cancelar" OnClick="btnCancelarEstado_Click"/>
                                            </div>
                                            <div class="col-lg-2">
                                            </div>
                                             <div class="col-lg-2">
                                                 <asp:Button runat="server" Enabled="true" ID="Button8" Visible="true" CssClass="btn btn-info btn-block" Width="100%" Text="Procesos" OnClick="btnMuestraProcesos_Click"/>
                                            </div>
                                        </div>
                                   </div>

                                    <%--el otro Div General templates de la derecha--%>
                                    <div class="panel-body" visible="false" runat="server" id="DivTemplate">  
                                        <%--Si es la Consulta del Estado--%>
                                        <div class="col-lg-12">
                                            <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label70" Width="80%"   Text="Template Master:"> </asp:Label>
                                            <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrTemplates" Width="100%"  OnSelectedIndexChanged="DrTemplates_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                            <asp:ImageButton ID="ImgEliminaTemplate" style="top:20px; "  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteTemplate_Click" ToolTip="Elimina la Template Master seleccionada de la lista" Width="15px" Height="15px"></asp:ImageButton>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                        <asp:ListBox ID="ListTemplate" Enabled="false" style="height:310px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" ondblclick="ListTemplate_DoubleClick"  SelectionMode="Multiple"></asp:ListBox>    
                                                        <asp:ListBox ID="ListTemplateID" Visible="false" style="height:310px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" ondblclick="ListTemplate_DoubleClick"  SelectionMode="Multiple"></asp:ListBox>    
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <asp:Button runat="server" Enabled="true" ID="Button20" Visible="true" CssClass="btn btn-info btn-block" Width="100%" Text="Consulta SQL" OnClick="btnCondicionSQL_Click"/>
                                            </div>
                                            <div class="col-lg-2">
                                            </div>
                                            <div class="col-lg-2">
                                                <%--<asp:Button runat="server" Enabled="true" ID="Button20" Visible="true" CssClass="btn btn-success btn-block" Width="100%" Text="Guardar" OnClick="btnguardaTemplate_Click"/>--%>
                                            </div>
                                            <div class="col-lg-2">
                                            </div>
                                            <div class="col-lg-2">
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:Button runat="server" Enabled="true" ID="Button21" Visible="true" CssClass="btn btn-success btn-block" Width="100%" Text="Volver" OnClick="btnCondicionTemplate_Click"/>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" runat="server" Visible="false" ID="DivTxtQuery" >
                                        <div class="col-lg-12">
                                                <asp:Label CssClass="cajaslabel" runat="server" ID="Label69"  Width="90%" Text="Consulta SQL:"> </asp:Label>
                                                <asp:TextBox ID="TxtCondicionQR"  Width="100%" Height="100%"  TextMode="MultiLine" Columns="50" Rows="19" runat="server"/>
                                            </div>
                                        <br />
                                         <div class="row">
                                            <div class="col-lg-2">
                                                <asp:Button runat="server" Enabled="true" ID="Button19" Visible="true" CssClass="btn btn-info btn-block" Width="100%" Text="Templates" OnClick="btnCondicionNOSQL_Click"/>
                                            </div>
                                            <div class="col-lg-2">
                                            </div>
                                            <div class="col-lg-2">
                                                <%--<asp:Button runat="server" Enabled="true" ID="Button19" Visible="true" CssClass="btn btn-success btn-block" Width="100%" Text="Guardar" OnClick="btnguardaTemplate_Click"/>--%>
                                            </div>
                                            <div class="col-lg-2">
                                            </div>
                                            <div class="col-lg-2">
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:Button runat="server" Enabled="true" ID="Button22" Visible="true" CssClass="btn btn-success btn-block" Width="100%" Text="Volver" OnClick="btnCondicionTemplate_Click"/>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Fin la Consulta del Estado--%>


                                    <%--Fin el otro Div General templates de la derecha--%>
                                </div>
                                <%--Fin Div General de la derecha--%>
                            </div>
                            
                        </div>
                     </div>
                </div>
                <%--Asignar Estados--%>
                <div class="row" runat="server" id="divAsigEstado">
                  <div class="col-lg-5 col-md-6" >
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="row" >
                                    <div class="col-lg-12" >
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label14" Width="80%" style="font-size:18px; font-weight: bold;"  Text="Estados Disponibles:"> </asp:Label>
                                    </div>
                                </div>
                            </div>
                           <div class="panel-body">
                               <div class="row">
                                  <div class="col-lg-10" >
                                    <asp:ListBox ID="ListBox1" style="height:300px;width:100%" AutoPostBack="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                  <div class="col-lg-2" >
                                      <asp:ListBox ID="ListBox1ID" style="height:300px; width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBox1ID_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                              </div>
                          </div>
                        </div>
                    </div>

                    <div class="col-lg-2 col-md-6" >
                            <div class="panel panel-default">
                                <div class="panel-heading" id="LbIDArchivo" runat="server" style="font-size:18px; font-weight: bold;">
                                    Relaciones 
                                </div>
                                <div class="panel-body">
                              <asp:Button ID="Button1" Enabled="false" CssClass="btn btn-success btn-block"  runat="server" Text="Asignar Estados >>" OnClick="btnPasarSeleccionados_Click" />
                              <asp:Button ID="Button2" Enabled="false" CssClass="btn btn-success btn-block" runat="server" Text="<< Desasignar Estados" OnClick="btnRegresarSeleccionados_Click" />

                              <asp:Button ID="Button4" Enabled="false" CssClass="btn btn-success btn-block" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                              <asp:Button ID="Button5" Enabled="true" CssClass="btn btn-success btn-block"  runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                    <br />
                              <asp:TextBox ID="TextBox1" Width="100%" CssClass="form-control" runat="server">Buscar...</asp:TextBox>    
                                    <br />
                              <asp:Button ID="Button6" Enabled="false" CssClass="btn btn-success btn-block"   runat="server" Text="Subir" OnClick="Subir_Click" />
                              <asp:Button ID="Button7" Enabled="false" CssClass="btn btn-success btn-block" runat="server"  Text="Bajar" OnClick="Bajar_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-5 col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-lg-12" >
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="LbRelFlujo" Width="80%" style="font-size:18px; font-weight: bold;"  Text="Estados Asociados:"> </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                  <div class="col-lg-10" >
                                    <asp:ListBox ID="ListBox2" style="height:300px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBox2_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
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
                    <div class="row">
                        <div class="col-lg-4">
                        </div>
                        <div class="col-lg-2">
                            <asp:Button runat="server" visible="false" Enabled="false" ID="BtGuardaRel" CssClass="btn btn-success btn-block" Width="100%" Text="Guardar" OnClick="btnGuardarRelacion_Click"/>
                        </div>
                        <div class="col-lg-2">
                            <asp:Button runat="server" visible="false" Enabled="false" ID="BtCancelRel" CssClass="btn btn-warning btn-block" Width="100%" Text="Cancelar" OnClick="btnCancelarRelacion_Click"/>
                        </div>
                        <div class="col-lg-4">
                        </div>
                    </div>
            </div >
                <%--Fin Asignar Estados--%>
            <%--</div>--%>

    <%--Panel general Procesos--%>
    <div id="PanelProcesos" visible="false" runat="server" class="panel-body">
            <%--Panel Procesos--%>
            <div class="col-lg-4" runat="server" id="MProcesos">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-lg-8" >
                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label15" Width="100%" style="font-size:18px; font-weight: bold;"   Text="Procesos:"> </asp:Label>
                             </div>
                            <div class="col-lg-4">
                                <asp:ImageButton ID="ImageCopiaProc"  runat="server" ImageUrl="~/Images/copiar.png" OnClick="btnCopiaProceso_Click" ToolTip="Copia el Proceso en pantalla y lo presenta pendiente de guardado" Width="30px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton2"  runat="server" ImageUrl="~/Images/newdoc.png" OnClick="btnCreaProceso_Click" ToolTip="Crear un nuevo  Proceso" Width="25px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton3"  runat="server" ImageUrl="~/Images/editdoc.png" OnClick="btnEditProceso_Click" ToolTip="Edita el Proceso seleccionado" Width="25px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton4"  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteProceso_Click" ToolTip="Elimina el Proceso seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                 <%--<asp:ImageButton ID="ImageButton5"  runat="server" ImageUrl="~/Images/allwhite.png" OnClick="btnAllProceso_Click" ToolTip="Muestra todos los Procesos en la lista de selección" Width="30px" Height="30px"></asp:ImageButton>--%>
                             </div>
                             <div class="col-lg-12" >
                                <asp:DropDownList runat="server" CssClass="form-control" Width="100%" ID="DrProcesos" OnSelectedIndexChanged="DrProcesos_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                            </div>                            
                        </div>
                    </div>




                     <div class="panel-body" visible="true" runat="server" id="Div1">                        
                            <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label23" Width="100%"   Text="Identificador:"> </asp:Label>
                            <asp:TextBox type="text" Enabled="false" CssClass="form-control" runat="server" ID="TextIdProceso" Width="100%"  Height="35px"  ></asp:TextBox>
                                    
                            <asp:Label CssClass="cajaslabel" runat="server" ID="Label24" Width="100%"   Text="Descripción:"> </asp:Label>
                            <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtDescProceso" Width="100%" OnTextChanged="TexTosProcesos_TextChanged" OnClick="TexTosProcesos_Click" AutoPostBack="true"  ></asp:TextBox>
                                    
                            <asp:Label CssClass="cajaslabel" runat="server" ID="Label25" Width="100%"   Text="Estado Inicial:"> </asp:Label>
                            <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrEstadoIni" Width="100%"  OnSelectedIndexChanged="DrEstadoIni_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                            <asp:Label CssClass="cajaslabel" runat="server" ID="Label28" Width="100%"   Text="Archivo Documental:"> </asp:Label>
                            <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrArchivoIni" Width="100%"  OnSelectedIndexChanged="DrArchivoIni_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                            <asp:Label CssClass="cajaslabel" runat="server" ID="Label30" Width="100%"   Text="Flujo Trabajo:"> </asp:Label>
                            <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrFlujoIni" Width="100%"  OnSelectedIndexChanged="DrFlujoIni_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                            <asp:Label CssClass="cajaslabel" runat="server" ID="Label32" Width="100%"   Text="Ejecución:"> </asp:Label>
                            <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrEjecucion" Width="100%"  OnSelectedIndexChanged="DrEjecucion_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                            <asp:Label CssClass="cajaslabel" runat="server" ID="Label93" ToolTip="Parametros como Tabla:'Nombre Tabla';Tablaobj:'Nombre Tabla Objetos' separados por punto y coma, crean un Archivo Documental con los campos y datos de la propia Consulta. Puede generar alias para las columnas con el nombre que necesite en el destino."  Width="90%" Text="Parametros:"> </asp:Label>
                            <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtParametros" Width="100%" OnTextChanged="TxtParametros_TextChanged" OnClick="TxtParametros_Click" AutoPostBack="true"  ></asp:TextBox>

                            <asp:Label CssClass="cajaslabel" runat="server" ID="Label53" Width="100%"   Text="Plantillas:"> </asp:Label>
                            <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrProcesoPlantilla" Width="100%"  OnSelectedIndexChanged="DrProcesoPlantilla_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                            <asp:Label CssClass="cajaslabel" runat="server" ID="Label26" Width="96%"   Text="Procesos/Plantillas:"> </asp:Label>
                             <asp:ImageButton ID="ImgDelProcePlan" style="top:20px; "  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteProcesPlant_Click" ToolTip="Elimina la Plantilla seleccionada de la lista Procesos/Plantillas" Width="15px" Height="15px"></asp:ImageButton>
                             <div class="row">
                                <div class="col-lg-12">
                                     <asp:ListBox ID="ListPlantillas" style="height:70px;width:100%" AutoPostBack="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListPlantillas_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                     <asp:ListBox ID="ListPlantillasID" visible="false" style="height:70px;width:100%" AutoPostBack="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListPlantillas_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                             </div>

                            <asp:Label CssClass="cajaslabel" runat="server" ID="Label34" ToolTip="Recuerde que si la Consulta SQL contiene comillas simples, debe escaparlas introduciendolas 2 veces"  Width="90%" Text="Consulta SQL:"> </asp:Label>
                            <asp:TextBox ID="TxtZQuery"  Width="100%" Height="100%"  TextMode="MultiLine" Columns="50" Rows="3" runat="server"/>
                       
                                <br />
                            <div class="row">
                                 <div class="col-lg-1">
                                </div>
                                <div class="col-lg-2">
                                </div>
                                <div class="col-lg-3">
                                    <asp:Button runat="server" Enabled="false" ID="Button12" Visible="true" CssClass="btn btn-success btn-block" Width="100%" Text="Guardar" OnClick="btnGuardarProceso_Click"/>
                                    </div>
                                    <div class="col-lg-3">
                                    <asp:Button runat="server" Enabled="false" ID="Button13" Visible="true" CssClass="btn btn-warning btn-block" Width="100%" Text="Cancelar" OnClick="btnCancelarProceso_Click"/>
                                </div>
                                <div class="col-lg-3">
                                </div>
                            </div>
                        </div>
                </div>
            </div>

            <%--Fin Panel Procesos--%>

            <%--Panel Plantillas--%>
            <div class="col-lg-4" runat="server" id="MPlantilas">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                                <div class="col-lg-8" >
                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label22" Width="80%" style="font-size:18px; font-weight: bold;"   Text="Plantillas:"> </asp:Label>
                                 </div>
                                 <div class="col-lg-4">
                                     <asp:ImageButton ID="ImageCopiaPlant"  runat="server" ImageUrl="~/Images/copiar.png" OnClick="btnCopiaPlantilla_Click" ToolTip="Copia la Plantilla en pantalla y lo presenta pendiente de guardado" Width="30px" Height="30px"></asp:ImageButton>
                                     <asp:ImageButton ID="ImageButton6"  runat="server" ImageUrl="~/Images/newdoc.png" OnClick="btnCreaPlantilla_Click" ToolTip="Crear una nueva Plantilla" Width="25px" Height="30px"></asp:ImageButton>
                                     <asp:ImageButton ID="ImageButton7"  runat="server" ImageUrl="~/Images/editdoc.png" OnClick="btnEditPlantilla_Click" ToolTip="Edita la Plantilla seleccionada" Width="25px" Height="30px"></asp:ImageButton>
                                     <asp:ImageButton ID="ImageButton8"  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeletePlantilla_Click" ToolTip="Elimina la Plantilla seleccionada" Width="30px" Height="30px"></asp:ImageButton>
                                     <%--<asp:ImageButton ID="ImageButton9"  runat="server" ImageUrl="~/Images/allwhite.png" OnClick="btnAllPlantilla_Click" ToolTip="Muestra todas las Plantillas en la lista de selección" Width="30px" Height="30px"></asp:ImageButton>--%>
                                 </div>
                                <div class="col-lg-12" >
                                    <asp:DropDownList runat="server" CssClass="form-control" Width="100%" ID="DrPlantillas" OnSelectedIndexChanged="DrPlantillas_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                </div>
                            </div>
                        </div>

                     <div class="panel-body" visible="true" runat="server" id="Div2">                        
                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label27" Width="100%"   Text="Identificador:"> </asp:Label>
                        <asp:TextBox type="text" Enabled="false" CssClass="form-control" runat="server" ID="TxtNPlantilla" Width="100%"  Height="35px"  ></asp:TextBox>
                                    
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label29" Width="100%"   Text="Descripción:"> </asp:Label>
                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtDPlantilla" Width="100%" OnTextChanged="TexTosPlantillas_TextChanged" OnClick="TexTosPlantillas_Click" AutoPostBack="true" ></asp:TextBox>
                                    
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label31" Width="100%"   Text="Ruta Origen:"> </asp:Label>
                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtRutaEPlantilla" OnTextChanged="TexTosPlantillas_TextChanged" OnClick="TexTosPlantillas_Click" AutoPostBack="true" Width="100%"  ></asp:TextBox>

                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label33" Width="100%"   Text="Ruta Destino:"> </asp:Label>
                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtRutaSPlantilla" Width="100%" OnTextChanged="TexTosPlantillas_TextChanged" OnClick="TexTosPlantillas_Click" AutoPostBack="true">  ></asp:TextBox>

                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label35" Width="100%"   Text="Copiar Documento Original:"> </asp:Label>
                        <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrCopia" Width="100%"  OnSelectedIndexChanged="DrMarcadorPlanti_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label36" Width="100%"   Text="Firmar en PDF:"> </asp:Label>
                        <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrfinPDF" Width="100%"  OnSelectedIndexChanged="DrMarcadorPlanti_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label37" Width="100%"   Text="Ruta Origen Alternativa:"> </asp:Label>
                        <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrRutaAlternativa" Width="100%"  OnSelectedIndexChanged="DrMarcadorPlanti_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
 
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label55" Width="100%"   Text="Enviar a Impresora:"> </asp:Label>
                        <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrPrinter" Width="100%"  OnSelectedIndexChanged="DrMarcadoresPlantillas_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label54" Width="100%"   Text="Marcadores:"> </asp:Label>
                        <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrMarcadoresPlantillas" Width="100%"  OnSelectedIndexChanged="DrMarcadoresPlantillas_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label50" Width="96%"   Text="Plantilla/Marcadores:"> </asp:Label>
                        <asp:ImageButton ID="ImgeDelPlanMarc" style="top:20px; "  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeletePlantMarc_Click" ToolTip="Elimina el Marcador seleccionado de la lista Plantilla/Marcadores" Width="15px" Height="15px"></asp:ImageButton>
                       
                         <div class="row">
                            <div class="col-lg-12">
                                <asp:ListBox ID="ListPlantMarca" style="height:90px;width:100%" AutoPostBack="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListPlantMarca_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                <asp:ListBox ID="ListPlantMarcaID" visible="false" style="height:70px;width:100%" AutoPostBack="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListPlantMarca_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                         <br />
                        <div class="row">
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-3">
                                <asp:Button runat="server" Enabled="false" ID="Button11" Visible="true" CssClass="btn btn-success btn-block" Width="100%" Text="Guardar" OnClick="btnGuardarPlantilla_Click"/>
                                </div>
                                <div class="col-lg-3">
                                <asp:Button runat="server" Enabled="false" ID="Button14" Visible="true" CssClass="btn btn-warning btn-block" Width="100%" Text="Cancelar" OnClick="btnCancelarPlantilla_Click"/>
                            </div>
                            <div class="col-lg-3">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--fin Panel Plantillas--%>

            <%--Panel Marcadores--%>
            <div class="col-lg-4" runat="server" id="MMarcadores">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-lg-7" >
                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label38" Width="80%" style="font-size:18px; font-weight: bold;"   Text="Marcadores:"> </asp:Label>
                            </div>
                             <div class="col-lg-5">
                                 <asp:ImageButton ID="ImageTemplate"  runat="server" ImageUrl="~/Images/template.png" OnClick="btnCopiaTemplate_Click" ToolTip="Genera una Template Master como Formulario o Grid para presentar/recoger resultados por la interacción con un usuario" Width="30px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageCopiaMarcador"  runat="server" ImageUrl="~/Images/copiar.png" OnClick="btnCopiaMarcador_Click" ToolTip="Copia el Marcador en pantalla y lo presenta pendiente de guardado" Width="30px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton10"  runat="server" ImageUrl="~/Images/newdoc.png" OnClick="btnCreaMarcador_Click" ToolTip="Crear un nuevo Marcador" Width="25px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton11"  runat="server" ImageUrl="~/Images/editdoc.png" OnClick="btnEditMarcador_Click" ToolTip="Edita el Marcador seleccionado" Width="25px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton12"  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteMarcador_Click" ToolTip="Elimina el Marcador seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                 <%--<asp:ImageButton ID="ImageButton13"  runat="server" ImageUrl="~/Images/allwhite.png" OnClick="btnAllMarcador_Click" ToolTip="Muestra todos los Marcadores en la lista de selección" Width="30px" Height="30px"></asp:ImageButton>--%>
                             </div>
                            <div class="col-lg-12" >
                                <asp:DropDownList runat="server" CssClass="form-control" Width="100%" ID="DrMarcadores" OnSelectedIndexChanged="DrMarcadores_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                            </div>
                        </div>
                    </div>
                     <div class="panel-body" visible="true" runat="server" id="Div3">                        
                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label39" Width="100%"   Text="Identificador:"> </asp:Label>
                        <asp:TextBox type="text" Enabled="false" CssClass="form-control" runat="server" ID="TxtIdMarcador" Width="100%"  Height="35px"  ></asp:TextBox>
                                    
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label40" Width="100%"   Text="Descripción:"> </asp:Label>
                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtDMarcador" Width="100%" OnTextChanged="TexTosMarcador_TextChanged" OnClick="TexTosMarcador_Click" AutoPostBack="true"  ></asp:TextBox>

                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label46" Width="90%"   Text="Tipo Marcador:"> </asp:Label>
                        <asp:ImageButton ID="ImgDummie" Visible="false" Style="overflow:auto;"  runat="server" ImageUrl="~/Images/relacion.png" OnClick="btnAbreDummie_Click" ToolTip="Abre la ventana del orden de secuencias de Dummies" Width="25px" Height="25px"></asp:ImageButton>
                        <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrTipoMarcador" Width="100%"  OnSelectedIndexChanged="TexTosMarcador_TextChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                         <%-- <div class="row">
                            <div class="col-lg-6" >
                                <asp:Label CssClass="cajaslabel" runat="server" ID="Label41" Width="100%"   Text="Caracter Entrada:"> </asp:Label>
                                <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtMarEntrada" Width="100%"  ></asp:TextBox>
                            </div>
                             <div class="col-lg-6" >
                                <asp:Label CssClass="cajaslabel" runat="server" ID="Label42" Width="100%"   Text="Caracter Salida:"> </asp:Label>
                                <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtMarSalida" Width="100%"  ></asp:TextBox>
                            </div>
                        </div>--%>

                         <asp:Label CssClass="cajaslabel" runat="server" ID="Label41" Width="90%"   Text="Caracter Entrada:"> </asp:Label>
                         <asp:ImageButton ID="ImgEjecDummie" Visible="false" Style="overflow:auto;"  runat="server" ImageUrl="~/Images/play.jpg" OnClick="btnEjecutarMarcador_Click" ToolTip="Ejecuta el Dummie seleccionado" Width="25px" Height="25px"></asp:ImageButton>
                         <asp:ImageButton ID="ImgEditDummie" Visible="false" Style="overflow:auto;"  runat="server" ImageUrl="~/Images/lee.png" OnClick="btnEditDummieMarcador_Click" ToolTip="Edita el Dummie seleccionado" Width="25px" Height="25px"></asp:ImageButton>

                         
                         <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtMarEntrada" Width="100%" OnTextChanged="TexTosMarcador_TextChanged" OnClick="TexTosMarcador_Click" AutoPostBack="true"  ></asp:TextBox>
                         <asp:TextBox runat="server" Width="100%" Height="100%" TextMode="MultiLine" Columns="50" Rows="14" visible="false" CssClass="form-control" ID="TxtMarEntradaDumie" OnClick="TexTosEntradaDumie_Click" AutoPostBack="true"  ></asp:TextBox>

                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label42" Width="100%"   Text="Caracter Salida:"> </asp:Label>
                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtMarSalida" Width="100%"  OnTextChanged="TexTosMarcador_TextChanged" OnClick="TexTosMarcador_Click" AutoPostBack="true" ></asp:TextBox>

                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label43" Width="100%"   Text="En Páginas:"> </asp:Label>
                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="Txtpagina" Width="100%" OnTextChanged="TexTosMarcador_TextChanged" OnClick="TexTosMarcador_Click" AutoPostBack="true" ></asp:TextBox>

                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label44" Width="100%"   Text="Posiciones eje X:"> </asp:Label>
                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtejeX" Width="100%" OnTextChanged="TexTosMarcador_TextChanged" OnClick="TexTosMarcador_Click" AutoPostBack="true" ></asp:TextBox>

                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label45" Width="100%"   Text="Posiciones eje Y:"> </asp:Label>
                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtejeY" Width="100%" OnTextChanged="TexTosMarcador_TextChanged" OnClick="TexTosMarcador_Click" AutoPostBack="true" ></asp:TextBox>

                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label47" Width="100%"   Text="Posiciones rotación:"> </asp:Label>
                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtRotacion" Width="100%" OnTextChanged="TexTosMarcador_TextChanged" OnClick="TexTosMarcador_Click" AutoPostBack="true" ></asp:TextBox>

                         <asp:Label CssClass="cajaslabel" runat="server" ID="Label48" Width="100%"   Text="Sello/firma Empresa:"> </asp:Label>
                         <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TextSello" Width="100%"  ></asp:TextBox>

                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label49" Width="100%"   Text="Nivel Proceso:"> </asp:Label>
                        <asp:DropDownList runat="server" Enabled="false"  CssClass="form-control" ID="Drroot" Width="100%"  OnSelectedIndexChanged="TexTosMarcador_TextChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                            

<%--                         <div class="row">
                             <div class="col-lg-6">
                                 <asp:Label CssClass="cajaslabel" runat="server" ID="Label48" Width="100%" Text="Sello/firma Empresa:"> </asp:Label>
                                 <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrSello" Width="100%" OnSelectedIndexChanged="DrSello_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />

                             </div>
                             <div class="col-lg-6">
                                 <asp:Label CssClass="cajaslabel" runat="server" ID="Label49" Width="100%" Text="Nivel Proceso:"> </asp:Label>
                                 <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="Drroot" Width="100%" OnSelectedIndexChanged="Drroot_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                             </div>
                         </div>--%>
                         <br />
                        <div class="row">

                            </div>
                            <div class="col-lg-4">
                                 <asp:Button runat="server"  ID="Button24" Visible="false" CssClass="btn btn-info btn-block" Width="100%" Text="Ejecuta Dummie" OnClick="btnEjecutarMarcador_Click"/>
                                  <asp:Button runat="server" ID="Button25" Visible="false" CssClass="btn btn-success btn-block" Width="100%" Text="Edita Dummie" OnClick="btnEditDummieMarcador_Click"/>
                             </div>

                            <div class="col-lg-3">
                                <asp:Button runat="server" Enabled="false" ID="Button15" Visible="true" CssClass="btn btn-success btn-block" Width="100%" Text="Guardar" OnClick="btnGuardarMarcador_Click"/>
                                </div>
                                <div class="col-lg-3">
                                <asp:Button runat="server" Enabled="false" ID="Button16" Visible="true" CssClass="btn btn-warning btn-block" Width="100%" Text="Cancelar" OnClick="btnCancelarMarcador_Click"/>
                            </div>
                            <div class="col-lg-2">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--Fin Panel Marcadores--%>

<%--       <div id="DivDummie" visible="false" runat="server" class="panel-body">
   
           <div class="col-lg-4" runat="server" id="Div5">
               <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-7" >
                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label86" Width="100%" style="font-size:18px; font-weight: bold;"   Text="Tarea:"> </asp:Label>
                                 </div>
                                <div class="col-lg-5">
                                    <asp:ImageButton ID="ImageButton32"  runat="server" ImageUrl="~/Images/copiar.png" OnClick="btnCopiaProceso_Click" ToolTip="Copia el Proceso en pantalla y lo presenta pendiente de guardado" Width="30px" Height="30px"></asp:ImageButton>
                                     <asp:ImageButton ID="ImageButton33"  runat="server" ImageUrl="~/Images/newdoc.png" OnClick="btnCreaProceso_Click" ToolTip="Crear un nuevo  Proceso" Width="25px" Height="30px"></asp:ImageButton>
                                     <asp:ImageButton ID="ImageButton34"  runat="server" ImageUrl="~/Images/editdoc.png" OnClick="btnEditProceso_Click" ToolTip="Edita el Proceso seleccionado" Width="25px" Height="30px"></asp:ImageButton>
                                     <asp:ImageButton ID="ImageButton35"  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteProceso_Click" ToolTip="Elimina el Proceso seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                     <asp:ImageButton ID="ImageButton36"  runat="server" ImageUrl="~/Images/allwhite.png" OnClick="btnAllProceso_Click" ToolTip="Muestra todos los Procesos en la lista de selección" Width="30px" Height="30px"></asp:ImageButton>
                                 </div>
                                 <div class="col-lg-12" >
                                    <asp:DropDownList runat="server" CssClass="form-control" Width="100%" ID="DrDummies" OnSelectedIndexChanged="DrDummies_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                </div>                            
                            </div>
                        </div>
                    </div>
           </div>
           <div class="col-lg-4" runat="server" id="Div9">
                           <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-7" >
                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label87" Width="100%" style="font-size:18px; font-weight: bold;"   Text="Tarea:"> </asp:Label>
                                 </div>
                                <div class="col-lg-5">
                                    <asp:ImageButton ID="ImageButton37"  runat="server" ImageUrl="~/Images/copiar.png" OnClick="btnCopiaProceso_Click" ToolTip="Copia el Proceso en pantalla y lo presenta pendiente de guardado" Width="30px" Height="30px"></asp:ImageButton>
                                     <asp:ImageButton ID="ImageButton38"  runat="server" ImageUrl="~/Images/newdoc.png" OnClick="btnCreaProceso_Click" ToolTip="Crear un nuevo  Proceso" Width="25px" Height="30px"></asp:ImageButton>
                                     <asp:ImageButton ID="ImageButton39"  runat="server" ImageUrl="~/Images/editdoc.png" OnClick="btnEditProceso_Click" ToolTip="Edita el Proceso seleccionado" Width="25px" Height="30px"></asp:ImageButton>
                                     <asp:ImageButton ID="ImageButton40"  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteProceso_Click" ToolTip="Elimina el Proceso seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                     <asp:ImageButton ID="ImageButton41"  runat="server" ImageUrl="~/Images/allwhite.png" OnClick="btnAllProceso_Click" ToolTip="Muestra todos los Procesos en la lista de selección" Width="30px" Height="30px"></asp:ImageButton>
                                 </div>
                                 <div class="col-lg-12" >
                                    <asp:DropDownList runat="server" CssClass="form-control" Width="100%" ID="DropDownList3" OnSelectedIndexChanged="DrDummies_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                </div>                            
                            </div>
                        </div>
                    </div>
           </div>
            <div class="col-lg-4" runat="server" id="Div8">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-lg-7" >
                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label81" Width="100%" style="font-size:18px; font-weight: bold;"   Text="Procesos:"> </asp:Label>
                             </div>
                            <div class="col-lg-5">
                                <asp:ImageButton ID="ImageButton15"  runat="server" ImageUrl="~/Images/copiar.png" OnClick="btnCopiaProceso_Click" ToolTip="Copia el Proceso en pantalla y lo presenta pendiente de guardado" Width="30px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton28"  runat="server" ImageUrl="~/Images/newdoc.png" OnClick="btnCreaProceso_Click" ToolTip="Crear un nuevo  Proceso" Width="25px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton29"  runat="server" ImageUrl="~/Images/editdoc.png" OnClick="btnEditProceso_Click" ToolTip="Edita el Proceso seleccionado" Width="25px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton30"  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteProceso_Click" ToolTip="Elimina el Proceso seleccionado" Width="30px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton31"  runat="server" ImageUrl="~/Images/allwhite.png" OnClick="btnAllProceso_Click" ToolTip="Muestra todos los Procesos en la lista de selección" Width="30px" Height="30px"></asp:ImageButton>
                             </div>
                             <div class="col-lg-12" >
                                <asp:DropDownList runat="server" CssClass="form-control" Width="100%" ID="DropDownList2" OnSelectedIndexChanged="DrProcesos_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                            </div>                            
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>




                   <%--Asignar Tareas--%>
                <div class="row" runat="server" visible="false" id="divTarea">
                  <div class="col-lg-5 col-md-6" visible="true" runat="server" id="col6List1" >
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="row" >
                                    <div class="col-lg-5" >
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label81" Width="80%" style="font-size:18px; font-weight: bold;"  Text="Tareas Disponibles:"> </asp:Label>
                                        <asp:ImageButton ID="ImgDelTar" style="top:20px; "  runat="server" ImageUrl="~/Images/elimina.png" OnClick="BtEliminaDummie_Click" ToolTip="Elimina la Plantilla para Dummie seleccionada" Width="25px" Height="25px"></asp:ImageButton>

                                    </div>
                                    <div class="col-lg-4" >
                                        <asp:DropDownList runat="server" Visible="true" CssClass="form-control" Width="100%"  ID="DrDummie"  OnSelectedIndexChanged="DrDummie_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                        <asp:TextBox ID="TxtNewDummie" style="background-color:#bdecb6;" Visible="false" Width="100%" CssClass="form-control" runat="server"></asp:TextBox>    
                                    </div>
                                    <div class="col-lg-3" >
                                        <asp:Button ID="BtNewDummie" Enabled="true" CssClass="btn btn-info btn-block"  runat="server" Text="Nueva Plantilla" OnClick="btnNewDummie_Click" />
                                       <asp:Label CssClass="cajaslabel" type="text" visible="false" runat="server" ID="LbIDPlantDummie" Width="80%" style="font-size:18px; font-weight: bold;"  Text="Tareas Disponibles:"> </asp:Label>

                                    </div>
                                </div>
                            </div>
                           <div class="panel-body">
                               <div class="row">
                                  <div class="col-lg-10" >
                                    <asp:ListBox ID="ListBox1Tar" style="height:300px;width:100%" AutoPostBack="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBox1Tar_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                  <div class="col-lg-2" >
                                      <asp:ListBox ID="ListBox1IDTar" style="height:300px; width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBox1IDTar_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                              </div>
                          </div>
                        </div>
                    </div>

                    <div class="col-lg-2 col-md-6" visible="true" runat="server" id="col6Relacion" >
                            <div class="panel panel-default">
                                <div class="panel-heading" id="DivRel" visible="true" runat="server" style="font-size:18px; font-weight: bold;">
                                    Relaciones 
                                </div>
                                <div class="panel-heading" id="DivCancel" visible="false" runat="server" style="font-size:18px; font-weight: bold;">
                                    <asp:Button ID="BtCancelTarea" Enabled="true" CssClass="btn btn-warning btn-block"  runat="server" Text="Cancelar Plantilla" OnClick="btnCancelaTar_Click" />
                                </div>
                                 <div class="panel-heading" id="DivEdita" visible="false" runat="server" style="font-size:18px; font-weight: bold;">
                                     <asp:Button ID="BtEditTarea"  CssClass="btn btn-success btn-block"  runat="server" Text="Editar Tarea" OnClick="btnEditaTar_Click" />
                                </div>
                                <div class="panel-body">
                              <asp:Button ID="BtAsigTar" Enabled="false" CssClass="btn btn-info btn-block"  runat="server" Text="Asignar Tareas >>" OnClick="btnPasarSeleccionadosTar_Click" />
                              <asp:Button ID="BtDesAsigTat" Enabled="false" CssClass="btn btn-info btn-block" runat="server" Text="<< Desasignar Tareas" OnClick="btnRegresarSeleccionadosTar_Click" />

                              <asp:Button ID="BtlimpiaTar"  CssClass="btn btn-info btn-block" runat="server" Text="Nueva Tarea" OnClick="btnLimpiarTar_Click" />
                              <asp:Button ID="BtBuscaTar" Enabled="true" CssClass="btn btn-info btn-block"  runat="server" Text="Buscar" OnClick="btnBuscarTar_Click" />
                                    <br />
                              <asp:TextBox ID="TxtBuscaTar" Width="100%" CssClass="form-control" runat="server">Buscar...</asp:TextBox>    
                                    <br />
                              <asp:Button ID="BtsubeTar" Enabled="false" CssClass="btn btn-info btn-block"   runat="server" Text="Subir" OnClick="SubirTar_Click" />
                              <asp:Button ID="BtBajaTar" Enabled="false" CssClass="btn btn-info btn-block" runat="server"  Text="Bajar" OnClick="BajarTar_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-5 col-md-6" visible="true" runat="server" id="col6List2" >
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-lg-8" >
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label86" Width="100%" style="font-size:18px; font-weight: bold;"  Text="Tareas Asociadas:"> </asp:Label>
                                     </div>

                                    <div class="col-lg-4" >
                                        <asp:Button ID="Button26" Enabled="false"  CssClass="btn btn-success btn-block"    runat="server" Text="Asignar Plantilla Dummie" OnClick="GuardaTareas_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                  <div class="col-lg-10" >
                                    <asp:ListBox ID="ListBox2Tar" style="height:300px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBox2Tar_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                   <div class="col-lg-2" >
                                      <asp:ListBox ID="ListBox2IDTar" style="height:300px;width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBox2IDTar_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                  <asp:ListBox ID="ListBox9" style="height:300px;width:10%" Visible="false" CssClass="form-control" runat="server"  SelectionMode="Multiple"></asp:ListBox>
                                </div>
                             </div>
                        </div>
                    </div>
                </div>

                 <div class="row" runat="server" visible="false" id="divNePlantilla">
                     <div class="panel panel-default">
                         <div class="panel-heading">
                            <div class="row" >
                                <div class="col-lg-5" >
                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label90" Width="80%" style="font-size:18px; font-weight: bold;"  Text="Nueva Tarea:"> </asp:Label>
                                </div>
                                <div class="col-lg-4" >
                                </div>
                                <div class="col-lg-3" >
                                </div>
                            </div>
                        </div>
 
                        <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-6 col-md-6"  runat="server" id="DivColTarea" >
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <div class="row" >
                                            <div class="col-lg-6" >
                                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label87" Width="100%" style="font-size:18px; font-weight: bold;"  Text="ID Tarea:"> </asp:Label>
                                            </div>
                                            <div class="col-lg-6" >
                                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="LbIDTarea" Width="80%" style="font-size:18px; font-weight: bold;"  Text=""> </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-lg-12" >
                                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label91" Width="100%" style="font-size:18px; font-weight: bold;"  Text="Descripción de la Tarea:"> </asp:Label>
                                                <asp:TextBox type="text" CssClass="form-control" runat="server" ID="TxtDescTarea" Width="100%"  Height="100%" TextMode="MultiLine" Columns="50" Rows="8"  ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="col-lg-6 col-md-6"  runat="server" id="DivTextTarea" >
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <div class="row" >
                                            <div class="col-lg-12" >
                                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label88" Width="80%" style="font-size:18px; font-weight: bold;"  Text="Área de Trabajo/Dummie para esta Tarea:"> </asp:Label>
                                            </div>
                                            
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-lg-5" >
                                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label89" Width="80%" style="font-size:18px; font-weight: bold;"  Text="Asignar a Usuario:"> </asp:Label>
                                                <asp:ImageButton ID="ImgUserAdd" style="top:20px; "  runat="server" ImageUrl="~/Images/mas.gif" OnClick="BtSelUser_Click" ToolTip="Asignar a Usuario seleccionado" Width="25px" Height="25px"></asp:ImageButton>

                                            </div>
                                            <div class="col-lg-4" >
                                                <asp:DropDownList runat="server" Visible="true" CssClass="form-control" Width="100%"  ID="DrUsuarios"  OnSelectedIndexChanged="DrUsuarios_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                            </div>
                                            <div class="col-lg-3" >
                                                <asp:Button ID="Button28"  CssClass="btn btn-info btn-block"  runat="server" Text="Llamar a Dummie" OnClick="btnNewDummie_Click" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-5" >
                                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label92" Width="80%" style="font-size:18px; font-weight: bold;"  Text="Lista de Asignados:"> </asp:Label>
                                            </div>
                                            <div class="col-lg-4" >
                                                <asp:DropDownList runat="server" Visible="true" CssClass="form-control" Width="100%"  ID="DrAsigUser"  OnSelectedIndexChanged="DrAsigUsuarios_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                            </div>
                                            <div class="col-lg-3" >
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12" >
                                                <asp:TextBox type="text" CssClass="form-control" runat="server" ID="TxtDummieE" Width="100%"  Height="100%" TextMode="MultiLine" Columns="50" Rows="6"  ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4" >
                                </div>
                                <div class="col-lg-2" >
                                     <asp:Button ID="Button27" CssClass="btn btn-success btn-block"  runat="server" Text="Guardar Tarea" OnClick="btnSaveNewDummie_Click" />
                                </div>
                                 <div class="col-lg-2" >
                                     <asp:Button ID="Button29" CssClass="btn btn-warning btn-block"  runat="server" Text="Cancelar Tarea" OnClick="btnCancelNewDummie_Click" />
                                </div>
                                <div class="col-lg-4" >
                                </div>
                            </div>

                        </div>

                        <%--</div>--%>
                      <%--      <div class="row">
                                <div class="col-lg-4">
                                </div>
                                <div class="col-lg-2">
                                    <asp:Button runat="server"  Enabled="false" ID="BtGuardaRelTar" CssClass="btn btn-success btn-block" Width="100%" Text="Guardar" OnClick="btnGuardarRelacionTar_Click"/>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Button runat="server"  Enabled="false" ID="BtCanRelacionTar" CssClass="btn btn-warning btn-block" Width="100%" Text="Cancelar" OnClick="btnCancelarRelacionTar_Click"/>
                                </div>
                                <div class="col-lg-4">
                                </div>
                            </div>--%>
                      </div>
                </div >
                </div>
                <%--Fin Asignar Tareas--%>


















 
            <%--Panel Tree--%>
              <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-lg-12" >
                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label51" Width="80%" style="font-size:18px; font-weight: bold;"   Text="Configuración y Secuencia del Proceso:"> </asp:Label>
                            </div>
                        </div>
                    </div>
                     <div class="panel-body" visible="true" runat="server" id="Div4">    
                         <div class="col-lg-2">
                          </div>
  

                          <div id="DivTreeDoc" visible="true" runat="server" style=" height:180px;overflow:auto;" class="col-lg-8">
							<div class="panel-body">
								
									<asp:TreeView ID="TreeDoc" class="tabla1" runat="server" ImageSet="XPFileExplorer" NodeIndent="15" NodeStyle-CssClass="treeNode"
									RootNodeStyle-CssClass="rootNode" LeafNodeStyle-CssClass="leafNode" OnSelectedNodeChanged="TreeDoc_SelectedNodeChanged" >
										<HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
										<NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
											NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
										<ParentNodeStyle Font-Bold="False" />
										<SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
											VerticalPadding="0px" />
									</asp:TreeView> 

							</div>
						</div>



                          </div>
                          <div class="col-lg-2">
                          </div>
                    </div>
                </div>
            <%--Fin Panel Tree--%>

            <%--Panel cambio a Flujos--%>
             <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-10" >
                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label52" Width="80%" style="font-size:18px; font-weight: bold;"   Text="Volver a Flujos de Trabajo:"> </asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Button runat="server" Enabled="true" ID="Button10" Visible="true" CssClass="btn btn-warning btn-block" Width="100%" Text="Ir a Flujos" OnClick="btnRetroflujos_Click"/>
                              </div>
                        </div>
                    </div>
                </div>
             </div>
            <%--Panel cambio a Flujos--%>

    </div>

    <%--Panel general Procesos--%>

    <%--Panel general Templates--%>
       <div id="DivGralTemplates" visible="false" runat="server" class="panel-body">
            <%--Panel Procesos--%>
            <div class="col-lg-4" runat="server" id="DivTemplateCuestion">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-lg-6" >
                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label71" Width="100%" style="font-size:18px; font-weight: bold;"   Text="Templates Master:"> </asp:Label>
                             </div>
                            <div class="col-lg-6">
                                 <asp:ImageButton ID="ImageButton17"  runat="server" ImageUrl="~/Images/TemplateMaster.png" OnClick="btnTipoTemplateM_Click" ToolTip="Genera una Template Master como Formulario o Grid para presentar/recoger resultados por la interacción con un usuario" Width="30px" Height="30px"></asp:ImageButton>
                                <asp:ImageButton ID="ImageButton18"  runat="server" ImageUrl="~/Images/copiar.png" OnClick="btnCopiaTemplateM_Click" ToolTip="Copia la Template Master en pantalla y lo presenta pendiente de guardado" Width="30px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton19"  runat="server" ImageUrl="~/Images/newdoc.png" OnClick="btnCreaTemplateM_Click" ToolTip="Crear una nueva Template Master" Width="25px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton20"  runat="server" ImageUrl="~/Images/editdoc.png" OnClick="btnEditTemplateM_Click" ToolTip="Edita la Template Master seleccionada" Width="25px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton21"  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteTemplateM_Click" ToolTip="Elimina la Template Master seleccionada" Width="30px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton22"  runat="server" ImageUrl="~/Images/allwhite.png" OnClick="btnAllTemplateM_Click" ToolTip="Muestra todas las Templates Master en la lista de selección" Width="30px" Height="30px"></asp:ImageButton>
                             </div>
                             <div class="col-lg-12" >
                                <asp:DropDownList runat="server" CssClass="form-control" Width="100%" ID="DrTemplatesTemp" OnSelectedIndexChanged="DrTemplatesTemp_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                            </div>                            
                        </div>
                    </div>




                     <div class="panel-body" visible="true" runat="server" id="Div7"> 
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label72" Width="100%"   Text="Identificador:"> </asp:Label>
                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtIDtemplate" Width="100%" OnTextChanged="TxtIDtemplate_TextChanged" OnClick="TxTemplate_click" AutoPostBack="true"  ></asp:TextBox>
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label73" Width="100%"   Text="Descripción:"> </asp:Label> 
                         <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtDescrTemplate" Width="100%" OnTextChanged="TxtIDtemplate_TextChanged" OnClick="TxTemplate_click" AutoPostBack="true"  ></asp:TextBox>
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label74"  Width="100%"  Text="Seleccione Archivo Documental:"> </asp:Label>
                        <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrArchivTempla" Width="100%"  OnSelectedIndexChanged="DrArchivTempla_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label75" Width="96%"   Text="Archivos Documentales Asociados:"> </asp:Label>
                            <asp:ImageButton ID="ImageButton23" style="top:20px; "  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteArchivoTemplate_Click" ToolTip="Elimina el Archivo Documental seleccionado de la lista" Width="15px" Height="15px"></asp:ImageButton>
                        <div class="row">
                            <div class="col-lg-12">
                                    <asp:ListBox ID="ListTempla" Enabled="false" style="height:235px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" ondblclick="ListTemplaListBoxArchivo_DoubleClick"  SelectionMode="Multiple"></asp:ListBox>    
                                    <asp:ListBox ID="ListTemplaID" Visible="false" style="height:235px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" ondblclick="ListTemplaListBoxArchivo_DoubleClick"  SelectionMode="Multiple"></asp:ListBox>    

                            </div>

                        </div>
                        
                     </div>
                 </div>                            
              </div>

             <div class="col-lg-8" runat="server" id="DivCuestion">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-lg-7" >
                                <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label77" Width="100%" style="font-size:18px; font-weight: bold;"   Text="Preguntas/Cuestiones:"> </asp:Label>
                             </div>
                            <div class="col-lg-5">
                                <asp:ImageButton ID="ImageButton16"  runat="server" ImageUrl="~/Images/copiar.png" OnClick="btnCopiaCustion_Click" ToolTip="Copia la Template en pantalla y lo presenta pendiente de guardado" Width="30px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton24"  runat="server" ImageUrl="~/Images/newdoc.png" OnClick="btnCreaCustion_Click" ToolTip="Crear una nueva Template" Width="25px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton25"  runat="server" ImageUrl="~/Images/editdoc.png" OnClick="btnEditCustion_Click" ToolTip="Edita la Template seleccionada" Width="25px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton26"  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteCustion_Click" ToolTip="Elimina la Template seleccionada" Width="30px" Height="30px"></asp:ImageButton>
                                 <asp:ImageButton ID="ImageButton27"  runat="server" ImageUrl="~/Images/allwhite.png" OnClick="btnAllCustion_Click" ToolTip="Muestra todas las Templates en la lista de selección" Width="30px" Height="30px"></asp:ImageButton>
                             </div>
                             <div class="col-lg-12" >
                                <asp:DropDownList runat="server" CssClass="form-control" Width="100%" ID="DropDownList1" OnSelectedIndexChanged="DrCuestion_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                            </div>                            
                        </div>
                    </div>




                     <div class="panel-body" visible="true" runat="server" id="Div6"> 
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label78" Width="100%"   Text="Identificador:"> </asp:Label>
                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtIdCuestion" Width="100%" OnTextChanged="TxtIDcuestion_TextChanged" OnClick="TxIDCuestion_click" AutoPostBack="true"  ></asp:TextBox>
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label82" Width="100%"   Text="Titulo:"> </asp:Label> 
                        <asp:TextBox runat="server" Enabled="false"  Width="100%"  TextMode="MultiLine" Columns="50" Rows="2" CssClass="form-control" ID="TxttituloCuestion" OnTextChanged="TxIDCuestion_click" OnClick="TxTemplate_click" AutoPostBack="true"  ></asp:TextBox>
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label79" Width="100%"   Text="Descripción:"> </asp:Label> 
                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" TextMode="MultiLine" Columns="50" Rows="2" ID="TxtDescCuestion" Width="100%" OnTextChanged="TxIDCuestion_click" OnClick="TxTemplate_click" AutoPostBack="true"  ></asp:TextBox>
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label80"  Width="100%"  Text="Marcador:"> </asp:Label>
                        <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrMarcadorCuestion" Width="100%"  OnSelectedIndexChanged="DrMarcadorCuestion_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label83"  Width="100%"  Text="Archivo Documental:"> </asp:Label>
                        <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrArchiCuestion" Width="100%"  OnSelectedIndexChanged="DrMarcadorCuestion_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label84"  Width="100%"  Text="Flujo de Trabajo:"> </asp:Label>
                        <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrFlujoCuestion" Width="100%"  OnSelectedIndexChanged="DrMarcadorCuestion_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                        <asp:Label CssClass="cajaslabel" runat="server" ID="Label85"  Width="100%"  Text="Estado de Flujo:"> </asp:Label>
                        <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrEstadoCuestion" Width="100%"  OnSelectedIndexChanged="DrMarcadorCuestion_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                     </div>
                 </div>                            
              </div>

             <div class="col-lg-12" runat="server" id="DivLista">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-lg-12" >
                                <asp:GridView ID="gvLista"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                AllowSorting="true" OnSorting="gvLista_OnSorting"
                                CellPadding="4"  GridLines="None"  OnRowDataBound="gvLista_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="10" OnRowCommand="gvLista_RowCommand" DataKeyNames="ZID"
                                oonselectedindexchanged="gvLista_SelectedIndexChanged"  OnRowEditing="gvLista_RowEditing" OnRowCancelingEdit="gvLista_RowCancelingEdit" OnRowUpdating="gvLista_RowUpdating" 
                                onpageindexchanging="gvLista_PageIndexChanging" EnablePersistedSelection="True"  >
                            <RowStyle />                     
                                <Columns>
                                    <asp:TemplateField HeaderText="Ver"  SortExpression="ZID">
                                       <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                <itemtemplate>
                                                <a id="linkLee" href= "Download.ashx?file=images/cat1.jpg">
                                                    <asp:ImageButton ID="ibtveDoc" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/leer.png"         
                                                    CommandName="LveDoc" ToolTip="Apertura del Registro" Width="30px" Height="30px"></asp:ImageButton>
                                                </a>
                                            </ItemTemplate>
                                       <ItemStyle Height="8px"></ItemStyle>   
                                    </asp:TemplateField>
                                     


                                    <asp:TemplateField HeaderText="Selección Todos">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkb1" ToolTip="Selecciona o no todos los Documentos leídos por el Empleado." runat="server" OnCheckedChanged="sellectAll"
                                                AutoPostBack="true" />
                                        </HeaderTemplate>
                                            <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                            <itemtemplate>
                                                <asp:CheckBox ID="chbItem" runat="server"   />
                                            </itemtemplate>
                                            <itemstyle horizontalalign="center" />
                                        </asp:TemplateField>


									<asp:TemplateField HeaderText="Identificador" visible="true" SortExpression="ZID">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="Tabid"  runat="server" Text='<%# Eval("ZID") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="Labid"  runat="server" Text='<%# Bind("ZID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
               
                                    <asp:TemplateField HeaderText="Template"  SortExpression="ZID_TEMPLATE">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="Tabtemplate"  runat="server" Text='<%# Eval("ZID_TEMPLATE") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="Labtemplate"  runat="server" Text='<%# Bind("ZID_TEMPLATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha" SortExpression="ZFECHA">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="TabLFecha" runat="server" Text='<%# Eval("ZFECHA") %>' class="gridListDinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="LabFecha" runat="server" Text='<%# Bind("ZFECHA") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pregunta/Cuestion" SortExpression="ZID_CUESTION">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="TabCustion" runat="server" Text='<%# Eval("ZID_CUESTION") %>' class="gridListDinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="LabCuestion" runat="server" Text='<%# Bind("ZID_CUESTION") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Archivo" SortExpression="ZID_ARCHIVO">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="Tabarchivo" runat="server" Text='<%# Eval("ZID_ARCHIVO") %>' class="gridListDinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="Labarchivo" runat="server" Text='<%# Bind("ZID_ARCHIVO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Flujo" SortExpression="ZID_FLUJO">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="Tabflujo" runat="server" Text='<%# Eval("ZID_FLUJO") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="Labflujo" runat="server" Text='<%# Bind("ZID_FLUJO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>     
                                     <asp:TemplateField HeaderText="Estado" Visible="true" SortExpression="ZID_ESTADO">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="Tabestado" runat="server" Text='<%# Eval("ZID_ESTADO") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="Labestado" runat="server" Text='<%# Bind("ZID_ESTADO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>     

                                    <asp:TemplateField HeaderText="Registro" Visible="true" SortExpression="ZID_REGISTRO">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="Tabregistro" runat="server" Text='<%# Eval("ZID_REGISTRO") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="Labregistro" runat="server" Text='<%# Bind("ZID_REGISTRO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Dato" Visible="true" SortExpression="ZID_DATO">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="Tabdato" runat="server" Text='<%# Eval("ZID_DATO") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="Labdato" runat="server" Text='<%# Bind("ZID_DATO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                </Columns>
                                <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
                                <EditRowStyle BackColor="#eaf5dc" />   
                                <rowstyle Height="20px" />
                            </asp:GridView>


                           </div>
                        </div>
                    </div>





                 </div>                            
              </div>

           <%--Panel cambio a Flujos--%>
             <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-lg-10" >
                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label76" Width="80%" style="font-size:18px; font-weight: bold;"   Text="Volver a Procesos:"> </asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Button runat="server" Enabled="true" ID="Button23" Visible="true" CssClass="btn btn-warning btn-block" Width="100%" Text="Ir a Procesos" OnClick="btnRetroProcesos_Click"/>
                              </div>
                        </div>
                    </div>
                </div>
             </div>
            <%--Panel cambio a Flujos--%>
       </div>
    <%--Fin Panel general Templates--%>

</asp:Content>
