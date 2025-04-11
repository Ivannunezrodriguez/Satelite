<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false" CodeBehind="RecoNomina.aspx.cs" Inherits="QRCode_Demo.RecoNomina" MaintainScrollPositionOnPostback="true" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<%--<A HREF="http://www.example.com/myfile.pdf#page=4"> abrir pdf por pagina

    Para definir un destino en Acrobat 7.0 (Professional o Standard):
1. Defina el zoom y la ubicación de página a su conveniencia en la pantalla.
2. Seleccione Ver > Fichas de navegación > Destinos para abrir la ficha Destinos.
3. En la ficha Destinos, seleccione Opciones > Nuevo destino.
4. Asigne un nombre al destino.
5. Cree un vínculo HTML que señale a este destino; para ello, añada #[nombre de destino] al final de las direcciones URL de los vínculos.
Por ejemplo, esta etiqueta HTML abre una página de destino denominada "glossary" en un archivo PDF denominado myfile.pdf: <A HREF="http://www.example.com/myfile.pdf#glossary">
asp:HiddenField ID="HiddenField1" runat="server" />--%>

<html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">

    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <title>Viveros Rio Eresma</title>

    <link href="css/Container.css" rel="stylesheet" />

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet" />

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <!-- MetisMenu CSS -->
    <link href="css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet"/>

    <!-- Timeline CSS -->
    <link href="css/plugins/timeline.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Morris Charts CSS -->
    <link href="css/plugins/morris.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>

    <link href="css/print.css" media="print" rel="stylesheet" />

        
        <style id="gridStyles" runat="server" type="text/css">
            body
            {
                font-family: Arial;
                font-size: 10pt;
            }
            table
            {
                border: 1px solid #ccc;
                border-collapse: collapse;
            }
            table th
            {
                color: #0090CB;
                font-weight: bold;
            }
            table th, table td
            {
                padding: 5px;
                border: 1px solid #ccc;
            }
            table, table table td
            {
                border: 0px solid #ccc;
            }
            thead
            {
                display: table-header-group;
            }
            tfoot
            {
                display: table-footer-group;
            }
        </style>



        <style type="text/css">
              /* The switch - the box around the slider */

            .gridinput {
                width: 100px;
                padding: 2px 4px;
            }
            .gridCabAinput {
                width: 100px;
                padding: 2px 4px;
            }
            .gridCabBinput {
                width: 120px;
                padding: 2px 4px;
            }
            .gridContAinput {
                width: 70px;
                padding: 2px 4px;
            }
            .gridContBinput {
                width: 100px;
                padding: 2px 4px;
            }
            .gridContCinput {
                width: 50px;
                padding: 2px 4px;
            }
            .gridListAinput {
                width: 70px;
                padding: 2px 4px;
            }
            .gridListBinput {
                width: 100px;
                padding: 2px 4px;
            }
            .gridListCinput {
                width: 50px;
                padding: 2px 4px;
            }
            .gridListDinput {
                width: 120px;
                padding: 2px 4px;
            }

            .switch {
              position: absolute;
              display: inline-block;
              width: 45px;
              height: 28px;
            }

            /* Hide default HTML checkbox */
            .switch input {
              opacity: 0;
              width: 0;
              height: 0;
            }

            /* The slider */
            .slider {
              position: absolute;
              cursor: pointer;
              top: 0;
              left: 0;
              right: 0;
              bottom: 0;
              background-color: #ccc;
              -webkit-transition: .4s;
              transition: .4s;
            }

            .slider:before {
              position: absolute;
              content: "";
              height: 20px;
              width: 20px;
              left: 4px;
              bottom: 4px;
              background-color: white;
              -webkit-transition: .4s;
              transition: .4s;
            }

            input:checked + .slider {
              background-color: #0caeb4;
            }

            input:focus + .slider {
              box-shadow: 0 0 1px #21c9cf;
            }

            input:checked + .slider:before {
              -webkit-transform: translateX(20px);
              -ms-transform: translateX(20px);
              transform: translateX(20px);
            }

            /* Rounded sliders */
            .slider.round {
              border-radius: 34px;
            }

            .slider.round:before {
              border-radius: 50%;
            } 

    </style>

    <script src="<%=Page.ResolveUrl("js/jquery-1.11.0.js") %>" type="text/javascript"></script>

    <script>
        $(function () {
            $("[data-toggle='tooltip']").tooltip();
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#BtnImprimir").click(imprimirDiv);    //Asociando la función "imprimirDiv" al clic del botón para Imprimir Reporte
        });

        function imprimirDiv() {
            var divImprimir = $("div[id$='PNreportLista']").parent();        //Obteniendo el padre del DIV que contiene al reporte 
            var estilos = $("head style[id$='ReportControl_styles']");    //Obteniendo los estilos del reporte
            newWin = window.open("");        //Abriendo una nueva ventana

            //Construyendo el HTML de la nueva ventana, con los estilos del reporte y el div que contiene el reporte
            newWin.document.write('<html xmlns="http://www.w3.org/1999/xhtml"><head><style type="text/css">' + estilos.html() + '</style></head><body>' + divImprimir.html() + '</body>');
            newWin.document.close();        //Finalizando la escritura
            newWin.print();        //Imprimir contenido de nueva ventana
            newWin.close();        //Cerrar nueva ventana
        }
    </script>

</head>

<body>

    <div id="wrapper">

    <form id="formCode" runat="server">  
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">      
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnAcepta" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>

                           


                            <div runat="server" id="DvPreparado" visible="false"  style="height: 20%; width: 30%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
                                <button type="button" class="close" data-dismiss="alert">&times;</button> 
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
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>


           <!-- Navigation -->
        <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="index.html">Viveros Rio Eresma</a>
                <button id="BtMenus" type="button" runat="server" style="width:10px; border-style:none; background-color:transparent; padding: 10px ;" class="pull-center text-muted "  onserverClick="BtMenus_Click"><i id="MasMinMenu" runat="server" title="Muestra y oculta el Menú lateral." class="fa fa-chevron-left fa-2x"></i></button>         
            </div>
            <%--<div class="navbar-header" >
                <h3 id="H3Titulo" runat="server" style="top:-10px;"   visible="true">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Selección de pedidos, "Pedidos pendientes de cargar en camión" <i class="fa fa-long-arrow-right"></i> <small> " Nóminas "  </small>              
                <asp:Label  type="text"  style=" font-weight: bold; font-size:14px;"  runat="server" ID="Lbhost1"  Text="" > </asp:Label>
                </h3>
            </div>--%>

            <!-- /.navbar-header -->
            
            <ul class="nav navbar-top-links navbar-right">
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-envelope fa-fw"></i>  <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-messages">
                        <li>
                            <a href="#">
                                <div>
                                    <strong>Panel de Control</strong>
                                    <span class="pull-right text-muted">
                                        <em>ayuda</em>
                                    </span>
                                </div>
                                <div>Datos generales de la aplicación Rio Eresma y acceso
                                    a los formulario principales 
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <strong>Importaciones</strong>
                                    <span class="pull-right text-muted">
                                        <em>ayuda</em>
                                    </span>
                                </div>
                                <div>Acceso a las importaciones en GoldenSoft desde archivo a tabla</div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <strong>Tablas</strong>
                                    <span class="pull-right text-muted">
                                        <em>ayuda</em>
                                    </span>
                                </div>
                                <div>Selección de una tabla y vista del contenido de sus datos con inserción, modificación o eliminación de información</div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a class="text-center" href="#">
                                <strong>
                                    Por determinar
                                </strong>
                                <i class="fa fa-angle-right"></i>
                            </a>
                        </li>
                    </ul>
                    <!-- /.dropdown-messages -->
                </li>
                <!-- /.dropdown -->
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-tasks fa-fw"></i>  <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-tasks">
                        <li>
                            <a href="#">
                                <div>
                                    <p>
                                        <strong>Tarea 1</strong>
                                        <span class="pull-right text-muted">40% Completo</span>
                                    </p>
                                    <div class="progress progress-striped active">
                                        <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 40%">
                                            <span class="sr-only">40% Completo (informativo)</span>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <p>
                                        <strong>Tarea 2</strong>
                                        <span class="pull-right text-muted">20% Completo</span>
                                    </p>
                                    <div class="progress progress-striped active">
                                        <div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="20" aria-valuemin="0" aria-valuemax="100" style="width: 20%">
                                            <span class="sr-only">20% Completo</span>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <p>
                                        <strong>Tarea 3</strong>
                                        <span class="pull-right text-muted">60% Completo</span>
                                    </p>
                                    <div class="progress progress-striped active">
                                        <div class="progress-bar progress-bar-warning" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                            <span class="sr-only">60% Completo (precaución)</span>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="#">
                                <div>
                                    <p>
                                        <strong>Tarea 4</strong>
                                        <span class="pull-right text-muted">80% Completo</span>
                                    </p>
                                    <div class="progress progress-striped active">
                                        <div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 80%">
                                            <span class="sr-only">80% Completo (peligro)</span>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a class="text-center" href="#">
                                <strong>Ver todas las tareas</strong>
                                <i class="fa fa-angle-right"></i>
                            </a>
                        </li>
                    </ul>
                    <!-- /.dropdown-tasks -->
                </li>
                <!-- /.dropdown -->
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-bell fa-fw"></i>  <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-alerts">

                        <li>
                        <a href="#" class="list-group-item">
                                    <i class="fa fa-comment fa-fw"></i> Importación para GoldenSoft
                                    <span class="pull-right text-muted small"><em>4 minutos</em>
                                    </span>
                                </a>
                        </li>
                        <li>
                                <a href="#" class="list-group-item">
                                    <i class="fa fa-twitter fa-fw"></i> Preparación fichajes Recodat
                                    <span class="pull-right text-muted small"><em>12 minutos</em>
                                    </span>
                                </a>
                        </li>
                        <li>
                                <a href="#" class="list-group-item">
                                    <i class="fa fa-envelope fa-fw"></i> Proceso Paletizado
                                    <span class="pull-right text-muted small"><em>27 minutos</em>
                                    </span>
                                </a>
                        </li>
                        <li>
                                <a href="#" class="list-group-item">
                                    <i class="fa fa-tasks fa-fw"></i> Etiquetado fitosanitario
                                    <span class="pull-right text-muted small"><em>43 minutos</em>
                                    </span>
                                </a>
                        </li>
                        <li>
                                <a href="#" class="list-group-item">
                                    <i class="fa fa-upload fa-fw"></i> Usuarios con acceso
                                    <span class="pull-right text-muted small"><em>11:32 AM</em>
                                    </span>
                                </a>
                        </li>
                        <li>
                                <a href="#" class="list-group-item">
                                    <i class="fa fa-bolt fa-fw"></i> Usuarios en sesión
                                    <span class="pull-right text-muted small"><em>11:13 AM</em>
                                    </span>
                                </a>
                        </li>
                        <li>
                                <a href="#" class="list-group-item">
                                    <i class="fa fa-bolt fa-fw"></i> Usuarios fuera de sesión
                                    <span class="pull-right text-muted small"><em>23:45 AM</em>
                                    </span>
                                </a>
                        </li>

                        <li>
                            <a class="text-center" href="#">
                                <strong>Ver todas las Alertas</strong>
                                <i class="fa fa-angle-right"></i>
                            </a>
                        </li>
                    </ul>
                    <!-- /.dropdown-alerts -->
                </li>
                <!-- /.dropdown -->
                <li class="dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                        <i class="fa fa-user fa-fw"></i>  <i class="fa fa-caret-down"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-user">
                        <li><a href="#"><i class="fa fa-user fa-fw"></i> Perfil de usuario</a>
                        </li>
                        <li><a href="#"><i class="fa fa-gear fa-fw"></i> Configuración</a>
                        </li>
                        <li class="divider"></li>
                        <li><a href="login.aspx"><i class="fa fa-sign-out fa-fw"></i> Salir</a>
                        </li>
                    </ul>
                    <!-- /.dropdown-user -->
                </li>
                <!-- /.dropdown -->
            </ul>

            <!-- /.navbar-top-links -->
            <div id="divMenu" runat="server" visible="true" class="navbar-default sidebar" role="navigation">
                <div class="sidebar-nav navbar-collapse">                
                    <ul class="nav" id="side-menu">
                        <li class="sidebar-search">
                            <div class="input-group custom-search-form">
                                <input type="text" class="form-control" placeholder="Search..." />
                                <span class="input-group-btn">
                                <button class="btn btn-default" type="button">
                                    <i class="fa fa-search"></i>
                                </button>
                            </span>
                            </div>
                            <!-- /input-group -->
                        </li>
                        <li>
                            <a class="active" href="Principal.aspx"><i class="fa fa-dashboard fa-fw"></i> Panel de Control</a>
                        </li>
                        <li>
                            <a href="#"><i class="fa fa-bar-chart-o fa-fw"></i> Importaciones<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="ImportTabla.aspx">Desde Tablas</a>
                                </li>
                                <li>
                                    <a href="ImportArchivo.aspx">Desde Archivo</a>
                                </li>
                            </ul>
                            <!-- /.nav-second-level -->
                        </li>
                        <li>
                            <a href="#"><i class="fa fa-bar-chart-o fa-fw"></i> Estructura Documental<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="AltaArchivo.aspx"> Archivos</a>
                                </li>
                                <li>
                                    <a href="AltaCampo.aspx"> Campos</a>
                                </li>
                            </ul>
                            <!-- /.nav-second-level -->
                        </li>

                        <li>
                            <a href="#"><i class="fa fa-wrench fa-fw"></i> Formularios<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="#"> Empleados</a>
                                </li>
                                <li>
                                    <a href="#"> Productos</a>
                                </li>
                                <li>
                                    <a href="#"> Proveedores</a>
                                </li>
                                <li>
                                    <a href="#"> Clientes</a>
                                </li>
                                <li>
                                    <a href="#"> Campos Cultivo</a>
                                </li>
                                <li>
                                    <a href="#"> Fichajes</a>
                                </li>
                            </ul>
                            <!-- /.nav-second-level -->
                        </li>
                        <li>
                            <a href="#"><i class="fa fa-sitemap fa-fw"></i> Codigos QR<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <a href="LoteAuto.aspx"><i class="fa fa-long-arrow-right"></i> Generación e impresión de códigos de lote</a>
                                </li>
                                <li>
                                    <a href="LoteRevi.aspx"><i class="fa fa-long-arrow-right"></i> Revisión lotes y preparación importación a GoldenSoft</a>
                                    <!-- /.nav-third-level -->
                                </li>
                                 <li>
                                    <a href="LoteRevision.aspx"><i class="fa fa-long-arrow-right"></i> Revisión lotes y preparación importación a GoldenSoft sobre lista</a>
                                    <!-- /.nav-third-level -->
                                </li>
                                
                                <li>
                                    <a href="LoteManu.aspx"><i class="fa fa-long-arrow-right"></i> Generación lotes manuales (para lotes externos)</a>
                                    <!-- /.nav-third-level -->
                                </li>
                            </ul>
                            <!-- /.nav-second-level -->
                        </li>
                        <li>
                            <a href="#"><i class="fa fa-sitemap fa-fw"></i> Expediciones<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <%--<li>
                                    <a href="OrdenCarga.aspx"><i class="fa fa-long-arrow-right"></i> Órdenes de Carga</a>
                                </li> --%> 
                                <li>
                                    <a href="OrdenCargaLin.aspx"><i class="fa fa-long-arrow-right"></i> Órdenes de Carga</a>
                                </li>   
                            </ul>
                            <!-- /.nav-second-level -->
                        </li>
                        <li id="Nominas" runat="server" visible="false">
                            <a href="#"><i class="fa fa-sitemap fa-fw"></i> RecoDat<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">      
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="LinkNomina" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:LinkButton id="LinkNomina" type="button" runat="server" style="width:100%; border-style:none; background-color:transparent;" class="pull-left "  onClick="BtLinkNomina_Click" Text=" Nominas de Recodat"></asp:LinkButton>    
                                        </ContentTemplate>
                                     </asp:UpdatePanel>
                                    <%--<a id="MenuNomina" href="RecoNomina.aspx"><i class="fa fa-long-arrow-right"></i> Nominas de Recodat</a>--%>          
                                </li>                               
                            </ul>
                        </li> 
                    </ul>
                </div>
                <!-- /.sidebar-collapse -->
            </div>
            <!-- /.navbar-static-side -->
        </nav>
        <!-- Navigation -->


        
        <div id="pagevistaform" runat="server" ><!-- /#page-wrapper  class="portada"-->


