<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="OrdenCarga.aspx.cs" Inherits="Satelite.OrdenCarga" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page_wrapper" runat="server" ><!-- /#page-wrapper  class="portada"-->

            <div class="windowmessaje" visible="false" runat="server" id="windowmessaje">
			<div class="contenedormessaje">
			    <div class="content-text">
        <%--          <div runat="server" id="DvPreparado" visible="false"  style=" width: 30%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">--%>
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
                         <div class="row" id="Decide" visible="false" runat="server">
                            <div class="col-lg-4">
                                <asp:Button runat="server" ID="Button2" Visible="true" tooltip="Sólo Elimina la línea seleccionada" CssClass="btn btn-success btn-block" Width="100%"  Text="Sólo Eliminar" OnClick="checkSiEl_Click"/>                                                    
                            </div>
                             <div class="col-lg-4">
                                <asp:Button runat="server" ID="Button4" Visible="true" tooltip="Elimina la línea seleccionada y corrige las posiciones en el camión" CssClass="btn btn-success btn-block" Width="100%"  Text="Eliminar y Corregir" OnClick="checkSiElC_Click"/>                 
                            </div>
                            <div class="col-lg-4">
                                <asp:Button runat="server" ID="Button3" Visible="true" tooltip="Cancela el procedimiento" CssClass="btn btn-success btn-block" Width="100%"  Text="Cancelar" OnClick="checkNo_Click"/>                 
                            </div>
                        </div>
                           <div class="row" id="Modifica" visible="false" runat="server">
                             <div class="col-lg-6">
                                <asp:Button runat="server" ID="Button6" Visible="true" tooltip="Guarda el registro y corrige las posiciones del camión" CssClass="btn btn-success btn-block" Width="100%"  Text="Aceptar" OnClick="checkSiMlC_Click"/>                 
                            </div>
                            <div class="col-lg-6">
                                <asp:Button runat="server" ID="Button7" Visible="true" tooltip="Cancela el procedimiento" CssClass="btn btn-success btn-block" Width="100%"  Text="Cancelar" OnClick="checkNoMlC_Click"/>                 
                            </div>
                        </div>
                         <div class="row" id="Asume" visible="false" runat="server">
                             <div class="col-lg-1">
                             </div>
                             <div class="col-lg-1">
                             </div>
                             <div class="col-lg-1">
                             </div>
                             <div class="col-lg-1">
                             </div>
                            <div class="col-lg-4" style="text-align:center;">
                                <asp:Button runat="server" ID="btnasume" Visible="true" tooltip="Aceptar" CssClass="btn btn-success btn-block" Width="100%"  Text="Aceptar" OnClick="checkOk_Click"/>
                            </div>
                             <div class="col-lg-1">
                             </div>
                             <div class="col-lg-1">
                             </div>
                             <div class="col-lg-1">
                             </div>
                             <div class="col-lg-1">
                             </div>
                        </div>
                    </div>
                </div>
            </div>

