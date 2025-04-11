<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="LotesAutoManualS.aspx.cs" Inherits="Satelite.LotesAutoManualS" %>

<asp:Content ID="Content1" Visible="true" ContentPlaceHolderID="head" runat="server">
         <%--aqui ponemos el javascript--%>
     <script src="<%=Page.ResolveUrl("js/jquery-1.11.0.js") %>"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="<%=Page.ResolveUrl("js/bootstrap.min.js") %>"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="<%=Page.ResolveUrl("js/plugins/metisMenu/metisMenu.min.js") %>"></script>

    <script src="<%=Page.ResolveUrl("js/sb-admin-2.js") %>"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write('<div style="height:100px;"></div>');
            printWindow.document.write('<div align=center>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</div>');
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }

        function PrintPanelPaletAlv() {
            var panel = document.getElementById("<%=pnlContentsPaletAlv.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write('<div style="height:100px;"></div>');
            printWindow.document.write('<div align=center>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</div>');
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }


        function PrintQR() {
            var panel = document.getElementById("<%=pnlContentsQR.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write('<div align=center>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</div>');
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }

        function PrintFT() {
            //var panel = document.getElementById("<%=pnlContentsFT.ClientID %>").contentWindow.print();
            var panel = document.getElementById("<%=pnlContentsFT.ClientID %>").innerHTML;
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write(panel);
            printWindow.document.close();
            printWindow.focus();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }

        function PrintPaletAlv() {
            //var panel = document.getElementById("<%=pnlContents.ClientID %>").contentWindow.print();
            var panel = document.getElementById("<%=pnlContentsPaletAlv.ClientID %>").innerHTML;
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

        function submitit() {
            document.getElementById('btn').click();
        }
        function submititA() {
            document.getElementById('btnQRA').click();
        }
        function submititB() {
            document.getElementById('btnQRB').click();
        }
        function submititC() {
            document.getElementById('btnQRC').click();
        }
        function submititD() {
            document.getElementById('btnQRD').click();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div id="page_wrapper" runat="server" ><!-- /#page-wrapper  class="portada" LotesAutoManualS-->

		  <!-- /#impresora  class="portada"-->
          <div class="row">
                <div class="col-lg-10">
                </div>
                <div class="col-lg-2">
                    <div id="dvPrinters" runat="server" style="text-align:right;" visible="true" >
                        <button id="BtPrinters" type="button" runat="server" class="text-muted" style=" border-style:none; background-color:transparent; padding-top:15px;"  onserverClick="btPrinter_Click"><i title="Selección otra Impresora"  class="fa fa-qrcode fa-2x"></i></button>                                      
                    </div>
                    <div id="dvDrlist" runat="server" style="text-align:right;padding-top:15px;" Visible="false" >
                        <asp:DropDownList runat="server"  CssClass="form-control"  Width="100%"  ID="DrPrinters" tooltip="Contiene las distintas impresoras configuradas" OnSelectedIndexChanged="DrPrinters_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                    </div>
                </div>
            </div>

		  <!-- /#Mensajes"-->

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



<%--          <div runat="server" id="DvPreparado" visible="false"  style="height: 20%; width: 60%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
            <button type="button" class="close" data-dismiss="alert">&times;</button> 
            <i id="I2" runat="server" class="fa fa-exclamation-circle fa-2x"></i>
            <asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="Lbmensaje" BorderStyle="None" border="0" Width="100%" Text=" El registro seleccionado se eliminará y las existencias volverán a Listas de Pedidos pendientes ¿Desea continuar?"  />
            <div class="row" id="cuestion" visible="true" runat="server">
                <div class="col-lg-4">
				</div>
				<div class="col-lg-2">
                    <asp:Button runat="server" ID="BtnAcepta" Visible="true" tooltip="Aceptar" CssClass="btn btn-success btn-block" Width="100%"  Text="Aceptar" OnClick="checkSi_Click"/>                                                    
                </div>
                <div class="col-lg-2">
                    <asp:Button runat="server" ID="BTnNoAcepta" Visible="true" tooltip="Cancelar" CssClass="btn btn-success btn-block" Width="100%"  Text="Cancelar" OnClick="checkNo_Click"/>                 
                </div>
				<div class="col-lg-4">
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
                    <div class="col-lg-4">
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
        </div>--%>


          <%--  <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="Update">
                <ProgressTemplate>
                    <div class="centrado" style="z-index:1100;">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>



             <div class="row">
                  <div class="col-lg-12">
                    <%--<h3> Generación de lotes automáticos <i class="fa fa-long-arrow-right"></i> <small> “Para generar lotes de forma automática e imprimir código QR”  </small></h3>--%>
                      <br />
                  </div>

              </div><!-- /.row -->

            <div class="row" id="BodyAll" runat="server">
				<div class="col-lg-5">
					<div class="panel panel-default">
						<div class="panel-heading">                    
							<h3 class="panel-title" id="H3PanelTitle" runat="server" ><i class="fa fa-long-arrow-right"></i>
						<%--<button id="Button1" type="button" runat="server" class="pull-right text-muted "  OnServerClick="btnUser_Click"><i tooltip="Usuario valido para la edición casillas sobre esta página" class="fa fa-user"></i></button>
						<asp:Button ID="btnPrint" runat="server"  CssClass="btn btn-success " Width="100%" Text="Imprimir código QR" OnClientClick="return PrintPanel();" />--%>
						<asp:Label  type="text" Width="70%" visible="true" style=" font-weight: bold;"  runat="server" ID="LbQR"  Text="Listas para la generación de QR en modo automático" > </asp:Label>
							<button id="BtMasCodeManu" type="button" visible="false" style="margin-top:-5px; border-style:none; background-color:transparent;" runat="server" class="pull-right text-muted "  onserverClick="BtMasCodeQR_Click"><i title="Genera puntualmente un código de lote manual para este tipo de lote seleccionado" id="IManual" runat="server"  class="fa fa-plus-square fa-2x"></i></button>
						</h3>
						</div>
						<div class="panel-body" id="BodyLote" runat="server" >
							<div class="col-lg-10" >
								<asp:Label  type="text" Width="100%" visible="false" style=" font-weight: bold;"  runat="server" ID="LbIDLote"  Text="" > </asp:Label>
								<asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="LbLote"  Text="Tipo Lote:" > </asp:Label>
							</div>
							<div class="col-lg-2" >
							</div>
							<div class="col-lg-8" >
								<asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Lote para asignar una Secuencia"  ID="DrVariedad"  OnSelectedIndexChanged="DrVariedad_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
							</div>

							<div class="col-lg-3" >
								<asp:Button runat="server" ID="btNew" Enabled="false" tooltip="Nuevo Lote" CssClass="btn btn-success btn-block" Width="100%"  Text="Nuevo" OnClick="btnNew_Click"/>
							</div>
							<br />
							<div class="col-lg-12" >
							<br />
							<asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label1"  Text="Código Lote:" > </asp:Label>
	<%--                                <asp:Label ID="txtQRCodebis" Visible="true" style="text-align:center; color:red; font-size:18px; font-weight: bold;" runat="server" Width="100%" CssClass="form-control" ></asp:Label>--%>
							<asp:TextBox ID="txtQRCode" Visible="true" Enabled="false"  style="text-align:center; font-size:36px; font-weight: bold;" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
							<asp:TextBox ID="txtQRCodeManu" Visible="false"  style="text-align:center; background:white; font-size:36px; font-weight: bold;" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
								<%--  <br />
							<asp:Button runat="server" ID="BtnLanzaPro" tooltip="Inserta en la Tabla de GlodenSoft para importar" CssClass="btn btn-success btn-block" Width="100%"  Text="Lanza procedimiento" OnClick="BtnLanzaPro_Click"/>
							<br /> --%>
							</div>
							<br />

							<%--<div class="col-lg-12"   >                                                        
							<asp:Button runat="server" ID="BTerminado" visible="false" tooltip="Finaliza el Lote para ser importado en GoldenSoft" CssClass="btn btn-info btn-block" Width="100%"  Text="Finalizar Lote" OnClick="BTerminado_Click"/>
								<asp:Button runat="server" ID="Btfin" visible="false" tooltip="Lote cerrado para GoldenSoft" CssClass="btn btn-warning btn-block" Width="100%"  Text="Lote Procesado" OnClick="BTfin_Click"/>
							</div>                            
							<div class="col-lg-6" >
								<asp:Button ID="btnGenerate" visible="false" runat="server" Width="100%" CssClass="btn btn-warning " Text=" Volver a generar código QR manualmente" OnClick="btnGenerate_Click" />
								</div>
							<br />
							<div class="col-lg-6" >
								<asp:Button runat="server" visible="false" ID="btnNuevo" Width="100%" CssClass="btn btn-warning "  Text="Asignar nuevo código QR manualmente" OnClick="btnNuevo_Click"/>
							</div>--%>
						</div>
					</div>


					<%-- Lotes creados y escaneados--%>
					<div class="panel panel-default">
						<div class="panel-heading" style="height:40px;">
							<div class="row panel-title" runat="server">
						<%--<h3 class="panel-title">--%> 
								<div class="col-lg-1" >
									<i class="fa fa-long-arrow-right"></i> 
								</div>
								<div class="col-lg-7" >
									<asp:Label  type="text"  style="font-weight: bold; text-align:left; font-size:14px;"  runat="server" ID="lbtitleLote"  Text="Seleccionar código lote. Existen Duplicados:" > </asp:Label>
								</div>
								<div class="col-lg-1" >
									<asp:Label  type="text"  style=" font-weight: bold;text-align:left; font-size:14px;"  runat="server" ID="LbDuplicados"  Text="No" > </asp:Label>
								</div>
								<div class="col-lg-2" >
								</div>
								<div class="col-lg-1" >
									<%--<label runat="server" style="float:right;" visible="true" tooltip="Muestra los enviados a GoldenSoft" id="LBCheck" class="switch pull-right">
										<input runat="server" onclick="return submitit();" id="chkOnOff" data-toggle='tooltip' data-original-title='Muestra los enviados a GoldenSoft con Estado a 2' type="checkbox"/><span class="slider round"></span>
										<asp:Button ID="btn" runat="server" OnClick="checkListas_Click" Height="0" Width="0" CssClass="hidden" />
									</label>--%>
									<asp:ImageButton id="Imageprocesa2" visible="true" runat="server" AlternateText="ImageButton 1" ImageAlign="left" Width="25px" Height="25px" ImageUrl="images/pnegro.png" OnClick="checkListas_Click"/>
									<asp:ImageButton id="Imageprocesa1" visible="false" runat="server" AlternateText="ImageButton 1" ImageAlign="left" Width="25px" Height="25px" ImageUrl="images/pazul.png" OnClick="checkListas_Click"/>
								</div>
						<%--</h3>--%> 
							</div>
						</div>
						<div class="panel-body" id="BodyLotes" runat="server">
            
							<%--<div class="col-lg-12"  >
							<asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="lbBuscaCod"  Text="Códigos QR recibidos / finalizados:" > </asp:Label>
                                
								<asp:DropDownList runat="server" CssClass="form-control"  Width="100%"  tooltip="Contiene los código QR dados de alta y que aún no contienen registro desde formulario Scan-IT" ID="DrLotes"  OnSelectedIndexChanged="DrLotes_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
							<br />
							</div>--%>
							<div class="col-lg-12"  >
								 <div class="col-lg-6"   >
										  <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="lbBuscaCod"  Text="Códigos QR recibidos / finalizados:" > </asp:Label>
								 </div>
								<div class="col-lg-1"  >
								</div>
							   <div class="col-lg-1" id="lbFiltro" runat="server" visible="false" >
								   <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label14"  Text="Filtro:" > </asp:Label>
								 </div>
								 <div class="col-lg-3"  id="dvFiltro" runat="server" visible="false">
										<asp:TextBox ID="TxtfindTXT"  style="text-align:center;  font-weight: bold;" Text="" runat="server" Height="20px" Width="100%" CssClass="form-control" ></asp:TextBox>
								 </div>	
								 <div class="col-lg-1"  id="ImgFiltro" runat="server" visible="false">
									<asp:ImageButton id="ImgFind" visible="true" runat="server" AlternateText="Buscar Lote" ImageAlign="left" Width="15px" Height="15px" ImageUrl="images/filtro25x25.png" OnClick="FindLote_Click"/>
         
								 </div>
							 </div>
							<div class="col-lg-12"  >
								  <asp:DropDownList runat="server" CssClass="form-control"  Width="100%"  tooltip="Contiene los código QR dados de alta y que aún no contienen registro desde formulario Scan-IT" ID="DrLotes"  OnSelectedIndexChanged="DrLotes_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
							</div>
							<br />


							<div class="col-lg-6" >
								<asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server"  ID="lbBuscaCodEscaneado"  Text="Códigos QR creados / escaneados:" > </asp:Label>
							</div>
							<div class="col-lg-5" >
								<asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server"  ID="lbFechaLote"  Text="Creación: " > </asp:Label>
							</div>
							<div class="col-lg-1" > 
							 <asp:ImageButton ID="ibtDelete"  runat="server" ImageUrl="~/Images/elimina.png" OnClick="btnDeleteTabla_Click" ToolTip="Elimina el Lote creado seleccionado" Width="20px" Height="20px"></asp:ImageButton>
							</div>

							<div class="col-lg-12" >
								<asp:DropDownList runat="server" CssClass="form-control"  Width="100%"  ID="DrScaneados" tooltip="Contiene los código QR pendientes y finalizados pero no importados" OnSelectedIndexChanged="DrScaneados_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
							</div>
							

							<%--  <div class="col-lg-12" >
								<asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%"  ID="DrDuplicados"  OnSelectedIndexChanged="DrDuplicados_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
							</div>--%>
						</div>
					</div>
					<%-- Fin Lotes creados y escaneados--%>


					<%-- alerta--%>
					<div runat="server" id="alerta" visible="false" class="alert alert-info alert-dismissable">
								<button type="button" class="close" data-dismiss="alert">&times;</button> 
								<%--<a href="#" id="alerT" runat="server" class="alert alert-info alert-dismissable">--%>
									<i id="IAlert" runat="server" class="fa fa-exclamation-circle"></i>
									<asp:Label runat="server" class="alert alert-info alert-dismissable" ID="TextAlerta" BorderStyle="None" border="0" Width="100%" Text=""  />
								<%--</a> --%> 
								<asp:Button runat="server" ID="btProcesa" tooltip="Busca si el Lote tiene registro de Formulario Scan-IT" visible="false" CssClass="btn btn-success btn-block" Width="100%"  Text="Procesar Lote" OnClick="btnPorcesa_Click"/>
								<asp:Button ID="btnPrint2" runat="server" tooltip="Lote con Formulario Scan-IT para imprimir con el total de su información" visible="false" CssClass="btn btn-success " Width="100%" Text="Imprimir código QR" OnClientClick="return PrintPanel();" />
								<asp:Button ID="btnPrintPaletAlv" runat="server" tooltip="Lote con Formulario Scan-IT para imprimir con el total de su información" visible="false" CssClass="btn btn-success " Width="100%" Text="Imprimir código QR" OnClientClick="return PrintPaletAlv();" />



					</div>

					<%-- warning--%>
					<div runat="server" id="alertaLog" visible="false" class="alert alert-warning alert-dismissable">
								<button type="button" class="close" data-dismiss="alert">&times;</button> 
								<%--<a href="#" id="a1" runat="server" class="alert alert-warning alert-dismissable">--%>
									<i id="I1" runat="server" class="fa fa-exclamation-circle"></i>
									<asp:Label runat="server" class="alert alert-warning alert-dismissable" ID="TextAlertaLog" BorderStyle="None" border="0" Width="100%" Text=""  />
								<%--</a>--%>  
								<asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label9"  Text="Usuario:" > </asp:Label>
								<asp:TextBox ID="TextUser"  style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
								<asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label10"  Text="Password:" > </asp:Label>
								<asp:TextBox ID="TextPass"  TextMode="Password" style="text-align:center; font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
							<br />      
							<asp:Button runat="server" ID="Btvalidauser" tooltip="Comprobará si el usuario dispone de permisos para editar la página" CssClass="btn btn-danger btn-block" Width="100%"  Text="Validar usuario" OnClick="btnValidaUser_Click"/>
					</div>

					<%-- alerta Error--%>
					<div runat="server" id="alertaErr" visible="false" class="alert alert-danger alert-dismissable">
								<button type="button" class="close" data-dismiss="alert">&times;</button> 
								<%--<a href="#" id="alerTErr" runat="server" class="alert alert-danger alert-dismissable">--%>
								<i id="IAlertErr" runat="server" class="fa fa-exclamation-circle"></i>
								<asp:Label runat="server" class="alert alert-danger alert-dismissable" ID="TextAlertaErr" BorderStyle="None" border="0" Width="100%" Text=""  />
								<%--</a>--%>   
								<asp:Button runat="server" ID="btPorcesa" tooltip="Busca si el Lote tiene registro de Formulario Scan-IT" visible="false" CssClass="btn btn-success btn-block" Width="100%"  Text="Procesar Lote" OnClick="btnPorcesa_Click"/>
						</div>
				</div>
                                    



                   

            <div class="col-lg-7" >
                <div id="PanelQR" runat="server" class="panel panel-default" style="min-height:420px;">

                    <%--<div id="DivPrinters" runat="server" visible="true" class="panel-heading">
                        <h3 class="panel-title" runat="server" id="panelPrinter" style="color: black; font-weight:bold;"><i class="fa fa-long-arrow-right"></i> Seleccione un Lote                               
                            <button id="btnPrintA1" type="button" runat="server" visible="false" class="pull-right text-muted "  onclick="return PrintPanel();"><i title="Imprime la vista previa presentada en pantalla" class="fa fa-print"></i></button>
                        </h3>                              
                    </div>    --%>
                            
                    <div id="H1Normal" runat="server" visible="true" class="panel-heading">
                        <div class="row panel-title" runat="server">
                    <%--<h3 class="panel-title">--%> 
                            <div class="col-lg-10 panel-title">
                                <i class="fa fa-long-arrow-right">Seleccione un Lote</i>
                            </div>
                            <div class="col-lg-1" >

                                    <%--<asp:Button runat="server" ID="BtQRCodeA1" visible="true" class="pull-right text-muted " style=" border-style:none; background-color:transparent;" CssClass="pull-right text-muted" Width="100%"  title="Vista previa con QRCoder submititA()"  OnClick="BtQRCodeA1_Click" />--%>
                                <%--<button id="ButtonA1" type="button" runat="server" visible="true" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="submititA();"><i style="margin-top:-8px; color:dimgray;" title="Vista previa con QRCoder submititA()" class="fa fa-cogs fa-2x"></i></button>
                                <button id="ButtonA2" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="submititA();"><i style="margin-top:-8px; color:darkblue;" title="Vista previa con ZSing submititA()" class="fa fa-cogs fa-2x"></i></button>
                                <asp:Button ID="btnQRA" runat="server" OnClick="checkQR_Click" Height="0" Width="0" CssClass="hidden" />--%>
                                <%--<label runat="server" visible="true" id="LbDrCodeQRA" class="switch pull-right">
                                        <input runat="server" style="float:right;" onclick="return submititA();" id="ChecQRA" type="checkbox"/><span class="slider round"></span>
                                           
                                </label>--%>
                            </div>
                        <%--<h3 class="panel-title" style="color: black; font-weight:bold;"><i class="fa fa-long-arrow-right"></i> Seleccione un Lote--%>    
                            <%--<asp:DropDownList runat="server"  CssClass="form-control"  Width="20%"  ID="DrCodeQR" tooltip="Contiene las distintas APIS QR" OnSelectedIndexChanged="DrPrintQR_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />--%>
                                     
                                <div class="col-lg-1" >
                                <button id="btnPrintA1" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintPanel();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPanel()" class="fa fa-print fa-2x"></i></button>
                                <button id="btnPrintB1" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintQR();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintQR()" class="fa fa-print fa-2x"></i></button>   
                                <button id="btnPrintC1" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintFT();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintFT()" class="fa fa-print fa-2x"></i></button>
                                <button id="btnPrintD1" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintPaletAlv();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPaletAlv()" class="fa fa-print fa-2x"></i></button>
                            </div>
                        <%--  </h3>     --%>   
                        </div>
                    </div>    
                    <div id="H1Seleccion" runat="server" visible="false" class="panel-heading">
                        <div class="row panel-title" runat="server">
                            <div class="col-lg-10 panel-title" >
                                <i class="fa fa-long-arrow-right">Lote con Código QR seleccionado</i>
                            </div>
                                <div class="col-lg-1" >
                                    <%--<asp:ImageButton id="ImgQRCodeB1" visible="true" runat="server" AlternateText="ImageButton 1" ImageAlign="left" Width="30px" Height="30px" ImageUrl="images/alfabeto03.gif" OnClick="BtQRCodeA1_Click"/>
                                    <asp:ImageButton id="ImgQRCodeB2" visible="false" runat="server" AlternateText="ImageButton 1" ImageAlign="left" Width="30px" Height="30px" ImageUrl="images/alfabeto26.gif" OnClick="BtQRCodeA1_Click"/>--%>
                                      
                                    <%--   <asp:Button runat="server" ID="Button1" visible="true" class="pull-right text-muted " style=" border-style:none; background-color:transparent;" CssClass="pull-right text-muted" Width="100%"  title="Vista previa con QRCoder submititA()"  OnClick="BtQRCodeA1_Click" />
                                <button id="ButtonB1" type="button" runat="server" visible="true" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="submititB();"><i style="margin-top:-8px; color:dimgray;" title="Vista previa con QRCoder submititA()" class="fa fa-cogs fa-2x"></i></button>
                                <button id="ButtonB2" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="submititB();"><i style="margin-top:-8px; color:darkblue;" title="Vista previa con ZSing submititA()" class="fa fa-cogs fa-2x"></i></button>
                                <asp:Button ID="btnQRB" runat="server" OnClick="checkQR_Click" Height="0" Width="0" CssClass="hidden" />--%>

                            <%--<h3 class="panel-title" style="color: black; font-weight:bold;"><i class="fa fa-long-arrow-right"></i> Lote con Código QR seleccionado--%>
                                    <%--<asp:DropDownList runat="server"  CssClass="form-control"  Width="20%"  ID="DrCodeQR1" tooltip="Contiene las distintas APIS QR" OnSelectedIndexChanged="DrPrintQR_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />--%>
                                <%--<label runat="server" visible="true"  id="LbDrCodeQRB" class="switch pull-right">
                                    <input runat="server" onclick="return submititB();" id="ChecQRB"  type="checkbox"/><span class="slider round"></span>
                                    <asp:Button ID="btnQRB" runat="server" OnClick="checkQR_Click" Height="0" Width="0" CssClass="hidden" />
                                </label>--%>
                            </div>
                            <div class="col-lg-1" >
                                <button id="btnPrintA2" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintPanel();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPanel()" class="fa fa-print fa-2x"></i></button>
                                <button id="btnPrintB2" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintQR();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintQR()" class="fa fa-print fa-2x"></i></button>
                                <button id="btnPrintC2" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintFT();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintFT()" class="fa fa-print fa-2x"></i></button>
                                <button id="btnPrintD2" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintPaletAlv();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPaletAlv()" class="fa fa-print fa-2x"></i></button>
                            <%--</h3>--%>    
                            </div>    
                        </div>  
                    </div>    
                        <div id="H1Red" runat="server" visible="false" class="panel-heading">
                            <div class="row panel-title" runat="server">
                            <div class="col-lg-10 panel-title"  >
                                <i class="fa fa-long-arrow-right">Código QR YA ASIGNADO, PENDIENTE PROCESAR</i>
                            </div>
                            <div class="col-lg-1" >
                                <%-- <asp:ImageButton id="ImgQRCodeC1" visible="true" runat="server" AlternateText="ImageButton 1" ImageAlign="left" Width="30px" Height="30px" ImageUrl="images/alfabeto03.gif" OnClick="BtQRCodeA1_Click"/>
                                    <asp:ImageButton id="ImgQRCodeC2" visible="false" runat="server" AlternateText="ImageButton 1" ImageAlign="left" Width="30px" Height="30px" ImageUrl="images/alfabeto26.gif" OnClick="BtQRCodeA1_Click"/>--%>
                                <%--<button id="ButtonC1" type="button" runat="server" visible="true" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="submititC();"><i style="margin-top:-8px; color:dimgray;" title="Vista previa con QRCoder submititA()" class="fa fa-cogs fa-2x"></i></button>
                                <button id="ButtonC2" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="submititC();"><i style="margin-top:-8px; color:darkblue;" title="Vista previa con ZSing submititA()" class="fa fa-cogs fa-2x"></i></button>
                                <asp:Button ID="btnQRC" runat="server" OnClick="checkQR_Click" Height="0" Width="0" CssClass="hidden" />--%>

                        <%--<h3 class="panel-title" style="color: red; font-weight:bold;"><i class="fa fa-long-arrow-right"></i> Código QR YA ASIGNADO, PENDIENTE PROCESAR--%>
                                <%--<asp:DropDownList runat="server"  CssClass="form-control"  Width="20%"  ID="DrCodeQR2" tooltip="Contiene las distintas APIS QR" OnSelectedIndexChanged="DrPrintQR_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />--%>
                                <%-- <label runat="server" visible="true" id="LbDrCodeQRC" class="switch pull-right">
                                    <input runat="server" onclick="return submititC();" id="ChecQRC"   type="checkbox"/><span class="slider round"></span>
                                    <asp:Button ID="btnQRC" runat="server" OnClick="checkQR_Click" Height="0" Width="0" CssClass="hidden" />
                                </label>--%>
                            </div>
                            <div class="col-lg-1" >
                                <button id="btnPrintA3" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintPanel();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPanel()" class="fa fa-print fa-2x"></i></button>
                                <button id="btnPrintB3" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintQR();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintQR()" class="fa fa-print fa-2x"></i></button>
                                <button id="btnPrintC3" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintFT();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintFT()" class="fa fa-print fa-2x"></i></button>
                                <button id="btnPrintD3" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintPaletAlv();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPaletAlv()" class="fa fa-print fa-2x"></i></button>
                        <%--</h3>--%>    
                            </div>    
                        </div>  
                    </div>    
                        <div id="H1Green" runat="server" visible="false" class="panel-heading">
                            <div class="row panel-title" runat="server">
                            <div class="col-lg-10 panel-title"  >
                                <i class="fa fa-long-arrow-right">Código QR PROCESADO</i>
                            </div>
                            <div class="col-lg-1" >
                            <%--     <asp:ImageButton id="ImgQRCodeD1" visible="true" runat="server" AlternateText="ImageButton 1" ImageAlign="left" Width="30px" Height="30px" ImageUrl="images/alfabeto03.gif" OnClick="BtQRCodeA1_Click"/>
                                    <asp:ImageButton id="ImgQRCodeD2" visible="false" runat="server" AlternateText="ImageButton 1" ImageAlign="left" Width="30px" Height="30px" ImageUrl="images/alfabeto26.gif" OnClick="BtQRCodeA1_Click"/>--%>

                                <%-- <asp:Button runat="server" ID="Button3"  visible="true" class="pull-right text-muted " style=" border-style:none; background-color:transparent;" CssClass="pull-right text-muted" Width="100%"  title="Vista previa con QRCoder submititA()"  OnClick="BtQRCodeA1_Click" />
                                    <button id="ButtonD1" type="button" runat="server" visible="true" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="submititD();"><i style="margin-top:-8px; color:dimgray;" title="Vista previa con QRCoder submititA()" class="fa fa-cogs fa-2x"></i></button>
                                <button id="ButtonD2" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="submititD();"><i style="margin-top:-8px; color:darkblue;" title="Vista previa con ZSing submititA()" class="fa fa-cogs fa-2x"></i></button>
                                <asp:Button ID="btnQRD" runat="server" OnClick="checkQR_Click" Height="0" Width="0" CssClass="hidden" />--%>
                        <%--<h3 class="panel-title" style="color: LimeGreen; font-weight:bold;"><i class="fa fa-long-arrow-right"></i> Código QR PROCESADO--%>
                                <%--<asp:DropDownList runat="server"  CssClass="form-control"  Width="20%"  ID="DrCodeQR3" tooltip="Contiene las distintas APIS QR" OnSelectedIndexChanged="DrPrintQR_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />--%>
                                <%-- <label runat="server" visible="true"  id="LbDrCodeQRD" class="switch pull-right">
                                    <input runat="server" onclick="return submititD();" id="ChecQRD"  type="checkbox"/><span class="slider round"></span>
                                    <asp:Button ID="btnQRD" runat="server" OnClick="checkQR_Click" Height="0" Width="0" CssClass="hidden" />
                                </label>--%>
                            </div>
                            <div class="col-lg-1" >
                                <button id="btnPrintA4" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintPanel();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPanel()" class="fa fa-print fa-2x"></i></button>
                                <button id="btnPrintB4" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintQR();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintQR()" class="fa fa-print fa-2x"></i></button>
                                <button id="btnPrintC4" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintFT();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintFT()" class="fa fa-print fa-2x"></i></button>
                                <button id="btnPrintD4" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintPaletAlv();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPaletAlv()" class="fa fa-print fa-2x"></i></button>
                        <%--</h3>--%>  
                            </div>    
                        </div>  
                    </div>  


                             


                    <%--<Vistas de Impresora >--%>
                    <div class="panel-body" style="height:900px;" id="BodyQR" runat="server"> 

                        <asp:Panel ID="pnlContentsFT" Visible="false" Height="100%" Width="100%" runat="server">
                                    <asp:PlaceHolder ID="PlaceHolderFito"  runat="server"></asp:PlaceHolder>
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbDateFT"  Text="" > </asp:Label>
                        </asp:Panel>

                            <div class="col-lg-4" >
                        </div>

                        <div class="col-lg-7" >
                            <asp:Panel ID="pnlContents" Visible="true" runat="server">
                                    <br />
                                    <br />
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbCodigoLote"  Text="SIN CÓDIGO LOTE" > </asp:Label>
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:48px;"  runat="server" ID="LbSecuenciaLote"  Text="" > </asp:Label>
                                    <asp:PlaceHolder ID="PlaceHolder1"  runat="server"></asp:PlaceHolder>
                                    <br />
                                    <br />
                                    <div class="col-lg-12" >
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbCampoS"  Text="" > </asp:Label>
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbPlantaS"  Text="" > </asp:Label>
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbFechaS"  Text="" > </asp:Label>
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbVariedadS"  Text="" > </asp:Label>
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbCajasS"  Text="" > </asp:Label>
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbPlantasS"  Text="" > </asp:Label>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="col-lg-12" >
                                        <asp:PlaceHolder ID="PlaceHolder2"  runat="server"></asp:PlaceHolder>
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Lbcompleto"  Text="" > </asp:Label>
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbDateContents"  Text="" > </asp:Label>

                                    </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlContentsQR" Visible="false" runat="server">
                                    <br />
                                    <br />
                                <div class="col-lg-6" style="text-align: center;" >
                                    <%--&nbsp;&nbsp;<asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px; text-align:left; "  runat="server" ID="LbCodigoLoteQR"  Text="SIN CÓDIGO LOTE" > </asp:Label>--%>
                                    <asp:Label  type="text" style=" font-weight: bold; font-size:48px;"  runat="server" ID="LbSecuenciaLoteQR"  Text="" > </asp:Label>
                                    <br />
                                    <asp:PlaceHolder ID="PlaceHolderQR" runat="server"></asp:PlaceHolder>
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbDateQR"  Text="" > </asp:Label>

                                    <%--<div class="col-lg-8" >
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbCampoSQR"  Text="" > </asp:Label>
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbPlantaSQR"  Text="" > </asp:Label>
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbFechaSQR"  Text="" > </asp:Label>
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbVariedadSQR"  Text="" > </asp:Label>
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbCajasSQR"  Text="" > </asp:Label>
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbPlantasSQR"  Text="" > </asp:Label>
                                    </div>--%>

                                    </div>
                            </asp:Panel>
                                <asp:Panel ID="pnlContentsPaletAlv" Visible="false" runat="server">
                                <div class="row">
                                    <div class="col-lg-7" alig="left" >
                                        <br />
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="LbCodePaletAlv"  Text="" > </asp:Label>
                                    </div>
                                        <div class="col-lg-5" >
                                    </div>
                                </div>
                                <div class="row">
                                    <%-- <div class="col-lg-1" alig="left">
                                    </div>--%>
                                    <div class="col-lg-10"  alig="left">
                                                
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:48px;"  runat="server" ID="LbCodeQRPalteAlv"  Text="SIN CÓDIGO LOTE" > </asp:Label>        
                                    </div>
                                    <div class="col-lg-2" alig="left">

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12" alig="center">
                                        <asp:PlaceHolder ID="PlaceHolderPaletAlv"  runat="server"></asp:PlaceHolder>
                                        <br />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12" alig="center" >
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="LbTipoPlantaP"  Text="" > </asp:Label>
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="LbVariedadP"  Text="" > </asp:Label>
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="lbUnidadesP"  Text="" > </asp:Label>
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="lbNumPlantasP"  Text="" > </asp:Label>                                            
                                    </div>
                                    <br />
                                </div>
                                <div class="row">
                                    <div class="col-lg-6" >

                                    </div>
                                    <div class="col-lg-2" alig="right" >
                                        <asp:PlaceHolder ID="PlaceHolderPaletAlvMin"  runat="server"></asp:PlaceHolder>
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label18"  Text="" > </asp:Label>
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbDatePaletAlv"  Text="" > </asp:Label> 

                                    </div>
                                    <div class="col-lg-4" >

                                    </div>
                                </div>
                            </asp:Panel>


                                <br />
                        </div>
                        <div class="col-lg-1" >
                            </div>
                    </div>   
                    <%--<Final de Vistas de Impresora >--%> 
                    <asp:ImageButton id="ImgQRCodeA1" visible="false" runat="server" AlternateText="ImageButton 1" ImageAlign="left" Width="25px" Height="20px" ImageUrl="images/alfabeto03.gif" OnClick="BtQRCodeA1_Click"/>
                    <asp:ImageButton id="ImgQRCodeA2" visible="false" runat="server" AlternateText="ImageButton 1" ImageAlign="left" Width="25px" Height="20px" ImageUrl="images/alfabeto26.gif" OnClick="BtQRCodeA1_Click"/>
                    <asp:Label  type="text" Width="80%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LBReadQR"  Text="" > </asp:Label> 
                    </div> 
                </div>
            </div> 


			<%-- Ficha Campos--%>
			<div class="row" id="Div1" runat="server">
                        
                <div class="panel panel-default">
                    <div class="panel-heading">
                    <h3 class="panel-title"><i class="fa fa-long-arrow-right"></i>
                        <asp:Label  type="text" Width="70%" visible="true" style=" font-weight: bold;"  runat="server" ID="Label12"  Text="Formulario de entrada de datos desde Móvil: " > </asp:Label>
                        <asp:Label  type="text" Width="10%" visible="true" style=" font-weight: bold;"  runat="server" ID="TxtID"  Text="" ></asp:Label>

                    </h3>                              
                    </div>
					<div class="panel-body" id="BodyCampos" runat="server">


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




					</div>

				</div>
            </div>
			<%-- Botones edición Ficha Campos--%>
                <div class="row"  >
					<div class="col-lg-2" >
					</div>
					<div class="col-lg-2" >
					<%--<asp:Button runat="server" ID="btnNuevoLote" visible="false" tooltip="Para crear códigos QR ajenos e independientes" CssClass="btn btn-info btn-block" Width="100%"  Text="Nuevo" OnClick="btnNuevoLote_Click"/>--%>
						<asp:Button runat="server" ID="BtGuardaLote" visible="false" tooltip="Guardar los campos del código QR que tienes en edición en este momento en la Base de Datos" CssClass="btn btn-success btn-block" Width="100%"  Text="Guardar" OnClick="BtGuardaLote_Click"/>
						<asp:Button runat="server" ID="BtModifica" visible="true" CssClass="btn btn-success btn-block" Width="100%"  Text="Modificar" OnClick="btnModifica_Click"/> 
					</div>
                                    
					<div class="col-lg-2" >
					<asp:Button runat="server" ID="BtCancelaLote" Enabled="false" visible="true" tooltip="Cancela el código QR que tienes en edición en este momento" CssClass="btn btn-warning btn-block" Width="100%"  Text="Cancelar" OnClick="btnCancelaLote_Click"/>
					</div>
						<div class="col-lg-4" >
					</div>
					<div class="col-lg-2" >
						<asp:Button runat="server" ID="BtDelete" Enabled="false" visible="true" tooltip="Marca como borrado el código QR seleccionado" CssClass="btn btn-danger btn-block" Width="100%"  Text="Eliminar" OnClick="btnDelete_Click"/>
						<%--<asp:Button runat="server" ID="btGeneraNew" visible="false" tooltip="Genera nuevamente un código QR para el  registro seleccionado" CssClass="btn btn-info btn-block" Width="100%"  Text="Generar" OnClick="btnGeneraNew_Click"/>--%>
					</div>
				</div>
			<%-- fin Ficha Campos--%>
       <!-- /#fin row -->

           <%-- <div class="row"><!-- /.row -->
                <asp:Panel ID="pnlContents" runat="server"></asp:Panel>   
            </div><!-- /.row -->--%>
  </div><!-- /#page-wrapper -->


</asp:Content>