<%--            <asp:UpdatePanel runat="server" ID="popup">
                <ContentTemplate>
                </ContentTemplate>
             </asp:UpdatePanel>

            <asp:UpdateProgress runat="server" ID="Uppopup" AssociatedUpdatePanelID="popup">
                <ProgressTemplate>
                    <div class="centrado">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
            <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="Update">
                <ProgressTemplate>
                    <div class="centrado" style="z-index:1100;">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <%--<asp:UpdateProgress ID="Progress2" runat="server" AssociatedUpdatePanelID="Update2">
                <ProgressTemplate>
                    <div class="centrado" style="z-index:1100;">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
            <asp:UpdateProgress ID="Progress3" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                <ProgressTemplate>
                    <div class="centrado" style="z-index:1100;">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                        <%--<asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="LbmensajeET" BorderStyle="None" border="0" Width="100%" Text=""  />--%>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

             
                    


 <%--           <div runat="server" id="DvPreparado" visible="false" style="height:20%; width:30%;z-index:999;" class="alert alert-grey centrado">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <i id="I1" runat="server" class="fa fa-exclamation-circle fa-2x"></i>
                <asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="Lbmensaje" BorderStyle="None" border="0" Width="100%" Text=" Se eliminarán los registros actuales con una nueva consulta ¿Desea continuar?"  />
                <div class="row" id="cuestion" visible="true" runat="server">
                    <div class="col-lg-6">
                         <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">      
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnAcepta" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Button runat="server" ID="BtnAcepta" Visible="true" tooltip="Aceptar" CssClass="btn btn-success btn-block" Width="100%"  Text="Aceptar" OnClick="checkSi_Click"/>                                                    
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-lg-6">
                        <asp:Button runat="server" ID="BTnNoAcepta" Visible="true" tooltip="Cancelar" CssClass="btn btn-success btn-block" Width="100%"  Text="Cerrar" OnClick="checkNo_Click"/>                 
                    </div>
                </div>
                 <div class="row" id="Asume" visible="false" runat="server">
                    <div class="col-lg-12">
                        <asp:Button runat="server" ID="btnasume" Visible="true" tooltip="Aceptar" CssClass="btn btn-success btn-block" Width="100%"  Text="Aceptar" OnClick="checkOk_Click"/>
                    </div>
                </div>
            </div>--%>
 
           <div runat="server" id="alerta" visible="false" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <i id="IAlert" runat="server" class="fa fa-exclamation-circle"></i>
                <asp:Label runat="server" class="alert alert-info alert-dismissable" ID="TextAlerta" BorderStyle="None" border="0" Width="100%" Text=""  />
           </div>
           <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Lberror"  Text="" > </asp:Label>

            <div class="row">
                  <div class="col-lg-12">
                        <div class="col-lg-8" >
                                <h3 id="H3Titulo" runat="server" visible="true"> Nóminas RecoDat <i class="fa fa-long-arrow-right"></i> <small> “Gestión de Empleados, Jornal, Destajo y Tareas ”  </small>
                                    <asp:Label  type="text"  style=" font-weight: bold; font-size:14px;"  runat="server" ID="Lbhost1"  Text="" > </asp:Label>
                                </h3>
                                <h3 id="H3Desarrollo" runat="server" style="color:red;" visible="false"> DESARROLLO --> Nóminas RecoDat <i class="fa fa-long-arrow-right"></i> <small> “Gestión de Empleados, Jornal, Destajo y Tareas ”  </small>
                                    <asp:Label  type="text"  style=" font-weight: bold; font-size:14px; color:black;"  runat="server" ID="Lbhost2"  Text="" > </asp:Label>
                                </h3>
                        </div>

                        <div runat="server" class="col-lg-1" style=" top:16px;">
                                <input type="image"  class="pull-right text-muted " title="Limpia el filtro general seleccionado a la derecha" src="images/ordencarga25x25.png" onserverclick="ImgOrdenMin_Click"  runat="server" id="ImgOrdenMin" style="border: 0px; " />    
                        </div>
                        <div  runat="server" class="col-lg-3" style=" top:16px;"> 
                                <asp:DropDownList runat="server"  CssClass="form-control" style="position:relative; background-color:#fff; font-size:20px;"  Width="100%"  ID="DrVistaEmpleado" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrVistaEmpleado_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                        </div>

                           
                    </div>
              </div><!-- /.row data-parent="#accordion"-->
            
            <div class="row">
              <div class="col-lg-12">
                 <div class="bs-example">
                    <ul class="nav nav-tabs" style="margin-bottom: 15px;">
                        <li id="Menu0" class="active" runat="server" ><asp:LinkButton ID="aMenu0" runat="server" OnClick="HtmlAnchor_Click" >FILTROS</asp:LinkButton></li>                        
                        <li id="Menu1" class="" runat="server" ><asp:LinkButton ID="aMenu1" runat="server" OnClick="HtmlAnchor_Click" >EMPLEADOS</asp:LinkButton></li>
                        <li id="Menu3" class=""  runat="server" ><asp:LinkButton ID="aMenu3" runat="server" OnClick="HtmlAnchor_Click" >JORNADAS</asp:LinkButton></li>
                        <li id="Menu4" class=""  runat="server" ><asp:LinkButton ID="aMenu4" runat="server" OnClick="HtmlAnchor_Click" >JORNADA-DIA</asp:LinkButton></li>
                        <li id="Menu5" class=""  runat="server" ><asp:LinkButton ID="aMenu5" runat="server" OnClick="HtmlAnchor_Click" >JORNADAS-MES</asp:LinkButton></li>
                        <li id="Menu2" class=""  runat="server" ><asp:LinkButton ID="aMenu2" runat="server" OnClick="HtmlAnchor_Click" >PRODUCCION</asp:LinkButton></li>
                        <li id="Menu6" class=""  runat="server" ><asp:LinkButton ID="aMenu6" runat="server" OnClick="HtmlAnchor_Click" >PROD.ENVASE-DIA</asp:LinkButton></li>
                        <li id="Menu7" class=""  runat="server" ><asp:LinkButton ID="aMenu7" runat="server" OnClick="HtmlAnchor_Click" >PROD.IMPORTE-DIA</asp:LinkButton></li>
                        <li id="Menu8" class=""  runat="server" ><asp:LinkButton ID="aMenu8" runat="server" OnClick="HtmlAnchor_Click" >PROD.IMPORTE-MES</asp:LinkButton></li> 
                        <li id="Menu11" class="" runat="server" ><asp:LinkButton ID="aMenu11" runat="server" OnClick="HtmlAnchor_Click" >FICH.TAREAS</asp:LinkButton></li>
                        <li id="Menu10" class=""  runat="server" ><asp:LinkButton ID="aMenu10" runat="server" OnClick="HtmlAnchor_Click" >TRABAJOS-DIA</asp:LinkButton></li>
                        <li id="Menu9" class=""  runat="server" ><asp:LinkButton ID="aMenu9" runat="server" OnClick="HtmlAnchor_Click" >INFORMES</asp:LinkButton></li>
                   </ul>
                </div>
             </div>
          </div>



            <div class="tab-pane fade active in" visible="true" runat="server" id="accordion0">
                <%--Menus--%>
                <div id="PanelGralFiltro" visible="true" runat="server" class="panel panel-default">
                    <div class="panel-heading" runat="server" id="PanelFiltro" >
                        <div class="row">
                             <div class="col-lg-12"> 
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" style=" font-weight: bold;"> Recogida muestra datos desde RecoDat (Rango mensual)
                                        <%--fa-box-open             <asp:Label  type="text"  Width="90%" style=" font-weight: bold; color:green;"  runat="server" ID="LbCabecera"  Text="" > </asp:Label>--%>                           
                                    </a>                            
                                </h4>
                            </div>                            
                        </div>
                    </div>

                    <div id="Div5" runat="server"  class="panel-collapse collapse in">
                        
                        <div class="panel panel-default">           
                            <div class="panel-body" id="Div6" visible="true" runat="server">
                                 <div class="row">                                                                       
                                        <div class="col-sm-1" >
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label7"  Text="Fecha:" > </asp:Label>
                                            
                                        </div>
                                        <div class="col-sm-2" >
                                            <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca Fecha Desde para filtrar datos"  ID="TxtDesde"  Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-1" >
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label8"  Text="Fecha:" > </asp:Label>
                                        </div>
                                        <div class="col-sm-2" >
                                            <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca Fecha Desde para filtrar datos"  ID="TxtHasta"  Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-1" >
                                            <label runat="server" visible="true"  id="LBCheck" class="switch pull-center">
                                                <input runat="server" id="chkOnOff"  title="Mes anterior o mes en curso" onclick="submitit()" type="checkbox"/><span class="slider round"></span>
                                                <asp:Button ID="btn" runat="server" OnClick="checkListas_Click" Height="0" Width="0" CssClass="hidden" />
                                            </label>
                                        </div>
                                        <div class="col-sm-4" >
                                              <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="LbUltConsulta"  Text="" > </asp:Label>
                                              <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="LdDia"  Text="" > </asp:Label>
                                             <%--<asp:LinkButton id="Lanza80" type="button" runat="server" style="width:40%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onClick="Lanza80_Click" Text='<i title="Consulta General con filtro o sin filtro según selección sobre la base de datos de RecoDat. Elimina la consulta actual visualizada en pantalla." class="fa fa-ban fa-2x"></i>'></asp:LinkButton>--%>    
                                        </div>
                                        <div class="col-lg-1" >
                                           <%-- <asp:UpdatePanel ID="Update2" runat="server" UpdateMode="Conditional">      
                                               <Triggers>
                                                  <asp:AsyncPostBackTrigger ControlID="BtGralConsulta" EventName="Click" />
                                               </Triggers>
                                                <ContentTemplate>--%>
                                                    <asp:LinkButton id="BtGralConsulta" type="button" runat="server" style="width:40%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onClick="BtCuestionGralConsulta_Click" Text='<i title="Consulta General con filtro o sin filtro según selección sobre la base de datos de RecoDat. Elimina la consulta actual visualizada en pantalla." class="fa fa-outdent fa-2x"></i>'></asp:LinkButton>    
                                          <%--      </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>





                    <div id="PanelParticularFiltro" visible="true" runat="server" class="panel panel-default">
                    <%--filtros--%>
                    <div class="panel-heading" runat="server" id="Div33" >
                        <div class="row">
                             <div class="col-lg-10"> 
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" style=" font-weight: bold;"> Filtro de datos para RecoDat (Las casillas activas se aplicarán como filtro)
                                    </a>                            
                                </h4>
                            </div>  
                            <div class="col-sm-1" >
                                         <%--<asp:Button runat="server" ID="BtLimpiafiltro" visible="true" tooltip="Elimina todos los filtros configurados" CssClass="btn btn-success btn-block" Width="100%"  Text="Limpia filtros" OnClick="BtLimpiaTodo_Click"/>--%>
                                 <asp:LinkButton id="BtLimpiafiltro" type="button" runat="server" style="width:40%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onClick="BtLimpiaTodo_Click" Text='<i title="Elimina todos los filtros configurados." class="fa fa-eraser fa-2x"></i>'></asp:LinkButton>    
                           </div>
                            <div class="col-lg-1 " >
                                <asp:UpdatePanel ID="Update" runat="server" UpdateMode="Conditional">      
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="BtGralConsultaMin" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:LinkButton id="BtGralConsultaMin" type="button" runat="server" style="width:40%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onClick="BtGralConsultaMin_Click" Text='<i title="Consulta con filtro o sin filtro según selección de estas casillas, sobre los datos ya recogidos de RecoDat. Esta consulta es ajena a la Base de Datos de RecoDat." class="fa fa-filter fa-2x"></i>'></asp:LinkButton>    
                                                    
                                    </ContentTemplate>
                                </asp:UpdatePanel>    
                            </div>                           
                        </div>
                    </div>
                    <%--filtros--%>

                    <div id="Div34" runat="server"  class="panel-collapse collapse in">
                        
                        <div class="panel panel-default">     
                            <div class="panel-heading" runat="server" id="Div21" >
                                <div class="row">
                                     <div class="col-lg-9"> 
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" style=" font-weight: bold;"> Empleados
                                            </a>                            
                                        </h4>
                                    </div>   
                                    <div class="col-lg-2">
                                        <label runat="server" visible="true"  id="LabelAltas" title="Todos los empleados con y sin alta">Todos los Empleados:</label>
                                    </div>
                                    <div class="col-lg-1" style="text-align:center;"> 
                                        <label runat="server" visible="true"  id="Label50" class="switch pull-center">
                                            <input runat="server" id="CheckTodo"  title="Todos los empleados con y sin alta" onclick="submititTodo()" type="checkbox"/><span class="slider round"></span>
                                            <asp:Button ID="Button1" runat="server" OnClick="checkTodo_Click" Height="0" Width="0" CssClass="hidden" />
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body" id="Div35" visible="true" runat="server">
                                 <div class="row"> 
                                        <div class="col-sm-1" style="text-align:right;" >
                                            <%--<asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label27"  Text="Código:" > </asp:Label>--%>
                                            <asp:linkbutton id="lbFilCodigo" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Código:"></asp:linkbutton>

                                            <button id="BtCodigo" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="IContent" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                        </div>
                                        <div class="col-sm-2" >
                                            <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca uno o más Códigos de Empleado separados por coma. Ejemplo: 00000001, 000000232 "  ID="TxtCodigo"  Font-Bold="True" />
                                        </div>

                                        <div class="col-sm-2" style="text-align:right;">
                                            <%--<asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label18"  Text="Nombre:" > </asp:Label>--%>
                                            <asp:linkbutton id="LinkNombre" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Nombre:"></asp:linkbutton>
                                            <button id="BtNombre" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="INombre" style="color:darkred;" runat="server" title="No deberá contener estos Datos." class="fa fa-hand-o-up fa-2x"></i></button> 
                                        </div>
                                        <div class="col-sm-2" >
                                            <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca un nombre de trabajador. Ejemplo: Rosa "  ID="TxtNombre"  Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-2" style="text-align:right;">
                                            <%--<asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label22"  Text="Apellidos:" > </asp:Label>--%>
                                            <asp:linkbutton id="LinkApellidos" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Apellidos:"></asp:linkbutton>
                                            <button id="BtApellido" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="IApellido" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                        </div>
                                        <div class="col-sm-2" >
                                            <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca un apellido de trabajador. Ejemplo: Sanchez"  ID="TxtApellidos"  Font-Bold="True" />
                                        </div>
                                         <div class="col-sm-1" >
                                        </div>
                                 </div>
                              </div>


                            <div class="panel-body" id="Div36" visible="true" runat="server">
                                 <div class="row"> 
                                    
                                        <div class="col-sm-1" style="text-align:right;">
                                            <%--<asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label32"  Text="Centro:" > </asp:Label>--%>
                                            <asp:linkbutton id="LinkCentro" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Centro:"></asp:linkbutton>
                                            <button id="BtCentro" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="ICentro" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                        </div>
                                        <div class="col-sm-2" >
                                            <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca uno o más Centros de Trabajo separados por coma. Ejemplo: 001, 002 "  ID="TxtCentro"  Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-2" style="text-align:right;">
                                            <%--<asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label33"  Text="Categoría:" > </asp:Label>--%>
                                            <asp:linkbutton id="LinkCategoria" ForeColor="Black"  onClick="lbFilClose_Click" runat="server" Text="Categoría:"></asp:linkbutton>
                                            <button id="BtCategoria" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="ICategoria" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                        </div>
                                        <div class="col-sm-2" >
                                            <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca una o más Categorías separadas por coma. Ejemplo: S1, S2, H1"  ID="TxtCategoria"  Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-2" style="text-align:right;">
                                            <%--<asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label31"  Text="Vivienda:" > </asp:Label>--%>
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
                        </div>





                        <div class="panel panel-default"> 
                            <div class="panel-heading" runat="server" id="Div40" >
                                <div class="row">
                                     <div class="col-lg-12"> 
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" style=" font-weight: bold;"> Fichajes
                                            </a>                            
                                        </h4>
                                    </div>                            
                                </div>
                            </div>
                            <div class="panel-body" id="Div38" visible="true" runat="server">

                            <div class="row"> 
                                <div class="col-sm-1" style="text-align:right;">
                                    <%--<asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label10"  Text="Desde:" > </asp:Label>--%>
                                    <asp:linkbutton id="LinkDesde" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Fecha:"></asp:linkbutton>
                                    <button id="BtnFechaIni" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="IFechaIni" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca una Fecha desde donde buscar."  ID="TxtBFechaIni" Text="" Font-Bold="True" />
                                </div>

                                <div class="col-sm-2" style="text-align:right;">
                                    <%--<asp:Label  type="text"  Width="70%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label34"  Text="Hasta:" > </asp:Label>--%>
                                    <asp:linkbutton id="LinkHasta" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Fecha:"></asp:linkbutton>
                                    <button id="BtnFechaFin" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="IFechaFin" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2" >
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca una Fecha hasta donde buscar."  ID="TxtBFechaFin" Text=""  Font-Bold="True" />
                                </div>
                                <div class="col-sm-2" style="text-align:right;">
                                    <%--<asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label35"  Text="Tablet:" > </asp:Label>--%>
                                    <asp:linkbutton id="LinkTablet" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Tablet:"></asp:linkbutton>
                                    <button id="BtnTablet" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="ITablet"  style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2" >
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca una o más Tablets separadas por coma. Ejemplo: T1, T2"  ID="TxtBTablet" Text=""  Font-Bold="True" />
                                </div>
                            </div>
                        </div>


                            <%--<div class="panel-body" id="Div37" visible="true" runat="server">
                                <div class="row"> 
                                    <div class="col-sm-4" >                                          
                                    </div>
                                    <div class="col-sm-1" >
                                            <asp:Button runat="server" ID="BtDefaultFiltro" visible="true" tooltip="Configura los filtros por defecto" CssClass="btn btn-info btn-block" Width="100%"  Text="Buscar y filtrar" OnClick="BtFiltroDefault_Click"/>
                                    </div>
                                    <div class="col-sm-2" > 
                                            
                                    </div>
                                    <div class="col-sm-1" >
                                    </div>
                                    <div class="col-sm-4" >
                                    </div>
                                </div>
                            </div>--%>

                         </div>



                        <div class="panel panel-default"> 
                            <div class="panel-heading" runat="server" id="Div39" >
                                <div class="row">
                                     <div class="col-lg-12"> 
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" style=" font-weight: bold;"> Producción
                                            </a>                            
                                        </h4>
                                    </div>                            
                                </div>
                            </div>
                            <div class="panel-body" id="Div2" visible="true" runat="server">

                                <div class="row"> 
                                    <div class="col-sm-1" style="text-align:right;">
                                        <%--<asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label28"  Text="Envase:" > </asp:Label>--%>
                                        <asp:linkbutton id="LinkEnvase" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Envase/Inicio-Fin:"></asp:linkbutton>
                                        <button id="BtEnvase" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="IEnvase" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                    </div>
                                    <div class="col-sm-2" >
                                        <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca uno o más Envases separados por coma. Ejemplo: 85, 90 ó Códigos Inicio-Fin: Z1, Z0 "  ID="TxtEnvase" Text="" Font-Bold="True" />
                                    </div>

                                    <div class="col-sm-2" style="text-align:right;">
                                        <%--<asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label29"  Text="Variedad/Tarea:" > </asp:Label>--%>
                                        <asp:linkbutton id="LinkVariedad" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Variedad/Tarea:"></asp:linkbutton>
                                        <button id="BtVariedad" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="IVariedad" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                    </div>
                                    <div class="col-sm-2" >
                                        <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca una o más Variedades o tareas separadas por coma. Ejemplo: V1, T5 "  ID="TxtVariedad" Text=""  Font-Bold="True" />
                                    </div>
                                    <div class="col-sm-2" style="text-align:right;">
                                        <%--<asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label30"  Text="Zona:" > </asp:Label>--%>
                                        <asp:linkbutton id="LinkZona" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Zona:"></asp:linkbutton>
                                        <button id="BtZona" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="IZona" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                    </div>
                                    <div class="col-sm-2" >
                                        <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca una o más Zonas separadas por coma. Ejemplo: S1, P2 "  ID="TxtZona" Text=""  Font-Bold="True" />
                                    </div>
                                </div>


                            </div>

                        </div>










                    </div>
                  </div>  
                </div>                
            </div>   
            <%--Final Menus--%>


           <div class="tab-pane fade" visible="true" runat="server" id="accordion">
               <%--gvEmpleado--%>
                <div id="PanelGeneralCabecera" visible="true" runat="server" class="panel panel-default">
                    <div class="panel-heading" runat="server" id="PanelCabecera" >
                        <div class="row">
                             <div class="col-lg-2"> 
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" style=" font-weight: bold;" onclick="submitit1()"  href="#collapse1"> Lísta Empleados
                                        <%--<asp:Label  type="text"  Width="90%" style=" font-weight: bold; color:green;"  runat="server" ID="LbCabecera"  Text="" > </asp:Label>--%>                           
                                    </a>                            
                                </h4>
                            </div>
 <%--                           <div class="col-sm-1" style="text-align:right;">
                                <asp:linkbutton id="LinkFilEmpleado" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Buscar por:"></asp:linkbutton>
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato en la casilla de texto"  ID="DRgvEmpleado"  OnSelectedIndexChanged="DrFiltro1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="BtnFilEmpleado" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i title="Desactivado" style="color:darkred;" id="IFilEmpleado" runat="server" class="fa fa-hand-o-up fa-2x"></i> </button>
                            </div>
                            <div class="col-sm-2" >
                                <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="TxtConsultaEmpleado"  Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="Button3" type="button" runat="server" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtFilEmpleado_Click"><i title="Busca por la Columna y el contenido de la casilla de texto." class="fa fa-search fa-2x"></i></button> 
                            </div>--%>
                            <%--<div class="col-lg-1">
                                <input type="image"  class="pull-right text-muted " src="images/ordencarga25x25.png" onserverclick="ImageOrden_Click"  runat="server" id="ImgOrden" style="border: 0px; " />    
                            </div>--%>
                            <div class="col-lg-9"> 
                                <%--<asp:DropDownList runat="server"  CssClass="form-control" style="position:relative;"  Width="100%"  ID="DrSelCab" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrSelCab_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                <asp:Button ID="btn1" runat="server" OnClick="check1_Click" Height="0" Width="0" CssClass="hidden" />--%>
                            </div>
                            <div class="col-lg-1"> 


                                <%--<input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/print25X25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image1" style="border: 0px; " />--%>
                                <button id="BtPrintReport" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintNomina_Click">
                                    <i title="Imprime el reporte de la lista de empleados" class="fa fa-file-text-o fa-2x"></i>
                                </button>
                                <button id="btPrintCabecera" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Empleado" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>
                        </div>
                    </div>

                    <div id="collapse1" runat="server"  class="panel-collapse collapse in">
                        <%--gvEmpleado--%>
                        <div class="panel panel-default">              
                            <div class="panel-body" id="EmpresaGV" visible="true" runat="server">
                                 <div class="row">                                                                       
                                        <div class="col-sm-1" >
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label19"  Text="Filas:" > </asp:Label>
                                        </div>
                                        <div class="col-sm-1" >                              
                                           <asp:DropDownList ID="ddCabeceraPageSize" style=" position:absolute;" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvEmpleado_PageSize_Changed">  
                                            </asp:DropDownList>  
                                         </div>
                                        <div class="col-sm-2" >
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowEmpleado"  Text="" > </asp:Label>
                                        </div>
                                        <div class="col-sm-8" >
                                        </div>

                                        <%--<div class="col-sm-3" >
                                            <asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label24"  Text="Ordenar por columna (XLS):" ></asp:Label>
                                        </div>
                                        <div class="col-sm-2" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione primer campo para ordenar datos"  ID="DrEmpleado1"  OnSelectedIndexChanged="DrEmpleado1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-2" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione segundo campo para ordenar datos"  ID="DrEmpleado2"  OnSelectedIndexChanged="DrEmpleado1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>--%>
                                        
                                    </div>
                                 <div class="row">
                                    <div id="Div1" runat="server" style="overflow:auto;" class="panel-body"> <%-- AutoGenerateSelectButton="True"--%>  
                                        <asp:GridView ID="gvEmpleado"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                            AllowSorting="true" OnSorting="gvEmpleado_OnSorting"
                                            CellPadding="4"  GridLines="None"  OnRowDataBound="gvEmpleado_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="15" OnRowCommand="gvEmpleado_RowCommand" DataKeyNames="ID"
                                            oonselectedindexchanged="gvEmpleado_SelectedIndexChanged"  OnRowEditing="gvEmpleado_RowEditing" OnRowCancelingEdit="gvEmpleado_RowCancelingEdit" OnRowUpdating="gvEmpleado_RowUpdating" 
                                            OnRowDeleting="gvEmpleado_RowDeleting" onpageindexchanging="gvEmpleado_PageIndexChanging"  >
                                        <RowStyle />                     
                                            <Columns>

                                                <%--<asp:CommandField ButtonType="Image" 
                                                EditImageUrl="~/Images/editar20x20.png" 
                                                ShowEditButton="True" 
                                                CancelImageUrl="~/Images/cancelar20x20.png" 
                                                CancelText="" 
                                                DeleteText="" 
                                                UpdateImageUrl="~/Images/guardar18x18.png"          
                                                UpdateText="" />   --%>   
                                                                                                   
                                               <%-- <asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtSubeCab" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/sendup20x20.png"
                                                   CommandName="SubeCabecera" ToolTip="Selecciona Registro por Código" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtUbicacion" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/print25X25.png"
                                                   CommandName="Ubicacion" ToolTip="Imprime registro" Width="20px" Height="20px"></asp:ImageButton>
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
                                                <asp:TemplateField HeaderText="Código" SortExpression="COD_EMPLEADO">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="Tabcodigo" runat="server" Text='<%# Eval("COD_EMPLEADO") %>' class="gridCabAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="Labcodigo" runat="server" Text='<%# Bind("COD_EMPLEADO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nombre" SortExpression="NOMBRE">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="Tabnombre" runat="server" Text='<%# Eval("NOMBRE") %>' class="gridCabAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="Labnombre" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Apellidos" SortExpression="APELLIDOS">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="Tabapellido" runat="server" Text='<%# Eval("APELLIDOS") %>' class="gridCabAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="Labapellido" runat="server" Text='<%# Bind("APELLIDOS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Centro" SortExpression="CENTRO">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="Tabcentro" runat="server" Text='<%# Eval("CENTRO") %>' class="gridCabBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="Labcentro" runat="server" Text='<%# Bind("CENTRO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cotización" SortExpression="COTIZACION">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="Tabtrabajo" runat="server" Text='<%# Eval("COTIZACION") %>' class="gridCabBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="Labtrabajo" runat="server" Text='<%# Bind("COTIZACION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Categoría" SortExpression="CATEGORIA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="Tabcategoria" runat="server" Text='<%# Eval("CATEGORIA") %>' class="gridCabAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="Labcategoria" runat="server" Text='<%# Bind("CATEGORIA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Alta" SortExpression="FECHAALTA" >
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="Tabfechaalta" runat="server" Text='<%# Eval("FECHAALTA") %>' class="gridCabAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="Labfechaalta" runat="server" Text='<%# Bind("FECHAALTA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Baja" SortExpression="FECHABAJA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="Tabfechabaja" runat="server" Text='<%# Eval("FECHABAJA") %>' class="gridCabAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="Labfechabaja" runat="server" Text='<%# Bind("FECHABAJA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vivienda" SortExpression="VIVIENDA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="Tabvivienda" runat="server" Text='<%# Eval("VIVIENDA") %>' class="gridCabBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="Labvivienda" runat="server" Text='<%# Bind("VIVIENDA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Alq. Vivienda" SortExpression="VIVIENDA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabAqluilavivienda" runat="server" Text='<%# Eval("ALQVIVIENDA") %>' class="gridCabBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabAqluilavivienda" runat="server" Text='<%# Bind("ALQVIVIENDA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Días-Mes" SortExpression="DIASMES">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="Tabdiames" runat="server" Text='<%# Eval("DIASMES") %>' class="gridCabBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="Labdiames" runat="server" Text='<%# Bind("DIASMES") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Coste Vivienda" SortExpression="COSTEVIVIENDA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabCostevivienda" runat="server" Text='<%# Eval("COSTEVIVIENDA") %>' class="gridCabBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabCostevivienda" runat="server" Text='<%# Bind("COSTEVIVIENDA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nómina" SortExpression="IMPORTE">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="Tabimporte" runat="server" Text='<%# Eval("IMPORTE") %>' class="gridCabBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="Labimporte" runat="server" Text='<%# Bind("IMPORTE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Golden" SortExpression="NOMINA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="Tabnomina" runat="server" Text='<%# Eval("NOMINA") %>' class="gridCabBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="Labnomina" runat="server" Text='<%# Bind("NOMINA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>

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
            <%--final gvEmpleado--%>

            <div class="tab-pane fade active" visible="true" runat="server" id="accordion2">
                <%--gvProduccion--%>          
                <%--<div id="collapse2" runat="server"  class="panel-collapse collapse in">--%>                     
                <div id="PanelGeneralProducion" visible="true" runat="server" class="panel panel-default">
                        <div class="panel-heading" runat="server" id="PanelOrden" >
                            <div class="row">
                                    <div class="col-lg-2"> 
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" style=" font-weight: bold;" onclick="submiti4()"  href="#collapse4"> Lístas de Producción</a>                                             
                                        </h4>
                                    </div>

                            <%--<div class="col-sm-1" style="text-align:right;">
                                <asp:linkbutton id="LinkFilProduccion" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Buscar por:"></asp:linkbutton>
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="DrrowProduccion"  OnSelectedIndexChanged="DrFiltro1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="BtnFilProduccion" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i title="Desactivado" style="color:darkred;" id="IfilProduccion" runat="server" class="fa fa-hand-o-up fa-2x"></i> </button>
                            </div>
                            <div class="col-sm-2" >
                                <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="TxtFilProduccion"  Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="Button29" type="button" runat="server" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtfilProduccion_Click"><i title="Busca por la Columna y el contenido de la casilla de texto." class="fa fa-search fa-2x"></i></button> 
                            </div>--%>
                            <%--<div class="col-lg-1">
                                <input type="image"  class="pull-right text-muted " src="images/ordencarga25x25.png" onserverclick="ImageOrden_Click"  runat="server" id="ImgOrden" style="border: 0px; " />    
                            </div>--%>
                            <div class="col-lg-9"> 
                                <%--<asp:DropDownList runat="server"  CssClass="form-control" style="position:relative;"  Width="100%"  ID="DrSelCab" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrSelCab_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                <asp:Button ID="btn1" runat="server" OnClick="check1_Click" Height="0" Width="0" CssClass="hidden" />--%>
                            </div>
                            <div class="col-lg-1"> 
                    <%--            <input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/print25X25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image2" style="border: 0px; " />
                                <button id="Button30" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                    <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                                </button>--%>
                                <button id="BtPrintOrden" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Producción" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>






                                <%--<div class="col-lg-1">
                                    <input type="image" class="pull-right text-muted " src="images/filtro25x25.png" onserverclick="ImageFiltro_Click" runat="server" id="imgFiltro" style="border: 0px;" />
                                </div>
                                <div class="col-lg-2">
                                    <asp:DropDownList runat="server" CssClass="form-control" Width="100%" ID="DrSelectFiltro" ToolTip="Contiene el filtro para las órdenes de pedidos" OnSelectedIndexChanged="DrSelectFiltro_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                </div>
                                <div class="col-lg-1">
                                    <button id="BtPrintOrden" type="button" runat="server" visible="true" style="width: 100%; border-style: none; background-color: transparent;" class="pull-right text-muted " onserverclick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de órdenes de carga" class="fa fa-file-excel-o fa-2x"></i></button>
                                </div>--%>
                            </div>
                            <asp:Button ID="btn4" runat="server" OnClick="check4_Click" Height="0" Width="0" CssClass="hidden" />
                            <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label15"  Text="" > </asp:Label>
                        </div>

                      
                        <div id="collapse4" runat="server"  class="panel-collapse collapse in">  
                            <%--gvProduccion--%>
                            <div class="panel panel-default">
                                <div class="panel-body" >
                                    <div class="row">
                                        <div class="col-sm-1" >
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label20"  Text="Filas:" > </asp:Label>
                                        </div>
                                        <div class="col-sm-1" >                              
                                           <asp:DropDownList ID="ddControlPageSize" CssClass="form-control" style=" position:absolute;" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvProduccion_PageSize_Changed">  
                                            </asp:DropDownList>
                                         </div>
                                        <div class="col-sm-2" >
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowProduccion"  Text="" > </asp:Label>
                                        </div>
                                        <div class="col-sm-8" >
                                        </div>

                                        <%--<div class="col-sm-3" >
                                           <asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align: right; padding:6px;"  runat="server" ID="Label25"  Text="Ordenar por columna (XLS):" ></asp:Label>      
                                        </div>
                                        <div class="col-sm-2" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione primer campo para ordenar datos"  ID="DrProduccion1"  OnSelectedIndexChanged="DrProduccion1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>
                                        <div class="col-sm-2" >
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione segundo campo para ordenar datos"  ID="DrProduccion2"  OnSelectedIndexChanged="DrProduccion1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                                        </div>--%>

                                    </div>
                                    <!-- barra vertical no actualiza la posicion. style="height:300px; overflow-x:hidden; overflow-:auto;" -->
                                    <div class="row" >
                                        <div id="ContainDiv" runat="server" style="overflow:auto;" class="panel-body"> <%-- AutoGenerateSelectButton="True"--%>  
                                            <asp:GridView ID="gvProduccion"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                                AllowSorting="true" OnSorting="gvProduccion_OnSorting"
                                                CellPadding="4"  GridLines="None"  OnRowDataBound="gvProduccion_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="15" OnRowCommand="gvProduccion_RowCommand" DataKeyNames="ID"
                                                oonselectedindexchanged="gvProduccion_SelectedIndexChanged"  OnRowEditing="gvProduccion_RowEditing" OnRowCancelingEdit="gvProduccion_RowCancelingEdit" OnRowUpdating="gvProduccion_RowUpdating" 
                                                onpageindexchanging="gvProduccion_PageIndexChanging" >
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
                                                    />          --%>   
                                                    
                                               <%-- <asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtbajaOrden" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/sendup20x20.png"
                                                   CommandName="BajaOrden" ToolTip="Selecciona Registro por Código" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                                </asp:TemplateField>

                                                <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtUbicacion" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/print25X25.png"
                                                   CommandName="Ubicacion" ToolTip="Imprime registro" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                                </asp:TemplateField>--%>
                                                
                                                <asp:TemplateField HeaderText="Código" SortExpression="COD_EMPLEADO">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOcodigo" runat="server" Text='<%# Eval("COD_EMPLEADO") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOcodigo" runat="server" Text='<%# Bind("COD_EMPLEADO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nombre" SortExpression="NOMBRE">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOnombre" runat="server" Text='<%# Eval("NOMBRE") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOnombre" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Apellidos" SortExpression="APELLIDOS">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOapellidos" runat="server" Text='<%# Eval("APELLIDOS") %>' class="gridContBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOapellidos" runat="server" Text='<%# Bind("APELLIDOS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Fecha" SortExpression="FECHA_EMPLEADOS">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOfecha" runat="server" Text='<%# Eval("FECHA_EMPLEADOS") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOfecha" runat="server" Text='<%# Bind("FECHA_EMPLEADOS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Hora" SortExpression="HORA_EMPLEADO">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOhora" runat="server" Text='<%# Eval("HORA_EMPLEADO") %>' class="gridContCinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOHora" runat="server" Text='<%# Bind("HORA_EMPLEADO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Tablet" SortExpression="TABLET">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOtablet" runat="server" Text='<%# Eval("TABLET") %>' class="gridContBinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOtablet" runat="server" Text='<%# Bind("TABLET") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Código Finca" SortExpression="CODFINCA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOcodefinca" runat="server" Text='<%# Eval("CODFINCA") %>' class="gridContCinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOcodefinca" runat="server" Text='<%# Bind("CODFINCA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Desc. Finca" SortExpression="DESCRFINCA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOdescfinca" runat="server" Text='<%# Eval("DESCRFINCA") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOdescfinca" runat="server" Text='<%# Bind("DESCRFINCA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Zona" SortExpression="ZONA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOzona" runat="server" Text='<%# Eval("ZONA") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOzona" runat="server" Text='<%# Bind("ZONA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Desc. Zona" SortExpression="DESCRZONAZ">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOdesczona" runat="server" Text='<%# Eval("DESCRZONAZ") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOdesczona" runat="server" Text='<%# Bind("DESCRZONAZ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Variedad" SortExpression="TAREA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOtarea" runat="server" Text='<%# Eval("TAREA") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOtarea" runat="server" Text='<%# Bind("TAREA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Desc. Variedad" SortExpression="DESCRTAREA">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOdescarea" runat="server" Text='<%# Eval("DESCRTAREA") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOdescarea" runat="server" Text='<%# Bind("DESCRTAREA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Envase" SortExpression="ENVASE">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOenvase" runat="server" Text='<%# Eval("ENVASE") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOevase" runat="server"  Text='<%# Bind("ENVASE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Desc. Envase" SortExpression="DESCRENVASE">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOdescenvase" runat="server" Text='<%# Eval("DESCRENVASE") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOdescenvase" runat="server"  Text='<%# Bind("DESCRENVASE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Marca Envase" SortExpression="MARCAENVASE">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOmarcaenvase" runat="server" Text='<%# Eval("MARCAENVASE") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOmarcaenvase" runat="server" Text='<%# Bind("MARCAENVASE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Plantas" SortExpression="PLANTAS">
                                                        <EditItemTemplate>
                                                             <asp:TextBox ID="TabOplantas" runat="server" Text='<%# Eval("PLANTAS") %>' class="gridContAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label ID="LabOplantas" runat="server" Text='<%# Bind("PLANTAS") %>'></asp:Label>
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
                <%--</div>--%>         
              </div><%--<div class="panel-group" id="accordion">--%>
            <%--Final gvProduccion--%>

            <div class="tab-pane fade active" visible="true" runat="server" id="accordion3">
                <%--gvJornada--%>
                <div class="panel panel-default">                     
                <div class="panel-heading" runat="server" id="PanelListas" >
                    <div class="row">
                            <div class="col-lg-2"> 
                            <h4 class="panel-title">
                                <a data-toggle="collapse" style="font-weight: bold;" onclick="submitit5()"  href="#collapse5"> Lístas de Jornada </a>                                            
                            </h4>
                        </div>


  <%--                      <div class="col-sm-1" style="text-align:right;">
                                <asp:linkbutton id="LinkFilJornada" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Buscar por:"></asp:linkbutton>
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="DrConsultaJornada"  OnSelectedIndexChanged="DrFiltro1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="BtnFilJornada" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i title="Desactivado" style="color:darkred;" id="IFilJornada" runat="server" class="fa fa-hand-o-up fa-2x"></i> </button>
                            </div>
                            <div class="col-sm-2" >
                                <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="TxtConsultaJornada"  Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="Button26" type="button" runat="server" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="ConsultagvJornada_Click"><i title="Busca por la Columna y el contenido de la casilla de texto." class="fa fa-search fa-2x"></i></button> 
                            </div>--%>
                            <%--<div class="col-lg-1">
                                <input type="image"  class="pull-right text-muted " src="images/ordencarga25x25.png" onserverclick="ImageOrden_Click"  runat="server" id="ImgOrden" style="border: 0px; " />    
                            </div>--%>
                            <div class="col-lg-9"> 
                                <%--<asp:DropDownList runat="server"  CssClass="form-control" style="position:relative;"  Width="100%"  ID="DrSelCab" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrSelCab_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                <asp:Button ID="btn1" runat="server" OnClick="check1_Click" Height="0" Width="0" CssClass="hidden" />--%>
                            </div>
                            <%--<div class="col-lg-1"> 
                                <button id="Button27" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Cabecera" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>--%>
                            <div class="col-lg-1"> 
                               <%-- <input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/print25X25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image3" style="border: 0px; " />
                                <button id="Button36" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                    <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                                </button>--%>
                                <button id="BtPrintListas" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Jornada" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>


                       <%-- <div class="col-lg-1"> 
                            <input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/etiquetaQR25x25.png" onserverclick="ImageBtn_Click"  runat="server" id="ImgTodo" style="border: 0px; " />
                        </div>
                        <div class="col-lg-1" id="DVtLista" runat="server" visible="true"> 
                            <button id="BtPrintReport" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                            </button>
                        </div>
                        <div class="col-lg-1" id="DVtListaOff" runat="server" visible="false"> 
                            <button id="BtPrintReportOff" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReportOff_Click">
                                <i title="Cierra la vista previa de reportes" class="fa fa-power-off fa-2x"></i>
                            </button>
                        </div>


                        <div class="col-lg-1"> 
                            <button id="BtPrintListas" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la listas de carga camión" class="fa fa-file-excel-o fa-2x"></i></button>
                        </div>--%>
                        
                    </div>
                    <asp:Button ID="btn5" runat="server" OnClick="check5_Click" Height="0" Width="0" CssClass="hidden" />
                    <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label23"  Text="" > </asp:Label>
                </div>
                <div id="collapse5" runat="server"  class="panel-collapse collapse in">
                    <%--gvJornada--%>
                    <div class="panel-body">
                        <div class="row" id="PNFiltrosLista" runat="server" visible="true">
                            <div class="col-sm-1" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label21"  Text="Filas:" > </asp:Label>
                            </div>
                            <div class="col-sm-1" >                              
                                <asp:DropDownList ID="ddListaPageSize" CssClass="form-control" Width="100%" style=" position:absolute;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvJornada_PageSize_Changed">  
                                <asp:ListItem Text="5" Value="5" />    
                                </asp:DropDownList>
                             </div>
                            <div class="col-sm-2" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowJornada"  Text="" > </asp:Label>
                            </div>
                            <div class="col-sm-8" >
                            </div>

                            <%--<div class="col-sm-3" >
                                <asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label26"  Text="Ordenar por columna (XLS):" > </asp:Label>
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione primer campo para ordenar datos"  ID="DrJornada1"  OnSelectedIndexChanged="DrJornada1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione segundo campo para ordenar datos"  ID="DrJornada2"  OnSelectedIndexChanged="DrJornada1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>--%>
                            
                        </div>
                        <div class="row" id="PNGridLista" runat="server" visible="true">
                            <div id="ContainDiv2" runat="server" style="overflow:auto;" class="panel-body"> <%-- AutoGenerateSelectButton="True"--%>                                  
                                <asp:GridView ID="gvJornada"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                    AllowSorting="true" OnSorting="gvJornada_OnSorting"
                                    CellPadding="4"  GridLines="None"  OnRowDataBound="gvJornada_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="15" OnRowCommand="gvJornada_RowCommand" DataKeyNames="ID"
                                    oonselectedindexchanged="gvJornada_SelectedIndexChanged"  OnRowEditing="gvJornada_RowEditing" OnRowCancelingEdit="gvJornada_RowCancelingEdit" OnRowUpdating="gvJornada_RowUpdating" 
                                    OnRowDeleting="gvJornada_RowDeleting" onpageindexchanging="gvJornada_PageIndexChanging" EnablePersistedSelection="True"  >
                                <RowStyle />                     
                                    <Columns>

                                    <%-- <asp:CommandField ButtonType="Image" 
                                                    EditImageUrl="~/Images/editar20x20.png" 
                                                    ShowEditButton="True" 
                                                    CancelImageUrl="~/Images/cancelar20x20.png" 
                                                    CancelText="" 
                                                    DeleteText="" 
                                                    UpdateImageUrl="~/Images/guardar18x18.png"          
                                                    UpdateText="" />          --%>

                                        <%--<asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtbajaOrden" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/sendup20x20.png"
                                                   CommandName="BajaOrden" ToolTip="Selecciona Registro por Código" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                                </asp:TemplateField>

                                                <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtUbicacion" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/print25X25.png"
                                                   CommandName="Ubicacion" ToolTip="Imprime registro" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                                </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Código" SortExpression="COD_EMPLEADO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLcodigo" runat="server" Text='<%# Eval("COD_EMPLEADO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLCodigo" runat="server" Text='<%# Bind("COD_EMPLEADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre" Visible="true" SortExpression="NOMBRE">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNombre" Visible="true" runat="server" Text='<%# Eval("NOMBRE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNombre" Visible="true" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apellidos" SortExpression="APELLIDOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLapellidosa" runat="server" Text='<%# Eval("APELLIDOS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLapellidos" runat="server" Text='<%# Bind("APELLIDOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Jornada" SortExpression="FECHA_JORNADA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLjornada" runat="server" Text='<%# Eval("FECHA_JORNADA") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLjornada" runat="server" Text='<%# Bind("FECHA_JORNADA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hora Inicio" SortExpression="HORAINI">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLhorainicio" runat="server" Text='<%# Eval("HORAINI") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLhorainicio" runat="server" Text='<%# Bind("HORAINI") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hora Fin" SortExpression="HORAFIN">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLhorafin" runat="server" Text='<%# Eval("HORAFIN") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLhorafin" runat="server" Text='<%# Bind("HORAFIN") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fichaje" SortExpression="TRANSCURRIDO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLhoratras" runat="server" Text='<%# Eval("TRANSCURRIDO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLhoratras" runat="server" Text='<%# Bind("TRANSCURRIDO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tablet" SortExpression="RECOTABLET">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLLtablet" runat="server" Text='<%# Eval("RECOTABLET") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLtablet" runat="server" Text='<%# Bind("RECOTABLET") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="Tiempo transcurrido" >
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLtiempo" runat="server" Text='<%# Eval("TOTALMINUTOS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLtiempo" runat="server" Text='<%# Bind("TOTALMINUTOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                       
                                        <asp:TemplateField HeaderText="Importe" >
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLLtablet" runat="server" Text='<%# Eval("TOTALIMPORTE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLIMPORTE" runat="server" Text='<%# Bind("TOTALIMPORTE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        
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
            <%--final gvJornada--%>

            <div class="tab-pane fade active" visible="true" runat="server" id="accordion4">
                 <%--gvJornalHora--%>
                <div class="panel panel-default">                     
                <div class="panel-heading" runat="server" id="Div3" >
                    <div class="row">
                            <div class="col-lg-2"> 
                            <h4 class="panel-title">
                                <a data-toggle="collapse" style="font-weight: bold;" onclick="submitit5()"  href="#collapse5"> Importes por Jornada</a>                                            
                            </h4>
                        </div>