<%--                <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="Update">
                <ProgressTemplate>
                    <div class="centrado" style="z-index:1100;">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>



           <div runat="server" id="alerta" visible="false" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <i id="IAlert" runat="server" class="fa fa-exclamation-circle"></i>
                <asp:Label runat="server" class="alert alert-info alert-dismissable" ID="TextAlerta" BorderStyle="None" border="0" Width="100%" Text=""  />
           </div>
           <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Lberror"  Text="" > </asp:Label>


            <div class="row">
                  <div class="col-lg-12">
                        <div class="col-lg-8" >
                                <h3 id="H3Titulo" runat="server" visible="true"> Selección de pedidos <i class="fa fa-long-arrow-right"></i> <small> “Pedidos pendientes de cargar en camión ”  </small>
                                    <asp:Label  type="text"  style=" font-weight: bold; font-size:14px;"  runat="server" ID="Lbhost1"  Text="" > </asp:Label>
                                </h3>
                                <h3 id="H3Desarrollo" runat="server" style="color:red;" visible="false"> DESARROLLO --> Selección de pedidos <i class="fa fa-long-arrow-right"></i> <small> “Pedidos pendientes de cargar en camión ”  </small>
                                    <asp:Label  type="text"  style=" font-weight: bold; font-size:14px; color:black;"  runat="server" ID="Lbhost2"  Text="" > </asp:Label>
                                </h3>
                        </div>

                        <div id="VistaOrden" runat="server" visible ="true" class="col-lg-4">
                            <div runat="server" class="col-lg-1" style=" top:16px;">
                                    <input type="image"  class="pull-right text-muted " src="images/orden25x25.png" onserverclick="ImgOrdenMin_Click"  runat="server" id="ImgOrdenMin" style="border: 0px; " />    
                            </div>
                            <div  runat="server" class="col-lg-11" style=" top:16px;"> 
                                    <asp:DropDownList runat="server"  CssClass="form-control" style="position:relative; background-color:#ffffff; font-size:20px;"  Width="100%"  ID="DrOrdenMin" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrOrdenMin_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                    <asp:Button ID="Button1" runat="server" OnClick="check1_Click" Height="0" Width="0" BackColor="" CssClass="hidden" />
                            </div>
                        </div>
                        <div id="VistaOrdenNO" runat="server" visible ="true" class="col-lg-5">
                        </div>
                        
                           
                    </div>
              </div>
        <!-- /.row data-parent=titulo descripcion-->

            <div class="row">
              <div class="col-lg-12">
                 <div class="bs-example">
                    <ul class="nav nav-tabs" style="margin-bottom: 15px;">
                        <%--<li id="Menu1" class="active" ><a href="#accordion" id="aMenu1" runat="server" onclick="HtmlAnchor_Click()" data-toggle="tab">Selección de pedidos pendientes de cargar</a></li>--%>
                        
                        <li id="Menu1" class="active" runat="server" ><asp:LinkButton ID="aMenu1" runat="server" OnClick="HtmlAnchor_Click" >CABECERAS ÓRDENES</asp:LinkButton></li>
                        <li id="Menu2" class=""  runat="server" ><asp:LinkButton ID="aMenu2" runat="server" OnClick="HtmlAnchor_Click" >LISTA PEDIDOS PENDIENTES</asp:LinkButton></li>
                        <li id="Menu3" class=""  runat="server" ><asp:LinkButton ID="aMenu3" runat="server" OnClick="HtmlAnchor_Click" >LINEAS ORDEN</asp:LinkButton></li>
                        <li id="Menu4" class=""  runat="server" ><asp:LinkButton ID="aMenu4" Visible="false" runat="server" OnClick="HtmlAnchor_Click" >CARGA Y POSICION EN CAMION</asp:LinkButton></li>
                        <li id="Menu5" class=""  runat="server" ><asp:LinkButton ID="aMenu5" runat="server" OnClick="HtmlAnchor_Click" >INFORMES / IMPRESION</asp:LinkButton></li>

                        <li id="Menu6" class=""  runat="server" ><asp:LinkButton ID="aMenu6" runat="server" OnClick="HtmlAnchor_Click" >ALTA VARIEDADES:</asp:LinkButton></li>
                         
                        <%--<li id="Menu2" class=""  ><a href="#accordion2" id="aMenu2" runat="server" onclick="HtmlAnchor_Click()" data-toggle="tab">Listas de Carga</a></li>--%>               
                   </ul>
                </div>
             </div>
          </div>

           <div class="tab-pane fade active in" visible="true" runat="server" id="accordion">
                <div id="PanelGeneralCabecera" visible="true" runat="server" class="panel panel-default">
                    <div class="panel-heading" runat="server" id="PanelCabecera" >
                        <div class="row">
                             <div class="col-lg-6"> 
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" id="aTitulo" runat="server" style=" font-weight: bold;" onclick="submitit1()"  href="#collapse1"> ORDENES DE CARGA: Cabeceras Órdenes en tratamiento
                                        <%--<asp:Label  type="text"  Width="90%" style=" font-weight: bold; color:green;"  runat="server" ID="LbCabecera"  Text="" > </asp:Label>--%>                           
                                    </a>                            
                                </h4>
                            </div>
                            <div class="col-lg-2"> 
                             <asp:Label  type="text"  Width="100%" Visible="true" style=" font-weight: bold;text-align:center; color:green; padding:6px;"  runat="server" ID="LBCountLista"  Text="Contiene: 0 Líneas" > </asp:Label>
                            </div>
                            <div class="col-lg-1">
                                <%--<asp:Label  type="text"  Width="60%" style=" font-weight: bold; text-align:right; position:absolute;"  runat="server" ID="Label7"  Text="Orden: " > </asp:Label><i class="fa fa-long-arrow-right"></i>--%>
                                <input type="image"  class="pull-right text-muted " src="images/orden25x25.png" onserverclick="ImageOrden_Click"  runat="server" id="ImgOrden" style="border: 0px; " />    
                            </div>
                            <div class="col-lg-2"> 
                                <asp:DropDownList runat="server"  CssClass="form-control" style="position:relative;"  Width="100%"  ID="DrSelCab" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrSelCab_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                <asp:Button ID="btn1" runat="server" OnClick="check1_Click" Height="0" Width="0" CssClass="hidden" />
                            </div>
                            <div class="col-lg-1"> <%--onserverClick="PrintGridView"--%>
                                <button id="btPrintCabecera" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onclick="doPrintGRCabecera();"><i title="Imprime la vista previa presentada en la lista de Cabecera" class="fa fa-print fa-2x"></i></button>
                            </div>
                        </div>
                    </div>

                    <div id="collapse1" runat="server"  class="panel-collapse collapse in">
                        
                        <div class="panel panel-default">              
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-1"> 
                                    <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label8"  Text="Número:" > </asp:Label>
                                    <asp:TextBox runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="TxtNumero"  Font-Bold="True" />
                                    </div>
                                    <div class="col-lg-1"> 
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label9"  Text="Empresa:" > </asp:Label>
                                        <asp:DropDownList runat="server" CssClass="form-control" Visible="false"  Width="100%" tooltip="Seleccione un Tipo de Fecha desde para filtrar datos"  ID="DrEmpresa"  OnSelectedIndexChanged="DrEmpresa_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                        <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca un Tipo de Fecha desde para filtrar datos"  ID="TxtEmpresa"  Font-Bold="True" />
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label10"  Text="Pais:" > </asp:Label>
                                        <asp:DropDownList runat="server" CssClass="form-control" Visible="false" Width="100%" tooltip="Seleccione un Tipo de Fecha hasta para filtrar datos"  ID="DrPais"  OnSelectedIndexChanged="DrPais_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                        <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca un Tipo de Empresa para filtrar datos"  ID="TxtPais"  Font-Bold="True" />
                                    </div>
                                    <div class="col-lg-2"> 
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label7"  Text="Fecha preparación:" > </asp:Label>
                                        <asp:TextBox runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="TxtFechaPrepara"  Font-Bold="True" />
                                    </div>
                                    <div class="col-lg-2"> 
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label11"  Text="Fecha carga:" > </asp:Label>
                                        <asp:TextBox runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="TxtFecha"  Font-Bold="True" />
                                    </div>
                                    <div class="col-lg-1"> 
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label12"  Text="Teléfono:" > </asp:Label>
                                        <asp:TextBox runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="TxtTelefono"  Font-Bold="True" />
                                    </div>
                                    <div class="col-lg-1"> 
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label13"  Text="Matricula:" > </asp:Label>
                                        <asp:TextBox runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="TxtMatricula"  Font-Bold="True" />
                                    </div>
                                    <div class="col-lg-2"> 
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label14"  Text="Transportista:" > </asp:Label>
                                        <asp:DropDownList runat="server" CssClass="form-control" Visible="false"  Width="100%" tooltip="Seleccione un Tipo de Clientes para filtrar datos"  ID="DrTransportista"  OnSelectedIndexChanged="DrTransportista_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                        <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca un Tipo de Empresa para filtrar datos"  ID="TxtTransportista"  Font-Bold="True" />
                                    </div>                                            

                                    <div class="col-lg-1">                     
                                        <%--<asp:Button runat="server" ID="BtnuevaCabecera" Visible="true" tooltip="Crea una nueva lista de carga" CssClass="btn btn-success btn-block" Width="100%"  Text="Nuevo" OnClick="BtnuevaCabecera_Click"/>    fa-2x--%> 
                                        <br />
                                        <%--<button id="BtnNewCabecera" type="button"  visible="true" style="width:50%; border-style:none; background-color:transparent;"  runat="server" class="pull-center text-muted "  onserverClick="BtNewCabecera_Click"><i title="Crea una nueva orden de carga"  class="fa fa-plus-square fa-2x"></i></button>--%>
                                        <button id="BtnuevaCabecera" type="button"  visible="true" style="width:50%; border-style:none; background-color:transparent;"  runat="server" class="pull-center text-muted "  onserverClick="BtnuevaCabecera_Click"><i title="Guarda nueva orden de carga"  class="fa fa-plus-square fa-2x"></i></button>
                                        <button id="BtCancelCabecera" type="button" visible="false" style="width:50%; border-style:none; background-color:transparent;" runat="server" class="pull-center text-muted "  onserverClick="BtCancelCabecera_Click"><i title="Quita de selección la orden de carga seleccionada"  class="fa fa-minus-circle fa-2x"></i></button>
                                        <asp:ImageButton id="ImgEnvidos" visible="true" runat="server" ToolTip="Muestra elementos de selección ya creados en las Listas" ImageAlign="right" Width="30px" Height="30px" ImageUrl="images/pnegro.png" OnClick="checkListas_Click"/>
                                        <asp:ImageButton id="ImgNoEnvidos" visible="false" runat="server" ToolTip="Oculta los elementos de selección ya creados en las Listas" ImageAlign="right" Width="30px" Height="30px" ImageUrl="images/pazul.png" OnClick="checkListas_Click"/>

                                        <%--<label runat="server" visible="true" tooltip="Muestra los enviados a GoldenSoft"  id="LBCheck" class="switch pull-center">
                                            <input runat="server" id="chkOnOff" data-toggle='tooltip' data-original-title='Edita campos con listas' onclick="submitit()" type="checkbox"/><span class="slider round"></span>
                                            <asp:Button ID="btn" runat="server" OnClick="checkListas_Click" Height="0" Width="0" CssClass="hidden" />
                                        </label>--%>
                                        <%--<asp:Button runat="server" ID="btListaEmpresas" Visible="true" tooltip="Muestra todas las listas de Cabecera creadas" CssClass="btn btn-info btn-block" Width="100%"  Text="Buscar" OnClick="btListaEmpresas_Click"/>--%> 
                                        
                                        
                                    </div> 
                                    
                                </div>
                                <div class="row">
                                    <div class="col-lg-9">
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label17"  Text="Observaciones:" > </asp:Label>
                                        <asp:TextBox runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="TxtObservaciones"  Font-Bold="True" />
                                    </div>
                                     <div class="col-lg-2">
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label18"  Text="Estado:" > </asp:Label>
                                        <asp:TextBox runat="server" CssClass="form-control"  Width="100%" tooltip="Muestra el Estado de la Órden de Carga"  ID="TxtEstadoCab"  Font-Bold="True" />
                                    </div>
                                    <div class="col-lg-1"> 
                                        <br />
                                        <button id="btListaEmpresas" type="button" runat="server" style="width:50%; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="btListaEmpresas_Click"><i id="idgvCabecera" runat="server" title="Despliega u oculta la lista de Cabecera para pedidos"  class="fa fa-angle-up fa-2x"></i></button>
                                        <asp:ImageButton id="ImgAbiertos" visible="true" runat="server" ToolTip="Muestra las Órdenes de cabecera cerradas" ImageAlign="right" Width="30px" Height="30px" ImageUrl="images/pnegro.png" OnClick="checkCabeceraListas_Click"/>
                                        <asp:ImageButton id="ImgCerrados" visible="false" runat="server" ToolTip="Muestra las Órdenes de cabecera en tratamiento" ImageAlign="right" Width="30px" Height="30px" ImageUrl="images/pazul.png" OnClick="checkCabeceraListas_Click"/>

                                        <%--<label runat="server" visible="true" tooltip="Muestra las Órdenes de cabecera cerradas"  id="LbCaCheck" class="switch pull-center">
                                            <input runat="server" id="CaCheck" data-toggle='tooltip' data-original-title='Muestra las Órdenes de cabecera cerradas' onclick="submititCab()" type="checkbox"/><span class="slider round"></span>
                                            <asp:Button ID="btnCaCheck" runat="server" OnClick="checkCabeceraListas_Click" Height="0" Width="0" CssClass="hidden" />
                                        </label>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="panel-body" id="EmpresaGV" visible="true" runat="server">
                                 <div class="row">                                                                       
                                        <div class="col-sm-1" >
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label19"  Text="Filas:" > </asp:Label>
                                        </div>
                                        <div class="col-sm-1" >                              
                                           <asp:DropDownList ID="ddCabeceraPageSize" style=" position:absolute;" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvCabecera_PageSize_Changed">  
                                            </asp:DropDownList>  
                                         </div>
                                        <div class="col-sm-2" style="left:20px;" alig="right" >
                                            <asp:Button runat="server" ID="Btreviertelote" Visible="false" tooltip="Posiciona a estado Confirmado la orden de Cabecera seleccionada" CssClass="btn btn-success btn-block" Width="100%"  Text="Revertir orden" OnClick="Btreviertelote_Click"/>
                                        </div>
                                        <div class="col-sm-2" >
                                            <%--<asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label24"  Text="Ordenar por columna:" > </asp:Label>--%>
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowCabecera"  Text="" > </asp:Label>
                                        </div>

                                        
                                       <%--  <div class="col-sm-1" >
                                         </div>--%>
                                        <div class="col-sm-1" >
                                            <%--<asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label27"  Text="Código:" > </asp:Label>--%>
                                            <asp:linkbutton id="lbFilCodigo" ForeColor="Black" style=" font-weight: bold;text-align:right; top:10px; padding:6px;" onClick="lbFilClose_Click" runat="server" Text="Buscar:"></asp:linkbutton>
                                            <button id="BtnAsc" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnDesc_Click"><i title="Orden ascendente de la columna seleccionada" class="fa fa-sort-alpha-asc fa-2x"></i></button>
                                        </div>
                                        <div class="col-sm-2" >
                                            <%--<asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label27"  Text="Código:" > </asp:Label>--%>
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione campo para buscar datos"  ID="DrNombre"  OnSelectedIndexChanged="DrOrden1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />

                                         </div>
                                       <div class="col-sm-1" >
                                             <button id="BtnDesc" type="button" runat="server" visible="true" style=" position: absolute; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnAsc_Click"><i title="Orden descendente de la columna seleccionada" class="fa fa-sort-alpha-desc fa-2x"></i></button>
                                       </div>

                                        <div class="col-sm-1" >
                                            <asp:TextBox runat="server" CssClass="form-control" Visible="true" style="overflow:auto;"  Width="100%" tooltip="Seleccione un campo e introduzca un dato a buscar sobre esta casilla"  ID="TxtCodigo" Text=""  Font-Bold="True" /> 
                                        </div>
                                        <div class="col-sm-1" >
                                            <button id="BtBusca" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBusca_Click"><i id="Ibusca" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                                            <%--<asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione primer campo para ordenar datos"  ID="DrOrden1"  OnSelectedIndexChanged="DrOrden1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />--%>
                                        </div>
                                        
                                        <%--<div class="col-sm-1" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione segundo campo para ordenar datos"  ID="DrOrden2"  OnSelectedIndexChanged="DrOrden1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-1" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione tercer campo para ordenar datos"  ID="DrOrden3"  OnSelectedIndexChanged="DrOrden1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-1" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione cuarto campo para ordenar datos"  ID="DrOrden4"  OnSelectedIndexChanged="DrOrden1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-1" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione quinto campo para ordenar datos"  ID="DrOrden5"  OnSelectedIndexChanged="DrOrden1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-1" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione sexto campo para ordenar datos"  ID="DrOrden6"  OnSelectedIndexChanged="DrOrden1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-1" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione septimo campo para ordenar datos"  ID="DrOrden7"  OnSelectedIndexChanged="DrOrden1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>--%>

                                    </div>
                                 <div class="row">
                                    <div id="Div1" runat="server" style="overflow:auto;" class="panel-body"> <%-- AutoGenerateSelectButton="True"--%>  
                                        <asp:GridView ID="gvCabecera"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                            AllowSorting="true" OnSorting="gvCabecera_OnSorting"
                                            CellPadding="4"  GridLines="None"  OnRowDataBound="gvCabecera_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="10" OnRowCommand="gvCabecera_RowCommand" DataKeyNames="ID"
                                            oonselectedindexchanged="gvCabecera_SelectedIndexChanged"  OnRowEditing="gvCabecera_RowEditing" OnRowCancelingEdit="gvCabecera_RowCancelingEdit" OnRowUpdating="gvCabecera_RowUpdating" 
                                            OnRowDeleting="gvCabecera_RowDeleting" onpageindexchanging="gvCabecera_PageIndexChanging"  >
                                        <RowStyle />                     
                                            <Columns>

                                                <asp:CommandField ButtonType="Image" 
                                                 
                                                EditImageUrl="~/Images/editar20x20.png" 
                                                ShowEditButton="True" 
                                                CancelImageUrl="~/Images/cancelar20x20.png" 
                                                CancelText="" 
                                                DeleteText="" 
                                                UpdateImageUrl="~/Images/guardar18x18.png"          
                                                UpdateText="" />      
                                                                                                   
                                                <asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtSubeCab" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/sendup20x20.png"
                                                   CommandName="SubeCabecera" ToolTip="Selecciona orden de Cabecera para tramitar pedidos" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                                </asp:TemplateField>

                                                <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtUbicacion" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/IcubicacionBW20x20.png"
                                                   CommandName="Ubicacion" ToolTip="Localización GPRS del transporte" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                                </asp:TemplateField>


                                                <%--    A MODIFICAR EN CADA GRID PARA EVITAR LA MODIFICACIÓN DE ANCHO COLUMNA
                                                    <asp:TemplateField HeaderText="27" SortExpression="27">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="L27" runat="server" Text='<%# Eval("27") %>' CssClass="ejemplo-input"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="Label27" runat="server" Text='<%# Bind("27") %>'></asp:Label>
                                                    </ItemTemplate>
                                                        //CSS
                                                    .Gridinput{
                                                        width: 10px;
                                                        padding: 2px 4px;
                                                    }--%>
                                                <asp:TemplateField HeaderText="Número" SortExpression="NUMERO">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabNumero" runat="server" Text='<%# Eval("NUMERO") %>' class="gridCabAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabNumero" runat="server" Text='<%# Bind("NUMERO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Empresa" SortExpression="EMPRESA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabEmpresa" runat="server" Text='<%# Eval("EMPRESA") %>' class="gridCabAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabEmpresa" runat="server" Text='<%# Bind("EMPRESA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pais" SortExpression="PAIS">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabPais" runat="server" Text='<%# Eval("PAIS") %>' class="gridCabAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabPais" runat="server" Text='<%# Bind("PAIS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Preparación" SortExpression="FECHAPREPARACION" >
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabFechaPreparacion" runat="server" Text='<%# Eval("FECHAPREPARACION") %>' class="gridCabBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabFechaPreparacion" runat="server" Text='<%# Bind("FECHAPREPARACION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Carga" SortExpression="FECHACARGA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabFechaCarga" runat="server" Text='<%# Eval("FECHACARGA") %>' class="gridCabBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabFechaCarga" runat="server" Text='<%# Bind("FECHACARGA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Teléfono" SortExpression="TELEFONO">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabTelefono" runat="server" Text='<%# Eval("TELEFONO") %>' class="gridCabAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabTelefono" runat="server" Text='<%# Bind("TELEFONO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Matricula" SortExpression="MATRICULA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabMatricula" runat="server" Text='<%# Eval("MATRICULA") %>' class="gridCabAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabMatricula" runat="server" Text='<%# Bind("MATRICULA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transportista" SortExpression="TRANSPORTISTA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabTransportista" runat="server" Text='<%# Eval("TRANSPORTISTA") %>' class="gridCabAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabTransportista" runat="server" Text='<%# Bind("TRANSPORTISTA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Teléfono conductor" SortExpression="TELEFONO_USER">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabTconductor" runat="server" Text='<%# Eval("TELEFONO_USER") %>' class="gridCabBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabTconductor" runat="server" Text='<%# Bind("TELEFONO_USER") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Observaciones" SortExpression="OBSERVACIONES">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabObservaciones" Width="100%" runat="server" Text='<%# Eval("OBSERVACIONES") %>' class="gridCabBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabObservaciones" Width="100%" runat="server" Text='<%# Bind("OBSERVACIONES") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Modificación" SortExpression="ZSYSHORA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabZSYSDATE" Width="100%" runat="server" Text='<%# Eval("ZSYSHORA") %>' class="gridCabBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabZSYSDATE" Width="100%" runat="server" Text='<%# Bind("ZSYSHORA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <%--<asp:BoundField DataField="NUMERO" HeaderText="Número" />
                                                <asp:BoundField DataField="EMPRESA" HeaderText="Empresa" />
                                                <asp:BoundField DataField="PAIS" HeaderText="Pais" />
                                                <asp:BoundField DataField="FECHACARGA" HeaderText="Fecha Carga" />
                                                <asp:BoundField DataField="TELEFONO" HeaderText="Teléfono" />
                                                <asp:BoundField DataField="MATRICULA" HeaderText="Matricula" />
                                                <asp:BoundField DataField="TRANSPORTISTA" HeaderText="Transportista" /> 
                                                <asp:BoundField DataField="TELEFONO_USER" HeaderText="Teléfono conductor" />--%>
                                                <asp:BoundField DataField="LATITUD" HeaderText="Latitud" Visible="false" /> 
                                                <asp:BoundField DataField="LONGITUD" HeaderText="Longitud" Visible="false" /> 
                                                <%--<asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" />--%>

                                                <%--<asp:BoundField  HeaderText="Estado" />--%>
                                                <asp:TemplateField HeaderText="Estado" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("ZDESCRIPCION")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                       <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("ZDESCRIPCION")%>' Visible = "false" CssClass="Gridinput"></asp:Label>
                                                        <asp:DropDownList ID ="drDescripcionEstado" runat = "server" CssClass="Gridinput" >
                                                            <%--<asp:ListItem Value="-1" Text="Secuencia" />      height="26px--%>
                                                            <asp:ListItem Value="0" Text="Nuevo" />
                                                            <asp:ListItem Value="1" Text="En preparación" />
                                                            <asp:ListItem Value="2" Text="Confirmado" />
                                                            <asp:ListItem Value="3" Text="Cerrado" />
                                                            <asp:ListItem Value="4" Text="MODIFICADO" />
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                               </asp:TemplateField>
                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" Visible="false" />
                                                   
                                               <%--<asp:CommandField ShowEditButton="True" />--%>




                                                <%--<asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="drDescripcionEstado" HeaderText="Estado" runat="server">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ZDESCRIPCION" HeaderText="Estado" />--%>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
                                            <EditRowStyle BackColor="#eaf5dc" />                    
                                        </asp:GridView>                                  
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                 </div>            
           </div>    

            <div class="tab-pane fade active" visible="true" runat="server" id="accordion2">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" onclick="submitit6()" href="#collapse6"><i class="fa fa-long-arrow-right"></i> Carga Camión </a>
                            <asp:Button ID="btn6" runat="server" OnClick="check6_Click" Height="0" Width="0" CssClass="hidden" />
                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="LbPosicionCamion"  Text="Posición:" > </asp:Label>
                        </h4>
                    </div>
                    <br />           
                    <div id="collapse6" runat="server"   class="panel-collapse collapse in">                       
                        <%--<div class="panel panel-default">  align=center--%>               
                            <div class="panel-body">
                               <div class="row">
                                    <div class="col-lg-2" >
                                        <img src="images/leftCamion.png" alt="placeholder" style="border: 0px;" />
                                    </div>
                                   <div class="col-lg-10" >
                                        <div class="panel panel-default" style=" height: 100%;" >                                       
                                            <section id="container" runat="server" ondrop="drop(event)" ondragover="allowDrop(event)">
                                                <div class="padre" id="idPadre" runat="server" style="width: 100%; height: 100%;">
                                                    <div  id="fuego" class="tipo" runat="server"  align="left"  ondrop="drop(event)" ondragover="allowDrop(event)">Derecha

                                                    </div>
                                                    <div  id="agua" class="tipo"  runat="server" align="left"  ondrop="drop(event)" ondragover="allowDrop(event)">Izquierda

                                                    </div>
                                                    <%--<div class="tipo" id="fuego" ondrop="drop(event)" ondragover="allowDrop(event)"></div>
                                                    <div class="tipoB" id="agua" ondrop="drop(event)" ondragover="allowDrop(event)"></div>--%>
                                                </div>                                            
                                            </section>
                                        </div>
                                   </div>
                                   
                               </div>
                               
                                <div class="row">
                                    <div class="col-lg-2" >
                                        <%--<img src="images/traspaleta.png" runat="server" alt="placeholder" style="border: 0px;" />   onserverclick="ImageBtn_Click"--%>
                                        <input type="image" alt="placeholder" src="images/traspaletaempty.png"   runat="server" id="ImgU" style="border: 0px;" />
                                    </div>
                                    <div class="col-lg-10" >
                                        <div class="panel panel-default" style=" height: 100%; border-style:none;" >
                                            <section id="container2" runat="server" ondrop="drop(event)" ondragover="allowDrop(event)">
                                                <%--Palets align=center--%>                            
                                                <div class="contenedor"  draggable="true" onclick="submdrag0()" id="drag0" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
                                                        <img class="pokemon" id="Imgdrag0" runat="server" src="images/palet200X300.png" />
                                                        <div id="dragText0" runat="server" class="centrado">QR 21P322</div>
                                                        <asp:Button ID="Btndrag0" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" onclick="submdrag1()" id="drag1" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
                                                        <img class="pokemon" id="Imgdrag1" runat="server" src="images/palet200X300.png" />
                                                        <div id="dragText1" runat="server" class="centrado">QR 21P323</div>
                                                        <asp:Button ID="Btndrag1" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true"  onclick="submdrag2()" id="drag2" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
                                                        <img class="pokemon" id="Imgdrag2" runat="server" src="images/palet200X300.png" />
                                                        <div id="dragText2" runat="server" class="centrado">QR 21P324</div>
                                                        <asp:Button ID="Btndrag2" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag3" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
                                                    <img class="pokemon" id="Imgdrag3" runat="server" src="images/palet200X300.png" />
                                                    <div id="dragText3" runat="server" class="centrado">QR 21P325</div>
                                                    <asp:Button ID="Btndrag3" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag4" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
                                                    <img class="pokemon" id="Imgdrag4" runat="server" src="images/mediopalet200X300.png" />
                                                    <div id="dragText4" runat="server" class="centrado">QR 21P326</div>
                                                    <asp:Button ID="Btndrag4" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag5" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
                                                    <img class="pokemon" id="Imgdrag5" runat="server" src="images/palet200X300.png" />
                                                    <div id="dragText5" runat="server" class="centrado">QR 21P327</div>
                                                    <asp:Button ID="Btndrag5" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag6" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
                                                    <img class="pokemon" id="Imgdrag6" runat="server" src="images/palet200X300.png" />
                                                    <div id="dragText6" runat="server" class="centrado">QR 21P328</div>
                                                    <asp:Button ID="Btndrag6" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag7" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
                                                    <img class="pokemon" id="Imgdrag7" runat="server" src="images/palet200X300.png" />
                                                    <div id="dragText7" runat="server" class="centrado">QR 21P328</div>
                                                    <asp:Button ID="Btndrag7" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag8" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
                                                    <img class="pokemon" id="Imgdrag8" runat="server" src="images/palet200X300.png" />
                                                    <div id="dragText8" runat="server" class="centrado">QR 21P330</div>
                                                    <asp:Button ID="Btndrag8" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag9" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
                                                    <img class="pokemon" id="Imgdrag9" runat="server" src="images/palet200X300.png" />
                                                    <div id="dragText9" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag9" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag10" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
                                                    <img class="pokemon" id="Imgdrag10" runat="server" src="images/palet200X300.png" />
                                                    <div id="dragText10" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag10" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag11" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
                                                    <img class="pokemon" id="Imgdrag11" runat="server" src="images/palet200X300.png" />
                                                    <div id="dragText11" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag11" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag12" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
	                                                <img class="pokemon" id="Imgdrag12" runat="server" src="images/palet200X300.png" />
	                                                <div id="dragText12" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag12" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag13" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
	                                                <img class="pokemon" id="Imgdrag13" runat="server" src="images/palet200X300.png" />
	                                                <div id="dragText13" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag13" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag14" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
	                                                <img class="pokemon" id="Imgdrag14" runat="server" src="images/palet200X300.png" />
	                                                <div id="dragText14" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag14" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag15" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
	                                                <img class="pokemon" id="Imgdrag15" runat="server" src="images/palet200X300.png" />
	                                                <div id="dragText15" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag15" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag16" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
	                                                <img class="pokemon" id="Imgdrag16" runat="server" src="images/palet200X300.png" />
	                                                <div id="dragText16" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag16" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag17" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
	                                                <img class="pokemon" id="Imgdrag17" runat="server" src="images/palet200X300.png" />
	                                                <div id="dragText17" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag17" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag18" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
	                                                <img class="pokemon" id="Imgdrag18" runat="server" src="images/palet200X300.png" />
	                                                <div id="dragText18" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag18" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag19" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
	                                                <img class="pokemon" id="Imgdrag19" runat="server" src="images/palet200X300.png" />
	                                                <div id="dragText19" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag19" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag20" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
	                                                <img class="pokemon" id="Imgdrag20" runat="server" src="images/palet200X300.png" />
	                                                <div id="dragText20" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag20" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag21" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
	                                                <img class="pokemon" id="Imgdrag21" runat="server" src="images/palet200X300.png" />
	                                                <div id="dragText21" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag21" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag22" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
	                                                <img class="pokemon" id="Imgdrag22" runat="server" src="images/palet200X300.png" />
	                                                <div id="dragText22" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag22" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag23" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
	                                                <img class="pokemon" id="Imgdrag23" runat="server" src="images/palet200X300.png" />
	                                                <div id="dragText23" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag23" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>
                                                <div class="contenedor" draggable="true" id="drag24" runat="server" visible="false" ondragstart="drag(event)"  data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
	                                                <img class="pokemon" id="Imgdrag24" runat="server" src="images/palet200X300.png" />
	                                                <div id="dragText24" runat="server" class="centrado">QR 21P331</div>
                                                    <asp:Button ID="Btndrag24" runat="server" OnClick="MuevePalet_Click" Height="0" Width="0" CssClass="hidden" />
                                                </div>

                                            </section>
                                        </div>
                                    </div>
                                </div>
                                <!--</div> print-->
                       </div>
                        <%--</div>--%>
                    </div>
            
              </div><%--<div class="panel-group" id="accordion">--%>




            <div class="tab-pane fade active" visible="true" runat="server" id="accordion3">
                <div id="collapse2" runat="server"  class="panel-collapse collapse in">                     
                    <div class="panel panel-default">
                        <div class="panel-heading" runat="server" id="PanelOrden" >
                            <div class="row">
                                    <div class="col-lg-8"> 
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" style=" font-weight: bold;" onclick="submiti4()"  href="#collapse4"> SELECCION PEDIDOS A CARGAR: Lista pedidos pendientes</a>                                             
                                        </h4>
                                    </div>
                                    <div class="col-lg-1">
                                        <%--<asp:Label  type="text"  Width="60%" style=" font-weight: bold; text-align:right; position:absolute;"  runat="server" ID="Label18"  Text="Filtro: " > </asp:Label><i class="fa fa-long-arrow-right"></i>--%>
                                        <input type="image"  class="pull-right text-muted " src="images/filtro25x25.png" onserverclick="ImageFiltro_Click"  runat="server" id="imgFiltro" style="border: 0px; " />    
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:DropDownList runat="server"  CssClass="form-control"  Width="100%"  ID="DrSelectFiltro" tooltip="Contiene el filtro para las órdenes de pedidos" OnSelectedIndexChanged="DrSelectFiltro_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" /> 
                                    </div>
                                    <div class="col-lg-1"> <%--onserverClick="PrintGridView"--%>
                                        <button id="BtPrintOrden" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onclick="doPrintGRControl();"><i title="Imprime la vista previa presentada en la lista de órdenes de carga" class="fa fa-print fa-2x"></i></button>
                                    </div>
                            </div>
                            <asp:Button ID="btn4" runat="server" OnClick="check4_Click" Height="0" Width="0" CssClass="hidden" />
                            <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label15"  Text="" > </asp:Label>
                        </div>
                        <br />

                      <div id="PanelgeneralFiltro" runat="server" class="panel panel-default">
                        <div class="panel-heading" runat="server" id="PanelFiltros" >
                            <div class="row">
                                    <div class="col-lg-8"> 
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" style=" font-weight: bold;" onclick="submitit3()" href="#collapse3"> Filtros aplicados                                             
                                            <%--<asp:Label  type="text"  Width="90%" style=" font-weight: bold; color:green;"  runat="server" ID="LbFiltros"  Text="" > </asp:Label>--%>
                                        </a>
                                    </h4>
                                </div>
                                
                            </div>
                            <asp:Button ID="btn3" runat="server" OnClick="check3_Click" Height="0" Width="0" CssClass="hidden" />
                            <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label16"  Text="" > </asp:Label>
                        </div>
                        <br />

                        <div id="collapse3" runat="server"  class="panel-collapse collapse in">                               
                            <div class="panel panel-default">            
                                <div class="panel-body">
                                    <div class="row">
                                        <!-- consultas predefinidas -->
                                        <div class="col-lg-3"> 
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label1"  Text="Empresa:" > </asp:Label>
                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="DrConsultas"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-lg-2"> 
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label2"  Text="Fechas: Desde" > </asp:Label>
                                            <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Fecha desde para filtrar datos"  ID="DrDesde"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label5"  Text="Hasta" > </asp:Label>
                                            <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Fecha hasta para filtrar datos"  ID="DrHasta"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-lg-2"> 
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label3"  Text="Ruta Envio:" > </asp:Label>
                                            <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Ruta Envio para filtrar datos"  ID="DrRutas"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-lg-2"> 
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label4"  Text="Clientes:" > </asp:Label>
                                            <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Clientes para filtrar datos"  ID="DrCliente"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label6"  Text="" > </asp:Label>
                                            <button id="BtGralConsulta" type="button" runat="server" style="width:50%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtGralConsulta_Click"><i title="Consulta General con filtro o sin filtro según selección sobre la base de datos de GoldenSoft. Elimina la consulta actual visualizada en pantalla." class="fa fa-bars fa-2x"></i></button>
                                            <button id="Btfiltra" type="button" runat="server"  style="width:50%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="Btfiltra_Click"><i title="Consulta con filtro o sin filtro según selección, sobre los datos recuperados de la base de datos en local" class="fa fa-search fa-2x"></i></button>
                                        </div>                           
                                    </div>
                                    </div>
                                </div>
                            </div>
                    </div>
                        <div id="collapse4" runat="server"  class="panel-collapse collapse in">                               
                            <div class="panel panel-default">
                                <div class="panel-body" >
                                    <div class="row">
                                        <div class="col-sm-1" >
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label20"  Text="Filas:" > </asp:Label>
                                        </div>
                                        <div class="col-sm-1" >                              
                                           <asp:DropDownList ID="ddControlPageSize" CssClass="form-control" style=" position:absolute;" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvControl_PageSize_Changed">  
                                            </asp:DropDownList>
                                         </div>
                                        <div class="col-sm-1" >
                                        </div>
                                        <div class="col-sm-2" >
                                            <%--<asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label25"  Text="Ordenar por columna:" > </asp:Label>--%>
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="LbRowControl"  Text="" > </asp:Label>
                                        </div>
                                        <div class="col-sm-7" >
                                        </div>
                                        <%--<div class="col-sm-1" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione primer campo para ordenar datos"  ID="DrControl1"  OnSelectedIndexChanged="DrControl1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-1" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione segundo campo para ordenar datos"  ID="DrControl2"  OnSelectedIndexChanged="DrControl1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-1" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione tercer campo para ordenar datos"  ID="DrControl3"  OnSelectedIndexChanged="DrControl1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-1" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione cuarto campo para ordenar datos"  ID="DrControl4"  OnSelectedIndexChanged="DrControl1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-1" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione quinto campo para ordenar datos"  ID="DrControl5"  OnSelectedIndexChanged="DrControl1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-1" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione sexto campo para ordenar datos"  ID="DrControl6"  OnSelectedIndexChanged="DrControl1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-1" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione septimo campo para ordenar datos"  ID="DrControl7"  OnSelectedIndexChanged="DrControl1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>--%>
                                    </div>
                                    <!-- barra vertical no actualiza la posicion. style="height:300px; overflow-x:hidden; overflow-:auto;" -->
                                    <div class="row" >
                                        <div id="ContainDiv" runat="server" style="overflow:auto;" class="panel-body"> <%-- AutoGenerateSelectButton="True"--%>  
                                            <asp:GridView ID="gvControl"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                                AllowSorting="true" OnSorting="gvControl_OnSorting"
                                                CellPadding="4"  GridLines="None"  OnRowDataBound="gvControl_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="10" OnRowCommand="gvControl_RowCommand" DataKeyNames="ID"
                                                oonselectedindexchanged="gvControl_SelectedIndexChanged"  OnRowEditing="gvControl_RowEditing" OnRowCancelingEdit="gvControl_RowCancelingEdit" OnRowUpdating="gvControl_RowUpdating" 
                                                onpageindexchanging="gvControl_PageIndexChanging" >
                                            <RowStyle />       
                                                <Columns>

                                                <asp:CommandField ButtonType="Image" 
                                                    EditImageUrl="~/Images/editar20x20.png" 
                                                    ShowEditButton="True" 
                                                    CancelImageUrl="~/Images/cancelar20x20.png" 
                                                    CancelText="" 
                                                    DeleteText="" 
                                                    UpdateImageUrl="~/Images/guardar18x18.png"          
                                                    UpdateText="" 
                                                    />             
                                                    
                                                <asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtbajaOrden" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/sendDown20x20.png"
                                                   CommandName="BajaOrden" ToolTip="Envía la línea desglosada en Palets a listas de carga" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Empresa" SortExpression="EMPRESA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOEmpresa" runat="server" Text='<%# Eval("EMPRESA") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOEmpresa" runat="server" Text='<%# Bind("EMPRESA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cliente" SortExpression="CLIENTEPROVEEDOR">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOProveedor" runat="server" Text='<%# Eval("CLIENTEPROVEEDOR") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOProveedor" runat="server" Text='<%# Bind("CLIENTEPROVEEDOR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Nombre Fiscal" SortExpression="NOMBREFISCAL">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOFiscal" runat="server" Text='<%# Eval("NOMBREFISCAL") %>' class="gridContBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOFiscal" runat="server" Text='<%# Bind("NOMBREFISCAL") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Pedido" SortExpression="NUMERO">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabONumero" runat="server" Text='<%# Eval("NUMERO") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabONumero" runat="server" Text='<%# Bind("NUMERO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Linea" SortExpression="LINEA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOLinea" runat="server" Text='<%# Eval("LINEA") %>' class="gridContCinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOLinea" runat="server" Text='<%# Bind("LINEA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Articulo" SortExpression="ARTICULO">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOArticulo" runat="server" Text='<%# Eval("ARTICULO") %>' class="gridContBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOArticulo" runat="server" Text='<%# Bind("ARTICULO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Ruta" SortExpression="RUTA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabORuta" runat="server" Text='<%# Eval("RUTA") %>' class="gridContCinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabORuta" runat="server" Text='<%# Bind("RUTA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Fecha Entrega" SortExpression="FECHAENTREGA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOEntrega" runat="server" Text='<%# Eval("FECHAENTREGA") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOEntrega" runat="server" Text='<%# Bind("FECHAENTREGA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Uds Pedidas" SortExpression="UDSPEDIDAS">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOUdsPedidas" runat="server" Text='<%# Eval("UDSPEDIDAS") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOUdsPedidas" runat="server" Text='<%# Bind("UDSPEDIDAS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Uds Servidas" SortExpression="UDSSERVIDAS">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOUdsServidas" runat="server" Text='<%# Eval("UDSSERVIDAS") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOUdsServidas" runat="server" Text='<%# Bind("UDSSERVIDAS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Uds en Carga" SortExpression="UDSENCARGA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOUdsCarga" runat="server" Text='<%# Eval("UDSENCARGA") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOUdsCarga" runat="server" Text='<%# Bind("UDSENCARGA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Uds Pendientes" SortExpression="UDSPENDIENTES">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOPendientes" runat="server" Text='<%# Eval("UDSPENDIENTES") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOPendientes" runat="server" Text='<%# Bind("UDSPENDIENTES") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cajas/palet" SortExpression="CAJAS">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOTipo" runat="server" Text='<%# Eval("CAJAS") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOTipo" runat="server"  Text='<%# Bind("CAJAS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unidades Tipo" SortExpression="UNIDADES">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOCajas" runat="server" Text='<%# Eval("UNIDADES") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOCajas" runat="server"  Text='<%# Bind("UNIDADES") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Num. Palet" SortExpression="NUMPALET">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOPalet" runat="server" Text='<%# Eval("NUMPALET") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOPalet" runat="server" Text='<%# Bind("NUMPALET") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Uds a Cargar" SortExpression="UDSACARGAR">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOCargar" runat="server" Text='<%# Eval("UDSACARGAR") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOCargar" runat="server" Text='<%# Bind("UDSACARGAR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 
                                                 <asp:TemplateField HeaderText="Id Cabecera" SortExpression="ID_CABECERA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOCabecera" runat="server" Text='<%# Eval("ID_CABECERA") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOCabecera" runat="server" Text='<%# Bind("ID_CABECERA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Estado" visible="false" SortExpression="ESTADO">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOEstado" runat="server" Text='<%# Eval("ESTADO") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOEstado" runat="server"  Text='<%# Bind("ESTADO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Serie Pedido" visible="false" SortExpression="SERIE_PED">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOSerie" runat="server" Text='<%# Eval("SERIE_PED") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOSerie" runat="server"  Text='<%# Bind("SERIE_PED") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <%--<asp:BoundField DataField="EMPRESA" HeaderText="Empresa" />
                                                <asp:BoundField DataField="CLIENTEPROVEEDOR" HeaderText="Cliente Proveedor" />
                                                <asp:BoundField DataField="NOMBREFISCAL" HeaderText="Nombre Fiscal" />
                                                <asp:BoundField DataField="NUMERO" HeaderText="Numero" />
                                                <asp:BoundField DataField="LINEA" HeaderText="Linea" />
                                                <asp:BoundField DataField="ARTICULO" HeaderText="Articulo" />
                                                <asp:BoundField DataField="RUTA" HeaderText="Ruta" />
                                                <asp:BoundField DataField="FECHAENTREGA" HeaderText="Fecha Entrega" />
                                                <asp:BoundField DataField="UDSPEDIDAS" HeaderText="Uds Pedidas" />
                                                <asp:BoundField DataField="UDSSERVIDAS" HeaderText="Uds Servidas" />
                                                <asp:BoundField DataField="UDSENCARGA" HeaderText="Uds en Carga" />
                                                <asp:BoundField DataField="UDSPENDIENTES" HeaderText="Uds Pendientes" />
                                                <asp:BoundField DataField="UDSACARGAR" HeaderText="Uds a Cargar" />
                                                <asp:BoundField DataField="NUMPALET" HeaderText="Num. Palet" />
                                                <asp:BoundField DataField="ID_CABECERA" HeaderText="Id Cabecera" />
                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" Visible="false" />--%>
                                                <%--<asp:BoundField DataField="ID" HeaderText="ID" Visible="false"/>--%>
                                                <%--<asp:BoundField DataField="FECHAENTREGA" HeaderText="Fecha estimada entrega" />--%>
                    
                                                </Columns>
                                                <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
                                                <EditRowStyle BackColor="#f2eed5" />
                                                <%--<ItemStyle Wrap="False" Width="48px"></ItemStyle>
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#062d5c" Font-Bold="True" ForeColor="White" />
                    
                                                <AlternatingRowStyle BackColor="White" />--%>
                       
                                            </asp:GridView>                                  
                                        </div>
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="tab-pane fade active" visible="true" runat="server" id="accordion4">
                <div class="panel panel-default">                     
                <div class="panel-heading" runat="server" id="PanelListas" >
                    <div class="row">
                            <div class="col-lg-9"> 
                            <h4 class="panel-title">
                                <a data-toggle="collapse" style="font-weight: bold;" onclick="submitit5()"  href="#collapse5"> ORDENES DE CARGA: Líneas Orden </a>                                            
                            </h4>
                        </div>
                        <div class="col-lg-1"> 
                            <input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/etiquetaQR25x25.png" onserverclick="ImageBtn_Click"  runat="server" id="ImgTodo" style="border: 0px; " />
                        </div>
                        <div class="col-lg-1" id="DVtLista" runat="server" visible="true"> <%--onserverClick="PrintGridView"--%>
                            <button id="BtPrintReport" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-file-text-o fa-2x"></i>
                            </button>
                        </div>
                        <div class="col-lg-1" id="DVtListaOff" runat="server" visible="false"> <%--onserverClick="PrintGridView"--%>
                            <button id="BtPrintReportOff" type="button" runat="server" visible="true" style="width:50%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReportOff_Click">
                                <i title="Cierra la vista previa de reportes" class="fa fa-power-off fa-2x"></i>
                            </button>
                        </div>


                        <div class="col-lg-1"> <%--onserverClick="PrintGridView"--%>
                            <button id="BtPrintListas" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onclick="doPrintGRLista();"><i title="Imprime la vista previa presentada en la listas de carga camión" class="fa fa-print fa-2x"></i></button>
                        </div>
                        
                    </div>
                    <asp:Button ID="btn5" runat="server" OnClick="check5_Click" Height="0" Width="0" CssClass="hidden" />
                    <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label23"  Text="" > </asp:Label>
                </div>
                <div id="collapse5" runat="server"  class="panel-collapse collapse in">                 
                    <div class="panel-body">
                        <div class="row" id="PNFiltrosLista" runat="server" visible="true">
                            <div class="col-sm-1" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label21"  Text="Filas:" > </asp:Label>
                            </div>
                            <div class="col-sm-1" >                              
                                <asp:DropDownList ID="ddListaPageSize" CssClass="form-control" Width="100%" style=" position:absolute;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvLista_PageSize_Changed">  
                                <asp:ListItem Text="5" Value="5" />    
                                </asp:DropDownList>
                             </div>
                            <div class="col-sm-1" >
                            </div>
                            <div class="col-sm-2" >
                                <%--<asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label26"  Text="Ordenar por columna:" > </asp:Label>--%>
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="LbRowLista"  Text="" > </asp:Label>
                            </div>
                            <div class="col-sm-4" >
                            </div>
                            <div class="col-sm-3" >
                                <asp:CheckBox ID="chkbUdCarga" ToolTip="Imprime la lista de Carga que se presenta en pantalla sin operar con la tabla Envases (ZBANDEJAS)" Text=" Imprime lista de carga presentada en pantalla" runat="server" OnCheckedChanged="sellectAllEmpleado"
				                AutoPostBack="true" />
                            </div>
                            <%--<div class="col-sm-1" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione primer campo para ordenar datos"  ID="DrLista1"  OnSelectedIndexChanged="DrLista1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione segundo campo para ordenar datos"  ID="DrLista2"  OnSelectedIndexChanged="DrLista1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione tercer campo para ordenar datos"  ID="DrLista3"  OnSelectedIndexChanged="DrLista1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione cuarto campo para ordenar datos"  ID="DrLista4"  OnSelectedIndexChanged="DrLista1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione quinto campo para ordenar datos"  ID="DrLista5"  OnSelectedIndexChanged="DrLista1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione sexto campo para ordenar datos"  ID="DrLista6"  OnSelectedIndexChanged="DrLista1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione septimo campo para ordenar datos"  ID="DrLista7"  OnSelectedIndexChanged="DrLista1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>--%>
                        </div>
                        <div class="row" id="PNGridLista" runat="server" visible="true">
                            <div id="ContainDiv2" runat="server" style="overflow:auto;" class="panel-body"> <%-- AutoGenerateSelectButton="True"--%>                                  
                                <asp:GridView ID="gvLista"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                    AllowSorting="true" OnSorting="gvLista_OnSorting"
                                    CellPadding="4"  GridLines="None"  OnRowDataBound="gvLista_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="10" OnRowCommand="gvLista_RowCommand" DataKeyNames="ID"
                                    oonselectedindexchanged="gvLista_SelectedIndexChanged"  OnRowEditing="gvLista_RowEditing" OnRowCancelingEdit="gvLista_RowCancelingEdit" OnRowUpdating="gvLista_RowUpdating" 
                                    OnRowDeleting="gvLista_RowDeleting" onpageindexchanging="gvLista_PageIndexChanging" EnablePersistedSelection="True"  >
                                <RowStyle />                     
                                    <Columns>

                                     <asp:CommandField ButtonType="Image" 
                                                    EditImageUrl="~/Images/editar20x20.png" 
                                                    ShowEditButton="True" 
                                                    CancelImageUrl="~/Images/cancelar20x20.png" 
                                                    CancelText="" 
                                                    DeleteText="" 
                                                    UpdateImageUrl="~/Images/guardar18x18.png"          
                                                    UpdateText="" />          

                                        <asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtSubeCarga" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/sendUp20x20.png"
                                                   CommandName="SubeCarga" ToolTip="Devuelve la línea a órdenes de Carga" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                <asp:ImageButton ID="ibtCamion" runat="server" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' CommandName="CargaCamion" 
		                                            ToolTip="Tramita la línea directamente al Camión" Visible="true" ImageUrl="~/Images/etiqueta25x25.png" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cabecera" SortExpression="ID_CABECERA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLCabecera" runat="server" Text='<%# Eval("ID_CABECERA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLCabecera" runat="server" Text='<%# Bind("ID_CABECERA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nº Línea" Visible="false" SortExpression="NUMERO_LINEA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNumLinea" Visible="false" runat="server" Text='<%# Eval("NUMERO_LINEA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNumLinea" Visible="false" runat="server" Text='<%# Bind("NUMERO_LINEA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Empresa" SortExpression="EMPRESA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLEmpresa" runat="server" Text='<%# Eval("EMPRESA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLEmpresa" runat="server" Text='<%# Bind("EMPRESA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cliente" SortExpression="CLIENTEPROVEEDOR">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLProveedor" runat="server" Text='<%# Eval("CLIENTEPROVEEDOR") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLProveedor" runat="server" Text='<%# Bind("CLIENTEPROVEEDOR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre Fiscal" SortExpression="NOMBREFISCAL">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLFiscal" runat="server" Text='<%# Eval("NOMBREFISCAL") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLFiscal" runat="server" Text='<%# Bind("NOMBREFISCAL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ruta" SortExpression="RUTA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLRuta" runat="server" Text='<%# Eval("RUTA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLRuta" runat="server" Text='<%# Bind("RUTA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pedido" SortExpression="NUMERO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNumero" runat="server" Text='<%# Eval("NUMERO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNumero" runat="server" Text='<%# Bind("NUMERO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Linea" SortExpression="LINEA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLLinea" runat="server" Text='<%# Eval("LINEA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLinea" runat="server" Text='<%# Bind("LINEA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Articulo" SortExpression="ARTICULO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLArticulo" runat="server" Text='<%# Eval("ARTICULO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLArticulo" runat="server" Text='<%# Bind("ARTICULO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Uds Carga" SortExpression="UDSENCARGA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLCarga" runat="server" Text='<%# Eval("UDSENCARGA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLCarga" runat="server" Text='<%# Bind("UDSENCARGA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Num. Palet" SortExpression="NUMPALET">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLPalet" runat="server" Text='<%# Eval("NUMPALET") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLPalet" runat="server" Text='<%# Bind("NUMPALET") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pos. camión" SortExpression="POSICIONCAMION" >
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLcamion" runat="server" Text='<%# Eval("POSICIONCAMION") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLcamion" runat="server" Text='<%# Bind("POSICIONCAMION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                  <%--      <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUp" CssClass="button" CommandArgument = "up" runat="server" Text="&#x25B2;"  OnClick="ChangePreference" />
                                                <asp:LinkButton ID="lnkDown" CssClass="button" CommandArgument = "down" runat="server" Text="&#x25BC;"  OnClick="ChangePreference" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Observaciones" SortExpression="OBSERVACIONES">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLObservaciones" Width="100%" runat="server" Text='<%# Eval("OBSERVACIONES") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLObservaciones" Width="100%" runat="server" Text='<%# Bind("OBSERVACIONES") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estado" SortExpression="ESTADO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLEstado" runat="server" Text='<%# Eval("ESTADO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLEstado" runat="server" Text='<%# Bind("ESTADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
