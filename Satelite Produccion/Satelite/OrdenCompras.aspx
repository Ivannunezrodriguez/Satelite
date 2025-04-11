<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false" CodeBehind="OrdenCompras.aspx.cs" Inherits="Satelite.OrdenCompras" MaintainScrollPositionOnPostback="true" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

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



                <div runat="server" id="DvPreparado" visible="false"  style="height: 24%; width: 30%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
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
                        <asp:Button runat="server" ID="Button2" Visible="true" tooltip="Sólo Elimina la línea selecionada" CssClass="btn btn-success btn-block" Width="100%"  Text="Sólo Eliminar" OnClick="checkSiEl_Click"/>                                                    
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
                            <a class="active" href="#"><i class="fa fa-sitemap fa-fw"></i> Expediciones<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level in">
                                <%--<li>
                                    <a href="OrdenCarga.aspx"><i class="fa fa-long-arrow-right"></i> Órdenes de Carga</a>
                                </li>--%>  
                                <li>
                                    <a href="OrdenCargaLin.aspx" class="active "><i class="fa fa-long-arrow-right"></i> Órdenes de Carga</a>
                                </li>   
                            </ul>
                            <!-- /.nav-second-level -->
                        </li>
                       <li id="Nominas" runat="server" visible="false">
                            <a href="#"><i class="fa fa-sitemap fa-fw"></i> RecoDat<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level">
                                <li>
                                    <asp:UpdatePanel ID="Update" runat="server" UpdateMode="Conditional">      
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


        
        <div id="pagevistaform" runat="server"><!-- /#page-wrapper -->

            <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="Update">
                <ProgressTemplate>
                    <div class="centrado" style="z-index:1100;">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>



           <div runat="server" id="alerta" visible="false" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <i id="IAlert" runat="server" class="fa fa-exclamation-circle"></i>
                <asp:Label runat="server" class="alert alert-info alert-dismissable" ID="TextAlerta" BorderStyle="None" border="0" Width="100%" Text=""  />
           </div>
           <%--<asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Lberror"  Text="" > </asp:Label>--%>

            <div class="row">
                  <div class="col-lg-12">
                        <div class="col-lg-8" >
                                <h3 id="H3Titulo" runat="server" visible="true"> Selección de pedidos <i class="fa fa-long-arrow-right"></i> <small> “Orden de Compras ”  </small>
                                    <asp:Label  type="text"  style=" font-weight: bold; font-size:14px;"  runat="server" ID="Lbhost1"  Text="" > </asp:Label>
                                </h3>
                                <h3 id="H3Desarrollo" runat="server" style="color:red;" visible="false"> DESARROLLO --> Selección de pedidos <i class="fa fa-long-arrow-right"></i> <small> “Orden de Compras ”  </small>
                                    <asp:Label  type="text"  style=" font-weight: bold; font-size:14px; color:black;"  runat="server" ID="Lbhost2"  Text="" > </asp:Label>
                                </h3>
                        </div>

                        <div id="VistaOrden" runat="server" visible ="true" class="col-lg-4">
                            <div runat="server" class="col-lg-1" style=" top:16px;">
                                    <%--<asp:Label  type="text"  Width="60%" style=" font-weight: bold; text-align:right; position:absolute;"  runat="server" ID="Label7"  Text="Orden: " > </asp:Label><i class="fa fa-long-arrow-right"></i>--%>
                                    <input type="image"  class="pull-right text-muted " src="images/orden25x25.png" onserverclick="ImgOrdenMin_Click"  runat="server" id="ImgOrdenMin" style="border: 0px; " />    
                            </div>
                            <div  runat="server" class="col-lg-11" style=" top:16px;"> 
                                    <asp:DropDownList runat="server"  CssClass="form-control" style="position:relative; background-color:#ffffff; font-size:20px;"  Width="100%"  ID="DrOrdenMin" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrOrdenMin_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                    <asp:Button ID="Button1" runat="server" OnClick="check1_Click" Height="0" Width="0" BackColor="" CssClass="hidden" />
                            </div>
                        </div>
                        <div id="VistaOrdenNO" runat="server" visible ="true" class="col-lg-5">
                        </div>
                        <%--<div id="dvPrinters" runat="server" style="text-align:right;" visible="true" class="col-lg-2" >
                        <br />
                            <button id="BtPrinters" type="button" runat="server" style="width:100%; border-style:none; background-color:transparent; text-align:right;" class="text-muted"  onserverClick="btPrinter_Click"><i title="Selección otra Impresora"  class="fa fa-qrcode fa-2x"></i></button>                                      
                        </div>
                        <div id="dvDrlist" runat="server" style="text-align:right;" Visible="false" class="col-lg-2" >
                            <br />
                            <asp:DropDownList runat="server"  CssClass="form-control"  Width="100%"  ID="DrPrinters" tooltip="Contiene las distintas impresoras configuradas" OnSelectedIndexChanged="DrPrinters_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                            comentado -->  <i title="Selección otra Impresora" class="fa fa-undo"></i>
                        </div>
                        --%>
                           
                    </div>
              </div><!-- /.row data-parent="#accordion"-->

            <div class="row">
              <div class="col-lg-12">
                 <div class="bs-example">
                    <ul class="nav nav-tabs" style="margin-bottom: 15px;">
                        <li id="Menu2" class=""  runat="server" ><asp:LinkButton ID="aMenu2" runat="server" OnClick="HtmlAnchor_Click" >LISTA ÓRDENES COMPRAS</asp:LinkButton></li>
                        <li id="Menu4" class=""  runat="server" ><asp:LinkButton ID="aMenu4" runat="server" OnClick="HtmlAnchor_Click" >LOTES AUTOMÁTICOS</asp:LinkButton></li>
                        <li id="Menu3" class=""  runat="server" ><asp:LinkButton ID="aMenu3" runat="server" OnClick="HtmlAnchor_Click" >VERTICAL</asp:LinkButton></li>
                        <li id="Menu6" class=""  runat="server" ><asp:LinkButton ID="aMenu6" runat="server" OnClick="HtmlAnchor_Click" >HORIZONTAL</asp:LinkButton></li>
                        <li id="Menu5" class=""  runat="server" ><asp:LinkButton ID="aMenu5" runat="server" OnClick="HtmlAnchor_Click" >INFORMES / IMPRESIÓN</asp:LinkButton></li>
                   </ul>
                </div>
             </div>
          </div>


             <div class="panel-body" visible="false" style="width:100%;height:200px;"  runat="server" id="accordion8">        
                <div class="row" style="width:100%;">  
                    <%--<Ordenes de compra dos>--%>
                    <div class="col-lg-12" style="width:100%;">               
                                <div class="panel panel-default">
                                    <div class="panel-heading" runat="server" id="Div3" >
                                        <div class="row">
                                                <div class="col-lg-8"> 
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" style=" font-weight: bold;" onclick="submiti4()"  href="#collapse4"> SELECCION PEDIDOS COMPRAS: Lista pedidos pendientes</a>                                             
                                                    </h4>
                                                </div>
                                                <div class="col-lg-1">
                                                    <%--<asp:Label  type="text"  Width="60%" style=" font-weight: bold; text-align:right; position:absolute;"  runat="server" ID="Label18"  Text="Filtro: " > </asp:Label><i class="fa fa-long-arrow-right"></i>--%>
                                                    <input type="image"  class="pull-right text-muted " src="images/filtro25x25.png" onserverclick="ImageFiltro_Click"  runat="server" id="ImageFiltro2" style="border: 0px; " />    
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:DropDownList runat="server"  CssClass="form-control"  Width="100%"  ID="DropDownList1" tooltip="Contiene el filtro para las órdenes de pedidos" OnSelectedIndexChanged="DrSelectFiltro_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" /> 
                                                </div>
                                                <div class="col-lg-1"> <%--onserverClick="PrintGridView"--%>
                                                    <button id="Button5" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onclick="doPrintGRControl();"><i title="Imprime la vista previa presentada en la lista de órdenes de carga" class="fa fa-print fa-2x"></i></button>
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
                                                    </a>
                                                </h4>
                                               <asp:TextBox runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="TextBox1"  Font-Bold="True" />

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
                                                    <div class="col-lg-3"> 
                                                    <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label9"  Text="Empresa:" > </asp:Label>
                                                    <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="DropDownList2"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                    <div class="col-lg-2"> 
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label10"  Text="Fechas: Desde" > </asp:Label>
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Fecha desde para filtrar datos"  ID="DropDownList3"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label11"  Text="Hasta" > </asp:Label>
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Fecha hasta para filtrar datos"  ID="DropDownList4"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                    <div class="col-lg-2"> 
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label12"  Text="Ruta Envio:" > </asp:Label>
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Ruta Envio para filtrar datos"  ID="DropDownList5"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                    <div class="col-lg-2"> 
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label13"  Text="Clientes:" > </asp:Label>
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Clientes para filtrar datos"  ID="DropDownList6"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                    <div class="col-lg-1">
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label14"  Text="" > </asp:Label>
                                                        <button id="Button10" type="button" runat="server" style="width:50%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtGralConsulta_Click"><i title="Consulta General con filtro o sin filtro según selección sobre la base de datos de GoldenSoft. Elimina la consulta actual visualizada en pantalla." class="fa fa-bars fa-2x"></i></button>
                                                        <button id="Button11" type="button" runat="server"  style="width:50%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="Btfiltra_Click"><i title="Consulta con filtro o sin filtro según selección, sobre los datos recuperados de la base de datos en local" class="fa fa-search fa-2x"></i></button>
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
                                                             <asp:TemplateField HeaderText="Serie Pedido" visible="false" SortExpression="SERIE_PED">
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
                        <%---Fin <Ordenes de compra dos>--%>
                    </div>
                 </div>
                <div class="row" style="width:100%; height:100%;">  
                    <div class="col-lg-12" style="width:100%; height:1000px; overflow:auto;">  
                            <iframe style="width:100%; height:1000px; border:none;" scrolling="no" sandbox="allow-same-origin allow-forms allow-scripts" src="LoteAuto.aspx"></iframe>
                    </div> 
                </div>
            </div>
            <!-- Fin accordion8"-->

             <div class="panel-body" visible="false" style="width:100%;height:200px;"  runat="server" id="Horizontal">        
                <div class="row" style="width:100%;overflow:auto;">  
                    <%--<Ordenes de compra dos>--%>
                    <div class="col-lg-6" style="overflow:auto;">               
                                <div class="panel panel-default">
                                    <div class="panel-heading" runat="server" id="Div2" >
                                        <div class="row">
                                                <div class="col-lg-8"> 
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" style=" font-weight: bold;" onclick="submiti4()"  href="#collapse4"> SELECCION PEDIDOS COMPRAS: Lista pedidos pendientes</a>                                             
                                                    </h4>
                                                </div>
                                                <div class="col-lg-1">
                                                    <%--<asp:Label  type="text"  Width="60%" style=" font-weight: bold; text-align:right; position:absolute;"  runat="server" ID="Label18"  Text="Filtro: " > </asp:Label><i class="fa fa-long-arrow-right"></i>--%>
                                                    <input type="image"  class="pull-right text-muted " src="images/filtro25x25.png" onserverclick="ImageFiltro_Click"  runat="server" id="Image1" style="border: 0px; " />    
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:DropDownList runat="server"  CssClass="form-control"  Width="100%"  ID="DropDownList7" tooltip="Contiene el filtro para las órdenes de pedidos" OnSelectedIndexChanged="DrSelectFiltro_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" /> 
                                                </div>
                                                <div class="col-lg-1"> <%--onserverClick="PrintGridView"--%>
                                                    <button id="Button12" type="button" runat="server" visible="true" style="width:100%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onclick="doPrintGRControl();"><i title="Imprime la vista previa presentada en la lista de órdenes de carga" class="fa fa-print fa-2x"></i></button>
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
                                                    </a>
                                                </h4>
                                               <asp:TextBox runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="TextBox2"  Font-Bold="True" />

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
                                                    <!-- consultas predefinidas -->
                                                    <div class="col-lg-3"> 
                                                    <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label22"  Text="Empresa:" > </asp:Label>
                                                    <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="DropDownList8"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                    <div class="col-lg-2"> 
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label23"  Text="Fechas: Desde" > </asp:Label>
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Fecha desde para filtrar datos"  ID="DropDownList9"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label24"  Text="Hasta" > </asp:Label>
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Fecha hasta para filtrar datos"  ID="DropDownList10"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                    <div class="col-lg-2"> 
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label25"  Text="Ruta Envio:" > </asp:Label>
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Ruta Envio para filtrar datos"  ID="DropDownList11"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                    <div class="col-lg-2"> 
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label26"  Text="Clientes:" > </asp:Label>
                                                        <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Clientes para filtrar datos"  ID="DropDownList12"  OnSelectedIndexChanged="DrConsulta_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                                    </div>
                                                    <div class="col-lg-1">
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label27"  Text="" > </asp:Label>
                                                        <button id="Button15" type="button" runat="server" style="width:50%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtGralConsulta_Click"><i title="Consulta General con filtro o sin filtro según selección sobre la base de datos de GoldenSoft. Elimina la consulta actual visualizada en pantalla." class="fa fa-bars fa-2x"></i></button>
                                                        <button id="Button16" type="button" runat="server"  style="width:50%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="Btfiltra_Click"><i title="Consulta con filtro o sin filtro según selección, sobre los datos recuperados de la base de datos en local" class="fa fa-search fa-2x"></i></button>
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
                                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="Label29"  Text="" > </asp:Label>
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
                                                             <asp:TemplateField HeaderText="Serie Pedido" visible="false" SortExpression="SERIE_PED">
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
                        <%---Fin <Ordenes de compra dos>--%>
                    </div>
                    
                    <div class="col-lg-6" style="overflow:auto;">  
                            <iframe style="width:100%; height:1100px; border:none;" scrolling="auto" sandbox="allow-same-origin allow-forms allow-scripts" src="LoteAuto.aspx"></iframe>
                    </div> 
                </div>
            </div>
            <!-- Fin Horizontal"-->

            <!-- Inicio DivLotes"-->
              <div class="panel-body" visible="false" style="width:100%;height:200px;"  runat="server" id="DivLotes">        
                <div class="row" style="width:100%;overflow:auto;">  
                    <%--Lotes--%>                
                    <div class="col-lg-12" style="overflow:auto;">  
                            <iframe style="width:100%; height:1100px; border:none;" scrolling="auto" sandbox="allow-same-origin allow-forms allow-scripts" src="LoteAuto.aspx"></iframe>
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
                                   <asp:TextBox runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Empresa para filtrar datos"  ID="TxtNumero"  Font-Bold="True" />

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
                                                             <asp:TemplateField HeaderText="Serie Pedido" visible="false" SortExpression="SERIE_PED">
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
    </form>
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

             <%-- function doPrintGRCabecera() {
                  var prtContent = document.getElementById('<%= gvCabecera.ClientID %>');
                  //prtContent.border = 0; //set no border here
                  var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
                  WinPrint.document.write(prtContent.outerHTML);
                  WinPrint.document.close();
                  WinPrint.focus();
                  WinPrint.print();
                  WinPrint.close();
              }--%>

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

<%--              function doPrintGRLista() {
                  var prtContent = document.getElementById('<%= gvLista.ClientID %>');
                  //prtContent.border = 0; //set no border here
                  var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
                  WinPrint.document.write(prtContent.outerHTML);
                  WinPrint.document.close();
                  WinPrint.focus();
                  WinPrint.print();
                  WinPrint.close();
              }--%>

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


</body>
</html>
