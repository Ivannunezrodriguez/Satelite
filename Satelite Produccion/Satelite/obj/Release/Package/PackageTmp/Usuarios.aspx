<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Satelite.Usuarios" %>

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
                            <div class="col-lg-2" style="text-align:right">
                                <asp:Label  Font-Bold="true" type="text" runat="server" ID="Label10" Text="Selección:"> </asp:Label>
                            </div>
                            <div class="col-lg-4">
                                <asp:DropDownList runat="server" CssClass="form-control"  ID="DrArchivos"  OnSelectedIndexChanged="DrArchivos_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                            </div>
                             <div class="col-lg-3">
                                 <%--<asp:ImageButton ID="IbtAllFlujo"  runat="server" ImageUrl="~/Images/allwhite.png" OnClick="btnAllFlujo_Click" ToolTip="Muestra todos los Archivos documentales o tablas de Validación en la lista de selección" Width="30px" Height="30px"></asp:ImageButton>--%>
                            </div>
                            <div class="col-lg-2">
                            </div>
                        </div>
                     </div>
                    <br />


                    <%--Campos--%>
                    <div class="row">
                        <div class="col-lg-4" >
                            <div class="panel panel-default" style="height:530px;">
                               <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-12" >
                                            <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="lbTable" Width="80%" style="font-size:18px; font-weight: bold;"   Text="Tabla Seleccionada:"> </asp:Label>
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
										<asp:Label CssClass="cajaslabel" runat="server" ID="Lbtabla" Width="100%"   Text="Tabla:"> </asp:Label>
                                        <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxTabla" Width="100%"  ></asp:TextBox>
                                       <%-- <asp:Label CssClass="cajaslabel" runat="server" ID="Label1"  Width="100%"  Text="Registros:"> </asp:Label>
										<asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtRegistros" Width="100%"  ></asp:TextBox>--%>
										<asp:Label CssClass="cajaslabel" runat="server" ID="LbGrupo" Width="100%"   Text="Grupos:"> </asp:Label>
                                        <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrGrupo" Width="100%"  OnSelectedIndexChanged="dlGrupo_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="LbGrupos" Width="96%"   Text="Grupos asociados:"> </asp:Label>
                                         <asp:ImageButton ID="ImageButton1" style="top:20px; " Enabled="false"  runat="server" ImageUrl="~/Images/eliminaBlack.png" OnClick="btnDeleteGrupo_Click" ToolTip="Elimina el Grupo seleccionado de la lista a este usuario" Width="15px" Height="15px"></asp:ImageButton>
                                        <div class="row">
                                            <div class="col-lg-12">                                  
                                                 <asp:ListBox ID="ListBoxGrupo" Enabled="true" style="height:170px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBoxGrupo_SelectedIndexChanged"   SelectionMode="Multiple"></asp:ListBox>    
                                                 <asp:ListBox ID="ListBoxGrupoID" Visible="false" style="height:170px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="ListBoxGrupo_SelectedIndexChanged"  SelectionMode="Multiple"></asp:ListBox>    
                                            </div>

                                        </div>