<%--                                        <asp:BoundField DataField="ID_CABECERA" HeaderText="Cabecera" />
                                        <asp:BoundField DataField="NUMERO_LINEA" HeaderText="Nº Línea" />
                                        <asp:BoundField DataField="EMPRESA" HeaderText="Empresa" />
                                        <asp:BoundField DataField="CLIENTEPROVEEDOR" HeaderText="Cliente Proveedor" />
                                        <asp:BoundField DataField="NOMBREFISCAL" HeaderText="Nombre Fiscal" />
                                        <asp:BoundField DataField="RUTA" HeaderText="Ruta" />
                                        <asp:BoundField DataField="NUMERO" HeaderText="Número" />
                                        <asp:BoundField DataField="LINEA" HeaderText="Linea" />
                                        <asp:BoundField DataField="ARTICULO" HeaderText="Articulo" />
                                        <asp:BoundField DataField="UDSENCARGA" HeaderText="Uds Carga" />
                                        <asp:BoundField DataField="NUMPALET" HeaderText="Num. Palet" />
                                        <asp:BoundField DataField="POSICIONCAMION" HeaderText="Pos. camión" />
                                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" />   
                                        <asp:BoundField DataField="ESTADO" HeaderText="Estado" />  --%>
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
            </div>

            <div class="tab-pane fade active" style="height:800px;" visible="true" runat="server" id="accordion5">
               <div class="row" id="PNreportLista" style="height:800px;" runat="server" visible="false">
                    <div class="col-sm-12" >
                        <%--<asp:Button ID="BtnImprimir" runat="server" Text="Imprimir" CausesValidation="False" OnClientClick="return false;" UseSubmitBehavior="False" />
                        <br />--%>
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="800px" Font-Names="Verdana" ShowPrintButton="true" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                        <%--            <LocalReport ReportPath="rdlc\Report1.rdlc">
                            </LocalReport>--%>
                        </rsweb:ReportViewer>
                    </div>
               </div>

                <div id="DivEtiquetas" runat="server" visible="false"  class="panel-collapse collapse in">                 
                    <div class="panel-body">
                        <div class="row">
                                <div class="col-lg-4" >
                                </div>
                            <div class="col-lg-7" runat="server" id="EtiquetaVe" visible="false" >
                            </div>
                            <div class="col-lg-7" runat="server" id="Etiqueta0" visible="true" >
                                <asp:Panel ID="pnlContents2" Visible="true" runat="server">
                                    <div id="divArray0" class="panel panel-default" style="display:inline-block; border-style:none; width:100%; font-weight: bold; font-size:20px;" >
                                        <br />
                                        <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="DLbEmpresa0"  Text="" > </asp:Label>
                                        <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="DLbLote0"  Text="" > </asp:Label>
                                        <asp:PlaceHolder ID="PlaceHolderGR0"  runat="server"></asp:PlaceHolder>
                                        <div id="divLabel0" class="col-lg-12" >
                                            <asp:Label  type="text" Width="100%" style=" display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="DLbOrdenCarga0"  Text="" > </asp:Label>
                                            <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="DLbPosCamion0"  Text="" > </asp:Label>
                                            <asp:Label  type="text" Width="40%" style="display:inline-block; width:40%; font-weight: bold; font-size:20px;" runat="server" ID="DlbCliente0"  Text="" > </asp:Label>
                                            <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="DLbVariedad0"  Text="" > </asp:Label>
                                            <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="DLbNumerPlanta0"  Text="" > </asp:Label>
                                        </div>
                                        <div id="divMIN0" class="col-lg-12" >
                                            <asp:PlaceHolder ID="PlaceHolderMIN0"  runat="server"></asp:PlaceHolder>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-lg-1" >
                                <button id="BtnMuestra" type="button" runat="server" style="width:50%; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtnMuestra_Click"><i id="iMuestra" runat="server" title="Despliega la lista de etiquetas para imprimir"  class="fa fa-times fa-2x"></i></button>
                                <button id="BtprinterList" type="button" runat="server" visible="true" style="width:50%; border-style:none; background-color:transparent;" class="pull-right text-muted " onclick="doPrintGRQR();"><i title="Imprime la vista previa presentada de los códigos QR para carga camión" class="fa fa-print fa-2x"></i></button>
                                </div>
                        </div>     
                    </div>
                </div>
            </div>


            <div class="tab-pane fade active" style="height:850px;" visible="false" runat="server" id="accordion6">
                <div class="panel panel-default">                     
                    <div class="panel-heading" runat="server" id="Div32" >
                           
                       <div class="panel-body">
                           <div class="panel-heading" runat="server" id="Div2" >
                              <div class="row">
                                        <div class="col-lg-12"> 
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" style=" font-size:24px; font-weight: bold;" href="#">Alta de Variedades                                             
                                                <%--<asp:Label  type="text"  Width="90%" style=" font-weight: bold; color:green;"  runat="server" ID="LbFiltros"  Text="" > </asp:Label>--%>
                                            </a>
                                        </h4>
                                    </div>                            
                            </div>
                            </div>
                        </div>
                     </div>
                    <div class="panel-body">

                       <div class="row">
                            <div class="col-lg-3" runat="server" id="Div20"  >
                                <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="LTipoP"  Text="" >Tipo Planta:</asp:Label>
                             </div>
                            <div class="col-lg-3" runat="server" id="Div21"  >
                                <asp:TextBox ID="TxtTipoPlanta"  Width="100%" runat="server" Text="" class="gridListAinput"></asp:TextBox>
                             </div>

                            <div class="col-lg-1" runat="server" id="Div4"  >
                              <button id="Button5" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaUd_Click"><i id="I1" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                           </div>
                           <div class="col-lg-1" runat="server" id="Div7"  >
                              <button id="Button8" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtLimpiaUd_Click"><i id="I3" runat="server" title="Limpiar todo" class="fa fa-eraser fa-2x"></i></button> 
                           </div>
                             <%--<div class="col-lg-1" runat="server" id="Div22"  >
                                  <button id="Button11" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaTP_Click"><i id="I6" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                             </div>
                            <div class="col-lg-5" runat="server" id="Div23"  >
                                <asp:DropDownList ID="DropDownList2" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbTipoPlanta_SelectedIndexChanged"></asp:DropDownList>   
                             </div>--%>
                        </div>

                        <div class="row">
                            <div class="col-lg-3" runat="server" id="Div12"  >
                                <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="LbVariedad"  Text="" >Variedad:</asp:Label>
                             </div>
                            <div class="col-lg-3" runat="server" id="Div13"  >
                                <asp:TextBox ID="TxtVariedad"  Width="100%" runat="server" Text="" class="gridListAinput"></asp:TextBox>
                             </div>
                             <%--<div class="col-lg-1" runat="server" id="Div14"  >
                                  <button id="Button9" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaVa_Click"><i id="I4" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                             </div>
                            <div class="col-lg-5" runat="server" id="Div15"  >
                                <asp:DropDownList ID="cbVariedad" CssClass="form-control" Width="100%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbVariedad_SelectedIndexChanged"></asp:DropDownList>  
                             </div>--%>
                        </div>

                       <div class="row">
                           <div class="col-lg-3" runat="server" id="Div5"  >
                               <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="Label22"  Text="" >Unidad Base:</asp:Label>
                            </div>
                           <div class="col-lg-3" runat="server" id="Div3"  >
                               <asp:TextBox ID="TxtUdBase"  Width="100%" runat="server" Text="" class="gridListAinput"></asp:TextBox>
                            </div>
                           

                           <%--<div class="col-lg-5" runat="server" id="Div7"  >
                               <asp:DropDownList ID="cbUdbase" CssClass="form-control" Width="100%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbUdbase_SelectedIndexChanged"></asp:DropDownList>  
                            </div>--%>
                       </div>
                        <div class="row">
                            <div class="col-lg-3" runat="server" id="Div8"  >
                                <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="lbCodGol"  Text="" >Código GoldenSoft:</asp:Label>
                             </div>
                            <div class="col-lg-3" runat="server" id="Div9"  >
                                <asp:TextBox ID="TxtCodGolden"  Width="100%" runat="server" Text="" class="gridListAinput"></asp:TextBox>
                             </div>
                             <%--<div class="col-lg-1" runat="server" id="Div10"  >
                                  <button id="Button8" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaCG_Click"><i id="I3" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                             </div>
                            <div class="col-lg-5" runat="server" id="Div11"  >
                                <asp:DropDownList ID="DropDownList1" CssClass="form-control" Width="100%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbUdbase_SelectedIndexChanged"></asp:DropDownList>  
                             </div>--%>
                        </div>
                        





                        <div class="row">
                            <div class="col-lg-3" runat="server" id="Div16"  >
                                <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="LbTipoF"  Text="" >Tipo Formato:</asp:Label>
                             </div>
                            <div class="col-lg-3" runat="server" id="Div17"  >
                                <asp:TextBox ID="TextTipoFormato"  Width="100%" runat="server" Text="" class="gridListAinput"></asp:TextBox>
                             </div>
                             <%--<div class="col-lg-1" runat="server" id="Div18"  >
                                  <button id="Button10" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaTF_Click"><i id="I5" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                             </div>
                            <div class="col-lg-5" runat="server" id="Div19"  >
                                <asp:DropDownList ID="cbTipoFormato" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbTipoFormato_SelectedIndexChanged"></asp:DropDownList>   
                             </div>--%>
                        </div>


                        <div class="row">
                            <div class="col-lg-3" runat="server" id="Div24"  >
                                <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="Label30"  Text="" >Descripción Tipo Planta:</asp:Label>
                             </div>
                            <div class="col-lg-3" runat="server" id="Div25"  >
                                <asp:TextBox ID="TxtDTipoPlanta"  Width="100%" runat="server" Text="" class="gridListAinput"></asp:TextBox>
                             </div>
                             <%--<div class="col-lg-1" runat="server" id="Div26"  >
                                  <button id="Button12" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaDTP_Click"><i id="I7" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                             </div>
                            <div class="col-lg-5" runat="server" id="Div27"  >
                                <asp:DropDownList ID="cbDtipoplanta" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbDYipoPlanta_SelectedIndexChanged"></asp:DropDownList>   
                             </div>--%>
                        </div>
                                
                        <div class="row">
                            <div class="col-lg-3" runat="server" id="Div28"  >
                                <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="Label31"  Text="" >Número de Plantas:</asp:Label>
                             </div>
                            <div class="col-lg-3" runat="server" id="Div29"  >
                                <asp:TextBox ID="TxtNumPlanta"   Width="100%" runat="server" Text="PLANTAS" class="gridListAinput"></asp:TextBox> 
                             </div>
                             <%--<div class="col-lg-1" runat="server" id="Div30"  >
                                  <button id="Button13" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaNP_Click"><i id="I8" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                             </div>
                            <div class="col-lg-5" runat="server" id="Div31"  >
                                <asp:DropDownList ID="CbNumPlanta" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbNumPlanta_SelectedIndexChanged"></asp:DropDownList>   
                             </div>--%>
                        </div>                               
                        <br />
                         <div class="col-lg-12" runat="server" id="Div6"  >
                               <div class="panel-body">
                                   <div id="divAr0" class="panel panel-default" style="display:inline-block; border-style:none; width:100%; font-weight: bold; font-size:20px;" >
                                       <div class="row"  >
                                            <div class="col-lg-2" >
                                                
                                            </div>
                                            <div class="col-lg-2" >
                                            <%--<asp:Button runat="server" ID="btnNuevoLote" visible="false" tooltip="Para crear códigos QR ajenos e independientes" CssClass="btn btn-info btn-block" Width="100%"  Text="Nuevo" OnClick="btnNuevoLote_Click"/>--%>
                                                <asp:Button runat="server" ID="BtModificaVariedad" visible="false" CssClass="btn btn-success btn-block" Width="100%"  Text="Modificar" OnClick="btnModificaVariedad_Click"/> 
                                                <asp:Button runat="server" ID="btNuevaVariedad" visible="true" tooltip="Crea los campos para insertar una Variedad nueva con Código de GoldenSoft" CssClass="btn btn-success btn-block" Width="100%"  Text="Nuevo" OnClick="BtNuevaVariedad_Click"/>
                                            </div>
                                                                       
                                            <div class="col-lg-2" >
                                                <asp:Button runat="server" ID="BtGuardaVariedad" visible="false" tooltip="Guardar los campos del código QR que tienes en edición en este momento en la Base de Datos" CssClass="btn btn-success btn-block" Width="100%"  Text="Guardar" OnClick="BtGuardaVariedad_Click"/>
                                            </div>
                                            <div class="col-lg-2" >
                                            <asp:Button runat="server" ID="BtCancelaVariedad" Enabled="false" visible="true" tooltip="Cancela el código QR que tienes en edición en este momento" CssClass="btn btn-warning btn-block" Width="100%"  Text="Cancelar" OnClick="btnCancelaVariedad_Click"/>
                                            </div>
                                                <div class="col-lg-4" >
                                            </div>
                                            <div class="col-lg-2" >
                                                <asp:Button runat="server" ID="BtDeleteVariedad" Enabled="false" visible="true" tooltip="Marca como borrado el código QR seleccionado" CssClass="btn btn-danger btn-block" Width="100%"  Text="Eliminar" OnClick="btnDeleteVariedad_Click"/>
                                                <%--<asp:Button runat="server" ID="btGeneraNew" visible="false" tooltip="Genera nuevamente un código QR para el  registro seleccionado" CssClass="btn btn-info btn-block" Width="100%"  Text="Generar" OnClick="btnGeneraNew_Click"/>--%>
                                            </div>
                                   </div>
                               </div>
                           </div>
                       </div>     
                        <br />
                         <div class="row">

                            <div class="col-lg-4" styles=" height:80px; overflow-y: scroll;"  runat="server" id="Div33"  >
                                <div class="panel panel-default" styles="height:100px; overflow-y: scroll;" >                     
                                    <div class="panel-heading"  runat="server" id="Div36" >
                           
                                       <div class="panel-body">
                                           <div class="panel-heading" runat="server" id="Div37" >
                                              <div class="row">
                                                    <div class="col-lg-6"> 
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" style=" font-size:24px; font-weight: bold;" href="#">Bandejas</a>
                                                        </h4>
                                                    </div>   
                                                  <div class="col-lg-4">
                                                        <asp:DropDownList ID="DrBandejas" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbBuscaCampo_SelectedIndexChanged"></asp:DropDownList> 
                                                  </div>
                                                  <div class="col-lg-2">
                                                        <button id="Button14" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaNP_Click"><i id="I9" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                                                  </div>
                                                </div>
                                            </div>
                                        </div>
                                     </div>

                                   <%-- <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="Label24"  Text="" >Bandejas:</asp:Label>--%>
                                    <%--<asp:ListBox ID="ListBoxBandeja" style="height:300px;width:100%" AutoPostBack="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBoxBandeja_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>--%>
                               
                                    <%--SELECT  ZID, ZTIPO_PLANTA, ZTIPO_FORMATO, ZNUMERO_PLANTAS FROM ZBANDEJAS--%>
                                    <div class="panel-body" style="overflow: auto; width: 100%; height: 300px">                   
                                <asp:GridView ID="GvBandeja"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                    AllowSorting="true" OnSorting="GvBandeja_OnSorting"
                                    CellPadding="4"  GridLines="None"  OnRowDataBound="GvBandeja_OnRowDataBound"  width="100%" OnRowCommand="GvBandeja_RowCommand" DataKeyNames="ZID"
                                    oonselectedindexchanged="GvBandeja_SelectedIndexChanged"  
                                    EnablePersistedSelection="True"  >
                                <RowStyle />                     
                                    <Columns>
                                        <asp:TemplateField HeaderText="ZID"  SortExpression="ZID">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="BTabLCabecera" runat="server" Text='<%# Eval("ZID") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <%--<asp:Label ID="btBCabecera" runat="server" Text='<%# Bind("ZID") %>'></asp:Label>--%>
                                                    <asp:button ID="btBCabecera" CommandName="Identificador" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" Text='<%# Bind("ZID") %>'></asp:button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ZTIPO_PLANTA"  SortExpression="ZTIPO_PLANTA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="BTabLCabecera" runat="server" Text='<%# Eval("ZTIPO_PLANTA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="BLabLCabecera" runat="server" Text='<%# Bind("ZTIPO_PLANTA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ZTIPO_FORMATO" Visible="true" SortExpression="ZTIPO_FORMATO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="BTabLNumLinea3" Visible="true" runat="server" Text='<%# Eval("ZTIPO_FORMATO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="BLabLNumLinea3" Visible="true" runat="server" Text='<%# Bind("ZTIPO_FORMATO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>       
                                         <asp:TemplateField HeaderText="ZNUMERO_PLANTAS" Visible="true" SortExpression="ZNUMERO_PLANTAS">
                                         <EditItemTemplate>
                                                 <asp:TextBox ID="BTabLNumLinea4" Visible="true" runat="server" Text='<%# Eval("ZNUMERO_PLANTAS") %>' class="gridListAinput"></asp:TextBox>
                                         </EditItemTemplate>
                                         <ItemTemplate>
                                                 <asp:Label ID="BLabLNumLinea4" Visible="true" runat="server" Text='<%# Bind("ZNUMERO_PLANTAS") %>'></asp:Label>
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

                            <div class="col-lg-4" styles=" height:80px; overflow-y: scroll;"  runat="server" id="Div38"  >
                                <div class="panel panel-default" styles="height:100px; overflow-y: scroll;" >                     
                                    <div class="panel-heading"  runat="server" id="Div39" >
                           
                                       <div class="panel-body">
                                           <div class="panel-heading" runat="server" id="Div40" >
                                              <div class="row">
                                                        <div class="col-lg-6"> 
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" style=" font-size:24px; font-weight: bold;" href="#">Códigos GoldenSoft</a>
                                                        </h4>
                                                    </div>   
                                                   <div class="col-lg-4">
                                                       <asp:DropDownList ID="DrCodgolden" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbBuscaCampo_SelectedIndexChanged"></asp:DropDownList> 
                                                 </div>
                                                 <div class="col-lg-2">
                                                       <button id="Button15" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaNP_Click"><i id="I10" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                                                 </div>
                                            </div>
                                            </div>
                                        </div>
                                     </div>

                                   <%-- <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="Label24"  Text="" >Bandejas:</asp:Label>--%>
                                    <%--<asp:ListBox ID="ListBoxBandeja" style="height:300px;width:100%" AutoPostBack="true" CssClass="form-control btn-block" runat="server" OnSelectedIndexChanged="ListBoxBandeja_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>--%>
                               
                                    <%--SELECT  ZID, ZTIPO_PLANTA, ZVARIEDAD, ZCODGOLDEN, UD_BASE FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN--%> 
                                 <div class="panel-body" style="overflow: auto; width: 100%; height: 300px">                   
                                <asp:GridView ID="GvGolden"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                    AllowSorting="true" OnSorting="GvGolden_OnSorting"
                                    CellPadding="4"  GridLines="None"  OnRowDataBound="GvGolden_OnRowDataBound"  width="100%" OnRowCommand="GvGolden_RowCommand" DataKeyNames="ZID"
                                    oonselectedindexchanged="GvGolden_SelectedIndexChanged"  
                                    EnablePersistedSelection="True"  >
                                <RowStyle />                     
                                    <Columns>
                                        <asp:TemplateField HeaderText="ZID"  SortExpression="ZID">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLCabecera" runat="server" Text='<%# Eval("ZID") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <%--<asp:Label ID="btGCabecera" runat="server" Text='<%# Bind("ZID") %>'></asp:Label>--%>
                                                    <asp:button ID="btGCabecera" CommandName="Identificador" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" Text='<%# Bind("ZID") %>'></asp:button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ZTIPO_PLANTA"  SortExpression="ZTIPO_PLANTA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="GTabLCabecera" runat="server" Text='<%# Eval("ZTIPO_PLANTA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="GLabLCabecera" runat="server" Text='<%# Bind("ZTIPO_PLANTA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ZVARIEDAD" Visible="true" SortExpression="ZVARIEDAD">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="GTabLNumLinea" Visible="true" runat="server" Text='<%# Eval("ZVARIEDAD") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="GLabLNumLinea" Visible="true" runat="server" Text='<%# Bind("ZVARIEDAD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>          
                                        <asp:TemplateField HeaderText="ZCODGOLDEN" Visible="true" SortExpression="ZCODGOLDEN">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="GTabLNumLinea1" Visible="true" runat="server" Text='<%# Eval("ZCODGOLDEN") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="GLabLNumLinea1" Visible="true" runat="server" Text='<%# Bind("ZCODGOLDEN") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>      
                                    <asp:TemplateField HeaderText="UD_BASE" Visible="true" SortExpression="UD_BASE">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="GTabLNumLinea2" Visible="true" runat="server" Text='<%# Eval("UD_BASE") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="GLabLNumLinea2" Visible="true" runat="server" Text='<%# Bind("UD_BASE") %>'></asp:Label>
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


                            <div class="col-lg-4" styles=" height:80px; overflow-y: scroll;"  runat="server" id="Div34"  >
                                <div class="panel panel-default" styles="height:100px; overflow-y: scroll;" >                     
                                    <div class="panel-heading"  runat="server" id="Div35" >
                           
                                       <div class="panel-body">
                                           <div class="panel-heading" runat="server" id="Div41" >
                                              <div class="row">
                                                        <div class="col-lg-6"> 
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" style=" font-size:24px; font-weight: bold;" href="#">Tipo Planta</a>
                                                        </h4>
                                                    </div>  
                                                   <div class="col-lg-4">
                                                       <asp:DropDownList ID="DrTipoplanta" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbBuscaCampo_SelectedIndexChanged"></asp:DropDownList> 
                                                 </div>
                                                 <div class="col-lg-2">
                                                       <button id="Button16" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaNP_Click"><i id="I11" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                                                 </div>
                                            </div>
                                            </div>
                                        </div>
                                     </div>

                               

                             <%--ZID , ZTIPO_PLANTA, ZDESCRIPTIPO FROM ZTIPOPLANTADESCRIP--%>
                            <div class="panel-body" style="overflow: auto; width: 100%; height: 300px">                   
                                <asp:GridView ID="GvTablaDes"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                    AllowSorting="true" OnSorting="GvTablaDes_OnSorting"
                                    CellPadding="4"  GridLines="None"  OnRowDataBound="GvTablaDes_OnRowDataBound"  width="100%" OnRowCommand="GvTablaDes_RowCommand" DataKeyNames="ZID"
                                    oonselectedindexchanged="GvTablaDes_SelectedIndexChanged"  
                                    EnablePersistedSelection="True"  >
                                <RowStyle />                     
                                    <Columns>
                                        <asp:TemplateField HeaderText="ZID"  SortExpression="ZID">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TTabLCabecera" runat="server" Text='<%# Eval("ZID") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:button ID="BtCabecera" CommandName="Identificador" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" Text='<%# Bind("ZID") %>'></asp:button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ZTIPO_PLANTA"  SortExpression="ZTIPO_PLANTA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TTabLCabecera" runat="server" Text='<%# Eval("ZTIPO_PLANTA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="TLabLCabecera" runat="server" Text='<%# Bind("ZTIPO_PLANTA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ZDESCRIPTIPO" Visible="true" SortExpression="ZDESCRIPTIPO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TTabLNumLinea" Visible="true" runat="server" Text='<%# Eval("ZDESCRIPTIPO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="TLabLNumLinea" Visible="true" runat="server" Text='<%# Bind("ZDESCRIPTIPO") %>'></asp:Label>
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

                           <div class="row" styles=" height:90px; overflow-y: scroll;"  runat="server" id="Div10"  >
                                <div class="panel panel-default" styles="height:100px; overflow-y: scroll;" >                     
                                    <div class="panel-heading"  runat="server" id="Div11" >
                           
                                       <div class="panel-body">
                                           <div class="panel-heading" runat="server" id="Div14" >
                                              <div class="row">
                                                        <div class="col-lg-6"> 
                                                        <h4 class="panel-title">
                                                            <a data-toggle="collapse" style=" font-size:24px; font-weight: bold;" href="#">Resultados</a>
                                                        </h4>
                                                    </div>  
                                                </div>
                                            </div>
                                        </div>
                                     </div>

                            <div class="panel-body" style="overflow: auto; width: 100%; height: 100px">                   
                                <asp:GridView ID="GrResultado"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                    AllowSorting="true" OnSorting="GrResultado_OnSorting"
                                    CellPadding="4"  GridLines="None"  OnRowDataBound="GrResultado_OnRowDataBound"  width="100%" OnRowCommand="GrResultado_RowCommand" DataKeyNames="ZID"
                                    oonselectedindexchanged="GrResultado_SelectedIndexChanged"  
                                    EnablePersistedSelection="True"  >
                                <RowStyle />                     
                                    <Columns>
                                        <asp:TemplateField HeaderText="ZID"  SortExpression="ZID">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TTabLCabecera" runat="server" Text='<%# Eval("ZID") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:button ID="BtCabecera" CommandName="Identificador" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" Text='<%# Bind("ZID") %>'></asp:button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ZTIPO_PLANTA"  SortExpression="ZTIPO_PLANTA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TTabLCabecera" runat="server" Text='<%# Eval("ZTIPO_PLANTA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="TLabLCabecera" runat="server" Text='<%# Bind("ZTIPO_PLANTA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ZVARIEDAD" Visible="true" SortExpression="ZVARIEDAD">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TTabLNumLinea" Visible="true" runat="server" Text='<%# Eval("ZVARIEDAD") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="TLabLNumLinea" Visible="true" runat="server" Text='<%# Bind("ZVARIEDAD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="ZCODGOLDEN" Visible="true" SortExpression="ZCODGOLDEN">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TTabLcodLinea" Visible="true" runat="server" Text='<%# Eval("ZCODGOLDEN") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="TLabLcodLinea" Visible="true" runat="server" Text='<%# Bind("ZCODGOLDEN") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="UD_BASE" Visible="true" SortExpression="UD_BASE">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="TTabLVaLinea" Visible="true" runat="server" Text='<%# Eval("UD_BASE") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="TLabLVaLinea" Visible="true" runat="server" Text='<%# Bind("UD_BASE") %>'></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="ZDESCRIPTIPO" Visible="true" SortExpression="ZDESCRIPTIPO">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="TTabLVaLinea" Visible="true" runat="server" Text='<%# Eval("ZDESCRIPTIPO") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="TLabLVaLinea" Visible="true" runat="server" Text='<%# Bind("ZDESCRIPTIPO") %>'></asp:Label>
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
        </div>
        </div>
    </div>