<%--                        <div class="col-sm-1" style="text-align:right;">
                                <asp:linkbutton id="LinkFilJornalHora" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Buscar por:"></asp:linkbutton>    
                        </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="DrrowJornalHora"  OnSelectedIndexChanged="DrFiltro1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="BtnFilJornalHora" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i title="Desactivado" style="color:darkred;" id="IJornalHora" runat="server" class="fa fa-hand-o-up fa-2x"></i> </button>
                            </div>
                            <div class="col-sm-2" >
                                <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="TxtJornalHora"  Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="Button5" type="button" runat="server" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtfilJornalHora_Click"><i title="Busca por la Columna y el contenido de la casilla de texto." class="fa fa-search fa-2x"></i></button> 
                            </div>--%>
                            <%--<div class="col-lg-1">
                                <input type="image"  class="pull-right text-muted " src="images/ordencarga25x25.png" onserverclick="ImageOrden_Click"  runat="server" id="ImgOrden" style="border: 0px; " />    
                            </div>--%>
                            <div class="col-lg-9"> 
                                <%--<asp:DropDownList runat="server"  CssClass="form-control" style="position:relative;"  Width="100%"  ID="DrSelCab" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrSelCab_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                <asp:Button ID="btn1" runat="server" OnClick="check1_Click" Height="0" Width="0" CssClass="hidden" />--%>
                            </div>
                            <%--<div class="col-lg-1"> 
                                <button id="Button31" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Cabecera" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>--%>

                            <div class="col-lg-1"> 
                                <%--<input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/print25X25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image4" style="border: 0px; " />--%>
                               <%-- <button id="BtnPrintJornalHora" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                    <i title="Imprime el reporte de la lista de Importes por Jornada" class="fa fa-file-text-o fa-2x"></i>
                                </button>--%>
                                <button id="BtJornalHora" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Importes por Jornada" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>




                       <%-- <div class="col-lg-1"> 
                            <input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/etiquetaQR25x25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image1" style="border: 0px; " />
                        </div>
                        <div class="col-lg-1" id="Div4" runat="server" visible="true">
                            <button id="Button4" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                            </button>
                        </div>
                        <div class="col-lg-1" id="Div7" runat="server" visible="false">
                            <button id="Button5" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReportOff_Click">
                                <i title="Cierra la vista previa de reportes" class="fa fa-power-off fa-2x"></i>
                            </button>
                        </div>--%>


                        <%--<div class="col-lg-1"> 
                            <button id="BtJornalHora" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la listas de carga camión" class="fa fa-file-excel-o fa-2x"></i></button>
                        </div>--%>
                        
                    </div>
                    <asp:Button ID="Button7" runat="server" OnClick="check5_Click" Height="0" Width="0" CssClass="hidden" />
                    <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label1"  Text="" > </asp:Label>
                </div>
                <div id="Div8" runat="server"  class="panel-collapse collapse in">   
                    <%--gvJornalHora--%>
                    <div class="panel-body">
                        <div class="row" id="Div9" runat="server" visible="true">
                            <div class="col-sm-1" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label2"  Text="Filas:" > </asp:Label>
                            </div>
                            <div class="col-sm-1" >                              
                                <asp:DropDownList ID="DrJornalHora" CssClass="form-control" Width="100%" style=" position:absolute;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvJornalHora_PageSize_Changed">  
                                <asp:ListItem Text="5" Value="5" />    
                                </asp:DropDownList>
                             </div>
                            <div class="col-sm-2" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowJornalHora"  Text="" > </asp:Label>
                            </div>

                            <div class="col-sm-8" >
                            </div>

                           <%-- <div class="col-sm-3" >
                                <asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label3"  Text="Ordenar por columna (XLS):" ></asp:Label> 
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione primer campo para ordenar datos"  ID="DRJornalHora1"  OnSelectedIndexChanged="DRJornalHora1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione segundo campo para ordenar datos"  ID="DRJornalHora2"  OnSelectedIndexChanged="DRJornalHora1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>--%>
                            
                        </div>
                        <div class="row" id="Div10" runat="server" visible="true">
                            <div id="Div11" runat="server" style="overflow:auto;" class="panel-body">                                 
                                <asp:GridView ID="gvJornalHora"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                    AllowSorting="true" OnSorting="gvJornalHora_OnSorting"
                                    CellPadding="4"  GridLines="None"  OnRowDataBound="gvJornalHora_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="15" OnRowCommand="gvJornalHora_RowCommand" DataKeyNames="ID"
                                    oonselectedindexchanged="gvJornalHora_SelectedIndexChanged"  OnRowEditing="gvJornalHora_RowEditing" OnRowCancelingEdit="gvJornalHora_RowCancelingEdit" OnRowUpdating="gvJornalHora_RowUpdating" 
                                    OnRowDeleting="gvJornalHora_RowDeleting" onpageindexchanging="gvJornalHora_PageIndexChanging" EnablePersistedSelection="True"  >
                                <RowStyle />                     
                                    <Columns>

                                    <%-- <asp:CommandField ButtonType="Image" 
                                                    EditImageUrl="~/Images/editar20x20.png"  
                                                    ShowEditButton="True" 
                                                    CancelImageUrl="~/Images/cancelar20x20.png" 
                                                    CancelText="" 
                                                    DeleteText="" 
                                                    UpdateImageUrl="~/Images/guardar18x18.png"          
                                                    UpdateText="" />          --%>

                                       <%--<asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtbajaOrden" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/sendup20x20.png"
                                                   CommandName="BajaOrden" ToolTip="Selecciona Registro por Código" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                      </asp:TemplateField>

                                      <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtUbicacion" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/print25X25.png"
                                                   CommandName="Ubicacion" ToolTip="Imprime registro" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                       </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Código" SortExpression="COD_EMPLEADO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLcodigo" runat="server" Text='<%# Eval("COD_EMPLEADO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLCodigo" runat="server" Text='<%# Bind("COD_EMPLEADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre"  SortExpression="NOMBRE">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNombre" runat="server" Text='<%# Eval("NOMBRE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNombre"  runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apellidos" SortExpression="APELLIDOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNApellido" runat="server" Text='<%# Eval("APELLIDOS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNApellido"  runat="server" Text='<%# Bind("APELLIDOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha" SortExpression="FECHA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLFecha" runat="server" Text='<%# Eval("FECHA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLFecha" runat="server" Text='<%# Bind("FECHA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Hora Inicio" >
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLInicio" runat="server" Text='<%# Eval("HORAINI") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLInicio" runat="server" Text='<%# Bind("HORAINI") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hora Fin" >
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLhoraFin" runat="server" Text='<%# Eval("HORAFIN") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLhoraFin" runat="server" Text='<%# Bind("HORAFIN") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Tiempo transcurrido" >
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLTiempo" runat="server" Text='<%# Eval("TRANSCURRIDO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLTiempo" runat="server" Text='<%# Bind("TRANSCURRIDO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> --%>                                       
                                        <%--<asp:TemplateField HeaderText="Parcial Minutos" >
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabPTiempo" runat="server" Text='<%# Eval("TOTALMINUTOS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabPTiempo" runat="server" Text='<%# Bind("TOTALMINUTOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Importe Parcial" >
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNTiempo" runat="server" Text='<%# Eval("IMPORTEMINUTOS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNTiempo" runat="server" Text='<%# Bind("IMPORTEMINUTOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Total Minutos" >
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabTTiempo" runat="server" Text='<%# Eval("TOTALTIEMPO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabTTiempo" runat="server" Text='<%# Bind("TOTALTIEMPO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Tiempo invertido " SortExpression="TIEMPO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabTTiempoinv" runat="server" Text='<%# Eval("TIEMPO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabTTiempoinv" runat="server" Text='<%# Bind("TIEMPO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Importe" SortExpression="TOTALIMPORTE">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabTTTiempo" runat="server" Text='<%# Eval("TOTALIMPORTE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabTTTiempo" runat="server" Text='<%# Bind("TOTALIMPORTE") %>'></asp:Label>
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
            <%--final gvJornalHora--%>

           <div class="tab-pane fade active" visible="true" runat="server" id="PnJornalHora">
              <%--gvJornalNomina--%>
                <div class="panel panel-default">                     
                <div class="panel-heading" runat="server" id="Div12" >
                    <div class="row">
                            <div class="col-lg-2"> 
                            <h4 class="panel-title">
                                <a data-toggle="collapse" style="font-weight: bold;" onclick="submitit5()"  href="#collapse5">Importe mensual por Jornada</a>                                            
                            </h4>
                        </div>

