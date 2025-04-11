<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="FlujosDatos.aspx.cs" Inherits="Satelite.FlujosDatos" EnableEventValidation="false" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/tree.css" rel="stylesheet"/>
    

  <%--  <link rel="canonical" href="https://trvswgnr.github.io/bs5-lightbox" data-pjax-transient="">
	<link rel="icon" class="js-site-favicon" type="image/svg+xml" href="https://trvswgnr.github.io/bs5-lightbox/favicon.svg">
	<link rel="alternate icon" class="js-site-favicon" type="image/png" href="https://trvswgnr.github.io/bs5-lightbox/favicon.png">

	<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
	<link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/highlight.js/11.2.0/styles/github.min.css">--%>


<%--    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">
    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>  
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-lightbox/0.7.0/bootstrap-lightbox.css"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-lightbox/0.7.0/bootstrap-lightbox.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-lightbox/0.7.0/bootstrap-lightbox.min.css"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-lightbox/0.7.0/bootstrap-lightbox.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>--%>


    <meta http-equiv="Expires" content="0"> 
    <meta http-equiv="Last-Modified" content="0">
    <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"> 
    <meta http-equiv="Pragma" content="no-cache">


     <!-- Bootstrap Core CSS
    <link href="css/bootstrap.css" rel="stylesheet" />
    <!-- Bootstrap Core CSS
    <link href="css/bootstrap.min.css" rel="stylesheet" /> -->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">      
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnAcepta" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>        
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





                            <%--<div runat="server" id="DvPreparado" visible="false"  style=" width: 30%;z-index: 888;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155); overflow:auto; position: fixed;" class="alert alert-grey centrado position-fixed">
                                <i id="I1" runat="server" class="fa fa-exclamation-circle fa-2x"></i>
                                <asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="Lbmensaje" BorderStyle="None" border="0" Width="100%" Text=" Se eliminarán los registros actuales con una nueva consulta ¿Desea continuar?"  />
                                <div class="row" id="cuestion" visible="true" runat="server">
                                    <div class="col-lg-6">
                                        <asp:Button runat="server" ID="BtnAcepta" Visible="true" tooltip="Aceptar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Aceptar" OnClick="checkSi_Click"/>                                                    
                                        </div>
                                    <div class="col-lg-6">
                                        <asp:Button runat="server" ID="BTnNoAcepta" Visible="true" tooltip="Cancelar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Cerrar" OnClick="checkNo_Click"/>                 
                                    </div>
                                </div>
                                    <div class="row" id="Asume" visible="false" runat="server">
                                    <div class="col-lg-12">
                                        <asp:Button runat="server" ID="btnasume" Visible="true" tooltip="Aceptar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Aceptar" OnClick="checkOk_Click"/>
                                    </div>
                                </div>
                            </div>--%>
                        </ContentTemplate>
            </asp:UpdatePanel>

         <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">      
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BtnCerra" EventName="Click" />
                </Triggers>
                <ContentTemplate>     
                 <div id="DivFoto" runat="server"  class="centrado position-fixed" style="z-index:0; width:1000px; height:1000px; background-color: rgba(50,50,50,0.5);">
                        <asp:Button runat="server" ID="BtnCerra" Visible="true" tooltip="Cerrar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="40px"  Text="Cerrar" OnClick="Cierra_Click"/>                                                    
                        <div class="row">
                            <%--<a id="AFoto" runat="server"  data-toggle="lightbox" href="#demolightbox" class="span2 thumbnail">--%> 
                                <%--<asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="LbTitulo" BorderStyle="None" border="0" Width="100%" Text=" Etiqueta 1"  />
                                <asp:ImageButton id="ImgFoto" runat="server" src="img_fjords.jpg"  onclick="ImgFoto_Click"/>
                                <asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="LbPie" BorderStyle="None" border="0" Width="100%" Text=" Etiguetq 2"  />--%>
                            <%--</a>--%> 
                <%--    </div>
                 </div>
                </ContentTemplate>--%>
            <%--</asp:UpdatePanel>--%>




            <asp:UpdateProgress ID="Progress3" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                <ProgressTemplate>
                    <div class="centrado position-fixed" style="z-index:1100;position: fixed;">
                        <img src="images/rastreando.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdateMenu">
                <ProgressTemplate>
                    <div class="centrado position-fixed" style="z-index:1100;position: fixed;">
                        <img src="images/rastreando.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
              <%-- <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdateCampos">
                <ProgressTemplate>
                    <div class="centrado position-fixed" style="z-index:1100;position: fixed;">
                        <img src="images/rastreando.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>

 


              <%--Segundos Mensajes --%>
            <div runat="server" id="DivCuestion" visible="false"  style=" width: 30%;z-index: 1999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155); overflow:auto;position: fixed;" class="alert alert-grey centrado">
                <%--<button type="button" class="close" data-dismiss="alert">&times;</button>--%>
                <i id="I3" runat="server" class="fa fa-exclamation-circle fa-2x"></i>
                <asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="lbCuestion" BorderStyle="None" border="0" Width="100%" Text=" Se eliminarán los registros actuales con una nueva consulta ¿Desea continuar?"  />
                <div class="row" id="Div4" visible="true" runat="server">
                    <div class="col-lg-4">
                     </div>
                    <div class="col-lg-4">
                    <asp:Button runat="server" ID="Button2" Visible="true" tooltip="Aceptar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Aceptar" OnClick="Aceptar_Click"/>                                                    
                    </div>
                    <div class="col-lg-4">
                    </div>
                </div>
            </div>


 





      <div id="page_wrapper" runat="server" ><!-- /#page-wrapper  class="portada"-->
          <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="10000" OnTick="Timer1_Tick"></asp:Timer> 





              
            <div runat="server" id="alerta" visible="false" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <i id="IAlert" runat="server" class="fa fa-exclamation-circle"></i>
                <asp:Label runat="server" class="alert alert-info alert-dismissable" ID="TextAlerta" BorderStyle="None" border="0" Width="100%" Text=""  />
            </div>

            <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Lberror"  Text="" > </asp:Label>

            <div class="row" id="DivArchivos" runat="server" visible="true">
                    <div class="col-lg-8">
                    </div>

            </div><!-- /.row data-parent="#accordion"-->
            <br />
            <div class="row" >
                <div class="col-lg-12"  >
                    <div class="bs-example" id="MisMenus" runat="server">
                    <ul class="nav nav-tabs" style="margin-bottom: 15px;" >
                        <li id="Menu0" class="active" runat="server" ><asp:LinkButton ID="aMenu0" runat="server" OnClick="HtmlAnchor_Click" >FLUJOS DE TRABAJOS GENERALES</asp:LinkButton></li>                        
                        <%--<li id="Menu1" class="" runat="server" ><asp:LinkButton ID="aMenu1" runat="server" OnClick="HtmlAnchor_Click" >EMPLEADOS</asp:LinkButton></li>--%>
                    </ul>
                </div>
                </div>
            </div>

            <%--Menus--%>
            <div id="PanelGralFiltro" Enabled="true" visible="true" runat="server" class="panel panel-default">
                <div class="panel-heading" runat="server" id="PanelFiltro" >
                    <div class="row">
                            <div class="col-lg-12"> 
                            <h4 class="panel-title">
                                <a data-toggle="collapse" style=" font-weight: bold;"> Recogida muestra datos desde Flujo seleccionado
                                    <%--fa-box-open             <asp:Label  type="text"  Width="90%" style=" font-weight: bold; color:green;"  runat="server" ID="LbCabecera"  Text="" > </asp:Label>--%>                           
                                </a>                            
                            </h4>
                        </div>                            
                    </div>
                </div>

                <div id="Div5" runat="server"  class="panel-collapse collapse in">
                     <%--filtros Fecha--%>
                    <div class="panel panel-default">           
                        <div class="panel-body" id="Div6" visible="true" runat="server">
                                <div class="row" runat="server" id="DivGralConsulta">                                                                       
                                    <div class="col-sm-1" >
                                        <asp:Label  visible="false" type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label7"  Text="Fecha:" > </asp:Label>
                                            
                                    </div>
                                    <div class="col-sm-1" >
                                        <asp:TextBox visible="false" runat="server" CssClass="form-control" Enabled="false"   Width="100%" tooltip="Introduzca Fecha Desde para filtrar datos"  ID="TxtDesde"  Font-Bold="True" />
                                    </div>
                                    <div class="col-sm-1" >
                                        <asp:Label  type="text" visible="false"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label8"  Text="Fecha:" > </asp:Label>
                                    </div>
                                    <div class="col-sm-1" >
                                        <asp:TextBox runat="server" visible="false" CssClass="form-control" Enabled="false"  Width="100%" tooltip="Introduzca Fecha Desde para filtrar datos"  ID="TxtHasta"  Font-Bold="True" />
                                    </div>
                                    <div class="col-sm-3" >

                                        <div class="col-sm-1" >
                                            <asp:ImageButton visible="false" id="ImgFechamenos" Enabled="false"  runat="server" ToolTip="Atrás Meses" ImageAlign="right" Width="25px" Height="30px" ImageUrl="images/menos.png" OnClick="BtFechaMas_Click"/>
                                            </div>
                                        <div class="col-sm-5" >
                                            <asp:DropDownList visible="false" ID="DrAno" CssClass="form-control" Enabled="false" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrMeses_Changed"></asp:DropDownList>
                                            </div>
                                        <div class="col-sm-5" >
                                            <asp:DropDownList visible="false" ID="DrMeses" CssClass="form-control" Enabled="false" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrMeses_Changed"></asp:DropDownList>
                                            </div>
                                        <div class="col-sm-1" >
                                            <asp:ImageButton visible="false" id="ImgFechamas"  Enabled="false" runat="server" ToolTip="Adelante Meses" ImageAlign="left" Width="25px" Height="30px" ImageUrl="images/mas.png" OnClick="BtFechaMas_Click"/>
                                        </div>
                                    </div>

                                    <div class="col-sm-3" >
                                            <asp:UpdatePanel ID="Update2" runat="server" UpdateMode="Conditional">      
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btUltimo" EventName="Click" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <%--<a data-toggle="collapse" onclick="submitit8()" href="#"></a>--%>
                                                <asp:Button ID="btUltimo" visible="false"  runat="server" OnClick="Ultima_Click" Height="0" Width="0" CssClass="hidden" />
                                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="LbUltConsulta"  Text="" > </asp:Label>
                                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="LdDia" Text="" > </asp:Label>
                                                </ContentTemplate>
                                        </asp:UpdatePanel> 
                                        <%--<asp:LinkButton id="Lanza80" type="button" runat="server" style="width:40%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onClick="Lanza80_Click" Text='<i title="Consulta General con filtro o sin filtro según selección sobre la base de datos de RecoDat. Elimina la consulta actual visualizada en pantalla." class="fa fa-ban fa-2x"></i>'></asp:LinkButton>--%>    
                                    </div>
                                    <div class="col-lg-2" >
                                        <asp:LinkButton visible="false" id="BtGralConsulta" type="button" runat="server" style="width:40%; text-align:right; border-style:none; background-color:transparent;" class="pull-right text-muted "  onClick="BtCuestionGralConsulta_Click" Text='<i title="Consulta General con filtro o sin filtro según selección sobre la base de datos de RecoDat. Elimina la consulta actual visualizada en pantalla." class="fa fa-outdent fa-2x"></i>'></asp:LinkButton>    
                                    </div>
                                </div>

                                 <div class="row">      
                                    <div runat="server" class="col-lg-1" >
                                        <input type="image"  class="pull-right text-muted " title="Limpia el filtro general seleccionado a la derecha" src="images/ordencarga25x25.png" onserverclick="ImgOrdenMin_Click"  runat="server" id="ImgOrdenMin" style="border: 0px; " />    
                                        <%--<img src="https://drive.google.com/open?id=1OqE1KIvtsNDjmtwMJpCxY0Bz19ALiuQE" alt="placeholder" style="border: 0px; overflow:auto;" />--%> 

                                    </div>
                                    <div  runat="server" class="col-lg-2" > 
                                            <asp:DropDownList runat="server" Enabled="false"  CssClass="form-control" style="position:relative; background-color:#fff; font-size:14px;"  Width="100%"  ID="DrVistaEmpleado" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrVistaEmpleado_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                    </div>
                                     <div class="col-sm-1" >
                                         <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label2"  Text="Flujos:" > </asp:Label>
                                     </div>
                                     <div class="col-sm-2" >
                                         <asp:DropDownList ID="DrFlujo" Enabled="false" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrFlujo_Changed"></asp:DropDownList>
                                     </div>
                                     <div class="col-sm-2" >
                                         <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label1"  Text="Estado del Flujo:" > </asp:Label>
                                     </div>
                                     <div class="col-sm-2" >
                                         <asp:DropDownList Enabled="false" ID="DrFlujoEstado" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrFlujoEstado_Changed"></asp:DropDownList>
                                     </div>
                                     <div class="col-sm-2" >
                                     </div>
                                </div>


                            <br />
                            <br />


                            <div class="row">
                                <div class="col-lg-7"> 
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" style=" font-weight: bold; background-color:#c1c1c1"> Para el Archivo documental seleccionado:
                                        </a> 
                                        &nbsp&nbsp<label runat="server" visible="true" style="color:olivedrab ;"  id="LbArchivoDoc" title="Archivo documental seleccionado"></label>
                                    </h4>
                                    <br />
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" style=" font-weight: bold; background-color:#c1c1c1"> Estás en el Estado:
                                        </a> 
                                    </h4>
                                </div>   
								<div class="col-lg-5" runat="server" visible="false" id="DUserWacom">
									<div class="col-lg-2">
										<label runat="server" visible="true"  id="Label3" title="Envío de Documentación a firmar para Usuarios del Sistema Satelite. Cuando conecte su WACOM aparecerá este Documento para firmar, sin necesidad de conectarse al Sistema Satelite.">Enviar a Usuario:</label>
									</div>
									<div class="col-lg-8">
										 <asp:DropDownList ID="DropUserWa" style=" position:absolute;" CssClass="form-control" Width="55%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListaUserWacom_SelectedIndexChanged"> </asp:DropDownList>
										<asp:ImageButton id="ImageButton1" visible="true" runat="server" ToolTip="" ImageAlign="right" Width="100px" Height="30px" ImageUrl="images/blanco.png" OnClick="EnviaWacom_Click"/>
										<asp:ImageButton id="ImgWacom" visible="true" runat="server" ToolTip="Enviar el Documento a su WACOM" ImageAlign="right" Width="35px" Height="35px" ImageUrl="images/envio.png" OnClick="EnviaWacom_Click"/>

									</div>
								</div>
                            </div>
                            <br />
                            <div class="row"> 
                                <asp:UpdatePanel ID="UpdateMenu" runat="server" UpdateMode="Conditional">      
                                    <ContentTemplate>
                                        <div class="row"> 
                                            <div class="col-lg-2" style="text-align:center;"> 
                                                 <asp:ImageButton id="ImageEjecucion" Enabled="false" visible="false" runat="server" ToolTip="Estado Ejecutandose"  Width="40px" Height="40px" ImageUrl="images/loading-buffering.gif" OnClick="checknada_Click"/>
                                                 <asp:ImageButton id="ImagenEstado" Enabled="false" visible="false" runat="server" ToolTip="Estado del Flujo Seleccionado"  Width="40px" Height="40px" ImageUrl="images/pnegro.png" OnClick="checkTodo_Click"/>
                                            </div>
                                            <div class="col-lg-2" style="text-align:center;"> 
                                                  <asp:ImageButton id="ImgDetiene" Enabled="false" visible="false" runat="server" ToolTip="Detiene el Flujo en el Archivo documental seleccionado"  Width="40px" Height="40px" ImageUrl="images/boxcloseorange.png" OnClick="btFinFlujo_Click"/>
                                                  <asp:ImageButton id="ImgInicia" Enabled="false" visible="false" runat="server" ToolTip="Inicia el Flujo en el Archivo documental seleccionado"  Width="40px" Height="40px" ImageUrl="images/box-opengreen.png" OnClick="btIniFlujo_Click"/>
                                            </div>
                                            <div class="col-sm-1" >  
                                                  <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowEmpleado"  Text="" > </asp:Label>
                                            </div>
                                            <div class="col-sm-1" >
                                                <asp:LinkButton id="LinkFlujo" Enabled="false" type="button" runat="server" style="width:40%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onClick="BtiniciaFlujo_Click" Text='<i title="Inicia el Flujo de trabajo seleccionado." class="fa fa-address-card-o fa-2x"></i>'></asp:LinkButton>    
                                            </div>
                                
                                            <%--<div class="col-lg-2" style="text-align:center;"> 
                                                <asp:Button runat="server" ID="BtAtras" Enabled="false" Visible="false" tooltip="Aceptar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Rechazar Seleccionados" OnClick="RetrocedeEstado_Click"/>
                                             </div>
                                            <div class="col-sm-1" >
                                            </div>
                                            <div class="col-lg-2" style="text-align:center;"> 
                                                   <asp:Button runat="server" ID="Button1"  Enabled="false" Visible="true" tooltip="Actualiza sobre pantalla lo procesado hasta el momento por el Servicio Procesos RioEresma" BorderStyle="None" border="0" CssClass="btn btn-success btn-block" Width="100%" AutoPostBack="true"  Text="Actualizar" OnClick="ActualizaDatos_Click"/>
                                            </div>--%>
                                        </div>
                                       <%-- <div class="col-lg-1" style="text-align:center;"> 
                                             <asp:ImageButton id="ImgAyuda" visible="true" Enabled="false" runat="server" ToolTip="Visualiza en pantalla la descripcion del Estado en que se encuentra" ImageAlign="right" Width="30px" Height="30px" ImageUrl="images/ayuda.png" OnClick="BtAyuda_Click"/>
                                        </div> --%>



                                        <%-- <div class="col-lg-1" style="text-align:center;"> 
                                              <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowEmpleado"  Text="" > </asp:Label>
                                         </div>--%>
                                        <%--<div class="col-lg-1" style="text-align:center;"> 
                                        </div>--%>
                                        <div class="row"> 
                                             <div class="col-lg-2" style="text-align:center;"> 
                                            </div>
                                             <div class="col-lg-2" style="text-align:center;"> 
                                            </div>
                                            <div class="col-lg-1" style="text-align:center;"> 
                                                <asp:Button runat="server" ID="BtAtras" Enabled="false" Visible="true" tooltip="Envía al Estado definido cómo anterior los registros seleccionados" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Atrás" OnClick="RetrocedeEstado_Click"/>
                                             </div>
                                            <div class="col-lg-1" style="text-align:center;"> 
                                                <asp:Button runat="server" ID="BtSiguiente" Enabled="false" Visible="true" tooltip="Envía al Estado definido cómo siguiente los registros seleccionados" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Siguiente" OnClick="RetrocedeEstado_Click"/>
                                             </div>
                                            <div class="col-lg-1" style="text-align:center;"> 
                                                <asp:Button runat="server" ID="BtAlternate" Enabled="false" Visible="true" tooltip="Envía al Estado definido cómo rechazado los registros seleccionados" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Rechazado" OnClick="RetrocedeEstado_Click"/>
                                             </div>
                                            <div class="col-lg-1" style="text-align:center;"> 
                                                <asp:Button runat="server" ID="BtFinal" Enabled="false" Visible="true" tooltip="Envía al Estado definido cómo finalizado los registros seleccionados" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Finalizar" OnClick="RetrocedeEstado_Click"/>
                                             </div>
                                            <div class="col-lg-3" style="text-align:center;"> 
                                            </div>
                                             <div class="col-lg-1" style="text-align:center;"> 
                                                 <asp:ImageButton id="ImgAyuda" visible="true" Enabled="false" runat="server" ToolTip="Visualiza en pantalla la descripcion del Estado en que se encuentra" ImageAlign="right" Width="30px" Height="30px" ImageUrl="images/ayuda.png" OnClick="BtAyuda_Click"/>
                                             </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>



                            </div>
                            <div class="row"> 
                                 <asp:UpdatePanel ID="UpdatePanelEje" runat="server" UpdateMode="Conditional">      
                                <%--<Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="BtnAcepta" EventName="Click" />
                                </Triggers>--%>
                                    <ContentTemplate>
                                        <div class="col-lg-2" style="text-align:center;"> 
                                            <label runat="server" visible="false"  id="LbimgEstado" title="">Estado</label>
                                            <label runat="server" visible="false"  id="Lbejecutando" title="">Ejecutando...</label>
                                        </div>
                                        <div class="col-lg-2" style="text-align:center;"> 
                                              <label runat="server" visible="false"  id="LbAutomatico" title="">Inicio de proceso automático</label>
                                        </div>
                                        <div class="col-sm-1" >  

                                        </div>
                                        <div class="col-sm-1" >
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                   </div>
               </div>





        

                    <%--Fin filtros--%>

              
               <%-- <div class="panel panel-default">     
                    <div class="panel-heading" runat="server" id="Div21" >

                    </div>
                    <%--filtros Columnas--%>
                   <%-- <div class="panel-body" id="Div35" visible="true" runat="server">
                            <div class="row"> 
                                <div class="col-sm-1" style="text-align:right;" >
                                    <asp:linkbutton id="lbFilCodigo" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Código:"></asp:linkbutton>

                                    <button id="BtCodigo" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="IContent" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2" >
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca uno o más Códigos de Empleado separados por coma. Ejemplo: 00000001, 000000232 "  ID="TxtCodigo"  Font-Bold="True" />
                                </div>

                                <div class="col-sm-2" style="text-align:right;">
                                    <asp:linkbutton id="LinkNombre" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Nombre:"></asp:linkbutton>
                                    <button id="BtNombre" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="INombre" style="color:darkred;" runat="server" title="No deberá contener estos Datos." class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2" >
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca un nombre de trabajador. Ejemplo: Rosa "  ID="TxtNombre"  Font-Bold="True" />
                                </div>
                                <div class="col-sm-2" style="text-align:right;">
                                    <asp:linkbutton id="LinkApellidos" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Apellidos:"></asp:linkbutton>
                                    <button id="BtApellido" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="IApellido" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2" >
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca un apellido de trabajador. Ejemplo: Sanchez"  ID="TxtApellidos"  Font-Bold="True" />
                                </div>
                                    <div class="col-sm-1" >
                                </div>
                            </div>
                        </div>--%>
                    <%--Fin filtros Columnas--%>