</div>

    <!-- /#wrapper -->
          <script type="text/javascript">
              function drag(ev) {
                  ev.dataTransfer.setData("text", ev.target.id);
              }

              function allowDrop(ev) {
                  ev.preventDefault();
              }

              function drop(ev) {
                  ev.preventDefault();
                  var data = ev.dataTransfer.getData("text");
                  ev.target.appendChild(document.getElementById(data));
                  //document.getElementById('Btn' + data.id).click();
              }

              function submdrag0() {
                  document.getElementById("LbPosicionCamion").textContent = 'drag0';
                  var elem = document.getElementById('drag0');
                  document.getElementById('Btndrag0').click();
              }
              function submdrag1() {
                  document.getElementById("LbPosicionCamion").textContent = 'drag1';
                  var elem = document.getElementById('drag1');
                  document.getElementById('Btndrag1').click();
              }
              function submdrag2() {
                  document.getElementById("LbPosicionCamion").textContent = 'drag2';
                  var elem = document.getElementById('drag2');
                  document.getElementById('Btndrag2').click();
              }
              function submdrag3() {
                  document.getElementById("LbPosicionCamion").textContent = 'drag3';
                  var elem = document.getElementById('drag3');
                  document.getElementById('Btndrag3').click();
              }

              function submitit() {
                  document.getElementById('btn').click();
              }

              function submititCab() {
                  document.getElementById('btnCaCheck').click();
              }           
     
              function submitit1() {
                  document.getElementById('btn1').click();
              }

              function submitit2() {
                  document.getElementById('btn2').click();
              }

              function submitit3() {
                  document.getElementById('btn3').click();
              }

              function submiti4() {
                  document.getElementById('btn4').click();
              }

              function submitit5() {
                  document.getElementById('btn5').click();
              }

              function submitit6() {
                  document.getElementById('btn6').click();
              }

              function submitAnchor() {
                  document.getElementById('btnAnchor').click();
              }

              function PrintGrid(html, css) {
                  var printWin = window.open('', '', 'left=0,top=0,width=400,height=300,scrollbars=1');
                  printWin.document.write('<style type = "text/css">' + css + '</style>');
                  printWin.document.write(html);
                  printWin.document.close();
                  printWin.focus();
                  printWin.print();
                  printWin.close();
              }

              function doPrintGRCabecera() {
                  var prtContent = document.getElementById('<%= gvCabecera.ClientID %>');
                  //prtContent.border = 0; //set no border here
                  var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
                  WinPrint.document.write(prtContent.outerHTML);
                  WinPrint.document.close();
                  WinPrint.focus();
                  WinPrint.print();
                  WinPrint.close();
              }

              function doPrintGRControl() {
                  var prtContent = document.getElementById('<%= gvControl.ClientID %>');
                  //prtContent.border = 0; //set no border here
                  var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
                  WinPrint.document.write(prtContent.outerHTML);
                  WinPrint.document.close();
                  WinPrint.focus();
                  WinPrint.print();
                  WinPrint.close();
              }

              function doPrintGRLista() {
                  var prtContent = document.getElementById('<%= gvLista.ClientID %>');
                  //prtContent.border = 0; //set no border here
                  var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
                  WinPrint.document.write(prtContent.outerHTML);
                  WinPrint.document.close();
                  WinPrint.focus();
                  WinPrint.print();
                  WinPrint.close();
              }

              function doPrintGRQR() {
                  var panel = document.getElementById("<%=pnlContents2.ClientID %>").innerHTML;
                  var printWindow = window.open('', '', 'height=400,width=800');
                  printWindow.document.write('<html><head><title></title>');
                  printWindow.document.write('</head><body >');
                  printWindow.document.write('<div align=center>');
                  printWindow.document.write(panel);
                  printWindow.document.write('</div>');
                  printWindow.document.write('</body></html>');
                  printWindow.document.close();
                  printWindow.focus();
                  printWindow.print();
                  return false;
              }



          </script>

        <!-- jQuery Version 1.11.0 -->
    <script src="js/jquery-1.11.0.js"></script>
 
    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>
    
    <!-- Metis Menu Plugin JavaScript -->
    <script src="js/plugins/metisMenu/metisMenu.min.js"></script>

    <!-- Morris Charts JavaScript -->
 <%--     <script src="js/plugins/morris/raphael.min.js"></script>
  <script src="js/plugins/morris/morris.min.js"></script>
    <script src="js/plugins/morris/morris-data.js"></script>--%>

    <!-- Custom Theme JavaScript Menu derecha correcto -->
    <script src="js/sb-admin-2.js"></script>




</asp:Content>