<%--										<asp:Label CssClass="cajaslabel" runat="server" ID="LbRol" Width="100%"   Text="Roles:"> </asp:Label>
										<asp:DropDownList runat="server" Enabled="true" CssClass="form-control" ID="DrRol" Width="100%"  OnSelectedIndexChanged="DrRol_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="LbRoles" Width="96%"   Text="Roles asociados:"> </asp:Label>
                                         <asp:ImageButton ID="ImageButton2" style="top:20px; "  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteRol_Click" ToolTip="Elimina el Rol seleccionado de la lista a este usuario" Width="15px" Height="15px"></asp:ImageButton>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                 <asp:ListBox ID="ListBoxRol" Enabled="false" style="height:70px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" ondblclick="ListBoxrol_DoubleClick"  SelectionMode="Multiple"></asp:ListBox>    
                                                 <asp:ListBox ID="ListBoxRolID" Visible="false" style="height:70px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" ondblclick="ListBoxrol _DoubleClick"  SelectionMode="Multiple"></asp:ListBox>    

                                            </div>

                                        </div>--%>


                                        <%--<asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="DrConexion" Width="100%"  OnSelectedIndexChanged="dlConexion_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                        <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="LbGrupo" Width="96%"   Text="Grupos asociados:"> </asp:Label>
                                         <asp:ImageButton ID="ImageButton1" style="top:20px; "  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteArDoc_Click" ToolTip="Elimina el Archivo Documental seleccionado de la lista" Width="15px" Height="15px"></asp:ImageButton>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                 <asp:ListBox ID="ListBoxArchivo" Enabled="false" style="height:140px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" ondblclick="ListBoxArchivo_DoubleClick"  SelectionMode="Multiple"></asp:ListBox>    
                                                 <asp:ListBox ID="ListBoxArchivoID" Visible="false" style="height:140px;width:100%" AutoPostBack="true" CssClass="form-control" runat="server" ondblclick="ListBoxArchivo_DoubleClick"  SelectionMode="Multiple"></asp:ListBox>    

                                            </div>

                                        </div>--%>
                                       <%-- <div class="row" runat="server" Visible="false" ID="DivSQLProfile">
                                            <asp:Label CssClass="cajaslabel" runat="server" ID="Label16"  Width="90%" Text="Consulta SQL:"> </asp:Label>
                                            <asp:TextBox ID="TxtQueryProfile"  Width="100%" Height="100%"  TextMode="MultiLine" Columns="50" Rows="6" runat="server"/>
                                        </div>
                                        <br />--%>
                                        
                                    </div>
									<%--<div class="row">
                                        
                                        <div class="col-lg-1">
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Button runat="server" Enabled="false" ID="BtguardaFlujo" Visible="true" CssClass="btn btn-success btn-block" Width="100%" Text="Guardar" OnClick="btnGuardarFlujo_Click"/>
                                        </div>
										<div class="col-lg-2">
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Button runat="server" Enabled="false" ID="BtCancelFlujo" Visible="true" CssClass="btn btn-warning btn-block" Width="100%" Text="Cancelar" OnClick="btnCancelarFlujo_Click"/>
                                        </div>
                                        <div class="col-lg-1">
                                        </div>
                                    </div>--%>
                                <%--fin Flujos:--%>

                                  </div>
                            </div>
                         </div>

                        <%--Estado de Datos Tabla Validacion:--%>
                        <div class="col-lg-8"  style="height:420px;">
                            <div class="panel panel-default"  >
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-12" >
                                            <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label20" Width="80%" style="font-size:18px; font-weight: bold;"   Text="Usuario Seleccionado: "> </asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <%--Div General de la derecha  overflow-y: scroll;--%>
                                <div class="panel-body" visible="true" runat="server" id="DivCampoDer" style="height:410px;overflow-y: scroll;">   
                                    <div class="panel" visible="true" style="height:420px; " runat="server" id="DivEstados">
                                       <%--<div class="row">
                                            <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="Label2" Width="100%"   Text="Identificador:"> </asp:Label>
                                            <asp:TextBox type="text" Enabled="false" CssClass="form-control" runat="server" ID="TxtidEstado" Width="100%"  Height="35px"  ></asp:TextBox>
                                            <asp:Label CssClass="cajaslabel" runat="server" ID="Label4" Width="100%"   Text="Descripción:"> </asp:Label>
                                            <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="TxtEstado" Width="100%"  ></asp:TextBox>
                                        </div>--%>

										<div class="row" runat="server" id="DivReg0" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA0">
												<asp:TextBox id="lbL0" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL0" ToolTip="Identificador interno de este registro" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL0">
												<asp:DropDownList id="DrFindL0" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA0">
												<asp:TextBox id="TxL0" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL0" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB0">
												<asp:TextBox id="lbD0" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD0" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR0">
												<asp:DropDownList id="DrFindR0" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB0">
												<asp:TextBox id="TxD0" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR0" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg1" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA1">
												<asp:TextBox id="lbL1" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL1" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL1">
												<asp:DropDownList id="DrFindL1" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA1">
												<asp:TextBox id="TxL1" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL1" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB1">
												<asp:TextBox id="lbD1" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD1" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR1">
												<asp:DropDownList id="DrFindR1" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB1">
												<asp:TextBox id="TxD1" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR1" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg2" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA2">
												<asp:TextBox id="lbL2" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL2" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL2">
												<asp:DropDownList id="DrFindL2" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA2">
												<asp:TextBox id="TxL2" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL2" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB2">
												<asp:TextBox id="lbD2" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD2" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR2">
												<asp:DropDownList id="DrFindR2" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB2">
												<asp:TextBox id="TxD2" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR2" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg3" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA3">
												<asp:TextBox id="lbL3" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL3" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL3">
												<asp:DropDownList id="DrFindL3" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA3">
												<asp:TextBox id="TxL3" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL3" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB3">
												<asp:TextBox id="lbD3" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD3" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR3">
												<asp:DropDownList id="DrFindR3" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB3">
												<asp:TextBox id="TxD3" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR3" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg4" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA4">
												<asp:TextBox id="lbL4" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL4" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL4">
												<asp:DropDownList id="DrFindL4" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA4">
												<asp:TextBox id="TxL4" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL4" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB4">
												<asp:TextBox id="lbD4" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD4" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR4">
												<asp:DropDownList id="DrFindR4" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB4">
												<asp:TextBox id="TxD4" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR4" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg5" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA5">
												<asp:TextBox id="lbL5" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL5" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL5">
												<asp:DropDownList id="DrFindL5" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA5">
												<asp:TextBox id="TxL5" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL5" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB5">
												<asp:TextBox id="lbD5" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD5" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR5">
												<asp:DropDownList id="DrFindR5" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB5">
												<asp:TextBox id="TxD5" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR5" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg6" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA6">
												<asp:TextBox id="lbL6" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL6" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL6">
												<asp:DropDownList id="DrFindL6" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA6">
												<asp:TextBox id="TxL6" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL6" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB6">
												<asp:TextBox id="lbD6" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD6" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR6">
												<asp:DropDownList id="DrFindR6" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB6">
												<asp:TextBox id="TxD6" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR6" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg7" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA7">
												<asp:TextBox id="lbL7" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL7" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL7">
												<asp:DropDownList id="DrFindL7" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA7">
												<asp:TextBox id="TxL7" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL7" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB7">
												<asp:TextBox id="lbD7" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD7" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR7">
												<asp:DropDownList id="DrFindR7" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB7">
												<asp:TextBox id="TxD7" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR7" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg8" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA8">
												<asp:TextBox id="lbL8" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL8" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL8">
												<asp:DropDownList id="DrFindL8" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA8">
												<asp:TextBox id="TxL8" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL8" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB8">
												<asp:TextBox id="lbD8" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD8" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR8">
												<asp:DropDownList id="DrFindR8" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB8">
												<asp:TextBox id="TxD8" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR8" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg9" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA9">
												<asp:TextBox id="lbL9" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL9" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL9">
												<asp:DropDownList id="DrFindL9" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA9">
												<asp:TextBox id="TxL9" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL9" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB9">
												<asp:TextBox id="lbD9" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD9" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR9">
												<asp:DropDownList id="DrFindR9" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB9">
												<asp:TextBox id="TxD9" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR9" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg10" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA10">
												<asp:TextBox id="lbL10" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL10" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL10">
												<asp:DropDownList id="DrFindL10" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA10">
												<asp:TextBox id="TxL10" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL10" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB10">
												<asp:TextBox id="lbD10" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD10" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR10">
												<asp:DropDownList id="DrFindR10" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB10">
												<asp:TextBox id="TxD10" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR10" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg11" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA11">
												<asp:TextBox id="lbL11" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL11" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL11">
												<asp:DropDownList id="DrFindL11" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA11">
												<asp:TextBox id="TxL11" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL11" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB11">
												<asp:TextBox id="lbD11" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD11" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR11">
												<asp:DropDownList id="DrFindR11" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB11">
												<asp:TextBox id="TxD11" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR11" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg12" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA12">
												<asp:TextBox id="lbL12" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL12" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL12">
												<asp:DropDownList id="DrFindL12" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA12">
												<asp:TextBox id="TxL12" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL12" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB12">
												<asp:TextBox id="lbD12" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD12" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR12">
												<asp:DropDownList id="DrFindR12" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB12">
												<asp:TextBox id="TxD12" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR12" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg13" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA13">
												<asp:TextBox id="lbL13" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL13" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL13">
												<asp:DropDownList id="DrFindL13" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA13">
												<asp:TextBox id="TxL13" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL13" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB13">
												<asp:TextBox id="lbD13" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD13" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR13">
												<asp:DropDownList id="DrFindR13" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB13">
												<asp:TextBox id="TxD13" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR13" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg14" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA14">
												<asp:TextBox id="lbL14" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL14" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL14">
												<asp:DropDownList id="DrFindL14" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA14">
												<asp:TextBox id="TxL14" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL14" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB14">
												<asp:TextBox id="lbD14" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD14" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR14">
												<asp:DropDownList id="DrFindR14" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB14">
												<asp:TextBox id="TxD14" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR14" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg15" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA15">
												<asp:TextBox id="lbL15" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL15" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL15">
												<asp:DropDownList id="DrFindL15" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA15">
												<asp:TextBox id="TxL15" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL15" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB15">
												<asp:TextBox id="lbD15" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD15" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR15">
												<asp:DropDownList id="DrFindR15" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB15">
												<asp:TextBox id="TxD15" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR15" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg16" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA16">
												<asp:TextBox id="lbL16" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL16" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL16">
												<asp:DropDownList id="DrFindL16" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA16">
												<asp:TextBox id="TxL16" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL16" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB16">
												<asp:TextBox id="lbD16" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD16" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR16">
												<asp:DropDownList id="DrFindR16" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB16">
												<asp:TextBox id="TxD16" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR16" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg17" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA17">
												<asp:TextBox id="lbL17" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL17" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL17">
												<asp:DropDownList id="DrFindL17" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA17">
												<asp:TextBox id="TxL17" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL17" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB17">
												<asp:TextBox id="lbD17" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD17" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR17">
												<asp:DropDownList id="DrFindR17" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB17">
												<asp:TextBox id="TxD17" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR17" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg18" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA18">
												<asp:TextBox id="lbL18" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL18" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL18">
												<asp:DropDownList id="DrFindL18" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA18">
												<asp:TextBox id="TxL18" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL18" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB18">
												<asp:TextBox id="lbD18" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD18" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR18">
												<asp:DropDownList id="DrFindR18" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB18">
												<asp:TextBox id="TxD18" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR18" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg19" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA19">
												<asp:TextBox id="lbL19" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL19" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL19">
												<asp:DropDownList id="DrFindL19" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA19">
												<asp:TextBox id="TxL19" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL19" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB19">
												<asp:TextBox id="lbD19" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD19" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR19">
												<asp:DropDownList id="DrFindR19" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB19">
												<asp:TextBox id="TxD19" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR19" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg20" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA20">
												<asp:TextBox id="lbL20" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL20" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL20">
												<asp:DropDownList id="DrFindL20" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA20">
												<asp:TextBox id="TxL20" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL20" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB20">
												<asp:TextBox id="lbD20" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD20" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR20">
												<asp:DropDownList id="DrFindR20" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB20">
												<asp:TextBox id="TxD20" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR20" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg21" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA21">
												<asp:TextBox id="lbL21" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL21" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL21">
												<asp:DropDownList id="DrFindL21" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA21">
												<asp:TextBox id="TxL21" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL21" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB21">
												<asp:TextBox id="lbD21" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD21" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR21">
												<asp:DropDownList id="DrFindR21" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB21">
												<asp:TextBox id="TxD21" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR21" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg22" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA22">
												<asp:TextBox id="lbL22" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL22" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL22">
												<asp:DropDownList id="DrFindL22" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA22">
												<asp:TextBox id="TxL22" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL22" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB22">
												<asp:TextBox id="lbD22" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD22" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR22">
												<asp:DropDownList id="DrFindR22" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB22">
												<asp:TextBox id="TxD22" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR22" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg23" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA23">
												<asp:TextBox id="lbL23" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL23" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL23">
												<asp:DropDownList id="DrFindL23" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA23">
												<asp:TextBox id="TxL23" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL23" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB23">
												<asp:TextBox id="lbD23" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD23" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR23">
												<asp:DropDownList id="DrFindR23" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB23">
												<asp:TextBox id="TxD23" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR23" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg24" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA24">
												<asp:TextBox id="lbL24" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL24" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL24">
												<asp:DropDownList id="DrFindL24" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA24">
												<asp:TextBox id="TxL24" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL24" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB24">
												<asp:TextBox id="lbD24" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD24" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR24">
												<asp:DropDownList id="DrFindR24" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB24">
												<asp:TextBox id="TxD24" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR24" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg25" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA25">
												<asp:TextBox id="lbL25" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL25" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL25">
												<asp:DropDownList id="DrFindL25" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA25">
												<asp:TextBox id="TxL25" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL25" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB25">
												<asp:TextBox id="lbD25" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD25" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR25">
												<asp:DropDownList id="DrFindR25" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB25">
												<asp:TextBox id="TxD25" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR25" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg26" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA26">
												<asp:TextBox id="lbL26" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL26" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL26">
												<asp:DropDownList id="DrFindL26" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA26">
												<asp:TextBox id="TxL26" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL26" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB26">
												<asp:TextBox id="lbD26" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD26" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR26">
												<asp:DropDownList id="DrFindR26" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB26">
												<asp:TextBox id="TxD26" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR26" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg27" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA27">
												<asp:TextBox id="lbL27" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL27" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL27">
												<asp:DropDownList id="DrFindL27" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA27">
												<asp:TextBox id="TxL27" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL27" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB27">
												<asp:TextBox id="lbD27" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD27" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR27">
												<asp:DropDownList id="DrFindR27" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB27">
												<asp:TextBox id="TxD27" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR27" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg28" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA28">
												<asp:TextBox id="lbL28" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL28" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL28">
												<asp:DropDownList id="DrFindL28" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA28">
												<asp:TextBox id="TxL28" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL28" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB28">
												<asp:TextBox id="lbD28" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD28" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR28">
												<asp:DropDownList id="DrFindR28" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB28">
												<asp:TextBox id="TxD28" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR28" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg29" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA29">
												<asp:TextBox id="lbL29" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL29" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL29">
												<asp:DropDownList id="DrFindL29" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA29">
												<asp:TextBox id="TxL29" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL29" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB29">
												<asp:TextBox id="lbD29" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD29" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR29">
												<asp:DropDownList id="DrFindR29" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB29">
												<asp:TextBox id="TxD29" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR29" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg30" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA30">
												<asp:TextBox id="lbL30" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL30" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL30">
												<asp:DropDownList id="DrFindL30" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA30">
												<asp:TextBox id="TxL30" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL30" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB30">
												<asp:TextBox id="lbD30" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD30" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR30">
												<asp:DropDownList id="DrFindR30" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB30">
												<asp:TextBox id="TxD30" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR30" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg31" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA31">
												<asp:TextBox id="lbL31" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL31" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL31">
												<asp:DropDownList id="DrFindL31" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA31">
												<asp:TextBox id="TxL31" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL31" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB31">
												<asp:TextBox id="lbD31" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD31" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR31">
												<asp:DropDownList id="DrFindR31" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB31">
												<asp:TextBox id="TxD31" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR31" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg32" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA32">
												<asp:TextBox id="lbL32" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL32" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL32">
												<asp:DropDownList id="DrFindL32" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA32">
												<asp:TextBox id="TxL32" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL32" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB32">
												<asp:TextBox id="lbD32" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD32" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR32">
												<asp:DropDownList id="DrFindR32" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB32">
												<asp:TextBox id="TxD32" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR32" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg33" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA33">
												<asp:TextBox id="lbL33" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL33" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL33">
												<asp:DropDownList id="DrFindL33" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA33">
												<asp:TextBox id="TxL33" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL33" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB33">
												<asp:TextBox id="lbD33" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD33" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR33">
												<asp:DropDownList id="DrFindR33" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB33">
												<asp:TextBox id="TxD33" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR33" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg34" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA34">
												<asp:TextBox id="lbL34" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL34" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL34">
												<asp:DropDownList id="DrFindL34" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA34">
												<asp:TextBox id="TxL34" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL34" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB34">
												<asp:TextBox id="lbD34" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD34" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR34">
												<asp:DropDownList id="DrFindR34" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB34">
												<asp:TextBox id="TxD34" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR34" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg35" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA35">
												<asp:TextBox id="lbL35" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL35" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL35">
												<asp:DropDownList id="DrFindL35" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA35">
												<asp:TextBox id="TxL35" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL35" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB35">
												<asp:TextBox id="lbD35" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD35" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR35">
												<asp:DropDownList id="DrFindR35" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB35">
												<asp:TextBox id="TxD35" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR35" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg36" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA36">
												<asp:TextBox id="lbL36" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL36" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL36">
												<asp:DropDownList id="DrFindL36" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA36">
												<asp:TextBox id="TxL36" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL36" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB36">
												<asp:TextBox id="lbD36" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD36" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR36">
												<asp:DropDownList id="DrFindR36" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB36">
												<asp:TextBox id="TxD36" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR36" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg37" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA37">
												<asp:TextBox id="lbL37" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL37" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL37">
												<asp:DropDownList id="DrFindL37" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA37">
												<asp:TextBox id="TxL37" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL37" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB37">
												<asp:TextBox id="lbD37" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD37" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR37">
												<asp:DropDownList id="DrFindR37" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB37">
												<asp:TextBox id="TxD37" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR37" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg38" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA38">
												<asp:TextBox id="lbL38" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL38" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL38">
												<asp:DropDownList id="DrFindL38" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA38">
												<asp:TextBox id="TxL38" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL38" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB38">
												<asp:TextBox id="lbD38" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD38" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR38">
												<asp:DropDownList id="DrFindR38" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB38">
												<asp:TextBox id="TxD38" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR38" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg39" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA39">
												<asp:TextBox id="lbL39" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL39" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL39">
												<asp:DropDownList id="DrFindL39" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA39">
												<asp:TextBox id="TxL39" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL39" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB39">
												<asp:TextBox id="lbD39" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD39" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR39">
												<asp:DropDownList id="DrFindR39" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB39">
												<asp:TextBox id="TxD39" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR39" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg40" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA40">
												<asp:TextBox id="lbL40" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL40" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL40">
												<asp:DropDownList id="DrFindL40" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA40">
												<asp:TextBox id="TxL40" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL40" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB40">
												<asp:TextBox id="lbD40" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD40" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR40">
												<asp:DropDownList id="DrFindR40" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB40">
												<asp:TextBox id="TxD40" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR40" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg41" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA41">
												<asp:TextBox id="lbL41" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL41" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL41">
												<asp:DropDownList id="DrFindL41" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA41">
												<asp:TextBox id="TxL41" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL41" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB41">
												<asp:TextBox id="lbD41" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD41" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR41">
												<asp:DropDownList id="DrFindR41" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB41">
												<asp:TextBox id="TxD41" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR41" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg42" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA42">
												<asp:TextBox id="lbL42" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL42" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL42">
												<asp:DropDownList id="DrFindL42" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA42">
												<asp:TextBox id="TxL42" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL42" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB42">
												<asp:TextBox id="lbD42" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD42" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR42">
												<asp:DropDownList id="DrFindR42" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB42">
												<asp:TextBox id="TxD42" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR42" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg43" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA43">
												<asp:TextBox id="lbL43" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL43" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL43">
												<asp:DropDownList id="DrFindL43" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA43">
												<asp:TextBox id="TxL43" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL43" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB43">
												<asp:TextBox id="lbD43" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD43" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR43">
												<asp:DropDownList id="DrFindR43" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB43">
												<asp:TextBox id="TxD43" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR43" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg44" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA44">
												<asp:TextBox id="lbL44" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL44" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL44">
												<asp:DropDownList id="DrFindL44" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA44">
												<asp:TextBox id="TxL44" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL44" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB44">
												<asp:TextBox id="lbD44" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD44" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR44">
												<asp:DropDownList id="DrFindR44" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB44">
												<asp:TextBox id="TxD44" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR44" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg45" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA45">
												<asp:TextBox id="lbL45" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL45" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL45">
												<asp:DropDownList id="DrFindL45" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA45">
												<asp:TextBox id="TxL45" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL45" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB45">
												<asp:TextBox id="lbD45" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD45" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR45">
												<asp:DropDownList id="DrFindR45" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB45">
												<asp:TextBox id="TxD45" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR45" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg46" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA46">
												<asp:TextBox id="lbL46" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL46" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL46">
												<asp:DropDownList id="DrFindL46" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA46">
												<asp:TextBox id="TxL46" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL46" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB46">
												<asp:TextBox id="lbD46" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD46" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR46">
												<asp:DropDownList id="DrFindR46" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB46">
												<asp:TextBox id="TxD46" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR46" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg47" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA47">
												<asp:TextBox id="lbL47" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL47" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL47">
												<asp:DropDownList id="DrFindL47" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA47">
												<asp:TextBox id="TxL47" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL47" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB47">
												<asp:TextBox id="lbD47" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD47" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR47">
												<asp:DropDownList id="DrFindR47" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB47">
												<asp:TextBox id="TxD47" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR47" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg48" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA48">
												<asp:TextBox id="lbL48" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL48" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL48">
												<asp:DropDownList id="DrFindL48" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA48">
												<asp:TextBox id="TxL48" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL48" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB48">
												<asp:TextBox id="lbD48" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD48" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR48">
												<asp:DropDownList id="DrFindR48" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB48">
												<asp:TextBox id="TxD48" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR48" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg49" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA49">
												<asp:TextBox id="lbL49" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL49" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL49">
												<asp:DropDownList id="DrFindL49" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA49">
												<asp:TextBox id="TxL49" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL49" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB49">
												<asp:TextBox id="lbD49" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD49" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR49">
												<asp:DropDownList id="DrFindR49" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB49">
												<asp:TextBox id="TxD49" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR49" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>


										<div class="row" runat="server" id="DivReg50" visible="false">
											<div class="col-lg-2" runat="server" id="DivLabelA50">
												<asp:TextBox id="lbL50" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLL50" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindL50">
												<asp:DropDownList id="DrFindL50" runat="server" CssClass="form-control" Width ="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindL0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4" runat="server" id="DivTextA50">
												<asp:TextBox id="TxL50" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrL50" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
											<div class="col-lg-2" runat="server" id="DivLabelB50">
												<asp:TextBox id="lbD50" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:TextBox id="LBCOLD50" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
											</div>
											<div class="col-lg-1" runat="server" visible="false" id="DivFindR50">
												<asp:DropDownList id="DrFindR50" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrFindR0_SelectedIndexChanged" />
											</div>
											<div class="col-lg-4"  runat="server" id="DivTextB50">
												<asp:TextBox id="TxD50" visible="true" runat="server" style="width:100%" ReadOnly="true" class="form-control" ></asp:TextBox>
												<asp:DropDownList id="DrR50" visible="false" runat="server" CssClass="form-control" Width="100%" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrLs_SelectedIndexChanged" />
											</div>
										</div>



										<div class="row" id="DivEdicion" visible="false" runat="server">
											<br />
											<div class="col-lg-1">
											</div>
											<div class="col-lg-2">
											</div>
											<div class="col-lg-2">
												<button type="button" visible="true" id="BtnNewDato" style="width:100%;" runat="server" onServerClick="BtnNewDato_click" class="btn btn-success">Nuevos Datos</button>
												<button type="button" visible="false" id="BtnGuardaDato" style="width:100%;" runat="server" onServerClick="BtnGuardaDato_click" class="btn btn-primary">Guardar Datos</button>
											</div>
											<div class="col-lg-2" style="text-align:center;">
													<button id="btOpenFiles" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="btnOpenFiles_Click"><i title="Muestra los documentos asociados a este registro" class="fa fa-archive fa-3x"></i></button>
											</div>
											<div class="col-lg-2">
												<button type="button" visible="true" id="BtnModificaDato" style="width:100%;" runat="server" onServerClick="BtnModificaDato_Click" class="btn btn-success">Modificar Datos</button>
												<button type="button" visible="false" id="BtnCancelaDato" style="width:100%;" runat="server" onServerClick="BtnCancela_click" class="btn btn-warning">Cancelar Edición</button>
											</div>
											<div class="col-lg-1">
											</div>
											<div class="col-lg-2">
												<button type="button" visible="true" id="BtnEliminaDato" style="width:100%;" runat="server" onServerClick="BtnEliminaDato_click" class="btn btn-danger">Eliminar Datos</button>
											</div>
										</div>
										<br />
										<br />
										<div class="panel-heading" visible="false" runat="server" id="DivMenuTit">
											<div class="row" visible="true"  runat="server" id="DivMenudes">
												<div class="col-lg-6">
													<asp:Label CssClass="cajaslabel" visible="false" type="text" runat="server" ID="LbMenuAsig" Width="100%" style="font-size:18px; font-weight: bold;"   Text="Menú Grupo seleccionado: "> </asp:Label>
												</div>
												<div class="col-lg-6">
													<asp:Label CssClass="cajaslabel" visible="false" type="text" runat="server" ID="LbMenuJerar" Width="100%" style="font-size:18px; font-weight: bold;"   Text="Menú Usuario: "> </asp:Label>
												</div>
											</div>
										</div>
										<div class="row" visible="true"  style="height:340px;" runat="server" id="DivMenuSel">
											<div class="col-lg-6" runat="server" id="DivMenu">
												
											</div>
											<div class="col-lg-6" runat="server" id="DivMenuperm">

											</div>
										</div>
										<div class="row" id="DivLink" visible="false" runat="server">

											<div class="col-lg-1">
												 <a id="linkDown" href= "#">
													<asp:ImageButton ImageUrl="~/Images/hiperlink-blue.png"  runat="server"       
														ToolTip="Hipervinculo a una ruta o fichero en la Web" Width="30px" Height="30px"></asp:ImageButton>
												</a>
											</div>
											<div class="col-lg-11">
												 <asp:TextBox id="TxtLink" runat="server" Visible="true" style="width:100%;border-style:none;background-color:#ffffff;"  class="form-control" placeholder="Introduce una ruta o link de enlace"></asp:TextBox>
											</div>   
										</div>
									</div>     <!-- /.panel-body <i class="fa-duotone fa-block-question"></i> fa fa-archive fa-2x-->
                                
 


   
                                        
                                   </div>
										<div class="row">
                                            <div class="col-lg-3">
                                            </div>
                                            <div class="col-lg-1">
                                            </div>
                                            <div class="col-lg-2">
												<asp:Button runat="server" Enabled="true" ID="BtNuevo" Visible="true" CssClass="btn btn-info btn-block" Width="100%" Text="Nuevo" OnClick="btNuevo_Click"/>
												<asp:Button runat="server" Enabled="true" ID="BtGuarda" Visible="false" CssClass="btn btn-success btn-block" Width="100%" Text="Guardar" OnClick="btGuarda_Click"/>
											</div>
                                             <div class="col-lg-2">
												<asp:Button runat="server" Enabled="true" ID="BtEdita" Visible="true" CssClass="btn btn-success btn-block" Width="100%" Text="Editar" OnClick="btEdita_Click"/>
												<asp:Button runat="server" Enabled="true" ID="BtCancela" Visible="false" CssClass="btn btn-warning btn-block" Width="100%" Text="Cancelar" OnClick="btCancela_Click"/>
                                            </div>
											<div class="col-lg-2">
                                            </div>
                                             <div class="col-lg-2">
												<asp:Button runat="server" Enabled="true" ID="BtDelete" Visible="false" CssClass="btn btn-danger btn-block" Width="100%" Text="Eliminar" OnClick="btElimina_Click"/>

                                            </div>
                                        </div>
										<br />
										<br />
                                    <%--Fin el otro Div General templates de la derecha--%>
                                </div>
                                <%--Fin Div General de la derecha--%>
                            </div>
                            
                        </div>
                     </div>



        <div class="row" id="DivCampos0" runat="server">
			<div class="row">
				<div class="col-sm-2" >  
				<asp:Label  Font-Bold="true" type="text" style="color:olivedrab ;" runat="server" ID="Label2" Text="Registros en Tabla:"> </asp:Label>
			</div>
			<div class="col-sm-2" >  
				<asp:Label  Font-Bold="true" type="text" style="color:olivedrab ;" runat="server" ID="lbRowEntrada" Text="0"> </asp:Label>
			</div>
				<div class="col-sm-1" >
				</div>
				<div class="col-sm-1" >  
					<asp:Label  Font-Bold="true" type="text" runat="server" ID="Label4" Text="Filas en Lista:"> </asp:Label>
			</div>
				<div class="col-sm-2" >                              
				<asp:DropDownList ID="ddEntradaPageSize" style=" position:absolute;" CssClass="form-control" Width="90%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvEntrada_PageSize_Changed">  
				</asp:DropDownList>  
			</div>
			<div class="col-sm-4" >                              
			</div>
		</div>
			<br />
			<br />
        <asp:GridView ID="gvEntrada"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
        AllowSorting="true" OnSorting="gvEntrada_OnSorting"
        CellPadding="4"  GridLines="None"  OnRowDataBound="gvEntrada_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="10" OnRowCommand="gvEntrada_RowCommand" DataKeyNames="ZID"
        oonselectedindexchanged="gvEntrada_SelectedIndexChanged"  OnRowEditing="gvEntrada_RowEditing" OnRowCancelingEdit="gvEntrada_RowCancelingEdit" OnRowUpdating="gvEntrada_RowUpdating" 
        onpageindexchanging="gvEntrada_PageIndexChanging" EnablePersistedSelection="True"  >
        <RowStyle />                     
        <Columns>
            <%--<asp:TemplateField HeaderText="Ver"  SortExpression="ZID">
                <itemstyle horizontalalign="Center" verticalalign="Middle" />
                        <itemtemplate>
                        <a id="linkLee" href= "Download.ashx?file=images/cat1.jpg">
                            <asp:ImageButton ID="ibtveDoc" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/leer.png"         
                            CommandName="LveDoc" ToolTip="Apertura del Registro" Width="30px" Height="30px"></asp:ImageButton>
                        </a>
                    </ItemTemplate>
                <ItemStyle Height="8px"></ItemStyle>   
            </asp:TemplateField>
                                     
    
            <asp:TemplateField HeaderText="Descripción" visible="true"  SortExpression="ZDESCRIPCION">
                <EditItemTemplate>
                        <asp:TextBox ID="TabNombre"  runat="server" Text='<%# Eval("ZDESCRIPCION") %>' class="gridListAinput"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                        <asp:Label ID="LabNombre"  runat="server" Text='<%# Bind("ZDESCRIPCION") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>

            </Columns>
            <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
            <EditRowStyle BackColor="#eaf5dc" />   
            <rowstyle Height="20px" />
        </asp:GridView> 
        <br />
    </div>








            <%--</div>--%>



</asp:Content>