<%--                    <div class="panel-body" id="Div36" visible="true" runat="server">
                            <div class="row"> 
                                    
                                <div class="col-sm-1" style="text-align:right;">
                                    <asp:linkbutton id="LinkCentro" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Centro:"></asp:linkbutton>
                                    <button id="BtCentro" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="ICentro" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2" >
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca uno o más Centros de Trabajo separados por coma. Ejemplo: 001, 002 "  ID="TxtCentro"  Font-Bold="True" />
                                </div>
                                <div class="col-sm-2" style="text-align:right;">
                                    <asp:linkbutton id="LinkCategoria" ForeColor="Black"  onClick="lbFilClose_Click" runat="server" Text="Categoría:"></asp:linkbutton>
                                    <button id="BtCategoria" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="ICategoria" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2" >
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca una o más Categorías separadas por coma. Ejemplo: S1, S2, H1"  ID="TxtCategoria"  Font-Bold="True" />
                                </div>
                                <div class="col-sm-2" style="text-align:right;">
                                    <asp:linkbutton id="LinkVivienda" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Vivienda:"></asp:linkbutton>
                                    <button id="BtVivienda" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="IVivienda" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2" >
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca uno o más Códigos de Empleado separados por coma. Ejemplo: Propia, Casa1 "  ID="TxtVivienda"  Font-Bold="True" />
                                </div>
                                    <div class="col-sm-1" >
                                    </div>
                                </div>
                        </div>
                </div> --%> 

              </div>  

              <%--Menus usuario--%>
            <div id="DivUser" visible="false" runat="server" style="height:600px; width:100%;" class="panel panel-default">
                <br />

                <br />
            </div>  

			 







          
            <%--gvEmpleado--%>
            <div class="panel-heading" runat="server" visible="true" id="PanelCabecera" >

                  <div id="DivAyuda" visible="false" runat="server" style=" height:300px; width:100%; overflow:auto;"  class="col-lg-12">
                        <asp:TextBox ID="LbAyuda" Width="100%" Height="100%"  TextMode="MultiLine" style="Font-Size:20px;" Enabled="false" Columns="50" Rows="100" runat="server"
                            Text="-Flujos y Estados de Trabajo: Un flujo contiene una serie de Estados por los que pasa un registro documental en su ciclo de vida documental.

