<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AltaVariedades.aspx.cs" Inherits="Satelite.AltaVariedades" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page_wrapper" runat="server" ><!-- /#page-wrapper  class="portada"    http://192.168.1.87/login.aspx-->


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



<%--          <div runat="server" id="DvPreparado" visible="false"  style=" width: 30%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <i id="I2" runat="server" class="fa fa-exclamation-circle fa-2x"></i>
                
                <div class="row" id="cc" visible="false" runat="server">
                    <div class="col-lg-6">
                    </div>
                    <div class="col-lg-6">
                    </div>
                </div>
                 <div class="row" id="Decide" visible="false" runat="server">
                    <div class="col-lg-4">
                    </div>
                     <div class="col-lg-4">
                    </div>
                    <div class="col-lg-4">
                    </div>
                </div>
                   <div class="row" id="Modifica" visible="false" runat="server">
                     <div class="col-lg-6">
                    </div>
                    <div class="col-lg-6">
                    </div>
                </div>
                
                     <div class="col-lg-1">
                     </div>
                     <div class="col-lg-1">
                     </div>
                     <div class="col-lg-1">
                     </div>
                     <div class="col-lg-1">
                     </div>
                    <div class="col-lg-4" style="text-align:center;">
                    </div>
                     <div class="col-lg-1">
                     </div>
                     <div class="col-lg-1">
                     </div>
                     <div class="col-lg-1">
                     </div>
                     <div class="col-lg-1">
                     </div>
            </div>--%>

           <div runat="server" id="alerta" visible="false" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <i id="IAlert" runat="server" class="fa fa-exclamation-circle"></i>
                <asp:Label runat="server" class="alert alert-info alert-dismissable" ID="TextAlerta" BorderStyle="None" border="0" Width="100%" Text=""  />
           </div>
           <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Lberror"  Text="" > </asp:Label>


        <!-- /.row data-parent=titulo descripcion-->

            <div class="tab-pane " style="height:850px;" visible="true" runat="server" >
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
                           
                           <div class="col-lg-6" runat="server" id="Div30"  >
                               <div class="row">
                                    <div class="col-lg-3" runat="server" id="Div20"  >
                                        <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="LTipoP"  Text="" >Tipo Planta:</asp:Label>
                                     </div>
                                    <div class="col-lg-7" runat="server" id="Div21"  >
                                        <asp:TextBox ID="TxtTipoPlanta"  Width="100%" runat="server" Text="" class="gridListAinput"></asp:TextBox>
                                     </div>
                                   <div class="col-lg-1" runat="server" id="Div1"  >
                                      <asp:ImageButton ID="Img1" Visible="false"  runat="server" ImageUrl="~/Images/ok.png" ToolTip="Seleccionada Tabla Bandejas " Width="30px" Height="30px"></asp:ImageButton>
                                   </div>
                                    <div class="col-lg-1" runat="server" id="Div4"  >
                                      <button id="Button5" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaUd_Click"><i id="I1" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                                   </div>
                               </div>
                                 <%--<div class="col-lg-1" runat="server" id="Div22"  >
                                      <button id="Button11" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaTP_Click"><i id="I6" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                                 </div>
                                <div class="col-lg-5" runat="server" id="Div23"  >
                                    <asp:DropDownList ID="DropDownList2" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbTipoPlanta_SelectedIndexChanged"></asp:DropDownList>   
                                 </div>--%>
                               <div class="row">
                                    <div class="col-lg-3" runat="server" id="Div12"  >
                                        <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="LbVariedad"  Text="" >Variedad:</asp:Label>
                                     </div>
                                    <div class="col-lg-7" runat="server" id="Div13"  >
                                        <asp:TextBox ID="TxtVariedad"  Width="100%" runat="server" Text="" class="gridListAinput"></asp:TextBox>
                                     </div>
                                    <div class="col-lg-1" runat="server" id="Div15"  >
                                      <asp:ImageButton ID="Img2" Visible="false"  runat="server" ImageUrl="~/Images/ok.png" ToolTip="Seleccionada Tabla Bandejas " Width="30px" Height="30px"></asp:ImageButton>
                                   </div>
                                   <div class="col-lg-1" style="text-align:left;"  runat="server" id="Div7"  >
                                      <button id="Button8" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtLimpiaUd_Click"><i id="I3" runat="server" title="Limpiar todo" class="fa fa-eraser fa-2x"></i></button> 
                                   </div>
                                </div>
                                     <%--<div class="col-lg-1" runat="server" id="Div14"  >
                                          <button id="Button9" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaVa_Click"><i id="I4" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                                     </div>
                                    <div class="col-lg-5" runat="server" id="Div15"  >
                                        <asp:DropDownList ID="cbVariedad" CssClass="form-control" Width="100%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbVariedad_SelectedIndexChanged"></asp:DropDownList>  
                                     </div>--%>
                               <div class="row">
                                   <div class="col-lg-3" runat="server" id="Div5"  >
                                       <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="Label22"  Text="" >Unidad Base:</asp:Label>
                                    </div>
                                   <div class="col-lg-7" runat="server" id="Div3"  >
                                       <asp:TextBox ID="TxtUdBase"  Width="100%" runat="server" Text="" class="gridListAinput"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1" runat="server" id="Div18"  >
                                      <asp:ImageButton ID="Img3" Visible="false"  runat="server" ImageUrl="~/Images/ok.png" ToolTip="Seleccionada Tabla Bandejas " Width="30px" Height="30px"></asp:ImageButton>
                                   </div>
                                </div>
                                   <%--<div class="col-lg-5" runat="server" id="Div7"  >
                                       <asp:DropDownList ID="cbUdbase" CssClass="form-control" Width="100%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbUdbase_SelectedIndexChanged"></asp:DropDownList>  
                                    </div>--%>
                               <div class="row">
                                    <div class="col-lg-3" runat="server" id="Div8"  >
                                        <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="lbCodGol"  Text="" >Código GoldenSoft:</asp:Label>
                                     </div>
                                    <div class="col-lg-7" runat="server" id="Div9"  >
                                        <asp:TextBox ID="TxtCodGolden"  Width="100%" runat="server" Text="" class="gridListAinput"></asp:TextBox>
                                     </div>
                                    <div class="col-lg-1" runat="server" id="Div19"  >
                                      <asp:ImageButton ID="Img4" Visible="false"  runat="server" ImageUrl="~/Images/ok.png" ToolTip="Seleccionada Tabla Bandejas " Width="30px" Height="30px"></asp:ImageButton>
                                   </div>
                                </div>
                                     <%--<div class="col-lg-1" runat="server" id="Div10"  >
                                          <button id="Button8" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaCG_Click"><i id="I3" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                                     </div>
                                    <div class="col-lg-5" runat="server" id="Div11"  >
                                        <asp:DropDownList ID="DropDownList1" CssClass="form-control" Width="100%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbUdbase_SelectedIndexChanged"></asp:DropDownList>  
                                     </div>--%>

                                <div class="row">
                                    <div class="col-lg-3" runat="server" id="Div16"  >
                                        <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="LbTipoF"  Text="" >Tipo Formato:</asp:Label>
                                     </div>
                                    <div class="col-lg-7" runat="server" id="Div17"  >
                                        <asp:TextBox ID="TextTipoFormato"  Width="100%" runat="server" Text="" class="gridListAinput"></asp:TextBox>
                                     </div>
                                    <div class="col-lg-1" runat="server" id="Div22"  >
                                      <asp:ImageButton ID="Img5" Visible="false"  runat="server" ImageUrl="~/Images/ok.png" ToolTip="Seleccionada Tabla Bandejas " Width="30px" Height="30px"></asp:ImageButton>
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
                                        <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:20px;" runat="server" ID="Label30"  Text="" >Descripción Tipo:</asp:Label>
                                     </div>
                                    <div class="col-lg-7" runat="server" id="Div25"  >
                                        <asp:TextBox ID="TxtDTipoPlanta"  Width="100%" runat="server" Text="" class="gridListAinput"></asp:TextBox>
                                     </div>
                                    <div class="col-lg-1" runat="server" id="Div23"  >
                                      <asp:ImageButton ID="Img6" Visible="false"  runat="server" ImageUrl="~/Images/ok.png" ToolTip="Seleccionada Tabla Bandejas " Width="30px" Height="30px"></asp:ImageButton>
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
                                    <div class="col-lg-7" runat="server" id="Div29"  >
                                        <asp:TextBox ID="TxtNumPlanta"   Width="100%" runat="server" Text="PLANTAS" class="gridListAinput"></asp:TextBox> 
                                     </div>
                                    <div class="col-lg-1" runat="server" id="Div26"  >
                                      <asp:ImageButton ID="Img7" Visible="false"  runat="server" ImageUrl="~/Images/ok.png" ToolTip="Seleccionada Tabla Bandejas " Width="30px" Height="30px"></asp:ImageButton>
                                   </div>

                                     <%--<div class="col-lg-1" runat="server" id="Div30"  >
                                          <button id="Button13" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaNP_Click"><i id="I8" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button> 
                                     </div>
                                    <div class="col-lg-5" runat="server" id="Div31"  >
                                        <asp:DropDownList ID="CbNumPlanta" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbNumPlanta_SelectedIndexChanged"></asp:DropDownList>   
                                     </div>--%>
                                </div> 
                            </div>
                           <div class="col-lg-6" runat="server" id="Div31"  >
                                <div class="row">
                                    <div class="col-lg-12" runat="server" id="Div27"  >
                                     <asp:ImageButton id="ImgAyuda" visible="true" runat="server" ToolTip="Visualiza en pantalla la descripcion del formulario en que se encuentra" ImageAlign="right" Width="30px" Height="30px" ImageUrl="images/ayuda.png" OnClick="BtAyuda_Click"/>
                                     <asp:TextBox ID="multitxt"  Visible="false" Height="150px" Width="100%" style="text-align:left;" runat="server" TextMode="MultiLine" BorderStyle="None">- Para buscar: 