<%--                        <div class="col-sm-1" style="text-align:right;">
                            <asp:linkbutton id="LinkJornalNomina" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Buscar por:"></asp:linkbutton>    
                        </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="DrrowJornalNomina"  OnSelectedIndexChanged="DrFiltro1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="BtnfilJornalNomina" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i title="Desactivado" style="color:darkred;" id="IfilJornalNomina" runat="server" class="fa fa-hand-o-up fa-2x"></i> </button>
                            </div>
                            <div class="col-sm-2" >
                                <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="TxtFilJornalNomina"  Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="Button9" type="button" runat="server" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtfilJornalNomina_Click"><i title="Busca por la Columna y el contenido de la casilla de texto." class="fa fa-search fa-2x"></i></button> 
                            </div>--%>
                            <%--<div class="col-lg-1">
                                <input type="image"  class="pull-right text-muted " src="images/ordencarga25x25.png" onserverclick="ImageOrden_Click"  runat="server" id="ImgOrden" style="border: 0px; " />    
                            </div>--%>
                            <div class="col-lg-9"> 
                                <%--<asp:DropDownList runat="server"  CssClass="form-control" style="position:relative;"  Width="100%"  ID="DrSelCab" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrSelCab_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                <asp:Button ID="btn1" runat="server" OnClick="check1_Click" Height="0" Width="0" CssClass="hidden" />--%>
                            </div>
                            
                           <%-- <div class="col-lg-1"> 
                                <button id="Button32" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Cabecera" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>--%>
                        <div class="col-lg-1"> 