Los Estados son momentos de ciclo de vida de un registro por el cual debe pasar según el criterio personal de un Usuario o Proceso. Las posibles opciones que tiene cada Estado son las siguientes:

Atrás. Devuelve un registro a su estado anterior.
Adelante: Avanza el registro a su Estado siguiente.
Alternativo: Contiene cualquier otro Estado que se haya definido como una alternativa a un supuesto contemplado incluyendo el salto a otro Flujo de Trabajo diferente.
Fin: Envía el registro a su Estado final en el Flujo de Trabajo
"/>
                    </div> 

                <div id="VistaGrid" visible="true" runat="server"  class="row">
                    <%--gvEmpleado--%>
                    <div class="panel panel-default">  
                                        <br />
                            <div class="row">       
                                 <div class="col-sm-1" >
                                      <asp:Label  type="text"  Width="20%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label4"  Text="Orden:" > </asp:Label>
                                      <button id="BtnAsc" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnAsc_Click"><i title="Orden ascendente de la columna seleccionada" class="fa fa-sort-alpha-asc fa-2x"></i></button>
                                 </div>
                                 <div class="col-lg-1" >
                                     <asp:DropDownList ID="DrListaColumna" style=" position:absolute;" CssClass="form-control" Width="85%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListaColumnas_SelectedIndexChanged"> </asp:DropDownList>
                                 </div>

                                 <div class="col-sm-1" style="text-align:right;">
                                        <button id="BtnDesc" type="button" runat="server" visible="true" style=" position: absolute; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnDesc_Click"><i title="Orden descendente de la columna seleccionada" class="fa fa-sort-alpha-desc fa-2x"></i></button>
                                        <asp:Label  type="text"  Width="100%" style=" text-align:right; font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label5"  Text="Filas:" > </asp:Label>
                                </div>