Inserta un texto en la casilla "Tipo Planta" el sistema buscará en los tres contenedores.
- Para buscar desde cada contenedor:
Selecciona un registro de la lista de selección (Esta lista sólo contiene los valores distintos del contenedor)
- Para buscar por registro:
Puedes seleccionar cualquier registro desde la lista de resultados correspondiente a cada contenedor y verás como se ubica en cada casilla de busqueda.
- Para crear un registro:
Después de limpiar los datos, pulsa sobre el botón "Nuevo" y cumplimenta las casillas, cada casilla irá a su contenedor correspondiente. Pulsa guardar.
- Para modificar un registro:
Sobre aquel registro que tengas seleccionado, pulsa el botón "Modificar" y modifica las casillas necesarias. Pulsa guardar.
- Para eliminar un registro:
Sobre aquel registro que tengas seleccionado, pulsa el botón "Modificar" y posteriormente pulsa sobre el botón "Eliminar".

                                       </asp:TextBox>
                                   </div>
                                </div>
                           </div>
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
                                                <asp:Button runat="server" ID="BtModificaVariedad" visible="false" CssClass="c2 g2 s_ pc-btn-calc o1" Width="100%"  Text="Modificar" OnClick="btnModificaVariedad_Click"/> 
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
                                                    <div class="col-lg-5"> 
                                                        <h2 class="panel-title">
                                                            <a data-toggle="collapse" title="Tabla ZBANDEJAS" style=" font-size:24px; font-weight: bold;" href="#">Bandejas</a>
                                                        </h2>
                                                    </div>   
                                                  <div class="col-lg-2">
                                                      <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:14px;color:forestgreen;" runat="server" ID="LbcountBandeja"  Text="" ></asp:Label>
                                                  </div>
                                                  <div class="col-lg-4">
                                                        <asp:DropDownList ID="DrBandejas" CssClass="form-control" Width="100%" ToolTip="Busqueda por Tipo Fromato" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbBuscaCampo_SelectedIndexChanged"></asp:DropDownList> 
                                                  </div>
                                                  <div class="col-lg-1">
                                                        <%--<button id="Button14" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaNP_Click"><i id="I9" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button>--%> 
                                                        <asp:ImageButton ID="ImgBandeja" Visible="false"  runat="server" ImageUrl="~/Images/ok.png" OnClick="Imgatras_Click" ToolTip="Activa campos sobre Tabla" Width="30px" Height="30px"></asp:ImageButton>
                                                        <asp:ImageButton ID="ImgBandeja1" Visible="false" runat="server" ImageUrl="~/Images/cancel.png" OnClick="Imgadelante_Click" ToolTip="Desactiva campos sobre Tabla" Width="30px" Height="30px"></asp:ImageButton>
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
                                                        <div class="col-lg-5"> 
                                                        <h2 class="panel-title">
                                                            <a data-toggle="collapse" title="Tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN" style=" font-size:24px; font-weight: bold;" href="#">Códigos GoldenSoft</a>
                                                        </h2>
                                                    </div>  
                                                   <div class="col-lg-2">
                                                      <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:14px;color:forestgreen;" runat="server" ID="LbCountGolden"  Text="" ></asp:Label>
                                                  </div>

                                                   <div class="col-lg-4">
                                                       <asp:DropDownList ID="DrCodgolden" CssClass="form-control" Width="100%" ToolTip="Busqueda por Código Golden"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbBuscagolden_SelectedIndexChanged"></asp:DropDownList> 
                                                 </div>
                                                 <div class="col-lg-1">
                                                       <%--<button id="Button15" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaNP_Click"><i id="I10" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button>--%> 
                                                        <asp:ImageButton ID="ImgGolden" Visible="false"  runat="server" ImageUrl="~/Images/ok.png" OnClick="Imgatras_Click" ToolTip="Activa campos sobre Tabla" Width="30px" Height="30px"></asp:ImageButton>
                                                        <asp:ImageButton ID="ImgGolden1" Visible="false" runat="server" ImageUrl="~/Images/cancel.png" OnClick="Imgadelante_Click" ToolTip="Desactiva campos sobre Tabla" Width="30px" Height="30px"></asp:ImageButton>

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
                                                        <div class="col-lg-5"> 
                                                        <h2 class="panel-title">
                                                            <a data-toggle="collapse"  title="Tabla ZTIPOPLANTADESCRIP" style=" font-size:24px; font-weight: bold;" href="#">Tipo Planta</a>
                                                        </h2>
                                                    </div>  
                                                   <div class="col-lg-2">
                                                      <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:14px;color:forestgreen;" runat="server" ID="LbcountPlanta"  Text="" ></asp:Label>
                                                  </div>

                                                   <div class="col-lg-4">
                                                       <asp:DropDownList ID="DrTipoplanta" CssClass="form-control" Width="100%" runat="server" ToolTip="Busqueda por Descripción Tipo" AutoPostBack="true" OnSelectedIndexChanged="cbBuscaplanta_SelectedIndexChanged"></asp:DropDownList> 
                                                 </div>
                                                 <div class="col-lg-1">
                                                       <%--<button id="Button16" type="button" runat="server" style="width:100%; overflow:auto; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtBuscaNP_Click"><i id="I11" runat="server" title="Filtrar y buscar" class="fa fa-search fa-2x"></i></button>--%> 
                                                        <asp:ImageButton ID="ImgPlanta" Visible="false"  runat="server" ImageUrl="~/Images/ok.png" OnClick="Imgatras_Click" ToolTip="Activa campos sobre Tabla" Width="30px" Height="30px"></asp:ImageButton>
                                                        <asp:ImageButton ID="ImgPlanta1" Visible="false" runat="server" ImageUrl="~/Images/cancel.png" OnClick="Imgadelante_Click" ToolTip="Desactiva campos sobre Tabla" Width="30px" Height="30px"></asp:ImageButton>

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
                                                    <div class="col-lg-3"> 
                                                        <h2 class="panel-title">
                                                            <a data-toggle="collapse" style=" font-size:24px; font-weight: bold;" href="#">Composición de Resultados</a>
                                                        </h2>
                                                    </div>
                                                   <div class="col-lg-2">
                                                        <asp:Label  type="text" Width="100%" style="display:inline-block; width:100%; font-weight: bold; font-size:14px;color:forestgreen;" runat="server" ID="LbCountResultado"  Text="" ></asp:Label>
                                                    </div>
                                                </div>
                                        </div>
                                     </div>
                                 </div>

                            <div class="panel-body" style="overflow: auto; width: 100%; height: 200px">                   
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
                                                <asp:TextBox ID="TTaVaeLinea" Visible="true" runat="server" Text='<%# Eval("UD_BASE") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="TLaVaeLinea" Visible="true" runat="server" Text='<%# Bind("UD_BASE") %>'></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="ZTIPO_FORMATO" Visible="true" SortExpression="ZTIPO_FORMATO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TTaVaaLinea" Visible="true" runat="server" Text='<%# Eval("ZTIPO_FORMATO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="TLaVaaLinea" Visible="true" runat="server" Text='<%# Bind("ZTIPO_FORMATO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="ZDESCRIPTIPO" Visible="true" SortExpression="ZDESCRIPTIPO">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="TTabLoVaLinea" Visible="true" runat="server" Text='<%# Eval("ZDESCRIPTIPO") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="TLabLoVaLinea" Visible="true" runat="server" Text='<%# Bind("ZDESCRIPTIPO") %>'></asp:Label>
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
           <%-- <div>
	            <a href="https://planetcalc.com/6776/" data-lang="es" data-code="" data-colors="#263238,#435863,#090c0d,#fa7014,#fb9b5a,#c25004" data-v="4288">PLANETCALC, Convertir moles a gramos y gramos a moles.</a>
	            <script src="https://embed.planetcalc.com/widget.js?v=4288">
	            </script>
            </div>--%>
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