<%--                                <input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/print25X25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image5" style="border: 0px; " />
                                <button id="Button32" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                    <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                                </button>--%>
                                <button id="btnJornalNomina" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Importe mes Jornada" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>


                        <%--<div class="col-lg-1"> 
                            <input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/etiquetaQR25x25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image2" style="border: 0px; " />
                        </div>
                        <div class="col-lg-1" id="Div13" runat="server" visible="true"> 
                            <button id="Button8" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                            </button>
                        </div>
                        <div class="col-lg-1" id="Div14" runat="server" visible="false"> 
                            <button id="Button9" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReportOff_Click">
                                <i title="Cierra la vista previa de reportes" class="fa fa-power-off fa-2x"></i>
                            </button>
                        </div>--%>


                       <%-- <div class="col-lg-1"> 
                            <button id="btnJornalNomina" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la listas de carga camión" class="fa fa-file-excel-o fa-2x"></i></button>
                        </div>--%>
                        
                    </div>
                    <asp:Button ID="Button11" runat="server" OnClick="check5_Click" Height="0" Width="0" CssClass="hidden" />
                    <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label4"  Text="" > </asp:Label>
                </div>
                <div id="Div15" runat="server"  class="panel-collapse collapse in">
                    <%--gvJornalNomina--%>
                    <div class="panel-body">
                        <div class="row" id="Div16" runat="server" visible="true">
                            <div class="col-sm-1" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label5"  Text="Filas:" > </asp:Label>
                            </div>
                            <div class="col-sm-1" >                              
                                <asp:DropDownList ID="DrJornalNomina" CssClass="form-control" Width="100%" style=" position:absolute;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvJornalNomina_PageSize_Changed">  
                                <asp:ListItem Text="5" Value="5" />    
                                </asp:DropDownList>
                             </div>
                            <div class="col-sm-2" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowJornalNomina"  Text="" > </asp:Label>
                            </div>

                            <div class="col-sm-8" >
                            </div>

                            <%--<div class="col-sm-3" >
                               <asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label6"  Text="Ordenar por columna (XLS):" ></asp:Label> 
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione primer campo para ordenar datos"  ID="DRJornalNomina1"  OnSelectedIndexChanged="DRJornalNomina1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione segundo campo para ordenar datos"  ID="DRJornalNomina2"  OnSelectedIndexChanged="DRJornalNomina1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>--%>

                        </div>
                        <div class="row" id="Div17" runat="server" visible="true">
                            <div id="Div18" runat="server" style="overflow:auto;" class="panel-body">                                  
                                <asp:GridView ID="gvJornalNomina"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                    AllowSorting="true" OnSorting="gvJornalNomina_OnSorting"
                                    CellPadding="4"  GridLines="None"  OnRowDataBound="gvJornalNomina_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="15" OnRowCommand="gvJornalNomina_RowCommand" DataKeyNames="COD_EMPLEADO"
                                    oonselectedindexchanged="gvJornalNomina_SelectedIndexChanged"  OnRowEditing="gvJornalNomina_RowEditing" OnRowCancelingEdit="gvJornada_RowCancelingEdit" OnRowUpdating="gvJornalNomina_RowUpdating" 
                                    OnRowDeleting="gvJornalNomina_RowDeleting" onpageindexchanging="gvJornalNomina_PageIndexChanging"  EnablePersistedSelection="True"  >
                                <RowStyle />                     
                                    <Columns>

                                     <%--<asp:CommandField ButtonType="Image" 
                                                    EditImageUrl="~/Images/editar20x20.png" 
                                                    ShowEditButton="True" 
                                                    CancelImageUrl="~/Images/cancelar20x20.png" 
                                                    CancelText="" 
                                                    DeleteText="" 
                                                    UpdateImageUrl="~/Images/guardar18x18.png"          
                                                    UpdateText="" />      --%>    

                                        <%--<asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtbajaOrden" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/sendup20x20.png"
                                                   CommandName="BajaOrden" ToolTip="Selecciona Registro por Código" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                        </asp:TemplateField>

                                         <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtUbicacion" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/print25X25.png"
                                                   CommandName="Ubicacion" ToolTip="Imprime registro" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                         </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Código" SortExpression="COD_EMPLEADO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLcodigo" runat="server" Text='<%# Eval("COD_EMPLEADO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLCodigo" runat="server" Text='<%# Bind("COD_EMPLEADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre" SortExpression="NOMBRE">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNombre" runat="server" Text='<%# Eval("NOMBRE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNombre"  runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Apellidos" SortExpression="APELLIDOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNNombre" runat="server" Text='<%# Eval("APELLIDOS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNNombre"  runat="server" Text='<%# Bind("APELLIDOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Categoría" SortExpression="CATEGORIA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLjornada" runat="server" Text='<%# Eval("CATEGORIA") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLjornada" runat="server" Text='<%# Bind("CATEGORIA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Minutos" >
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLhorainicio" runat="server" Text='<%# Eval("MINUTOS") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLhorainicio" runat="server" Text='<%# Bind("MINUTOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Tiempo trabajado" SortExpression="TIEMPO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLhorainicioinv" runat="server" Text='<%# Eval("TIEMPO") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLhorainicioinv" runat="server" Text='<%# Bind("TIEMPO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Importe" SortExpression="IMPORTE">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLhorafin" runat="server" Text='<%# Eval("IMPORTE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLhorafin" runat="server" Text='<%# Bind("IMPORTE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>    
                                        
                                        <%--<asp:TemplateField HeaderText="Dias Mes" SortExpression="DIASMES">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLdiames" runat="server" Text='<%# Eval("DIASMES") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLdiames" runat="server" Text='<%# Bind("DIASMES") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Vivienda" SortExpression="VIVIENDA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLVivienda" runat="server" Text='<%# Eval("VIVIENDA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLVivienda" runat="server" Text='<%# Bind("VIVIENDA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Alquiler" SortExpression="ALQVIVIENDA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLAlquiler" runat="server" Text='<%# Eval("ALQVIVIENDA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLAlquiler" runat="server" Text='<%# Bind("ALQVIVIENDA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Coste vivienda" SortExpression="COSTEVIVIENDA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLCosteAlquiler" runat="server" Text='<%# Eval("COSTEVIVIENDA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLCosteAlquiler" runat="server" Text='<%# Bind("COSTEVIVIENDA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Total" SortExpression="TOTAL">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLCostetotal" runat="server" Text='<%# Eval("TOTAL") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLCostetotal" runat="server" Text='<%# Bind("TOTAL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="Nomina" SortExpression="NOMINA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLnomina" runat="server" Text='<%# Eval("NOMINA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLnomina" runat="server" Text='<%# Bind("NOMINA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  --%>

                                        
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
             <%--final gvJornalNomina--%>

          <div class="tab-pane fade active" visible="true" runat="server" id="PnJornalNomina">
               <%--gvDestajoNomina--%>
                <div class="panel panel-default">                     
                <div class="panel-heading" runat="server" id="Div19" >
                    <div class="row">
                            <div class="col-lg-2"> 
                            <h4 class="panel-title">
                                <a data-toggle="collapse" style="font-weight: bold;" onclick="submitit5()"  href="#collapse5"> Producción por envase y día</a>                                            
                            </h4>
                        </div>


                       <%-- <div class="col-sm-1" style="text-align:right;">
                            <asp:linkbutton id="LinkFilDestajoNomina" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Buscar por:"></asp:linkbutton>    
                        </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="DrrowDestajoNimina"  OnSelectedIndexChanged="DrFiltro1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="BtnFilDestajoNomina" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i title="Desactivado" style="color:darkred;" id="IfilDestajoNomina" runat="server" class="fa fa-hand-o-up fa-2x"></i> </button>
                            </div>
                            <div class="col-sm-2" >
                                <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="TxtfilDestajoNomina"  Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="Button13" type="button" runat="server" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtfilDestajoNomina_Click"><i title="Busca por la Columna y el contenido de la casilla de texto." class="fa fa-search fa-2x"></i></button> 
                            </div>--%>
                            <%--<div class="col-lg-1">
                                <input type="image"  class="pull-right text-muted " src="images/ordencarga25x25.png" onserverclick="ImageOrden_Click"  runat="server" id="ImgOrden" style="border: 0px; " />    
                            </div>--%>
                            <div class="col-lg-9"> 
                                <%--<asp:DropDownList runat="server"  CssClass="form-control" style="position:relative;"  Width="100%"  ID="DrSelCab" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrSelCab_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                <asp:Button ID="btn1" runat="server" OnClick="check1_Click" Height="0" Width="0" CssClass="hidden" />--%>
                            </div>
                            
                        <%--<div class="col-lg-1"> 
                                <button id="Button33" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Cabecera" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>--%>

                        <div class="col-lg-1"> 
                                <%--<input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/print25X25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image6" style="border: 0px; " />--%>
                                <button id="BtnPrintDestajoNomina" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                    <i title="Imprime el reporte de la lista de Producción envase día" class="fa fa-file-text-o fa-2x"></i>
                                </button>
                                <button id="BtnDestajoNomina" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Producción envase día" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>




                        <%--<div class="col-lg-1"> 
                            <input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/etiquetaQR25x25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image3" style="border: 0px; " />
                        </div>
                        <div class="col-lg-1" id="Div20" runat="server" visible="true"> 
                            <button id="Button12" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                            </button>
                        </div>
                        <div class="col-lg-1" id="Div21" runat="server" visible="false"> 
                            <button id="Button13" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReportOff_Click">
                                <i title="Cierra la vista previa de reportes" class="fa fa-power-off fa-2x"></i>
                            </button>
                        </div>--%>


                        <%--<div class="col-lg-1"> 
                            <button id="BtnDestajoNomina" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la listas de carga camión" class="fa fa-file-excel-o fa-2x"></i></button>
                        </div>--%>
                        
                    </div>
                    <asp:Button ID="Button15" runat="server" OnClick="check5_Click" Height="0" Width="0" CssClass="hidden" />
                    <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label11"  Text="" > </asp:Label>
                </div>
                <div id="Div22" runat="server"  class="panel-collapse collapse in">  
                    <%--gvDestajoNomina--%>
                    <div class="panel-body">
                        <div class="row" id="Div23" runat="server" visible="true">
                            <div class="col-sm-1" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label12"  Text="Filas:" > </asp:Label>
                            </div>
                            <div class="col-sm-1" >                              
                                <asp:DropDownList ID="DrDestajoNomina" CssClass="form-control" Width="100%" style=" position:absolute;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvDestajoNomina_PageSize_Changed">  
                                <asp:ListItem Text="5" Value="5" />    
                                </asp:DropDownList>
                             </div>
                            <div class="col-sm-2" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowDestajoNomina"  Text="" > </asp:Label>
                            </div>

                            <div class="col-sm-8" >
                            </div>

                           <%-- <div class="col-sm-3" >
                                <asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label13"  Text="Ordenar por columna (XLS):" ></asp:Label> 
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione primer campo para ordenar datos"  ID="DRDestajoNomina1"  OnSelectedIndexChanged="DRDestajoNomina1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione segundo campo para ordenar datos"  ID="DRDestajoNomina2"  OnSelectedIndexChanged="DRDestajoNomina1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>--%>
                            
                        </div>
                        <div class="row" id="Div24" runat="server" visible="true">
                            <div id="Div25" runat="server" style="overflow:auto;" class="panel-body">                                
                                <asp:GridView ID="gvDestajoNomina"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                    AllowSorting="true" OnSorting="gvDestajoNomina_OnSorting"
                                    CellPadding="4"  GridLines="None"  OnRowDataBound="gvDestajoNomina_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="15" OnRowCommand="gvDestajoNomina_RowCommand" DataKeyNames="COD_EMPLEADO"
                                    oonselectedindexchanged="gvDestajoNomina_SelectedIndexChanged"  OnRowEditing="gvDestajoNomina_RowEditing" OnRowCancelingEdit="gvDestajoNomina_RowCancelingEdit" OnRowUpdating="gvDestajoNomina_RowUpdating" 
                                    OnRowDeleting="gvDestajoNomina_RowDeleting" onpageindexchanging="gvDestajoNomina_PageIndexChanging" EnablePersistedSelection="True"  >
                                <RowStyle />                     
                                    <Columns>

                                     <%--<asp:CommandField ButtonType="Image" 
                                                    EditImageUrl="~/Images/editar20x20.png" 
                                                    ShowEditButton="True" 
                                                    CancelImageUrl="~/Images/cancelar20x20.png" 
                                                    CancelText="" 
                                                    DeleteText="" 
                                                    UpdateImageUrl="~/Images/guardar18x18.png"          
                                                    UpdateText="" /> --%>         

                                        <%--<asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtbajaOrden" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/sendup20x20.png"
                                                   CommandName="BajaOrden" ToolTip="Selecciona Registro por Código" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                        </asp:TemplateField>

                                         <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtUbicacion" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/print25X25.png"
                                                   CommandName="Ubicacion" ToolTip="Imprime registro" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                        </asp:TemplateField>--%>
                                  
                                        <asp:TemplateField HeaderText="Código" SortExpression="COD_EMPLEADO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLcodigo" runat="server" Text='<%# Eval("COD_EMPLEADO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="Lab6LCodigo" runat="server" Text='<%# Bind("COD_EMPLEADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre"  SortExpression="NOMBRE">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNombre" runat="server" Text='<%# Eval("NOMBRE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNombre"  runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>          
                                        <asp:TemplateField HeaderText="Apellidos"  SortExpression="APELLIDOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNApellidos" runat="server" Text='<%# Eval("APELLIDOS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabNApellidos"  runat="server" Text='<%# Bind("APELLIDOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>        
                                        <asp:TemplateField HeaderText="Fecha" SortExpression="FECHA_EMPLEADOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLjornada" runat="server" Text='<%# Eval("FECHA_EMPLEADOS") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLjornada" runat="server" Text='<%# Bind("FECHA_EMPLEADOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción Envases" SortExpression="DESCRCAJAS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLhorainicio" runat="server" Text='<%# Eval("DESCRCAJAS") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLhorainicio" runat="server" Text='<%# Bind("DESCRCAJAS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Envases" SortExpression="CAJAS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNEnvase" runat="server" Text='<%# Eval("CAJAS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNEnvase" runat="server" Text='<%# Bind("CAJAS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                       <%-- <asp:TemplateField HeaderText="Descripción Manojo" SortExpression="DESCRMANOJOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLDESCRMANOJOS" runat="server" Text='<%# Eval("DESCRMANOJOS") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLDESCRMANOJOS" runat="server" Text='<%# Bind("DESCRMANOJOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Envase" SortExpression="MANOJOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLMANOJOS" runat="server" Text='<%# Eval("MANOJOS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLMANOJOS" runat="server" Text='<%# Bind("MANOJOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>


                                        <asp:TemplateField HeaderText="Plantas" SortExpression="PLANTAS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNPlantas" runat="server" Text='<%# Eval("PLANTAS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNPlantas" runat="server" Text='<%# Bind("PLANTAS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Importe" SortExpression="IMPORTE">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLImporte" runat="server" Text='<%# Eval("IMPORTE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLImporte" runat="server" Text='<%# Bind("IMPORTE") %>'></asp:Label>
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
           <%--Final gvDestajoNomina--%>


          <div class="tab-pane fade active" visible="true" runat="server" id="PnProddiaImporte">
              <%--PnProddiaImporte--%>
               <div class="panel panel-default">                     
                <div class="panel-heading" runat="server" id="Div4" >
                    <div class="row">
                            <div class="col-lg-2"> 
                            <h4 class="panel-title">
                                <a data-toggle="collapse" style="font-weight: bold;" onclick="submitit5()"  href="#collapse5"> Producción importe día </a>                                            
                            </h4>
                        </div>


