<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="OrdenCompra.aspx.cs" Inherits="Satelite.OrdenCompra" MaintainScrollPositionOnPostback="true" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
             <%--aqui ponemos el javascript--%>
     <script src="<%=Page.ResolveUrl("js/jquery-1.11.0.js") %>"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="<%=Page.ResolveUrl("js/bootstrap.min.js") %>"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="<%=Page.ResolveUrl("js/plugins/metisMenu/metisMenu.min.js") %>"></script>

    <script src="<%=Page.ResolveUrl("js/sb-admin-2.js") %>"></script>

    <script type="text/javascript">
 <%--       function drag(ev) {
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

--%>


        function PrintPanel() {
            var panel = document.getElementById("<%=panelContentsLotes.ClientID %>");
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
            var panel = document.getElementById("<%=panelContentsQRLotes.ClientID %>");
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
            var panel = document.getElementById("<%=panelContentsPaletAlvLotes.ClientID %>");
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
            //var panel = document.getElementById("<%=panelContentsFTLotes.ClientID %>").contentWindow.print();
            var panel = document.getElementById("<%=panelContentsFTLotes.ClientID %>").innerHTML;
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
            //var panel = document.getElementById("<%=panelContentsPaletAlvLotes.ClientID %>").contentWindow.print();
            var panel = document.getElementById("<%=panelContentsPaletAlvLotes.ClientID %>").innerHTML;
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

        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args) {
            if ($get('<%=ColHorizontal2.ClientID%>') != null) {
                    xPos = $get('<%=ColHorizontal2.ClientID%>').scrollLeft;
                yPos = $get('<%=ColHorizontal2.ClientID%>').scrollTop;
            }
         }

         function EndRequestHandler(sender, args) {
             if ($get('<%=ColHorizontal2.ClientID%>') != null) {       
                 $get('<%=ColHorizontal2.ClientID%>').scrollLeft = xPos;
                $get('<%=ColHorizontal2.ClientID%>').scrollTop = yPos;
            }
        }
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="pagevistaform" runat="server"><!-- /#page-wrapper -->

   <%--         <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="Update">
                <ProgressTemplate>
                    <div class="centrado" style="z-index:1100;">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>


        
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




<%--          <div runat="server" id="DvPreparado" visible="false"  style="height: 80%; width: 50%; top:500px; z-index: 1999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
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
           
           <%--<asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Lberror"  Text="" > </asp:Label>--%>

            <div class="row">
                  <div class="col-lg-12">

                        <div class="col-lg-8" >
                             <%--   <h3 id="H3Titulo" runat="server" visible="true"> Selección de pedidos <i class="fa fa-long-arrow-right"></i> <small> “Orden de Compras ”  </small>
                                    <asp:Label  type="text"  style=" font-weight: bold; font-size:14px;"  runat="server" ID="Lbhost1"  Text="" > </asp:Label>
                                </h3>
                                <h3 id="H3Desarrollo" runat="server" style="color:red;" visible="false"> DESARROLLO --> Selección de pedidos <i class="fa fa-long-arrow-right"></i> <small> “Orden de Compras ”  </small>
                                    <asp:Label  type="text"  style=" font-weight: bold; font-size:14px; color:black;"  runat="server" ID="Lbhost2"  Text="" > </asp:Label>
                                </h3>--%>
                        </div>

<%--                        <div id="VistaOrden" runat="server" visible ="true" class="col-lg-4">
                            <div runat="server" class="col-lg-1" style=" top:16px;">
                                    <input type="image"  class="pull-right text-muted " src="images/orden25x25.png" onserverclick="ImgOrdenMin_Click"  runat="server" id="ImgOrdenMin" style="border: 0px; " />    
                            </div>
                            <div  runat="server" class="col-lg-11" style=" top:16px;"> 
                                    <asp:DropDownList runat="server"  CssClass="form-control" style="position:relative; background-color:#ffffff; font-size:20px;"  Width="100%"  ID="DrOrdenMin" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrOrdenMin_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                    <asp:Button ID="Button1" runat="server" OnClick="check1_Click" Height="0" Width="0" BackColor="" CssClass="hidden" />
                            </div>
                        </div>
                        <div id="VistaOrdenNO" runat="server" visible ="true" class="col-lg-5">
                        </div>--%>

              </div><!-- /.row data-parent="#accordion"-->

            <div class="row">
              <div class="col-lg-12">
                 <div class="bs-example">
                    <ul class="nav nav-tabs" style="margin-bottom: 15px;">
                        <li id="Menu4" class="" visible="false" runat="server" ><asp:LinkButton ID="aMenu4" runat="server" OnClick="HtmlAnchor_Click" >LOTES AUTOMÁTICOS</asp:LinkButton></li>
                        <li id="Menu3" class="" visible="false" runat="server" ><asp:LinkButton ID="aMenu3" runat="server" OnClick="HtmlAnchor_Click" >VERTICAL</asp:LinkButton></li>
                        <li id="Menu6" class="" visible="true" runat="server" ><asp:LinkButton ID="aMenu6" runat="server" OnClick="HtmlAnchor_Click" >PROCESO COMPRA PLANTA</asp:LinkButton></li>
                        <li id="Menu2" class="" visible="true" runat="server" ><asp:LinkButton ID="aMenu2" runat="server" OnClick="HtmlAnchor_Click" >LISTA ÓRDENES COMPRAS</asp:LinkButton></li>
                        <li id="Menu5" class="" visible="false"  runat="server" ><asp:LinkButton ID="aMenu5" runat="server" OnClick="HtmlAnchor_Click" >INFORMES / IMPRESIÓN</asp:LinkButton></li>
                   </ul>
                </div>
             </div>
          </div>


          


            <%--<Vistas de Ficha >--%>
          <div runat="server" id="BodyQR" visible="false"  style="height: 700px; width: 40%;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
            <%--<div class="panel-body" visible="false" style="height:900px;" id="BodyQR" runat="server"> --%>
                <button id="BtClose" type="button" runat="server" class="text-muted" style=" border-style:none; background-color:transparent; padding-top:15px;"  onserverClick="btclose_Click"><i title="Cierra esta tarjeta"  class="fa fa-times fa-2x"></i></button>                                      
                <asp:Panel ID="panelContentsFTOrdenCompra" Visible="false" Height="100%" Width="100%" runat="server">
                            <asp:PlaceHolder ID="PlaceHolderFitoOrdenCompra"  runat="server"></asp:PlaceHolder>
                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbDateFTOrdenCompra"  Text="" > </asp:Label>
                </asp:Panel>

                <asp:Panel ID="PaneContPrinter" Visible="false" Height="100%" Width="100%" runat="server">

                    <div class="row" id="drow1" Visible="false" runat="server">
                        <div class="col-lg-2" id="dr1col1" Visible="false" runat="server" >
                            <asp:Label  type="text" Visible="false" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbF1C1"  Text="" > </asp:Label>
                            <asp:PlaceHolder ID="PlaceHolderContPrinterF1C1"  runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="col-lg-4" id="dr1col2" Visible="false" runat="server">
                             <asp:Label  type="text" Visible="false" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="Label27"  Text="" > </asp:Label>
                            <asp:PlaceHolder ID="PlaceHolderContPrinterF1C2"  runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="col-lg-2" id="dr1col3" Visible="false" runat="server">
                            <asp:Label  type="text" Visible="false" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbF1C3"  Text="" > </asp:Label>
                            <asp:PlaceHolder ID="PlaceHolderContPrinterF1C3"  runat="server"></asp:PlaceHolder>
                        </div>
                    </div>





                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbDatePrinter"  Text="" > </asp:Label>
                    
                </asp:Panel>

                 <div class="col-lg-2" >
                </div>

                <div class="col-lg-9" >
                    <asp:Panel ID="panelContentsOrdenCompra" Visible="true" runat="server">
                            <br />
                            <br />
                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbCodigoLoteOrdenCompra"  Text="CÓDIGO LOTE:" > </asp:Label>
                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbSecuenciaLoteOrdenCompra"  Text="" > </asp:Label>
                            <asp:PlaceHolder ID="PlaceHolderOrdenCompra"  runat="server"></asp:PlaceHolder>
                            <br />
                            <br />
                            <div class="col-lg-12" >
                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbEmpresaSOrdenCompra"  Text="" > </asp:Label>
                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbProveedorSOrdenCompra"  Text="" > </asp:Label>
                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="lbSeriePedidoSOrdenCompra" Text="" > </asp:Label>
                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="lbNumPedidoSOrdenCompra"  Text="" > </asp:Label>
                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbLineaPedidoSOrdenCompra"   Text="" > </asp:Label>
                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbProductoSOrdenCompra"   Text="" > </asp:Label>
                            </div>
                            <br />
                            <br />
                            <div class="col-lg-12" >
                                <div class="col-lg-10" >
                                    <div class="col-lg-2" >
                                        <asp:PlaceHolder ID="PlaceHolderOrdenCompraMin"  runat="server"></asp:PlaceHolder>
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="LbVariedadLSOrdenCompra"  Text="" > </asp:Label>
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbDateContents"  Text="" > </asp:Label>
                                    </div>
                                 </div>
                            </div>
                    </asp:Panel>

                    <asp:Panel ID="panelContentsQROrdenCompra" Visible="false" runat="server">
                            <br />
                            <br />
                        <div class="col-lg-6" style="text-align: center;" >
                            <%--&nbsp;&nbsp;<asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px; text-align:left; "  runat="server" ID="LbCodigoLoteQR"  Text="SIN CÓDIGO LOTE" > </asp:Label>--%>
                            <asp:Label  type="text" style=" font-weight: bold; font-size:48px;"  runat="server" ID="LbSecuenciaLoteQROrdenCompra"  Text="" > </asp:Label>
                            <br />
                            <asp:PlaceHolder ID="PlaceHolderQROrdenCompra" runat="server"></asp:PlaceHolder>
                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbDateQROrdenCompra"  Text="" > </asp:Label>

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

                    <asp:Panel ID="panelContentsPaletAlvOrdenCompra" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-lg-9" alig="left" >
                                <br />
                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="LbCodePaletAlvOrdenCompra"  Text="" > </asp:Label>
                            </div>
                                <div class="col-lg-3" >
                            </div>
                        </div>
                        <div class="row">
                            <%-- <div class="col-lg-1" alig="left">
                            </div>--%>
                            <div class="col-lg-4"  alig="left">
                                                
                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:48px;"  runat="server" ID="LbCodeQRPalteAlvOrdenCompra"  Text="SIN CÓDIGO LOTE" > </asp:Label>        
                            </div>
                            <div class="col-lg-8" alig="left">

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12" alig="center">
                                <asp:PlaceHolder ID="PlaceHolderPaletAlvOrdenCompra"  runat="server"></asp:PlaceHolder>
                                <br />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12" alig="center" >
                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="LbEmpresaPOrdenCompra"  Text="" > </asp:Label>
                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="LbProveedorPOrdenCompra"  Text="" > </asp:Label>
                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="lbSeriePedidoPOrdenCompra"  Text="" > </asp:Label>
                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="lbNumPedidoPOrdenCompra"  Text="" > </asp:Label>   
                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="LbLineaPedidoPOrdenCompra"  Text="" > </asp:Label>
                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="LbProductoPOrdenCompra"  Text="" > </asp:Label>
                            </div>
                            <br />
                        </div>
                        <div class="row">
                            <div class="col-lg-7" >

                            </div>
                            <div class="col-lg-3" alig="right" >
                                <asp:PlaceHolder ID="PlaceHolderPaletAlvMinOrdenCompra"  runat="server"></asp:PlaceHolder>
                                <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="LbVariedadPOrdenCompra"  Text="" > </asp:Label>
                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbDatePaletAlvOrdenCompra"  Text="" > </asp:Label> 
                            </div>
                            <div class="col-lg-2" >

                            </div>
                        </div>
                    </asp:Panel>

                        <br />
                </div>
<%--                <div class="col-lg-1" >
                    </div>--%>
                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LBReadQROrdenCompra"  Text="" > </asp:Label> 
            </div>   
            <%--<Final de Vistas de Ficha >--%> 






             <div class="panel-body" visible="false" style="width:100%;height:1000px; overflow:auto;"  runat="server" id="accordion8">        
                <div class="row" style="width:100%;  overflow:auto;">  
                    <%--<Ordenes de compra dos>--%>
                    <div class="col-lg-12" style="width:100%;  overflow:auto;">               
                                <div class="panel panel-default">
                                    <div class="panel-heading" runat="server" id="Div3" >
                                        <div class="row">
                                                <div class="col-lg-8"> 
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" style=" font-weight: bold;" onclick="submiti4()"  href="#collapse4"> SELECCION PEDIDOS COMPRAS: Lista pedidos pendientes</a>                                             
                                                    </h4>
                                                </div>
                                                <div class="col-lg-3">
                                                </div>
                                                <%--<div class="col-lg-1">
                                                    <input type="image"  Visible="false" class="pull-right text-muted " src="images/filtro25x25.png" onserverclick="ImageFiltro_Click"  runat="server" id="ImageFiltro2" style="border: 0px; " />    
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:DropDownList runat="server" Visible="false"  CssClass="form-control"  Width="100%"  ID="DropDownList1" tooltip="Contiene el filtro para las órdenes de pedidos" OnSelectedIndexChanged="DrSelectFiltro_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" /> 
                                                </div>--%>
                                                <div class="col-lg-1"> <%--onserverClick="PrintGridView"--%>
                                                    <%--<button id="Button5" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onclick="doPrintGRControl();"><i title="Imprime la vista previa presentada en la lista de órdenes de carga" class="fa fa-print fa-2x"></i></button>--%>
                                                    <button id="Button10" type="button" runat="server" style="width:50%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtGralConsulta_Click"><i title="Consulta General con filtro o sin filtro según selección sobre la base de datos de GoldenSoft. Elimina la consulta actual visualizada en pantalla." class="fa fa-bars fa-2x"></i></button>
                                                </div>
                                        </div>
                                        <asp:Button ID="Button8" runat="server" OnClick="check4_Click" Height="0" Width="0" CssClass="hidden" />
                                        <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label7"  Text="" > </asp:Label>
                                    </div>
                                    <br />

                                  <div id="PanelgeneralFiltro2" runat="server" class="panel panel-default">
                                    <div class="panel-heading" runat="server" id="Div5" >
                                        <div class="row">
                                                <div class="col-lg-8"> 
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" style=" font-weight: bold;" onclick="submitit3()" href="#collapse3"> Filtros aplicados                                             
                                                        <%--<asp:Label  type="text"  Width="90%" style=" font-weight: bold; color:green;"  runat="server" ID="LbFiltros"  Text="" > </asp:Label>--%>
                                                        <%--<asp:TextBox runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="TextBox1"  Font-Bold="True" />--%>
                                                    </a>
                                                </h4>

                                            </div>
                                
                                        </div>
                                        <asp:Button ID="Button9" runat="server" OnClick="check3_Click" Height="0" Width="0" CssClass="hidden" />
                                        <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label8"  Text="" > </asp:Label>
                                    </div>
                                    <br />

                                    <div id="Div6" runat="server"  class="panel-collapse collapse in">                               
                                        <div class="panel panel-default">            
                                            <div class="panel-body">
                                                <div class="row">
                                                    <!-- consultas predefinidas -->
                                                    <div class="col-lg-1">
                                                     </div>
                                                    <div class="col-lg-1">
                                                          <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:right;"  runat="server" ID="Label9"  Text="Empresa:" > </asp:Label>
                                                     </div>
                                                    <div class="col-lg-1"> 
                                                    <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="DrConsultasA"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                     <div class="col-lg-1">
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:right;"  runat="server" ID="Label10"  Text="Proveedor:" > </asp:Label>
                                                     </div>
                                                    <div class="col-lg-1"> 
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Proveedor desde para filtrar datos"  ID="DropDownList3"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                     <div class="col-lg-1">
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:right;"  runat="server" ID="Label11"  Text="Número Pedido:" > </asp:Label>
                                                     </div>
                                                    <div class="col-lg-1">
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Número Pedido hasta para filtrar datos"  ID="DropDownList4"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                     <div class="col-lg-1">
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:right;"  runat="server" ID="Label12"  Text="Línea Pedido:" > </asp:Label>
                                                     </div>
                                                    <div class="col-lg-1"> 
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Línea Pedido Envio para filtrar datos"  ID="DropDownList5"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                     <div class="col-lg-1">
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:right;"  runat="server" ID="Label13"  Text="Serie Pedido:" > </asp:Label>
                                                     </div>
                                                    <div class="col-lg-1"> 
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Serie Pedido para filtrar datos"  ID="DropDownList6"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>

                                                    <div class="col-lg-1">
                                                        <%--<asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label14"  Text="" > </asp:Label>--%>
                                                        <button id="Button11" type="button" runat="server"  style="width:50%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="Carga_Combos_vacios_Click"><i title="Consulta con filtro o sin filtro según selección, sobre los datos recuperados de la base de datos en local" class="fa fa-eraser fa-2x"></i></button>
                                                    </div>                           
                                                </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                                    <div id="Div7" runat="server"  class="panel-collapse collapse in">                               
                                        <div class="panel panel-default">
                                            <div class="panel-body" >
                                                <div class="row">
                                                    <div class="col-sm-1" >
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label17"  Text="Filas:" > </asp:Label>
                                                    </div>
                                                    <div class="col-sm-1" >                              
                                                       <asp:DropDownList ID="ddControlPageSize2" CssClass="form-control" style=" position:absolute;" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvControl_PageSize_Changed">  
                                                        </asp:DropDownList>
                                                     </div>
                                                    <div class="col-sm-1" >
                                                    </div>
                                                    <div class="col-sm-2" >
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="Label18"  Text="" > </asp:Label>
                                                    </div>
                                                    <div class="col-sm-7" >
                                                    </div>

                                                </div>
                                                <!-- barra vertical no actualiza la posicion. style="height:300px; overflow-x:hidden; overflow-:auto;" -->
                                                <div class="row" >
                                                    <div id="Div8" runat="server" style="overflow:auto;" class="panel-body"> <%-- AutoGenerateSelectButton="True"--%>  
                                                        <asp:GridView ID="gvAmbos"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                                            AllowSorting="true" OnSorting="gvControl_OnSorting"
                                                            CellPadding="4"  GridLines="None"  OnRowDataBound="gvControl_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="2" OnRowCommand="gvControl_RowCommand" DataKeyNames="ID"
                                                            oonselectedindexchanged="gvControl_SelectedIndexChanged"  OnRowEditing="gvControl_RowEditing" OnRowCancelingEdit="gvControl_RowCancelingEdit" OnRowUpdating="gvControl_RowUpdating" 
                                                            onpageindexchanging="gvControl_PageIndexChanging" >
                                                        <RowStyle />       
                                                            <Columns>

                                                           <%-- <asp:CommandField ButtonType="Image" 
                                                                EditImageUrl="~/Images/editar20x20.png" 
                                                                ShowEditButton="True" 
                                                                CancelImageUrl="~/Images/cancelar20x20.png" 
                                                                CancelText="" 
                                                                DeleteText="" 
                                                                UpdateImageUrl="~/Images/guardar18x18.png"          
                                                                UpdateText="" 
                                                                />           --%>  
                                                    
                                                            <asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                            <ItemTemplate>
                                                               <asp:ImageButton ID="ibtbajaOrden" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/etiqueta25x25.png"
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
                                                            <asp:TemplateField HeaderText="Proveedor" SortExpression="CLIENTEPROVEEDOR">
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
                                                             <asp:TemplateField HeaderText="Número Pedido" SortExpression="NUMERO">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabONumero" runat="server" Text='<%# Eval("NUMERO") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabONumero" runat="server" Text='<%# Bind("NUMERO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Linea Pedido" SortExpression="LINEA">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOLinea" runat="server" Text='<%# Eval("LINEA") %>' class="gridContCinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOLinea" runat="server" Text='<%# Bind("LINEA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Serie Pedido"  SortExpression="SERIE_PED">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOSerie" runat="server" Text='<%# Eval("SERIE_PED") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOSerie" runat="server"  Text='<%# Bind("SERIE_PED") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Producto" SortExpression="PRODUCTO">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOArticulo" runat="server" Text='<%# Eval("PRODUCTO") %>' class="gridContBinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOArticulo" runat="server" Text='<%# Bind("PRODUCTO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Descripción" SortExpression="DESCRIPCION">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabODescriciob" runat="server" Text='<%# Eval("DESCRIPCION") %>' class="gridContBinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabODescriciob" runat="server" Text='<%# Bind("DESCRIPCION") %>'></asp:Label>
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
                                                             <asp:TemplateField HeaderText="Uds Pendientes" SortExpression="UDSPENDIENTES">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOPendientes" runat="server" Text='<%# Eval("UDSPENDIENTES") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOPendientes" runat="server" Text='<%# Bind("UDSPENDIENTES") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tipo Planta" SortExpression="TIPO_PLANTA">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOPlanta" runat="server" Text='<%# Eval("TIPO_PLANTA") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOPlanta" runat="server"  Text='<%# Bind("TIPO_PLANTA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Variedad" SortExpression="VARIEDAD">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOPalet" runat="server" Text='<%# Eval("VARIEDAD") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOPalet" runat="server" Text='<%# Bind("VARIEDAD") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Familia" SortExpression="FAMILIA">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOFamilia" runat="server" Text='<%# Eval("FAMILIA") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOfamilia" runat="server" Text='<%# Bind("FAMILIA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                             
                    
                                                            </Columns>
                                                            <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
                                                            <EditRowStyle BackColor="#f2eed5" />

                                                        </asp:GridView>                                  
                                                    </div>
                                                </div>
                                    
                                            </div>
                                        </div>
                                    </div>
                        </div>
                        <%---Fin <Ordenes de compra dos> LoteAuto--%>
                    </div>
                 </div>
                <div class="row" style="width:100%; height:100%; ">  
                    <div class="col-lg-12" style="width:100%; height:1000px;  ">  
                            <iframe runat="server" id="Pag1" style="width:100%; height:1000px; border:none;" scrolling="no" sandbox="allow-same-origin allow-forms allow-scripts" src="LotesAutoManualmin.aspx"></iframe>
                    </div> 
                </div>
            </div>
            <!-- Fin accordion8"-->

            <%--<Vistas Horizontal >--%>

             <div class="panel-body" visible="false" style="width:100%;height:200px;"  runat="server" id="Horizontal">        
                <div class="row" style="width:100%;overflow:auto;display: flex;">  
                    <%--<Ordenes de compra dos>--%>
                    <div class="col-lg-6" runat="server" id="ColHorizontal1" style="overflow:auto; height:1000px;">               
                                <div class="panel panel-default">
                                    <div class="panel-heading" runat="server" id="Div2" >
                                        <div class="row">
                                                <div class="col-lg-8"> 
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" style=" font-weight: bold;" onclick="submiti4()"  href="#collapse4"> SELECCION PEDIDOS COMPRAS: Lista pedidos pendientes</a>                                             
                                                    </h4>
                                                </div>
                                                <div class="col-lg-3">
                                                </div>
                                                <%--<div class="col-lg-1">
                                                    <input type="image" Visible="false"  class="pull-right text-muted " src="images/filtro25x25.png" onserverclick="ImageFiltro_Click"  runat="server" id="Image1" style="border: 0px; " />    
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:DropDownList runat="server" Visible="false"  CssClass="form-control"  Width="100%"  ID="DropDownList7" tooltip="Contiene el filtro para las órdenes de pedidos" OnSelectedIndexChanged="DrSelectFiltro_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" /> 
                                                </div>--%>
                                                <div class="col-lg-1"> <%--onserverClick="PrintGridView"--%>
                                                    <%--<button id="Button12" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onclick="doPrintGRControl();"><i title="Imprime la vista previa presentada en la lista de órdenes de carga" class="fa fa-print fa-2x"></i></button>--%>
                                                    <button id="BtGralConsulta2" type="button" runat="server" style="width:50%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtGralConsulta_Click"><i title="Consulta a GoldenSoft (con o sin filtro). Refresca la consulta actual visualizada en pantalla." class="fa fa-bars fa-2x"></i></button>
                                                </div>
                                        </div>
                                        <asp:Button ID="Button13" runat="server" OnClick="check4_Click" Height="0" Width="0" CssClass="hidden" />
                                        <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label19"  Text="" > </asp:Label>
                                    </div>
                                    <br />

                                  <div id="Div4" runat="server" class="panel panel-default">
                                    <div class="panel-heading" runat="server" id="Div9" >
                                        <div class="row">
                                                <div class="col-lg-8"> 
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" style=" font-weight: bold;" onclick="submitit3()" href="#collapse3"> Filtros aplicados                                             
                                                        <%--<asp:Label  type="text"  Width="90%" style=" font-weight: bold; color:green;"  runat="server" ID="LbFiltros"  Text="" > </asp:Label>--%>
                                                        <%--<asp:TextBox runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="TextBox2"  Font-Bold="True" />--%>
                                                    </a>
                                                </h4>

                                            </div>
                                
                                        </div>
                                        <asp:Button ID="Button14" runat="server" OnClick="check3_Click" Height="0" Width="0" CssClass="hidden" />
                                        <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label21"  Text="" > </asp:Label>
                                    </div>
                                    <br />

                                    <div id="Div10" runat="server"  class="panel-collapse collapse in">                               
                                        <div class="panel panel-default">            
                                            <div class="panel-body">
                                                <div class="row">
                                                    <!-- consultas predefinidas Horizontal -->
                                                    <div class="col-lg-1">
                                                    </div>
                                                    <div class="col-lg-2">
                                                     <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:left;"  runat="server" ID="Label22"  Text="Empresa:" > </asp:Label>
                                                    <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="DrConsultas2"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:left;"  runat="server" ID="Label23"  Text="Proveedor:" > </asp:Label>
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Proveedor desde para filtrar datos"  ID="DrProveedor2"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:left;"  runat="server" ID="Label24"  Text="Número Pedido:" > </asp:Label>
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Número Pedido hasta para filtrar datos"  ID="DrNumPedido2"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:left;"  runat="server" ID="Label25"  Text="Producto:" > </asp:Label>
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Producto para filtrar datos"  ID="DrProducto2"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:left;"  runat="server" ID="Label26"  Text="Fecha Entrega:" > </asp:Label>
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione una Fecha Entrega para filtrar datos"  ID="DrFechaEntrega2"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>  
                                                    <div class="col-lg-1">
                                                        <%--<asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label27"  Text="" > </asp:Label>--%>
                                                        <button id="Btfiltra2" type="button" runat="server"  style="width:50%; border-style:none; background-color:transparent;margin-top:20px;" class="pull-right text-muted "  onserverClick="Carga_Combos_vacios_Click"><i title="Borra los filtros seleccionados. Y refresca la consulta actual visualizada en pantalla, con nueva Consulta a GoldenSoft (sin filtro)." class="fa fa-eraser fa-2x"></i></button>
                                                    </div>                           
                                                </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                                    <div id="Div11" runat="server"  class="panel-collapse collapse in">                               
                                        <div class="panel panel-default">
                                            <div class="panel-body" >
                                                <div class="row">
                                                    <div class="col-sm-1" >
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label28"  Text="Filas:" > </asp:Label>
                                                    </div>
                                                    <div class="col-sm-1" >                              
                                                       <asp:DropDownList ID="ddControlPageSize3" CssClass="form-control" style=" position:absolute;" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvControl_PageSize_Changed">  
                                                        </asp:DropDownList>
                                                     </div>
                                                    <div class="col-sm-1" >
                                                    </div>
                                                    <div class="col-sm-2" >
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="LbRowControl1"  Text="" > </asp:Label>
                                                    </div>
                                                    <div class="col-sm-7" >
                                                    </div>

                                                </div>
                                                <!-- barra vertical no actualiza la posicion. style="height:300px; overflow-x:hidden; overflow-:auto;" -->
                                                <div class="row" >
                                                    <div id="Div12" runat="server" style="overflow:auto;" class="panel-body"> <%-- AutoGenerateSelectButton="True"--%>  
                                                        <asp:GridView ID="gvHorizontal"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                                            AllowSorting="true" OnSorting="gvControl_OnSorting"
                                                            CellPadding="4"  GridLines="None"  OnRowDataBound="gvControl_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="10" OnRowCommand="gvControl_RowCommand" DataKeyNames="ID"
                                                            oonselectedindexchanged="gvControl_SelectedIndexChanged"  OnRowEditing="gvControl_RowEditing" OnRowCancelingEdit="gvControl_RowCancelingEdit" OnRowUpdating="gvControl_RowUpdating" 
                                                            onpageindexchanging="gvControl_PageIndexChanging" >
                                                        <RowStyle />       
                                                            <Columns>

                                                            <%--<asp:CommandField ButtonType="Image" 
                                                                EditImageUrl="~/Images/editar20x20.png" 
                                                                ShowEditButton="True" 
                                                                CancelImageUrl="~/Images/cancelar20x20.png" 
                                                                CancelText="" 
                                                                DeleteText="" 
                                                                UpdateImageUrl="~/Images/guardar18x18.png"          
                                                                UpdateText="" 
                                                                />            --%> 
                                                    
                                                            <asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                            <ItemTemplate>
                                                               <asp:ImageButton ID="ibtbajaOrden" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/etiqueta25x25.png"
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
                                                            <asp:TemplateField HeaderText="Proveedor" SortExpression="CLIENTEPROVEEDOR">
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
                                                             <asp:TemplateField HeaderText="Número Pedido" SortExpression="NUMERO">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabONumero" runat="server" Text='<%# Eval("NUMERO") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabONumero" runat="server" Text='<%# Bind("NUMERO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Linea Pedido" SortExpression="LINEA">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOLinea" runat="server" Text='<%# Eval("LINEA") %>' class="gridContCinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOLinea" runat="server" Text='<%# Bind("LINEA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Serie Pedido"  SortExpression="SERIE_PED">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOSerie" runat="server" Text='<%# Eval("SERIE_PED") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOSerie" runat="server"  Text='<%# Bind("SERIE_PED") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Producto" SortExpression="PRODUCTO">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOArticulo" runat="server" Text='<%# Eval("PRODUCTO") %>' class="gridContBinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOArticulo" runat="server" Text='<%# Bind("PRODUCTO") %>'></asp:Label>
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
                                                             <asp:TemplateField HeaderText="Uds Pendientes" SortExpression="UDSPENDIENTES">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOPendientes" runat="server" Text='<%# Eval("UDSPENDIENTES") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOPendientes" runat="server" Text='<%# Bind("UDSPENDIENTES") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Descripción" SortExpression="DESCRIPCION">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabODescriciob" runat="server" Text='<%# Eval("DESCRIPCION") %>' class="gridContBinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabODescriciob" runat="server" Text='<%# Bind("DESCRIPCION") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tipo Planta" SortExpression="TIPO_PLANTA">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOPlanta" runat="server" Text='<%# Eval("TIPO_PLANTA") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOPlanta" runat="server"  Text='<%# Bind("TIPO_PLANTA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Variedad" SortExpression="VARIEDAD">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOPalet" runat="server" Text='<%# Eval("VARIEDAD") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOPalet" runat="server" Text='<%# Bind("VARIEDAD") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Familia" SortExpression="FAMILIA">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOFamilia" runat="server" Text='<%# Eval("FAMILIA") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOfamilia" runat="server" Text='<%# Bind("FAMILIA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                    
                                                            </Columns>
                                                            <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
                                                            <EditRowStyle BackColor="#f2eed5" />

                                                        </asp:GridView>                                  
                                                    </div>
                                                </div>
                                    
                                            </div>
                                        </div>
                                    </div>
                        </div>
                        <%---Fin <Ordenes de compra dos LoteAuto.aspx>--%>
                    </div>

                    <%---Pagina lotes automaticos--%>
                    <div class="col-lg-6" runat="server" id="ColHorizontal2" style="overflow:auto; height:1000px;"> 
                            <%--<iframe runat="server" id="Pag2" style="width:100%; height:1100px; border:none;" scrolling="auto" sandbox="allow-same-origin allow-forms allow-scripts" src="LotesAutoManualmin.aspx"></iframe>--%>
                   
                        
                  


                     <div class="row">
                        <div class="col-lg-4">
                        </div>
                        <div id="dvPrinters" runat="server" style="text-align:right;" visible="true" class="col-lg-8" >
                            <br />
                                <button id="BtPrinters" type="button" runat="server" style="width:90%; border-style:none; background-color:transparent; text-align:right;" class="text-muted"  onserverClick="btPrinter_Click"><i title="Selección otra Impresora"  class="fa fa-qrcode fa-2x"></i></button>                                      
                            </div>
                            <div id="dvDrlist" runat="server" style="text-align:right;" Visible="false" class="col-lg-8" >
                                <br />
                                <asp:DropDownList runat="server"  CssClass="form-control"  Width="100%"  ID="DrPrinters" tooltip="Contiene las distintas impresoras configuradas" OnSelectedIndexChanged="DrPrinters_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                            </div>
                    </div>

                      

                    <div class="row">
                  <div class="col-lg-12">
                    <%--<h3> Generación de lotes automáticos <i class="fa fa-long-arrow-right"></i> <small> “Para generar lotes de forma automática e imprimir código QR”  </small></h3>--%>
                      <br />
                  </div>

                </div><!-- /.row -->
                   <div class="row" id="BodyAll" runat="server">
                    <div class="col-lg-12">
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
                                <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label6"  Text="Código Lote:" > </asp:Label>
<%--                                <asp:Label ID="txtQRCodebis" Visible="true" style="text-align:center; color:red; font-size:18px; font-weight: bold;" runat="server" Width="100%" CssClass="form-control" ></asp:Label>--%>
                                <asp:TextBox ID="txtQRCode" Visible="true" Enabled="false"  style="text-align:center; font-size:36px; font-weight: bold;" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                <asp:TextBox ID="txtQRCodeManu" Visible="false"  style="text-align:center; background:white; font-size:36px; font-weight: bold;" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                   <%--  <br />
                                <asp:Button runat="server" ID="BtnLanzaPro" tooltip="Inserta en la Tabla de GlodenSoft para importar" CssClass="btn btn-success btn-block" Width="100%"  Text="Lanza procedimiento" OnClick="BtnLanzaPro_Click"/>
                               <br /> --%>
                               </div>
                                <br />
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

                         <div runat="server" id="alertaLog" visible="false" class="alert alert-warning alert-dismissable">
                                    <button type="button" class="close" data-dismiss="alert">&times;</button> 
                                    <%--<a href="#" id="a1" runat="server" class="alert alert-warning alert-dismissable">--%>
                                        <i id="I3" runat="server" class="fa fa-exclamation-circle"></i>
                                        <asp:Label runat="server" class="alert alert-warning alert-dismissable" ID="TextAlertaLog" BorderStyle="None" border="0" Width="100%" Text=""  />
                                    <%--</a>--%>  
                                  <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label30"  Text="Usuario:" > </asp:Label>
                                  <asp:TextBox ID="TextUser"  style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                  <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label31"  Text="Password:" > </asp:Label>
                                  <asp:TextBox ID="TextPass"  TextMode="Password" style="text-align:center; font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                             <br />      
                             <asp:Button runat="server" ID="Btvalidauser" tooltip="Comprobará si el usuario dispone de permisos para editar la página" CssClass="btn btn-danger btn-block" Width="100%"  Text="Validar usuario" OnClick="btnValidaUser_Click"/>
                        </div>

                        
                    </div>
                                    



                   

                    <div class="col-lg-12" >
                        <div id="PanelQR" runat="server" class="panel panel-default" style="min-height:420px;">
                        
                            <div id="H1Normal" runat="server" visible="true" class="panel-heading">
                                <div class="row panel-title" runat="server">
                            <%--<h3 class="panel-title">--%> 
                                    <div class="col-lg-10 panel-title">
                                        <i class="fa fa-long-arrow-right">Seleccione un Lote</i>
                                    </div>
                                    <div class="col-lg-1" >

                                    </div>
                                     
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
                            <div class="panel-body" style="height:900px;" id="Div15" runat="server"> 

                                <asp:Panel ID="panelContentsFTLotes" Visible="false" Height="100%" Width="100%" runat="server">
                                            <asp:PlaceHolder ID="PlaceHolderFitoLotes"  runat="server"></asp:PlaceHolder>
                                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbDateFTLotes"  Text="" > </asp:Label>
                                </asp:Panel>

                                 <div class="col-lg-4" >
                                </div>

                                <div class="col-lg-8" >
                                    <asp:Panel ID="panelContentsLotes" Visible="true" runat="server">
                                            <br />
                                            <br />
                                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbCodigoLoteLotes"  Text="SIN CÓDIGO LOTE" > </asp:Label>
                                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:48px;"  runat="server" ID="LbSecuenciaLoteLotes"  Text="" > </asp:Label>
                                            <asp:PlaceHolder ID="PlaceHolderLoteBing"  runat="server"></asp:PlaceHolder>
                                            <br />
                                            <br />
                                            <div class="col-lg-12" >
                                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbCampoSLotes"  Text="" > </asp:Label>
                                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbPlantaSLotes"  Text="" > </asp:Label>
                                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbFechaSLotes"  Text="" > </asp:Label>
                                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbVariedadSLotes"  Text="" > </asp:Label>
                                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbCajasSLotes"  Text="" > </asp:Label>
                                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbPlantasSLotes"  Text="" > </asp:Label>
                                            </div>
                                            <br />
                                            <br />
                                             <div class="row">
                                                <div class="col-lg-6" >

                                                </div>
                                                <div class="col-lg-2" >
                                                    <asp:PlaceHolder ID="PlaceHolderLoteBingB"  runat="server"></asp:PlaceHolder>
                                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Lbcompleto"  Text="" > </asp:Label>
                                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbDateContentsLotes"  Text="" > </asp:Label>
                                                </div>
                                                 <div class="col-lg-4" >

                                                </div>
                                             </div>
                                    </asp:Panel>
                                    <asp:Panel ID="panelContentsQRLotes" Visible="false" runat="server">
                                            <br />
                                            <br />
                                            <div class="col-lg-6" style="text-align: center;" >
                                                <%--&nbsp;&nbsp;<asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px; text-align:left; "  runat="server" ID="LbCodigoLoteQR"  Text="SIN CÓDIGO LOTE" > </asp:Label>--%>
                                                <asp:Label  type="text" style=" font-weight: bold; font-size:48px;"  runat="server" ID="LbSecuenciaLoteQRLotes"  Text="" > </asp:Label>
                                                <br />
                                                <asp:PlaceHolder ID="PlaceHolderQRLote" runat="server"></asp:PlaceHolder>
                                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbDateQRLotes"  Text="" > </asp:Label>
                                         </div>
                                    </asp:Panel>
                                     <asp:Panel ID="panelContentsPaletAlvLotes" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="col-lg-12" alig="left" >
                                                <br />
                                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="LbCodePaletAlvLotes"  Text="" > </asp:Label>
                                            </div>
                                            <%-- <div class="col-lg-5" >
                                            </div>--%>
                                        </div>
                                        <div class="row">
                                           <%-- <div class="col-lg-1" alig="left">
                                            </div>--%>
                                            <div class="col-lg-12"  alig="left">
                                                
                                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:48px;"  runat="server" ID="LbCodeQRPalteAlvLotes"  Text="SIN CÓDIGO LOTE" > </asp:Label>        
                                            </div>
                                            <%--<div class="col-lg-4" alig="left">

                                            </div>--%>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12" alig="center">
                                                <asp:PlaceHolder ID="PlaceHolderPaletAlvLote"  runat="server"></asp:PlaceHolder>
                                                <br />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12" alig="center" >
                                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="LbTipoPlantaPLotes"  Text="" > </asp:Label>
                                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="LbVariedadPLotes"  Text="" > </asp:Label>
                                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="lbUnidadesPLotes"  Text="" > </asp:Label>
                                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:20px;"  runat="server" ID="lbNumPlantasPLotes"  Text="" > </asp:Label>                                            
                                            </div>
                                            <br />
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6" >

                                            </div>
                                            <div class="col-lg-2" alig="right" >
                                                <asp:PlaceHolder ID="PlaceHolderPaletAlvMinLote"  runat="server"></asp:PlaceHolder>
                                            <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label181"  Text="" > </asp:Label>
                                            <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LbDatePaletAlvLotes"  Text="" > </asp:Label> 

                                            </div>
                                            <div class="col-lg-4" >

                                            </div>
                                        </div>
                                    </asp:Panel>


                                     <br />
                                </div>
<%--                                <div class="col-lg-1" >
                                 </div>--%>
                            </div>   <%--<Final de Vistas de Impresora >--%> 
                            <asp:ImageButton id="ImgQRCodeA1Lotes" visible="false" runat="server" AlternateText="ImageButton 1" ImageAlign="left" Width="25px" Height="20px" ImageUrl="images/alfabeto03.gif" OnClick="BtQRCodeA1_Click"/>
                            <asp:ImageButton id="ImgQRCodeA2Lotes" visible="false" runat="server" AlternateText="ImageButton 1" ImageAlign="left" Width="25px" Height="20px" ImageUrl="images/alfabeto26.gif" OnClick="BtQRCodeA1_Click"/>
                            <asp:Label  type="text" Width="80%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LBReadQRLotes"  Text="" > </asp:Label> 
                           </div> 

                            <%--<div runat="server" id="Mialerta" visible="false" class="alert alert-info alert-dismissable">
                                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                                <i id="IAlert" runat="server" class="fa fa-exclamation-circle"></i>
                                <asp:Label runat="server" class="alert alert-info alert-dismissable" ID="TextAlerta" BorderStyle="None" border="0" Width="100%" Text=""  />
                            </div>--%>
                           <div runat="server" id="alerta" visible="false" class="alert alert-info alert-dismissable">
                                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                                <%--<a href="#" id="alerT" runat="server" class="alert alert-info alert-dismissable">--%>
                                    <i id="I1" runat="server" class="fa fa-exclamation-circle"></i>
                                    <asp:Label runat="server" class="alert alert-info alert-dismissable" ID="TextAlerta" BorderStyle="None" border="0" Width="100%" Text=""  />
                                <%--</a> --%> 
                                <asp:Button runat="server" ID="btProcesa" tooltip="Busca si el Lote tiene registro de Formulario Scan-IT" visible="false" CssClass="btn btn-success btn-block" Width="100%"  Text="Procesar Lote" OnClick="btnPorcesa_Click"/>
                                <asp:Button ID="btnPrint2" runat="server" tooltip="Lote con Formulario Scan-IT para imprimir con el total de su información" visible="false" CssClass="btn btn-success " Width="100%" Text="Imprimir código QR" OnClientClick="return PrintPanel();" />
                                <asp:Button ID="btnPrintPaletAlv" runat="server" tooltip="Lote con Formulario Scan-IT para imprimir con el total de su información" visible="false" CssClass="btn btn-success " Width="100%" Text="Imprimir código QR" OnClientClick="return PrintPaletAlv();" />
                            </div>

                            <div runat="server" id="alertaErr" visible="false" class="alert alert-danger alert-dismissable">
                                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                                <%--<a href="#" id="alerTErr" runat="server" class="alert alert-danger alert-dismissable">--%>
                                <i id="IAlertErr" runat="server" class="fa fa-exclamation-circle"></i>
                                <asp:Label runat="server" class="alert alert-danger alert-dismissable" ID="TextAlertaErr" BorderStyle="None" border="0" Width="100%" Text=""  />
                                <%--</a>--%>   
                                <asp:Button runat="server" ID="btPorcesa" tooltip="Busca si el Lote tiene registro de Formulario Scan-IT" visible="false" CssClass="btn btn-success btn-block" Width="100%"  Text="Procesar Lote" OnClick="btnPorcesa_Click"/>
                             </div>

                      </div>

  

                 <%--</div> --%>

            <div class="row" id="Div16" runat="server">
                              <%-- Ficha Campos--%>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                            <h3 class="panel-title"><i class="fa fa-long-arrow-right"></i>
                                <asp:Label  type="text" Width="90%" visible="true" style=" font-weight: bold;"  runat="server" ID="Label45"  Text="Formulario de entrada de datos desde Móvil" > </asp:Label>
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

                        </div>
                         <%-- fin Ficha Campos--%>
                    </div>






























                    
                    </div> 
                    <%---fin Pagina lotes automaticos--%>

                </div>
            </div>
            <!-- Fin Horizontal"-->

            <!-- Inicio DivLotes automaticos"-->
              <div class="panel-body" visible="false" style="width:100%;height:200px;"  runat="server" id="DivLotes">        
                <div class="row" style="width:100%;overflow:auto;">  
                    <%--Lotes--%>                
                    <div class="col-lg-12">  
                            <iframe runat="server" id="Pag3" style="width:100%; height:1100px; border:none;" scrolling="auto" sandbox="allow-same-origin allow-forms allow-scripts" src="LotesAutoManualmin.aspx"></iframe>
                    </div> 
                </div>
            </div>
            <!-- Fin DivLotes"-->

              <%--<Ordenes de compra>--%>
            <div class="tab-pane fade active" visible="true" runat="server" id="accordion3">
                <div id="collapse2" runat="server"  class="panel-collapse collapse in">                     
                    <div class="panel panel-default">
                        <div class="panel-heading" runat="server" id="PanelOrden" >
                            <div class="row">
                                    <div class="col-lg-8"> 
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" style=" font-weight: bold;" onclick="submiti4()"  href="#collapse4"> SELECCION PEDIDOS COMPRAS: Lista pedidos pendientes</a>                                             
                                        </h4>
                                    </div>
                                    <div class="col-lg-3">
                                    </div>
                                    <%--<div class="col-lg-1">
                                        <input type="image"  class="pull-right text-muted " src="images/filtro25x25.png" onserverclick="ImageFiltro_Click"  runat="server" id="imgFiltro" style="border: 0px; " />    
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:DropDownList runat="server"  CssClass="form-control"  Width="100%"  ID="DrSelectFiltro" tooltip="Contiene el filtro para las órdenes de pedidos" OnSelectedIndexChanged="DrSelectFiltro_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" /> 
                                    </div>--%>
                                    <div class="col-lg-1"> <%--onserverClick="PrintGridView"--%>
                                        <%--<button id="BtPrintOrden" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onclick="doPrintGRControl();"><i title="Imprime la vista previa presentada en la lista de órdenes de carga" class="fa fa-print fa-2x"></i></button>--%>
                                        <button id="BtGralConsulta" type="button" runat="server" style="width:50%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtGralConsulta_Click"><i title="Consulta General con filtro o sin filtro según selección sobre la base de datos de GoldenSoft. Elimina la consulta actual visualizada en pantalla." class="fa fa-bars fa-2x"></i></button>
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
                                           <%--<asp:TextBox runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="TxtNumero"  Font-Bold="True" />--%>
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
                                        <div class="col-lg-1">
                                         </div>
                                         <div class="col-lg-1">
                                              <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:right;"  runat="server" ID="Label1"  Text="Empresa:" > </asp:Label>
                                          </div>
                                        <div class="col-lg-1"> 
                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="DrConsultas"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-lg-1">
                                             <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:right;"  runat="server" ID="Label2"  Text="Nombre Fiscal:" > </asp:Label>
                                        </div>
                                        <div class="col-lg-1"> 
                                            <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Proveedor desde para filtrar datos"  ID="DrProveedor"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:right;"  runat="server" ID="Label5"  Text="Número Pedido:" > </asp:Label>
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Número Pedido hasta para filtrar datos"  ID="DrNumPedido"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-lg-1">
                                           <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:right;"  runat="server" ID="Label3"  Text="Producto:" > </asp:Label>
                                        </div>
                                        <div class="col-lg-1"> 
                                            <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Producto para filtrar datos"  ID="DrProducto"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-lg-1">
                                             <asp:Label  type="text"  Width="100%" style=" font-weight: bold;margin-top:10px;text-align:right;"  runat="server" ID="Label4"  Text="Fecha Entrega:" > </asp:Label>
                                        </div>
                                        <div class="col-lg-1"> 
                                            <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione una Fecha para filtrar datos"  ID="DrFechaEntrega"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-lg-1">
                                            <%--<asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label6"  Text="" > </asp:Label>--%>
                                            <button id="Btfiltra" type="button" runat="server"  style="width:50%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="Carga_Combos_vacios_Click"><i title="Consulta con filtro o sin filtro según selección, sobre los datos recuperados de la base de datos en local" class="fa fa-eraser fa-2x"></i></button>
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
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="LbRowControl"  Text="" > </asp:Label>
                                        </div>
                                        <div class="col-sm-7" >
                                        </div>

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

                                               <%-- <asp:CommandField ButtonType="Image" 
                                                    EditImageUrl="~/Images/editar20x20.png" 
                                                    ShowEditButton="True" 
                                                    CancelImageUrl="~/Images/cancelar20x20.png" 
                                                    CancelText="" 
                                                    DeleteText="" 
                                                    UpdateImageUrl="~/Images/guardar18x18.png"          
                                                    UpdateText="" 
                                                    />           --%>  
                                                    
                                                <asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtbajaOrden" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/etiqueta25x25.png"
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
                                                            <asp:TemplateField HeaderText="Proveedor" SortExpression="CLIENTEPROVEEDOR">
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
                                                             <asp:TemplateField HeaderText="Número Pedido" SortExpression="NUMERO">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabONumero" runat="server" Text='<%# Eval("NUMERO") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabONumero" runat="server" Text='<%# Bind("NUMERO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Linea Pedido" SortExpression="LINEA">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOLinea" runat="server" Text='<%# Eval("LINEA") %>' class="gridContCinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOLinea" runat="server" Text='<%# Bind("LINEA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Serie Pedido" SortExpression="SERIE_PED">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOSerie" runat="server" Text='<%# Eval("SERIE_PED") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOSerie" runat="server"  Text='<%# Bind("SERIE_PED") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Producto" SortExpression="PRODUCTO">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOArticulo" runat="server" Text='<%# Eval("PRODUCTO") %>' class="gridContBinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOArticulo" runat="server" Text='<%# Bind("PRODUCTO") %>'></asp:Label>
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
                                                             <asp:TemplateField HeaderText="Uds Pendientes" SortExpression="UDSPENDIENTES">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOPendientes" runat="server" Text='<%# Eval("UDSPENDIENTES") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOPendientes" runat="server" Text='<%# Bind("UDSPENDIENTES") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Descripción" SortExpression="DESCRIPCION">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabODescriciob" runat="server" Text='<%# Eval("DESCRIPCION") %>' class="gridContBinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabODescriciob" runat="server" Text='<%# Bind("DESCRIPCION") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tipo Planta" SortExpression="TIPO_PLANTA">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOPlanta" runat="server" Text='<%# Eval("TIPO_PLANTA") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOPlanta" runat="server"  Text='<%# Bind("TIPO_PLANTA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Variedad" SortExpression="VARIEDAD">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOPalet" runat="server" Text='<%# Eval("VARIEDAD") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOPalet" runat="server" Text='<%# Bind("VARIEDAD") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Familia" SortExpression="FAMILIA">
                                                                    <EditItemTemplate>
                                                                         <asp:TextBox ID="TabOFamilia" runat="server" Text='<%# Eval("FAMILIA") %>' class="gridContAinput"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="LabOfamilia" runat="server" Text='<%# Bind("FAMILIA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                             </asp:TemplateField>
                    
                                                </Columns>
                                                <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
                                                <EditRowStyle BackColor="#f2eed5" />

                                            </asp:GridView>                                  
                                        </div>
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%---Fin <Ordenes de compra>--%>

            <%--<Reportes>--%>
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
            <%--<fin Reportes>--%>

             <%--<Impresoras>--%>
            <div class="tab-pane fade active" visible="false" runat="server" id="accordion2">
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

           


          
          </div>
            <%--<fin Impresoras>--%>
  

        </div>
 </div>


</asp:Content>