<%--                                <div class="col-sm-1" >
                                    <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label19"  Text="Filas:" > </asp:Label>
                                </div>--%>
                                <div class="col-sm-1" >                              
                                    <asp:DropDownList ID="ddCabeceraPageSize" style=" position:absolute;" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvEmpleado_PageSize_Changed">  
                                    </asp:DropDownList>  
                                    </div>
                                <div class="col-sm-5" >
                                </div>

                                <div class="col-sm-2" >
                                </div>
                                <div class="col-sm-1"> 
                                    <button id="BtPrintReport" type="button" runat="server" visible="false" style=" border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="btDriver_Click">
                                        <i title="Descarga el driver para tabletas de firma WACOM" class="fa fa-cog fa-2x"></i>
                                    </button>
                                    <button id="btPrintCabecera" type="button" runat="server" visible="false" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnSDK_Click"><i title="Descarga SDK para tabletas de firma WACOM" class="fa fa-cogs fa-2x"></i></button>
                                    <button id="BtnDescarFirma" type="button" runat="server" visible="false" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnSing_Click"><i title="Descarga formulario de firma para tabletas WACOM" class="fa fa-tablet fa-2x"></i></button>

                                </div>
                            </div>
                            <div class="row">

                            <asp:UpdatePanel ID="UpdatePanelGV" runat="server" UpdateMode="Conditional">      
                            <%--<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnAcepta" EventName="Click" />
                            </Triggers>--%>
                            <ContentTemplate>
                                    <%--Segundos Mensajes --%>
								<div id="Div1" runat="server"   class="panel-body">

								<div runat="server" id="DivManat" style=" width: 100%; height: 100%; z-index:11000; background-color: rgba(50,50,50,0.6); overflow:auto;position: fixed;" class="centrado">
									<%--<button type="button" class="close" data-dismiss="alert">&times;</button>   style="z-index:0; width:1000px; height:1000px; background-color: rgba(50,50,50,0.5);"--%>
									<%--<i id="I4" runat="server" class="fa fa-exclamation-circle fa-2x"></i>--%>
									<br />
									<br />
									<asp:Label runat="server" ID="LbTitulo" BorderStyle="None" ForeColor="White" border="0" Width="100%" Text=""  />
       
									<video autoplay="autoplay"  class="centrado" runat="server" width="1000" height="1000" loop="loop" id="videofoto" preload="auto" muted="muted" poster="Images/fresas.png">                                       
										<source src="Images/SlowMotion.mp4" type="video/mp4">
										<source src="video.webm" type="video/webm">
									</video>

									<video autoplay="autoplay"  class="centrado" runat="server" width="1000" height="1000" loop="loop" id="video_background" preload="auto" muted="muted" poster="Images/fresas.png" controls playsinline>                                       
										<source src="Images/SlowMotion.mp4" type="video/mp4">
										<source src="video.webm" type="video/webm">
									</video>
       
									<div class="row" id="Div8" visible="true" runat="server">
										<div class="col-lg-12">
											<%--<asp:ImageButton id="ImgFoto" runat="server" src=""  onclick="ImgFoto_Click"/>--%>
											</div>
										<div class="col-lg-4">
											<%--<asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="LbPie" BorderStyle="None" border="0" Width="100%" Text=" Etiguetq 2"  />--%>
											</div>
										<div class="col-lg-6">
										</div>
										<div class="col-lg-1">
											<asp:Button runat="server" ID="BtnCerra" Visible="true" tooltip="Cerrar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block"  Text="Cerrar" OnClick="Cierra_Click"/>                                                    
										</div>
										<div class="col-lg-1">
										</div>
									</div>
								</div>

								<div id="DivRegistros" runat="server" style="overflow:auto; height: 200px:" class="panel-body"> <%-- AutoGenerateSelectButton="True"--%> 
									<%--Automatico--%>
									<asp:GridView ID="gvEmpleado"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
										AllowSorting="true" OnSorting="gvEmpleado_OnSorting"
										CellPadding="4"  GridLines="None"  OnRowDataBound="gvEmpleado_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="5" OnRowCommand="gvEmpleado_RowCommand" DataKeyNames="ZID"
										oonselectedindexchanged="gvEmpleado_SelectedIndexChanged"  OnRowEditing="gvEmpleado_RowEditing" OnRowCancelingEdit="gvEmpleado_RowCancelingEdit" OnRowUpdating="gvEmpleado_RowUpdating" 
										OnRowDeleting="gvEmpleado_RowDeleting" onpageindexchanging="gvEmpleado_PageIndexChanging"  >
									<RowStyle />                     
										<Columns>

											<asp:TemplateField HeaderText="Todos"  >
											<HeaderTemplate>
												<asp:CheckBox ID="chkb1Empleado" ToolTip="Selecciona o no todos registros" runat="server" OnCheckedChanged="sellectAllEmpleado"
													AutoPostBack="true" />
											</HeaderTemplate>
												<itemstyle horizontalalign="Center" verticalalign="Middle" />
												<itemtemplate>
													<asp:CheckBox ID="chbItemEmpleado" runat="server" />
												</itemtemplate>
												<itemstyle horizontalalign="center" />
											</asp:TemplateField>


											<asp:TemplateField HeaderText="Id1" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid1" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:LinkButton ID="BTn1" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="true" runat="server"        
														CommandName="LId1"  Width="10px" Height="10px"></asp:LinkButton>
													<asp:Label ID="Lid1" visible="false" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid1" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId1"  Width="10px" Height="10px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id2" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid2" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid2" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid2" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId2"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id3" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid3" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid3" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid3" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId3"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>
                               
											<asp:TemplateField HeaderText="Id4" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid4" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid4" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid4" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId4"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id5" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid5" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid5" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid5" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId5"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id6" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid6" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid6" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid6" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId6"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id7" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid7" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid7" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid7" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId7"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id8" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid8" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid8" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid8" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId8"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id9" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid9" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid9" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid9" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId9"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id10" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid10" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid10" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid10" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId10"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id11" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid11" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid11" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid11" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId11"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id12" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid12" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid12" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid12" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId12"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id13" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid13" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid13" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid13" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId13"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>
                               
											<asp:TemplateField HeaderText="Id14" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid14" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid14" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid14" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId14"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id15" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid15" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid15" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid15" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId15"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id16" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid16" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid16" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid16" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId16"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id17" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid17" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid17" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid17" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId17"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id18" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid18" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid18" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid18" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId18"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id19" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid19" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid19" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid19" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId19"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id20" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid20" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid20" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid20" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId20"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>



											<asp:TemplateField HeaderText="Id21" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid21" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid21" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid21" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId21"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id22" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid22" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid22" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid22" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId22"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id23" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid23" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid23" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid23" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId23"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>
                               
											<asp:TemplateField HeaderText="Id24" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid24" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid24" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid24" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId24"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id25" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid25" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid25" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid25" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId25"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id26" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid26" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid26" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid26" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId26"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id27" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid27" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid27" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid27" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId27"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id28" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid28" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid28" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid28" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId28"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id29" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid29" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid29" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid29" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId29"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

											<asp:TemplateField HeaderText="Id30" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
													<asp:TextBox ID="Tid30" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
													<asp:Label ID="Lid30" visible="false" runat="server" Text=""></asp:Label>
													<asp:ImageButton ID="Xid30" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
														CommandName="LId30"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>



										<asp:TemplateField HeaderText="Id31" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid31" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid31" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid31" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId31"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id32" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid32" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid32" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid32" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId32"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id33" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid33" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid33" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid33" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId33"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id34" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid34" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid34" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid34" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId34"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id35" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid35" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid35" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid35" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId35"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id36" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid36" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid36" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid36" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId36"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id37" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid37" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid37" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid37" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId37"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id38" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid38" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid38" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid38" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId38"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id39" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid39" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid39" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid39" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId39"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id40" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid40" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid40" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid40" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId40"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id41" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid41" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid41" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid41" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId41"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id42" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid42" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid42" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid42" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId42"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id43" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid43" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid43" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid43" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId43"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id44" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid44" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid44" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid44" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId44"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id45" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid45" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid45" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid45" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId45"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id46" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid46" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid46" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid46" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId46"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id47" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid47" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid47" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid47" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId47"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id48" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid48" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid48" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid48" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId48"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id49" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid49" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid49" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid49" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId49"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField HeaderText="Id50" visible="false" SortExpression="ZId1">
											<EditItemTemplate>
												<asp:TextBox ID="Tid50" visible="false" runat="server" Text="" class="gridListAinput"></asp:TextBox>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label ID="Lid50" visible="false" runat="server" Text=""></asp:Label>
												<asp:ImageButton ID="Xid50" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible="false" runat="server" ImageUrl="~/Images/leer.png"         
												CommandName="LId50"  Width="100px" Height="100px"></asp:ImageButton>
											</ItemTemplate>
										</asp:TemplateField>											

										</Columns>
										<SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
										<EditRowStyle BackColor="#eaf5dc" />                    
									</asp:GridView>                    
                                
								</div>
							</div>

                                <div runat="server" id="FichaCampos" visible="false"  style=" width: 90%;z-index: 10999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
									<%--<div class="centrado" visible="false" runat="server" id="DivCampos0"  height: 700px; max-height: 700px;>--%>
									<button id="Button1" type="button" runat="server" visible="true" style=" text-align:right; position:absolute; top:10; left:97%; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="CierraRegistro_Click"><i title="Cierra la ficha Catalográfica" class="fa fa-times fa-2x"></i></button>
									<h3 id="H1" runat="server" style="text-align:center;font-size:16pt; font-weight: bold;" visible="true"> Ficha Catalográfica </h3>
						            <h3 id="HNota" runat="server"  style="text-align:center;font-size:12pt;" visible="false"> <strong>NOTA:</strong> Este Archivo Documental no dispone de vínculo a Documentación electrónica. Añada un nombre para la tabla de objetos editando este Archivo Documental desde <a href="#" runat="server" onserverclick="Aarchivos_clik">aquí</a>  </h3>
						
						            <br />


                                    <div runat="server" id="DivCampos" visible="false">
						            <div class="row" runat="server" id="DivReg0" visible="false">
							            <div class="col-lg-2" runat="server" id="DivLabelA0">
								            <asp:TextBox id="lbL0" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
								            <asp:TextBox id="LBCOLL0" visible="false" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" ></asp:TextBox>
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
                                
						                <%-- Fin campos--%> 

								<div runat="server" id="DivCampos0" visible="false">  <%--style=" height:560px; width: 80%;z-index: 1000;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black;position: fixed; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">--%>

                                
                                    <h3 id="H3Nombre" runat="server" style="text-align:center;font-size:14pt;font-weight: bold;" visible="true"> Documentación adjunta 
					                </h3>

                                    <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;text-align:left; padding:6px; font-size:18px;"  runat="server" ID="Lbusuario"  Text="Usuario:" > </asp:Label>


									<div class="row" runat="server" id="DivFicheros" visible="false" style="width:100%; height:180px;" >                        
										<div id="DivTreeDoc" visible="false" runat="server" style=" height:180px;overflow:auto;" class="col-lg-8">
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

										<div id="DivGridDoc" runat="server" style=" height:180px;overflow:auto;" class="col-lg-8"> <%-- AutoGenerateSelectButton="True"--%>                                  
											<br />
											<asp:GridView ID="gvLista"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
												AllowSorting="true" OnSorting="gvLista_OnSorting"
												CellPadding="4"  GridLines="None"  OnRowDataBound="gvLista_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="10" OnRowCommand="gvLista_RowCommand" DataKeyNames="ZID"
												oonselectedindexchanged="gvLista_SelectedIndexChanged"  OnRowEditing="gvLista_RowEditing" OnRowCancelingEdit="gvLista_RowCancelingEdit" OnRowUpdating="gvLista_RowUpdating" 
												onpageindexchanging="gvLista_PageIndexChanging" EnablePersistedSelection="True"  >
											<RowStyle />                     
												<Columns>

													<asp:CommandField ButtonType="Image" 
																EditImageUrl="~/Images/edicion.png" 
																ShowEditButton="True" 
																CancelImageUrl="~/Images/cancelar20x20.png" 
																CancelText="" 
																DeleteText="" 
																UpdateImageUrl="~/Images/guardar18x18.png"          
																UpdateText="" />          

													<asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
															<ItemTemplate>
																<a id="linkDown" href= "Download.ashx?file=images/cat1.jpg">
																	<asp:ImageButton ID="ibtSubeCarga" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/descargar.png"         
																	CommandName="SubeCarga" ToolTip="Devuelve la línea a órdenes de Carga" Width="30px" Height="30px"></asp:ImageButton>
																</a>
                                                   
															</ItemTemplate>
															<ItemStyle Height="8px"></ItemStyle>                                
													</asp:TemplateField>

													<asp:TemplateField HeaderText="Selección Todos">
																<HeaderTemplate>
																	<asp:CheckBox ID="chkb1" ToolTip="Trata todos los documentos seleccionados con la misma Categoría." runat="server" OnCheckedChanged="sellectAll"
																		AutoPostBack="true" />
																</HeaderTemplate>
																	<itemstyle horizontalalign="Center" verticalalign="Middle" />
																	<itemtemplate>
																		<asp:CheckBox ID="chbItem" runat="server" />
																	</itemtemplate>
																	<itemstyle horizontalalign="center" />
													</asp:TemplateField>

													<asp:TemplateField HeaderText="Identificador" visible="false" SortExpression="ZID">
														<EditItemTemplate>
																<asp:TextBox ID="Tabid"  runat="server" Text='<%# Eval("ZID") %>' class="gridListAinput"></asp:TextBox>
														</EditItemTemplate>
														<ItemTemplate>
																<asp:Label ID="Labid"  runat="server" Text='<%# Bind("ZID") %>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>

													<asp:TemplateField HeaderText="Nombre"  SortExpression="ZDESCRIPCION">
														<EditItemTemplate>
																<asp:TextBox ID="TabNombre"  runat="server" Text='<%# Eval("ZDESCRIPCION") %>' class="gridListAinput"></asp:TextBox>
														</EditItemTemplate>
														<ItemTemplate>
																<asp:Label ID="LabNombre"  runat="server" Text='<%# Bind("ZDESCRIPCION") %>'></asp:Label>
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
													<asp:TemplateField HeaderText="Categoria" SortExpression="ZCATEGORIA">
														<EditItemTemplate>
																<asp:TextBox ID="TabCategoria" runat="server" Text='<%# Eval("ZCATEGORIA") %>' class="gridListDinput"></asp:TextBox>
														</EditItemTemplate>
														<ItemTemplate>
																<asp:Label ID="LabCategoria" runat="server" Text='<%# Bind("ZCATEGORIA") %>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
														<asp:TemplateField HeaderText="Peso" SortExpression="ZPESO">
														<EditItemTemplate>
																<asp:TextBox ID="TabPeso" runat="server" Text='<%# Eval("ZPESO") %>' class="gridListDinput"></asp:TextBox>
														</EditItemTemplate>
														<ItemTemplate>
																<asp:Label ID="LabPeso" runat="server" Text='<%# Bind("ZPESO") %>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Usuario" SortExpression="ZUSER">
														<EditItemTemplate>
																<asp:TextBox ID="Tabuser" runat="server" Text='<%# Eval("ZUSER") %>' class="gridListAinput"></asp:TextBox>
														</EditItemTemplate>
														<ItemTemplate>
																<asp:Label ID="LabLuser" runat="server" Text='<%# Bind("ZUSER") %>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>     
													<asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
															<ItemTemplate>
																<asp:ImageButton ID="EliminaCarga" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/elimina.png"
																CommandName="EliminaCarga" ToolTip="Elimina el fichero del Servidor" Width="30px" Height="30px"></asp:ImageButton>
															</ItemTemplate>
															<ItemStyle Height="8px"></ItemStyle>                                
													</asp:TemplateField>

												</Columns>
												<SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
												<EditRowStyle BackColor="#eaf5dc" />   
												<rowstyle Height="20px" />
											</asp:GridView>   
										</div>   
										<%--<div id="SubeFile" runat="server" visible="true" class="col-lg-4" style="margin-top: 15px;" >
							                <button id="btSubirFicheros" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="text-muted "  onserverClick="BtnSubirFicheros_click"><i title="Subir Ficheros al Servidor" class="fa fa-refresh fa-3x"></i></button>
										</div>--%>
										<div id="AutoFile" runat="server" visible="true" class="col-lg-4" style="margin-top: 15px;height:180px;" >
											<button id="btSubirFicheros" type="button" runat="server" visible="true"  class="btn btn-success btn-block"  onserverClick="BtnSubirFicheros_click">Subir Ficheros</button>
											<iframe id="FrSubirFicheros" runat="server" visible="false"  style="width:100%; height:170px; border:none;" scrolling="no" sandbox="allow-same-origin allow-forms allow-scripts" src="importFiles.aspx"></iframe><%--src="filesuploads.html"></iframe>  --%>
											<asp:Label ID="Label6" Style="color:olivedrab ;text-align:right;" runat="server" Text=""></asp:Label>
										</div>

                                          <br />
                                          
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


					                    <div class="row" id="DivUpload" visible="false" runat="server">
                                            <br />
                                            <div class="col-lg-1">
                                                <%--<button type="button" visible="true" id="BtnActualizar" style="width:100%;" runat="server" onServerClick="BtnActualizar_click" class="btn btn-primary">Actualizar Registros</button>--%>
							                    <button id="BtnActualizar" type="button" runat="server" visible="false" style=" border-style:none; background-color:transparent;" class="text-muted "  onserverClick="BtnActualizar_click"><i title="Actualizar Documentos en el Servidor" class="fa fa-refresh fa-3x"></i></button>
						                    </div>
						                    <div class="col-lg-1" style="text-align:right;">
							                    <%--<asp:Label runat="server" class="alert alert-grey" ID="Label6" BorderStyle="None" border="0" Width="65%" Text="Jerarquía:"  />--%>
							                    <button id="BtInsertCategoria" type="button" runat="server" visible="false" style=" border-style:none; width:25px; height:30px; background-color:transparent;" class="text-muted "  onserverClick="BtInsertCategoria_Click"><i title="Actualiza navegación de la Categoría sobre la Jerarquía seleccionada" class="fa fa-refresh fa-2x"></i></button>
                                            </div>
                                            <div class="col-lg-2">
							                        <asp:DropDownList id="DrDependienteDe" Visible="false" runat="server" Width="100%" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrCategoria_SelectedIndexChanged" />
               				                        <asp:TextBox id="TxtNuevaCat" runat="server" Visible="false" style="width:100%;border-style:none;background-color:#ffffff;"  class="form-control" placeholder="Nueva Categoría"></asp:TextBox>
						                    </div>
						                    <div class="col-lg-2" style="text-align:left;"> 
							                    <button id="BtnCategoria" type="button" runat="server" visible="false" style=" border-style:none; width:25px; height:30px; background-color:transparent;" class="text-muted "  onserverClick="BtnCategoria_Click"><i title="Añade una nueva Categoría a la lista" class="fa fa-plus-square fa-2x"></i></button>
							                    <button id="BtnlinkCategoria" type="button" runat="server" visible="false" style=" border-style:none; width:25px; height:30px; background-color:transparent;" class="text-muted "  onserverClick="BtnlinkCategoria_Click"><i title="Añade un link o un ruta en vez de Fichero" class="fa fa-link fa-2x"></i></button>
							                    <button id="BtnDeleteCategoria" type="button" runat="server" visible="false" style=" border-style:none; width:25px; height:30px; background-color:transparent;" class="text-muted "  onserverClick="BtnDeleteCategoria_Click"><i title="Elimina la Categoría de la lista" class="fa fa-eraser fa-2x"></i></button>
							                    <button id="BtnsaveRuta" type="button" runat="server" visible="false" style=" border-style:none; width:25px; height:30px; background-color:transparent;" class="text-muted "  onserverClick="BtnSaveRutCat_Click"></button>
							                    <button id="BtRegistraFiles" type="button" runat="server" visible="false" class="btn btn-primary" tooltip="Insertar Documentos asociados a este registro"  onserverClick="btnCloseFiles_Click">Registrar Ficheros</button>
							                    <%--<div style="width:60%;">
							                    </div>--%>
						                    </div>
                                            <div class="col-lg-1">
												 <button id="BtnUp" type="button" runat="server" visible="true" style="text-align:right; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="BtnCierraUp_click"><i title="Cierra documentación adjunta" class="fa fa-th fa-3x"></i></button>
						                    </div>
						                    <div class="col-lg-1" style="text-align:left;">
                                            </div>
                                            <div class="col-lg-1" style="text-align:left;">
							                    <asp:Label runat="server" visible="false"  class="alert alert-grey" ID="Label10" BorderStyle="None" border="0"  Text="Categoría:"  />
						                    </div>
                                            <div class="col-lg-2" style="text-align:right;"> 
							                    <asp:DropDownList id="DrCategoria" Visible="false"  runat="server" Width="100%"  CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="DrCategoria_SelectedIndexChanged" />
						                    </div>

                                        </div>
                                     </div>  
						            <br />
						            <br />
     


                                    </div>     
                               </div>
								</div>
                            </ContentTemplate>
                        </asp:UpdatePanel>      
                    </div>
                </div>

                </div>
            </div>
            <%--final gvEmpleado--%>
        </div>
    <%--Final page_wrapper--%>



                    

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
              function submititTodo() {
                  document.getElementById('Button1').click();
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
              function submitit8() {
                  document.getElementById('btUltimo').click();
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
                  var prtContent = document.getElementById('<%= gvEmpleado.ClientID %>');
                  //prtContent.border = 0; //set no border here
                  var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
                  WinPrint.document.write(prtContent.outerHTML);
                  WinPrint.document.close();
                  WinPrint.focus();
                  WinPrint.print();
                  WinPrint.close();
              }


              function Cargando() {
                  document.getElementById("carga").style.display = "";
              }
              windows.onbeforeunload = Cargando;

             </script>

        <!-- jQuery Version 1.11.0 -->
    <script src="js/jquery-1.11.0.js"></script>
 
    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>
    
    <!-- Metis Menu Plugin JavaScript -->
    <script src="js/plugins/metisMenu/metisMenu.min.js"></script>

    <!-- Morris Charts JavaScript -->


    <!-- Custom Theme JavaScript Menu derecha correcto -->
    <script src="js/sb-admin-2.js"></script>







</asp:Content>