<%--                        <div class="col-sm-1" style="text-align:right;">
                            <asp:linkbutton id="LinkFilProddiaImporte" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Buscar por:"></asp:linkbutton>       
                        </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="DrrowProdDiaImporte"  OnSelectedIndexChanged="DrFiltro1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="BtnFilProddiaImporte" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i title="Desactivado" style="color:darkred;" id="IfilProddiaImporte" runat="server" class="fa fa-hand-o-up fa-2x"></i> </button>
                            </div>
                            <div class="col-sm-2" >
                                <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="TxtfilProddiaImporte"  Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="Button37" type="button" runat="server" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtFilProddiaImporte_Click"><i title="Busca por la Columna y el contenido de la casilla de texto." class="fa fa-search fa-2x"></i></button> 
                            </div>--%>
                            <%--<div class="col-lg-1">
                                <input type="image"  class="pull-right text-muted " src="images/ordencarga25x25.png" onserverclick="ImageOrden_Click"  runat="server" id="ImgOrden" style="border: 0px; " />    
                            </div>--%>
                            <div class="col-lg-9"> 
                                <%--<asp:DropDownList runat="server"  CssClass="form-control" style="position:relative;"  Width="100%"  ID="DrSelCab" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrSelCab_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                <asp:Button ID="btn1" runat="server" OnClick="check1_Click" Height="0" Width="0" CssClass="hidden" />--%>
                            </div>
                            
                        <%--<div class="col-lg-1"> 
                                <button id="Button33" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Cabecera" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>--%>

                        <div class="col-lg-1"> 
                                <%--<input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/print25X25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image8" style="border: 0px; " />
                                <button id="Button38" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                    <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                                </button>--%>
                                <button id="BtnProdImpDia" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Producción importe día" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>




                        <%--<div class="col-lg-1"> 
                            <input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/etiquetaQR25x25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image3" style="border: 0px; " />
                        </div>
                        <div class="col-lg-1" id="Div20" runat="server" visible="true"> 
                            <button id="Button12" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                            </button>
                        </div>
                        <div class="col-lg-1" id="Div21" runat="server" visible="false"> 
                            <button id="Button13" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReportOff_Click">
                                <i title="Cierra la vista previa de reportes" class="fa fa-power-off fa-2x"></i>
                            </button>
                        </div>--%>


                        <%--<div class="col-lg-1"> 
                            <button id="BtnDestajoNomina" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la listas de carga camión" class="fa fa-file-excel-o fa-2x"></i></button>
                        </div>--%>
                        
                    </div>
                    <asp:Button ID="Button40" runat="server" OnClick="check5_Click" Height="0" Width="0" CssClass="hidden" />
                    <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label43"  Text="" > </asp:Label>
                </div>
                <div id="Div7" runat="server"  class="panel-collapse collapse in">  
                    <%--PnProddiaImporte--%>
                    <div class="panel-body">
                        <div class="row" id="Div13" runat="server" visible="true">
                            <div class="col-sm-1" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label44"  Text="Filas:" > </asp:Label>
                            </div>
                            <div class="col-sm-1" >                              
                                <asp:DropDownList ID="DdgvProdImpDiaPage" CssClass="form-control" Width="100%" style=" position:absolute;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvProdImpDia_PageSize_Changed">  
                                <asp:ListItem Text="5" Value="5" />    
                                </asp:DropDownList>
                             </div>
                            <div class="col-sm-2" >
                                 <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowProddiaImporte"  Text="" > </asp:Label>
                            </div>
                            <div class="col-sm-8" >
                            </div>

                           <%-- <div class="col-sm-3" >
                                <asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label45"  Text="Ordenar por columna (XLS):" ></asp:Label> 
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione primer campo para ordenar datos"  ID="DrProdImpDia1"  OnSelectedIndexChanged="DrProdImpDia1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione segundo campo para ordenar datos"  ID="DrProdImpDia2"  OnSelectedIndexChanged="DrProdImpDia1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>--%>
                            
                        </div>
                        <div class="row" id="Div14" runat="server" visible="true">
                            <div id="Div20" runat="server" style="overflow:auto;" class="panel-body">                                
                                <asp:GridView ID="gvProdImpDia"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                    AllowSorting="true" OnSorting="gvProdImpDia_OnSorting"
                                    CellPadding="4"  GridLines="None"  OnRowDataBound="gvProdImpDia_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="15" OnRowCommand="gvProdImpDia_RowCommand" DataKeyNames="COD_EMPLEADO"
                                    oonselectedindexchanged="gvProdImpDia_SelectedIndexChanged"  OnRowEditing="gvProdImpDia_RowEditing" OnRowCancelingEdit="gvProdImpDia_RowCancelingEdit" OnRowUpdating="gvProdImpDia_RowUpdating" 
                                    OnRowDeleting="gvProdImpDia_RowDeleting" onpageindexchanging="gvProdImpDia_PageIndexChanging"  EnablePersistedSelection="True"  >
                                <RowStyle />                     
                                    <Columns>

                                     <%--<asp:CommandField ButtonType="Image" 
                                                    EditImageUrl="~/Images/editar20x20.png" 
                                                    ShowEditButton="True" 
                                                    CancelImageUrl="~/Images/cancelar20x20.png" 
                                                    CancelText="" 
                                                    DeleteText="" 
                                                    UpdateImageUrl="~/Images/guardar18x18.png"          
                                                    UpdateText="" /> --%>         

                                        <%--<asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtbajaOrden" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/sendup20x20.png"
                                                   CommandName="BajaOrden" ToolTip="Selecciona Registro por Código" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                        </asp:TemplateField>

                                         <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtUbicacion" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/print25X25.png"
                                                   CommandName="Ubicacion" ToolTip="Imprime registro" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                        </asp:TemplateField>--%>
                                  
                                        <asp:TemplateField HeaderText="Código" SortExpression="COD_EMPLEADO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLPcodigo" runat="server" Text='<%# Eval("COD_EMPLEADO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="Lab6LPCodigo" runat="server" Text='<%# Bind("COD_EMPLEADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre"  SortExpression="NOMBRE">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLPNombre" runat="server" Text='<%# Eval("NOMBRE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLPNombre"  runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>          
                                        <asp:TemplateField HeaderText="Apellidos"  SortExpression="APELLIDOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLPApellidos" runat="server" Text='<%# Eval("APELLIDOS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabPApellidos"  runat="server" Text='<%# Bind("APELLIDOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>        
                                        <asp:TemplateField HeaderText="Fecha" SortExpression="FECHA_EMPLEADOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLPjornada" runat="server" Text='<%# Eval("FECHA_EMPLEADOS") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLPjornada" runat="server" Text='<%# Bind("FECHA_EMPLEADOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plantas" SortExpression="PLANTAS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLPPlantas" runat="server" Text='<%# Eval("PLANTAS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLPPlantas" runat="server" Text='<%# Bind("PLANTAS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Importe" SortExpression="IMPORTE">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLPImporte" runat="server" Text='<%# Eval("IMPORTE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLPImporte" runat="server" Text='<%# Bind("IMPORTE") %>'></asp:Label>
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
            <%--Final PnProddiaImporte--%>
            



            <div class="tab-pane fade active" visible="true" runat="server" id="PanelRaw">
              <%--gvpanelTareas--%>
                <div class="panel panel-default">                     
                <div class="panel-heading" runat="server" id="Div45" >
                    <div class="row">
                            <div class="col-lg-2"> 
                            <h4 class="panel-title">
                                <a data-toggle="collapse" style="font-weight: bold;" onclick="submitit5()"  href="#collapse5">Recodat en bruto</a>                                            
                            </h4>
                        </div>

