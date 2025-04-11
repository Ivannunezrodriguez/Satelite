<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ImportTabla.aspx.cs" Inherits="Satelite.ImportTabla" %>
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

                  	<div class="windowmessaje" visible="true" runat="server" id="windowmessaje">
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





<%--                <div runat="server" id="DvPreparado" visible="false"  style=" width: 30%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
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
                         <div class="col-lg-4">
                         </div>
                         <div class="col-lg-4">
                            <asp:Button runat="server" ID="Button9" Visible="true" tooltip="Aceptar" CssClass="btn btn-success btn-block" Width="100%"  Text="Aceptar" OnClick="checkSi_Click"/>                                                    
                         </div>
                         <div class="col-lg-4">
                          </div>
                     </div>
                </div>--%>
                  <br />
                  <br />
                  <div class="row" >
                    <div class="col-lg-12"  >
                        <div class="bs-example" id="MisMenus" runat="server">
                            <ul class="nav nav-tabs" style="margin-bottom: 15px;" >
                                <li id="Menu0" class="active" runat="server" ><asp:LinkButton ID="aMenu0" runat="server" OnClick="HtmlAnchor_Click" >IMPORTACIÓN DESDE TABLA</asp:LinkButton></li>                        
                                <li id="Menu1" class="" runat="server" ><asp:LinkButton ID="aMenu1" runat="server" OnClick="HtmlAnchor_Click" >IMPORTACIÓN DESDE ARCHIVO</asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>
                      <br />
                      <br />
                    <div class="col-lg-12"  >
                        <div class="col-lg-5" style="text-align:center">
                            <asp:Label  Font-Bold="true" style="font-size:20px; font-weight: bold; text-decoration: underline;" type="text" runat="server" ID="LbOrigen" Text="ORIGEN"> </asp:Label>
                         </div>
                        <div class="col-lg-2" style="text-align:center">
                            <div class="col-lg-1" style="text-align:center">
                            </div>
                            <div class="col-lg-10" style="text-align:center">
                                 <asp:Button ID="BTIrBBDD" visible="true" CssClass="btn btn-success btn-block" runat="server" Text="Ir a otras Bases de Datos" OnClick="btCambiaBBDD_Click" />
                                 <asp:Button ID="BTIrArchivo" visible="false" CssClass="btn btn-info btn-block" runat="server" Text="Ir a Archivos Satelite" OnClick="btCambiaBBDD_Click" />
                            </div>
                            <div class="col-lg-1" style="text-align:center">
                            </div>
                        </div>
                        <div class="col-lg-5" style="text-align:center">
                            <asp:Label  Font-Bold="true" style="font-size:20px; font-weight: bold; text-decoration: underline;" type="text" runat="server" ID="LbDestino" Text="DESTINO"> </asp:Label>
                         </div>
                    </div>
                      
                </div>
                  <br />
                  <br />

                  <div class="row" visible="true" runat="server" id="MenuTablas">
                      <%--Primero--%>
                         <div class="col-lg-12">
                             <%--Conexion a Base de Datos--%>
                               <div class="col-lg-1">
                                </div>
                                <div class="col-lg-1">
                                  <asp:Label  Font-Bold="true" type="text" runat="server" ID="Label19" Text="Conexión BBDD:"> </asp:Label>
                                </div>
                               <div class="col-lg-2">
                                    <asp:DropDownList runat="server"  CssClass="form-control" ID="DrConexion" Width="100%"  OnSelectedIndexChanged="dlConexion_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />                                
                               </div>
                             <div class="col-lg-1">
                                </div>

                                <div class="col-lg-2">
                               </div>
                             <%--Archivos Satelite--%>
                                <div class="col-lg-1">
                                  <asp:Label  Font-Bold="true" type="text" runat="server" ID="lbTipoBBDD" Text="Archivos:"> </asp:Label>
                                </div>
                               <div class="col-lg-2">
                                   <asp:DropDownList runat="server" Visible="true" CssClass="form-control"  ID="DrArchivos"  OnSelectedIndexChanged="DrArchivos_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                   <asp:DropDownList runat="server" Visible="false" CssClass="form-control"  ID="DrConexionDest"  OnSelectedIndexChanged="dlConexionDest_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                </div>
                             <div class="col-lg-2">
                                </div>
                        </div>
                      <br />
                      <%--Segundo--%>
                        <div class="col-lg-12">
                                <%--Tablas Conexion a Base de Datos--%>
                             <div class="col-lg-1">
                                </div>
                                <div class="col-lg-1">
                                  <asp:Label  Font-Bold="true" type="text" runat="server" ID="LbTablas" Text="Tablas:"> </asp:Label>
                                </div>
                               <div class="col-lg-2">
                                    <asp:DropDownList runat="server"  CssClass="form-control" ID="DrTablas" Width="100%"  OnSelectedIndexChanged="dlTablas_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />                                
                               </div>
                               <div class="col-lg-1">
                                </div>
                                <div class="col-lg-1">
                               </div>
                                <div class="col-lg-1">
                                </div>
                                <div class="col-lg-1">
                                    <asp:Label  Font-Bold="true" Visible="false" type="text" runat="server" ID="LbtablaDest" Text="Tablas:"> </asp:Label>
                                </div>
                               <div class="col-lg-2">
                                   <asp:DropDownList runat="server" Visible="false"  CssClass="form-control" ID="DrTablasDest" Width="100%"  OnSelectedIndexChanged="dlTablasDest_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />           
                                </div>
                             <div class="col-lg-2">
                                </div>
                        </div>
                      <br />
                      <br />
                    </div>


                  <div class="row" visible="false" runat="server" id="MenuArchivos">
                      <%--Primero--%>
                         <div class="col-lg-12">
                              <%--Fichero plano--%>
                                <div class="col-lg-1">
                                  <asp:Label  Font-Bold="true" type="text" runat="server" ID="Label12" Text="Seleccione fichero:"> </asp:Label>
                                </div>
                               <div class="col-lg-2">
                                    <asp:TextBox runat="server"  CssClass="form-control" ID="TxFile" Width="100%" Height="35px" ></asp:TextBox>                               
                               </div>
                               <div class="col-lg-1">
                                   
                                </div>
                                <div class="col-lg-1">
                                    
                                </div>
                                <div class="col-lg-1">
                                    
                               </div>
                                <div class="col-lg-3">
                                </div>
                              <%--Archivos Satelite--%>
                                <div class="col-lg-1">
                                  <asp:Label  Font-Bold="true" type="text" runat="server" ID="Label22" Text="Archivos:"> </asp:Label>
                                </div>
                               <div class="col-lg-2">
                                   <asp:DropDownList runat="server" CssClass="form-control"  ID="DrArchivos2"  OnSelectedIndexChanged="DrArchivos2_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                </div>
                        </div>
                      <%--Segundo--%>
                      <div class="col-lg-12">
                                <div class="col-lg-1">
                                  <asp:Label  Font-Bold="true" type="text" runat="server" ID="Label21" Text="Separador:"> </asp:Label>
                                </div>
                               <div class="col-lg-2">
                                    <asp:TextBox runat="server"  CssClass="form-control" ID="TxSeparador" Width="100%" Height="35px" ></asp:TextBox>                                
                               </div>
                               <div class="col-lg-1">
                                   <asp:Button ID="BtFile"  CssClass="btn btn-success btn-block" runat="server" Text="Abrir" OnClick="btBuscaFile_Click" />
                                </div>
                                <div class="col-lg-1">
                                </div>
                                <div class="col-lg-1">
                               </div>
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-1">
                                </div>
                               <div class="col-lg-2">
                                </div>
                        </div>
                      <br />
                      <br />
                    </div>

                  <div class="row">
                  <br />
                  <br />
                  </div>

                <div class="row">
                  <div class="col-lg-6 col-md-6" >
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
                          </div>
                        </div>
                       <div class="row">
                            <div class="col-lg-3" >
                            </div>
                            <div class="col-lg-6" >
                                <asp:Button ID="Button1" Enabled="false" CssClass="btn btn-success btn-block"  runat="server" Text="Asignar Campos origen" OnClick="btnPasarSeleccionados_Click" />
                            </div>
                            <div class="col-lg-3" >
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-6 col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="row" style="text-align:center;">
                                    <div class="col-lg-1" >
                                    <asp:CheckBox ID="chkKey" ToolTip="Este Campo se utilizará como Key única." runat="server" OnCheckedChanged="sellectKey" AutoPostBack="true" />
                                    </div>
                                    <div class="col-lg-2" >
                                         <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label15" Width="80%"   Text="Disponibles"> </asp:Label>
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
                                  <div class="col-lg-4" >
                                    <asp:ListBox ID="ListBox2" style="height:300px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBox2_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                  <div class="col-lg-3" >
                                      <asp:ListBox ID="ListBox2Col" style="height:300px;width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBox2Col_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                  <div class="col-lg-3" >
                                      <asp:ListBox ID="ListBox2Form" style="height:300px;width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBox2Col_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                   <div class="col-lg-2" >
                                      <asp:ListBox ID="ListBox2ID" style="height:300px;width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBox2ID_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                  <asp:ListBox ID="ListKeys" style="height:300px;width:10%" Visible="false" CssClass="form-control" runat="server"  SelectionMode="Multiple"></asp:ListBox>
                                </div>
                             </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-3" >
                            </div>
                            <div class="col-lg-6" >
                                <asp:Button ID="Button2" Enabled="false" CssClass="btn btn-success btn-block" runat="server" Text="Asignar Campos destino" OnClick="btnRegresarSeleccionados_Click" />
                            </div>
                            <div class="col-lg-3" >
                            </div>
                        </div>
                    </div>

                    <%--</div>--%>
                    </div >

                    
                     <br />
                <div class="row">
                  <div class="col-lg-5 col-md-6" >
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="row" style="text-align:center;">
                                    <div class="col-lg-2" >
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label10" Width="80%"   Text="Asignados"> </asp:Label>
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label20" Width="10%"   Text=""> </asp:Label>
                                    </div>
                                    <div class="col-lg-3" >
                                         <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="Label23" Width="100%"   Text="Campo:"> </asp:Label>
                                    </div>
                                    <div class="col-lg-2" >
                                        <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="Label24" Width="100%"   Text="ID:"> </asp:Label>
                                    </div>
                                    <div class="col-lg-2" >
                                        <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="Label25" Width="100%"   Text="Tipo:"> </asp:Label>
                                    </div>
                                    <div class="col-lg-3" >
                                        <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="Label26" Width="100%"   Text="Tamaño:"> </asp:Label>
                                    </div>
                                </div>
                            </div>
                           <div class="panel-body">
                               <div class="row">
                                  <div class="col-lg-7" >
                                    <asp:ListBox ID="ListBox3" style="height:300px;width:100%" AutoPostBack="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                    <div class="col-lg-3" >
                                      <asp:ListBox ID="ListBox3Form" style="height:300px; width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBox1Col_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                  <div class="col-lg-2" >
                                      <asp:ListBox ID="ListBox3ID" style="height:300px; width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBox1ID_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                              </div>
                          </div>
                        </div>
                    </div>

                    <div class="col-lg-2 col-md-6" >
                        <div class="panel panel-default">
                            <div class="panel-heading" id="Div1" runat="server">
                                Actuación 
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <asp:Button ID="Button4" Enabled="false" CssClass="btn btn-success btn-block" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-lg-6" >
                                        <asp:Button ID="Button6" Enabled="false" CssClass="btn btn-success btn-block"   runat="server" Text="Subir" OnClick="Subir_Click" />
                                   </div>
                                     <div class="col-lg-6" >
                                        <asp:Button ID="Button3" Enabled="false"  CssClass="btn btn-success btn-block"   runat="server" Text="Subir" OnClick="Subir_Click" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-6" >
                                         <asp:Button ID="Button7" Enabled="false"  CssClass="btn btn-success btn-block" runat="server"  Text="Bajar" OnClick="Bajar_Click" />
                                    </div>
                                    <div class="col-lg-6" >
                                        <asp:Button ID="Button5" Enabled="false"  CssClass="btn btn-success btn-block" runat="server"  Text="Bajar" OnClick="Bajar_Click" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <asp:Button ID="Button13" Enabled="false" CssClass="btn btn-success btn-block"  runat="server" Text="Probar" OnClick="btnPasarSeleccionados_Click" />
                                </div>
                                <br />
                                <div class="row">
                                    <asp:Button ID="Button14" Enabled="false" CssClass="btn btn-success btn-block" runat="server" Text="Migrar" OnClick="btnRegresarSeleccionados_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-5 col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="row" style="text-align:center;">
                                    <div class="col-lg-1" >
                                    <asp:CheckBox ID="CheckBox1" ToolTip="Este Campo se utilizará como Key única." runat="server" OnCheckedChanged="sellectKey" AutoPostBack="true" />
                                    </div>
                                    <div class="col-lg-2" >
                                         <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label27" Width="80%"   Text="Asignados"> </asp:Label>
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label28" Width="10%"   Text=""> </asp:Label>
                                     </div>
                                    <div class="col-lg-3" >
                                        <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="Label29" Width="100%"   Text="Campo:"> </asp:Label>
                                    </div>
                                    <div class="col-lg-2" >
                                        <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="Label30" Width="100%"   Text="ID:"> </asp:Label>
                                    </div>
                                    <div class="col-lg-1" >
                                        <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="Label31" Width="100%"   Text="Tipo:"> </asp:Label>
                                    </div>
                                    <div class="col-lg-3" >
                                        <asp:Label CssClass="cajaslabel" style="color:olivedrab;" type="text" runat="server" ID="Label32" Width="100%"   Text="Tamaño:"> </asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                  <div class="col-lg-7" >
                                    <asp:ListBox ID="ListBox4" style="height:300px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBox2_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                  <div class="col-lg-3" >
                                      <asp:ListBox ID="ListBox4Form" style="height:300px;width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBox2Col_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                   <div class="col-lg-2" >
                                      <asp:ListBox ID="ListBox4ID" style="height:300px;width:100%"  AutoPostBack="true"  Visible="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBox2ID_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                  </div>
                                </div>
                             </div>
                        </div>
                    </div>

                    <%--</div>--%>
                    </div >

            </div>






</asp:Content>