<%--                        <div class="col-sm-1" style="text-align:right;">
                            <asp:linkbutton id="LinkJornalNomina" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Buscar por:"></asp:linkbutton>    
                        </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="DrrowJornalNomina"  OnSelectedIndexChanged="DrFiltro1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="BtnfilJornalNomina" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i title="Desactivado" style="color:darkred;" id="IfilJornalNomina" runat="server" class="fa fa-hand-o-up fa-2x"></i> </button>
                            </div>
                            <div class="col-sm-2" >
                                <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="TxtFilJornalNomina"  Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="Button9" type="button" runat="server" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtfilJornalNomina_Click"><i title="Busca por la Columna y el contenido de la casilla de texto." class="fa fa-search fa-2x"></i></button> 
                            </div>--%>
                            <%--<div class="col-lg-1">
                                <input type="image"  class="pull-right text-muted " src="images/ordencarga25x25.png" onserverclick="ImageOrden_Click"  runat="server" id="ImgOrden" style="border: 0px; " />    
                            </div>--%>
                            <div class="col-lg-9"> 
                                <%--<asp:DropDownList runat="server"  CssClass="form-control" style="position:relative;"  Width="100%"  ID="DrSelCab" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrSelCab_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                <asp:Button ID="btn1" runat="server" OnClick="check1_Click" Height="0" Width="0" CssClass="hidden" />--%>
                            </div>
                            
                           <%-- <div class="col-lg-1"> 
                                <button id="Button32" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Cabecera" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>--%>
                        <div class="col-lg-1"> 
<%--                                <input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/print25X25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image5" style="border: 0px; " />
                                <button id="Button32" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                    <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                                </button>--%>
                                <button id="BtnTareasXLS" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Importe mes Jornada" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>


                        <%--<div class="col-lg-1"> 
                            <input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/etiquetaQR25x25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image2" style="border: 0px; " />
                        </div>
                        <div class="col-lg-1" id="Div13" runat="server" visible="true"> 
                            <button id="Button8" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                            </button>
                        </div>
                        <div class="col-lg-1" id="Div14" runat="server" visible="false"> 
                            <button id="Button9" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReportOff_Click">
                                <i title="Cierra la vista previa de reportes" class="fa fa-power-off fa-2x"></i>
                            </button>
                        </div>--%>


                       <%-- <div class="col-lg-1"> 
                            <button id="btnJornalNomina" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la listas de carga camión" class="fa fa-file-excel-o fa-2x"></i></button>
                        </div>--%>
                        
                    </div>
                    <asp:Button ID="Button3" runat="server" OnClick="check5_Click" Height="0" Width="0" CssClass="hidden" />
                    <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label3"  Text="" > </asp:Label>
                </div>
                <div id="Div46" runat="server"  class="panel-collapse collapse in">
                    <%--gvpanelRaw--%>
                    <div class="panel-body">
                        <div class="row" id="Div47" runat="server" visible="true">
                            <div class="col-sm-1" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label6"  Text="Filas:" > </asp:Label>
                            </div>
                            <div class="col-sm-1" >                              
                                <asp:DropDownList ID="DrpanelRaw" CssClass="form-control" Width="100%" style=" position:absolute;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvpanelTareas_PageSize_Changed">  
                                <asp:ListItem Text="5" Value="5" />    
                                </asp:DropDownList>
                             </div>
                            <div class="col-sm-2" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="LbPanelRaw"  Text="" > </asp:Label>
                            </div>

                            <div class="col-sm-6" >
                            </div>
                            <div class="col-sm-2" >
                                  <asp:Button runat="server" ID="BtMarcaError" Visible="true" tooltip="Muestra en otro color los fichajes erróneos " CssClass="btn btn-success btn-block"  Width="100%"  Text="Mostrar erróneos" OnClick="BtMarcaError_Click"/>                              
                            </div>

                            <%--<div class="col-sm-3" >
                               <asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label6"  Text="Ordenar por columna (XLS):" ></asp:Label> 
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione primer campo para ordenar datos"  ID="DRJornalNomina1"  OnSelectedIndexChanged="DRJornalNomina1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione segundo campo para ordenar datos"  ID="DRJornalNomina2"  OnSelectedIndexChanged="DRJornalNomina1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>--%>

                        </div>
                        <div class="row" id="Div48" runat="server" visible="true">
                            <div id="Div49" runat="server" style="overflow:auto;" class="panel-body">                                  
                                <asp:GridView ID="gvpanelTareas"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                    AllowSorting="true" OnSorting="gvpanelTareas_OnSorting"
                                    CellPadding="4"  GridLines="None"  OnRowDataBound="gvpanelTareas_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="15" OnRowCommand="gvpanelTareas_RowCommand" DataKeyNames="COD_EMPLEADO"
                                    oonselectedindexchanged="gvpanelTareas_SelectedIndexChanged"  OnRowEditing="gvpanelTareas_RowEditing" OnRowCancelingEdit="gvpanelTareas_RowCancelingEdit" OnRowUpdating="gvpanelTareas_RowUpdating" 
                                    OnRowDeleting="gvpanelTareas_RowDeleting" onpageindexchanging="gvpanelTareas_PageIndexChanging"  EnablePersistedSelection="True"  >
                                <RowStyle />                     
                                    <Columns>

                                     <%--<asp:CommandField ButtonType="Image" 
                                                    EditImageUrl="~/Images/editar20x20.png" 
                                                    ShowEditButton="True" 
                                                    CancelImageUrl="~/Images/cancelar20x20.png" 
                                                    CancelText="" 
                                                    DeleteText="" 
                                                    UpdateImageUrl="~/Images/guardar18x18.png"          
                                                    UpdateText="" />      --%>    

                                        <%--<asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtbajaOrden" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/sendup20x20.png"
                                                   CommandName="BajaOrden" ToolTip="Selecciona Registro por Código" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                        </asp:TemplateField>

                                         <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtUbicacion" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/print25X25.png"
                                                   CommandName="Ubicacion" ToolTip="Imprime registro" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                         </asp:TemplateField>--%>

                                        

                                        <asp:TemplateField HeaderText="Código" SortExpression="COD_EMPLEADO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLcodigo" runat="server" Text='<%# Eval("COD_EMPLEADO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLCodigo" runat="server" Text='<%# Bind("COD_EMPLEADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre" SortExpression="NOMBRE">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNombre" runat="server" Text='<%# Eval("NOMBRE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNombre"  runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Apellidos" SortExpression="APELLIDOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNNombre" runat="server" Text='<%# Eval("APELLIDOS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNNombre"  runat="server" Text='<%# Bind("APELLIDOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        
                                        <asp:TemplateField HeaderText="Fecha" SortExpression="FECHA_EMPLEADOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLjornada" runat="server" Text='<%# Eval("FECHA_EMPLEADOS") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLjornada" runat="server" Text='<%# Bind("FECHA_EMPLEADOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Minutos" >
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLhorainicio" runat="server" Text='<%# Eval("MINUTOS") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLhorainicio" runat="server" Text='<%# Bind("MINUTOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Hora ajustada" SortExpression="HORA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLhorainicioinv" runat="server" Text='<%# Eval("HORA") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLhorainicioinv" runat="server" Text='<%# Bind("HORA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hora" SortExpression="HORA_AJUSTADA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLhoraajustada" runat="server" Text='<%# Eval("HORA_AJUSTADA") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLhoraajustada" runat="server" Text='<%# Bind("HORA_AJUSTADA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        
                                        <asp:TemplateField HeaderText="Tablet" SortExpression="TABLET">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLhorafin" runat="server" Text='<%# Eval("TABLET") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLhorafin" runat="server" Text='<%# Bind("TABLET") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>    
                                        
                                        <asp:TemplateField HeaderText="Cod. Finca" SortExpression="CODFINCA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLdiames" runat="server" Text='<%# Eval("CODFINCA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLdiames" runat="server" Text='<%# Bind("CODFINCA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        
                                        <asp:TemplateField HeaderText="Desc. Finca" SortExpression="DESCRFINCA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLVivienda" runat="server" Text='<%# Eval("DESCRFINCA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLVivienda" runat="server" Text='<%# Bind("DESCRFINCA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Zona" SortExpression="ZONA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLAlquiler" runat="server" Text='<%# Eval("ZONA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLAlquiler" runat="server" Text='<%# Bind("ZONA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        
                                        <asp:TemplateField HeaderText="Desc. Zona" SortExpression="DESCRZONAZ">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLCosteAlquiler" runat="server" Text='<%# Eval("DESCRZONAZ") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLCosteAlquiler" runat="server" Text='<%# Bind("DESCRZONAZ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Tarea" SortExpression="TAREA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLCostetotal" runat="server" Text='<%# Eval("TAREA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLCostetotal" runat="server" Text='<%# Bind("TAREA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="Desc. Tarea" SortExpression="DESCRTAREA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLnomina" runat="server" Text='<%# Eval("DESCRTAREA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLnomina" runat="server" Text='<%# Bind("DESCRTAREA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="Inicio-Fin" SortExpression="INIFIN">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLnomina" runat="server" Text='<%# Eval("INIFIN") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLnomina" runat="server" Text='<%# Bind("INIFIN") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="Desc. Inicio-Fin" SortExpression="DESCRINIFIN">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLnomina" runat="server" Text='<%# Eval("DESCRINIFIN") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLnomina" runat="server" Text='<%# Bind("DESCRINIFIN") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        
                                        <asp:TemplateField HeaderText="Transcurrido" SortExpression="ZPERIODO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLnomina" runat="server" Text='<%# Eval("ZPERIODO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLnomina" runat="server" Text='<%# Bind("ZPERIODO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>       
                                        
                                        <asp:TemplateField HeaderText="Horas" SortExpression="ZHORAS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLnomina" runat="server" Text='<%# Eval("ZHORAS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLnomina" runat="server" Text='<%# Bind("ZHORAS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>      
                                        <asp:TemplateField HeaderText="Estado" SortExpression="ZESTADO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLnomina" runat="server" Text='<%# Eval("ZESTADO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLnomina" runat="server" Text='<%# Bind("ZESTADO") %>'></asp:Label>
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
             <%--final gvpanelTareas--%>



























             <div class="tab-pane fade active" visible="true" style="height:700px;" runat="server" id="PnInformes">
              <%--PnInformes--%>
               <div class="panel panel-default" style="height:700px;">                     
                <div class="panel-heading" runat="server" id="Div27" >
                    <div class="row">
                            <div class="col-lg-12"> 
                            <h4 class="panel-title">
                                <a data-toggle="collapse" style="font-weight: bold;" onclick="submitit5()"  href="#collapse5"> Informes </a>                                            
                            </h4>
                        </div>                       
                    </div>

                </div>
                <%--<div id="Div28" runat="server"  class="panel-collapse collapse in">--%>  
                    <%--PnInformes--%>
                    <div class="panel-body" style="height:700px;">
                        <%--<div class="row" id="Div39" runat="server" visible="true">--%>
                        Recuerde que se aplicarán los filtros que tenga configurados en la salida de estos Reportes
                           <rsweb:ReportViewer ID="ReportViewer0" Height="600" runat="server" Width="100%" Font-Names="Verdana" ShowPrintButton="true" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            </rsweb:ReportViewer>
                        <%--</div>--%>          
                    </div>
                    </div>
                <%--</div>--%>
          </div>
            <%--Final Informes--%>










          <div class="tab-pane fade active" visible="true" runat="server" id="PnResumenNomina">
              <%--gvResumenNomina--%>
                <div class="panel panel-default">                     
                <div class="panel-heading" runat="server" id="Div26" >
                    <div class="row">
                            <div class="col-lg-2"> 
                            <h4 class="panel-title">
                                <a data-toggle="collapse" style="font-weight: bold;" onclick="submitit5()"  href="#collapse5"> Resumen Nómina </a>                                            
                            </h4>
                        </div>


                       <%-- <div class="col-sm-1" style="text-align:right;">
                            <asp:linkbutton id="LinkConsResumenNomina" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Buscar por:"></asp:linkbutton>      
                        </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="DrrowResumenNomina"  OnSelectedIndexChanged="DrFiltro1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="BtnConsResumenNomina" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i title="Desactivado" style="color:darkred;" id="IFilResumenNomina" runat="server" class="fa fa-hand-o-up fa-2x"></i> </button>
                            </div>
                            <div class="col-sm-2" >
                                <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="TxtFilResumenNomina"  Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="Button17" type="button" runat="server" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtfilResumenNomina_Click"><i title="Busca por la Columna y el contenido de la casilla de texto." class="fa fa-search fa-2x"></i></button> 
                            </div>--%>
                            <%--<div class="col-lg-1">
                                <input type="image"  class="pull-right text-muted " src="images/ordencarga25x25.png" onserverclick="ImageOrden_Click"  runat="server" id="ImgOrden" style="border: 0px; " />    
                            </div>--%>
                            <div class="col-lg-9"> 
                                <%--<asp:DropDownList runat="server"  CssClass="form-control" style="position:relative;"  Width="100%"  ID="DrSelCab" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrSelCab_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                <asp:Button ID="btn1" runat="server" OnClick="check1_Click" Height="0" Width="0" CssClass="hidden" />--%>
                            </div>

                            <%--<div class="col-lg-1"> 
                                <button id="Button34" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Cabecera" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>--%>
                        <div class="col-lg-1"> 
                               <%-- <input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/print25X25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image7" style="border: 0px; " />
                                <button id="Button34" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                    <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                                </button>--%>
                                <button id="BtnResumenNomina" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la lísta de Resumen Nómina" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>






                        <%--<div class="col-lg-1"> 
                            <input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/etiquetaQR25x25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image4" style="border: 0px; " />
                        </div>
                        <div class="col-lg-1" id="Div27" runat="server" visible="true"> 
                            <button id="Button16" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                            </button>
                        </div>
                        <div class="col-lg-1" id="Div28" runat="server" visible="false"> 
                            <button id="Button17" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReportOff_Click">
                                <i title="Cierra la vista previa de reportes" class="fa fa-power-off fa-2x"></i>
                            </button>
                        </div>--%>


                      <%--  <div class="col-lg-1">
                            <button id="BtnResumenNomina" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la listas de carga camión" class="fa fa-file-excel-o fa-2x"></i></button>
                        </div>--%>
                        
                    </div>
                    <asp:Button ID="Button19" runat="server" OnClick="check5_Click" Height="0" Width="0" CssClass="hidden" />
                    <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label14"  Text="" > </asp:Label>
                </div>
                <div id="Div29" runat="server"  class="panel-collapse collapse in">  
                    <%--gvResumenNomina--%>
                    <div class="panel-body">
                        <div class="row" id="Div30" runat="server" visible="true">
                            <div class="col-sm-1" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label16"  Text="Filas:" > </asp:Label>
                            </div>
                            <div class="col-sm-1" >                              
                                <asp:DropDownList ID="DrResumenNomina" CssClass="form-control" Width="100%" style=" position:absolute;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvResumenNomina_PageSize_Changed">  
                                <asp:ListItem Text="5" Value="5" />    
                                </asp:DropDownList>
                             </div>
                            <div class="col-sm-2" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowResumenNomina"  Text="" > </asp:Label>
                            </div>

                            <div class="col-sm-8" >
                            </div>
  
<%--                           <div class="col-sm-3" >
                                <asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label17"  Text="Ordenar por columna (XLS):" ></asp:Label> 
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione primer campo para ordenar datos"  ID="DRResumenNomina1"  OnSelectedIndexChanged="DRResumenNomina1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione segundo campo para ordenar datos"  ID="DRResumenNomina2"  OnSelectedIndexChanged="DRResumenNomina1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>--%>
                            
                        </div>
                        <div class="row" id="Div31" runat="server" visible="true">
                            <div id="Div32" runat="server" style="overflow:auto;" class="panel-body">                                 
                                <asp:GridView ID="gvResumenNomina"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                    AllowSorting="true" OnSorting="gvResumenNomina_OnSorting"
                                    CellPadding="4"  GridLines="None"  OnRowDataBound="gvResumenNomina_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="15" OnRowCommand="gvResumenNomina_RowCommand" DataKeyNames="COD_EMPLEADO"
                                    oonselectedindexchanged="gvResumenNomina_SelectedIndexChanged"  OnRowEditing="gvResumenNomina_RowEditing" OnRowCancelingEdit="gvResumenNomina_RowCancelingEdit" OnRowUpdating="gvResumenNomina_RowUpdating" 
                                    OnRowDeleting="gvResumenNomina_RowDeleting" onpageindexchanging="gvResumenNomina_PageIndexChanging" EnablePersistedSelection="True"  >
                                <RowStyle />                     
                                    <Columns>

                                     <%--<asp:CommandField ButtonType="Image" 
                                                    EditImageUrl="~/Images/editar20x20.png" 
                                                    ShowEditButton="True" 
                                                    CancelImageUrl="~/Images/cancelar20x20.png" 
                                                    CancelText="" 
                                                    DeleteText="" 
                                                    UpdateImageUrl="~/Images/guardar18x18.png"          
                                                    UpdateText="" />     --%>     

                                        <%--<asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtbajaOrden" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/sendup20x20.png"
                                                   CommandName="BajaOrden" ToolTip="Selecciona Registro por Código" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                         </asp:TemplateField>

                                          <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtUbicacion" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/print25X25.png"
                                                   CommandName="Ubicacion" ToolTip="Imprime registro" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                         </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Código" SortExpression="COD_EMPLEADO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLcodigo" runat="server" Text='<%# Eval("COD_EMPLEADO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLCodigo" runat="server" Text='<%# Bind("COD_EMPLEADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre" SortExpression="NOMBRE">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNombre"  runat="server" Text='<%# Eval("NOMBRE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNombre"  runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apellidos" SortExpression="APELLIDOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLapellidosa" runat="server" Text='<%# Eval("APELLIDOS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLapellidos" runat="server" Text='<%# Bind("APELLIDOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Categoría" SortExpression="CATEGORIA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLCategoria" runat="server" Text='<%# Eval("CATEGORIA") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLCategoria" runat="server" Text='<%# Bind("CATEGORIA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Plantas" SortExpression="PLANTAS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLDPlantas" runat="server" Text='<%# Eval("PLANTAS") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLDPlantas" runat="server" Text='<%# Bind("PLANTAS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Importe" SortExpression="IMPORTE">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLDImporte" runat="server" Text='<%# Eval("IMPORTE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLDImporte" runat="server" Text='<%# Bind("IMPORTE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                       <%-- <asp:TemplateField HeaderText="Dias Mes" SortExpression="DIASMES">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLdiames" runat="server" Text='<%# Eval("DIASMES") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLdiames" runat="server" Text='<%# Bind("DIASMES") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Vivienda" SortExpression="VIVIENDA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLVivienda" runat="server" Text='<%# Eval("VIVIENDA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLVivienda" runat="server" Text='<%# Bind("VIVIENDA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Alquiler" SortExpression="ALQVIVIENDA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLAlquiler" runat="server" Text='<%# Eval("ALQVIVIENDA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLAlquiler" runat="server" Text='<%# Bind("ALQVIVIENDA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Coste vivienda" SortExpression="COSTEVIVIENDA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLCosteAlquiler" runat="server" Text='<%# Eval("COSTEVIVIENDA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLCosteAlquiler" runat="server" Text='<%# Bind("COSTEVIVIENDA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Total" SortExpression="TOTAL">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLCostetotal" runat="server" Text='<%# Eval("TOTAL") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLCostetotal" runat="server" Text='<%# Bind("TOTAL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="Nomina" SortExpression="NOMINA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLnomina" runat="server" Text='<%# Eval("NOMINA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLnomina" runat="server" Text='<%# Bind("NOMINA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  --%>

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
            <%--Final gvResumenNomina--%>














             <div class="tab-pane fade active" visible="true" runat="server" id="PnlTrabajos">
              <%--gvTrabajos--%>
                <div class="panel panel-default">                     
                <div class="panel-heading" runat="server" id="Div37" >
                    <div class="row">
                            <div class="col-lg-2"> 
                            <h4 class="panel-title">
                                <a data-toggle="collapse" style="font-weight: bold;" onclick="submitit5()"  href="#collapse5"> Listas de Trabajos </a>                                            
                            </h4>
                        </div>


                       <%-- <div class="col-sm-1" style="text-align:right;">
                                <asp:linkbutton id="Linkfiltrabajos" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Buscar por:"></asp:linkbutton>      
                        </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="DrFindTrabajo"  OnSelectedIndexChanged="DrFiltro1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="Btnfiltrabajos" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i title="Desactivado" id="ITrabajos" style="color:darkred;" runat="server" class="fa fa-hand-o-up fa-2x"></i> </button>
                            </div>
                            <div class="col-sm-2" >
                                <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione una Columna e introduzca un dato"  ID="TxtFilTrabajos"  Font-Bold="True" />
                            </div>
                            <div class="col-sm-1" >
                                <button id="Button31" type="button" runat="server" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtfilTrabajos_Click"><i title="Busca por la Columna y el contenido de la casilla de texto." class="fa fa-search fa-2x"></i></button> 
                            </div>--%>
                            <%--<div class="col-lg-1">
                                <input type="image"  class="pull-right text-muted " src="images/ordencarga25x25.png" onserverclick="ImageOrden_Click"  runat="server" id="ImgOrden" style="border: 0px; " />    
                            </div>--%>
                            <div class="col-lg-9"> 
                                <%--<asp:DropDownList runat="server"  CssClass="form-control" style="position:relative;"  Width="100%"  ID="DrSelCab" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrSelCab_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                <asp:Button ID="btn1" runat="server" OnClick="check1_Click" Height="0" Width="0" CssClass="hidden" />--%>
                            </div>

                            <%--<div class="col-lg-1"> 
                                <button id="Button34" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la lista de Cabecera" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>--%>
                        <div class="col-lg-1"> 
                                <%--<input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/print25X25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image9" style="border: 0px; " />
                                <button id="Button39" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                    <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                                </button>--%>
                                <button id="BtnTrabajoXLS" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Imprime la Lísta de Trabajos" class="fa fa-file-excel-o fa-2x"></i></button>
                            </div>






                        <%--<div class="col-lg-1"> 
                            <input type="image"  alt="placeholder"  class="pull-right text-muted " src="images/etiquetaQR25x25.png" onserverclick="ImageBtn_Click"  runat="server" id="Image4" style="border: 0px; " />
                        </div>
                        <div class="col-lg-1" id="Div27" runat="server" visible="true"> 
                            <button id="Button16" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReport_Click">
                                <i title="Imprime el reporte de la lista interna de carga camión" class="fa fa-retweet fa-2x"></i>
                            </button>
                        </div>
                        <div class="col-lg-1" id="Div28" runat="server" visible="false"> 
                            <button id="Button17" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="PrintReportOff_Click">
                                <i title="Cierra la vista previa de reportes" class="fa fa-power-off fa-2x"></i>
                            </button>
                        </div>--%>


                      <%--  <div class="col-lg-1">
                            <button id="BtnResumenNomina" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="btnPrint_Click"><i title="Imprime la vista previa presentada en la listas de carga camión" class="fa fa-file-excel-o fa-2x"></i></button>
                        </div>--%>
                        
                    </div>
                    <asp:Button ID="Button43" runat="server" OnClick="check5_Click" Height="0" Width="0" CssClass="hidden" />
                    <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label47"  Text="" > </asp:Label>
                </div>
                <div id="Div41" runat="server"  class="panel-collapse collapse in">  
                    <%--gvTrabajo--%>
                    <div class="panel-body">
                        <div class="row" id="Div42" runat="server" visible="true">
                            <div class="col-sm-1" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label48"  Text="Filas:" > </asp:Label>
                            </div>
                            <div class="col-sm-1" >                              
                                <asp:DropDownList ID="DrTrabajo" CssClass="form-control" Width="100%" style=" position:absolute;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvTrabajos_PageSize_Changed">  
                                <asp:ListItem Text="5" Value="5" />    
                                </asp:DropDownList>
                             </div>
                            <div class="col-sm-2" >
                                  <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="LbCountTrabajo"  Text="" > </asp:Label>
                            </div>
                            <div class="col-sm-8" >
                            </div>

                           <%-- <div class="col-sm-3" >
                                <asp:Label  type="text"  Width="80%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label49"  Text="Ordenar por columna (XLS):" ></asp:Label> 
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione primer campo para ordenar datos"  ID="DrTrabajo0"  OnSelectedIndexChanged="DRTrabajo1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>
                            <div class="col-sm-2" >
                                <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Seleccione segundo campo para ordenar datos"  ID="DrTrabajo1"  OnSelectedIndexChanged="DRTrabajo1_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" />
                            </div>--%>
                            
                        </div>
                        <div class="row" id="Div43" runat="server" visible="true">
                            <div id="Div44" runat="server" style="overflow:auto;" class="panel-body">                                 
                                <asp:GridView ID="gvTrabajos"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                    AllowSorting="true" OnSorting="gvTrabajos_OnSorting"
                                    CellPadding="4"  GridLines="None"  OnRowDataBound="gvTrabajos_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="15" OnRowCommand="gvTrabajos_RowCommand" DataKeyNames="COD_EMPLEADO"
                                    oonselectedindexchanged="gvTrabajos_SelectedIndexChanged"  OnRowEditing="gvTrabajos_RowEditing" OnRowCancelingEdit="gvTrabajos_RowCancelingEdit" OnRowUpdating="gvTrabajos_RowUpdating" 
                                    OnRowDeleting="gvTrabajos_RowDeleting" onpageindexchanging="gvTrabajos_PageIndexChanging" EnablePersistedSelection="True"  >
                                <RowStyle />                     
                                    <Columns>

                                     <%--<asp:CommandField ButtonType="Image" 
                                                    EditImageUrl="~/Images/editar20x20.png" 
                                                    ShowEditButton="True" 
                                                    CancelImageUrl="~/Images/cancelar20x20.png" 
                                                    CancelText="" 
                                                    DeleteText="" 
                                                    UpdateImageUrl="~/Images/guardar18x18.png"          
                                                    UpdateText="" />     --%>     

                                        <%--<asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtbajaOrden" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/sendup20x20.png"
                                                   CommandName="BajaOrden" ToolTip="Selecciona Registro por Código" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                         </asp:TemplateField>

                                          <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                   <asp:ImageButton ID="ibtUbicacion" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/print25X25.png"
                                                   CommandName="Ubicacion" ToolTip="Imprime registro" Width="20px" Height="20px"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Height="8px"></ItemStyle>                                
                                         </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Código" SortExpression="COD_EMPLEADO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLcodigo" runat="server" Text='<%# Eval("COD_EMPLEADO") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLCodigo" runat="server" Text='<%# Bind("COD_EMPLEADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre" SortExpression="NOMBRE">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLNombre"  runat="server" Text='<%# Eval("NOMBRE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLNombre"  runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apellidos" SortExpression="APELLIDOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLapellidosa" runat="server" Text='<%# Eval("APELLIDOS") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLapellidos" runat="server" Text='<%# Bind("APELLIDOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>







                                        <asp:TemplateField HeaderText="Fecha" SortExpression="FECHA_EMPLEADOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLCategoria" runat="server" Text='<%# Eval("FECHA_EMPLEADOS") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLCategoria" runat="server" Text='<%# Bind("FECHA_EMPLEADOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="B.Fecha Jornada" SortExpression="FECHA_EMPLEADOS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabBCategoria" runat="server" Text='<%# Eval("FECHA_EMPLEADOS") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLBCategoria" runat="server" Text='<%# Bind("FECHA_EMPLEADOS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="B.Hora Empleado" SortExpression="HORA_EMPLEADO">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLDPlantas" runat="server" Text='<%# Eval("HORA_EMPLEADO") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLDPlantas" runat="server" Text='<%# Bind("HORA_EMPLEADO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Horas" SortExpression="ZHORAS">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLDaPlantas" runat="server" Text='<%# Eval("ZHORAS") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLDPalantas" runat="server" Text='<%# Bind("ZHORAS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                        <%--<asp:TemplateField HeaderText="Tablet" SortExpression="TABLET">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLDPslantas" runat="server" Text='<%# Eval("TABLET") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLDPslantas" runat="server" Text='<%# Bind("TABLET") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                       <%-- <asp:TemplateField HeaderText="Cod. Finca" SortExpression="CODFINCA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLDPstran" runat="server" Text='<%# Eval("CODFINCA") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLDPstran" runat="server" Text='<%# Bind("CODFINCA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Desc. Finca" SortExpression="DESCRFINCA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLDPstran" runat="server" Text='<%# Eval("DESCRFINCA") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLDPstran" runat="server" Text='<%# Bind("DESCRFINCA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cod. Zona" SortExpression="ZONA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLDPstran" runat="server" Text='<%# Eval("ZONA") %>' class="gridListDinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLDPstran" runat="server" Text='<%# Bind("ZONA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Desc. Zona" SortExpression="DESCRZONAZ">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLLtablet" runat="server" Text='<%# Eval("DESCRZONAZ") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLtablet" runat="server" Text='<%# Bind("DESCRZONAZ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tarea" SortExpression="TAREA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="TabLLtarea" runat="server" Text='<%# Eval("TAREA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLLtarea" runat="server" Text='<%# Bind("TAREA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descr. Tarea" SortExpression="DESCRTAREA">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="Tabdestarea" runat="server" Text='<%# Eval("DESCRTAREA") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLdestarea" runat="server" Text='<%# Bind("DESCRTAREA") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Importe" SortExpression="IMPORTE">
                                            <EditItemTemplate>
                                                    <asp:TextBox ID="Tabdestarea" runat="server" Text='<%# Eval("IMPORTE") %>' class="gridListAinput"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="LabLdestarea" runat="server" Text='<%# Bind("IMPORTE") %>'></asp:Label>
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
            <%--Final gvTrabajos--%>







            <%--Fila Leyenda--%>
              <div class="row">
                    <div class="col-sm-12" style="font-size:18px;background-color : #e6f2e1" >
                        Leyenda:                                                      
                    </div>
                  <br />
                  <br />
                    <div class="col-sm-1" >
                        <i id="I56" style="font-size:14px;color:darkred;" class="fa fa-hand-o-up fa-2x"> - Desactivado</i>                      
                    </div>
                    <div class="col-sm-2" >
                        <i id="I4" style="font-size:14px;" class="fa fa-circle fa-2x"> - Contiene estos datos</i>                      
                    </div>
                    
                    <div class="col-sm-2" >
                        <i id="I6" style="font-size:14px;" class="fa fa-circle-o fa-2x"> - No contiene estos datos</i>
                    </div>
                    <div class="col-sm-2" >
                        <i id="I5" style="font-size:14px;" class="fa fa-adjust fa-2x"> - Incluye en su contenido este dato</i>
                    </div>
                    <div class="col-sm-2" >
                        <i id="I7" style="font-size:14px;" class="fa fa-dot-circle-o fa-2x"> - No incluye en su contenido este datos</i>  
                    </div>
                  <div class="col-sm-1" >
                        <i id="I8" style="font-size:14px;" class="fa fa-chevron-left fa-2x"> - Menor que</i>  
                    </div>
                  <div class="col-sm-1" >
                        <i id="I19" style="font-size:14px;" class="fa fa-chevron-right fa-2x"> - Mayor que</i>  
                    </div>
                  <div class="col-sm-1" >
                        <i id="I20" style="font-size:14px;" class="fa fa-arrows-alt fa-2x"> -Distinto de</i>  
                    </div>
                  <%--<i class="fa fa-spinner fa-pulse fa-3x fa-fw"></i><span class="sr-only">Loading...</span>--%>
              </div><!-- /.row data-parent="#accordion"-->
      </div>
        <%--Pagevistaform--%>




                <%--<div class="row" id="PNreportLista" runat="server" visible="false">
                    <div class="col-sm-12" >
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Font-Names="Verdana" ShowPrintButton="true" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                        </rsweb:ReportViewer>
                    </div>
                </div>--%>
   </form>
</div>
 
<%--</div>--%>
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

              function doPrintGRControl() {
                  var prtContent = document.getElementById('<%= gvProduccion.ClientID %>');
                  //prtContent.border = 0; //set no border here
                  var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
                  WinPrint.document.write(prtContent.outerHTML);
                  WinPrint.document.close();
                  WinPrint.focus();
                  WinPrint.print();
                  WinPrint.close();
              }

              function doPrintGRLista() {
                  var prtContent = document.getElementById('<%= gvJornada.ClientID %>');
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
 <%--     <script src="js/plugins/morris/raphael.min.js"></script>
  <script src="js/plugins/morris/morris.min.js"></script>
    <script src="js/plugins/morris/morris-data.js"></script>--%>

    <!-- Custom Theme JavaScript Menu derecha correcto -->
    <script src="js/sb-admin-2.js"></script>


</body>
</html>




