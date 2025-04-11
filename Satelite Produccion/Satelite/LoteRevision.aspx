<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoteRevision.aspx.cs" Inherits="Satelite.LoteRevision" MaintainScrollPositionOnPostback="true" %>

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




              /* The switch - the box around the slider */
            .switch {
              position: relative;
              display: inline-block;
              width: 45px;
              height: 18px;
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
              height: 12px;
              width: 12px;
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
              -webkit-transform: translateX(26px);
              -ms-transform: translateX(26px);
              transform: translateX(26px);
            }

            /* Rounded sliders */
            .slider.round {
              border-radius: 34px;
            }

            .slider.round:before {
              border-radius: 50%;
            } 

    </style>


    <script>
        $(function () {
            $("[data-toggle='tooltip']").tooltip();
        });
    </script>

 
<%--<script type = "text/javascript">
    var GridId = "<%=gvEntrada.ClientID %>";
    var ScrollHeight = 300;
    window.onload = function () {
        var grid = document.getElementById(GridId);
        var gridWidth = grid.offsetWidth;
        var gridHeight = grid.offsetHeight;
        var headerCellWidths = new Array();
        for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
            headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
        }
        grid.parentNode.appendChild(document.createElement("div"));
        var parentDiv = grid.parentNode;

        var table = document.createElement("table");
        for (i = 0; i < grid.attributes.length; i++) {
            if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
            }
        }
        table.style.cssText = grid.style.cssText;
        table.style.width = gridWidth + "px";
        table.appendChild(document.createElement("tbody"));
        table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
        var cells = table.getElementsByTagName("TH");

        var gridRow = grid.getElementsByTagName("TR")[0];
        for (var i = 0; i < cells.length; i++) {
            var width;
            if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                width = headerCellWidths[i];
            }
            else {
                width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
            }
            cells[i].style.width = parseInt(width - 3) + "px";
            gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
        }
        parentDiv.removeChild(grid);

        var dummyHeader = document.createElement("div");
        dummyHeader.appendChild(table);
        parentDiv.appendChild(dummyHeader);
        var scrollableDiv = document.createElement("div");
        if (parseInt(gridHeight) > ScrollHeight) {
            gridWidth = parseInt(gridWidth) + 17;
        }
        scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
        scrollableDiv.appendChild(grid);
        parentDiv.appendChild(scrollableDiv);
    }

</script>--%>


</head>

<body>
    <form id="formCode" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="wrapper">

        
        <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0"><!-- Navigation Menu Cabecera e izquierda-->
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
                                    a los formularios principales 
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
                        <!-- /.buscar -->
                        <li class="sidebar-search">
                            <div class="input-group custom-search-form">
                                <input type="text" class="form-control" placeholder="Search..." />
                                <span class="input-group-btn">
                                <button class="btn btn-default" type="button">
                                    <i class="fa fa-search"></i>
                                </button>
                            </span>
                            </div>
                        </li>
                        <li>
                            <a href="Principal.aspx"><i class="fa fa-dashboard fa-fw"></i> Panel de Control</a>
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
                        </li>
                        <!-- /.Estructura Documenta -->
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
                        </li>
                        <!-- /.Formularios -->
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
                        </li>
                        <!-- /.Codigos QR -->
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
                                </li>  --%>
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
        </nav><!-- Navigation -->


        
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>    
        
        <!-- <div id="page-wrapper">/#page-wrapper -->
        <div id="pagevistaform" runat="server"><!-- /#page-wrapper -->

            <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="Update">
                <ProgressTemplate>
                    <div class="centrado" style="z-index:1100;">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

<%--          <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="Update">
                <ProgressTemplate>
                    <div class="centrado" style="z-index:1100;">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdateProgress ID="Progress1" runat="server" AssociatedUpdatePanelID="Update1">
                <ProgressTemplate>
                    <div class="centrado" style="z-index:1100;">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
            <%--<div runat="server" id="DivCampos0" visible="false"  style=" width: 90%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">--%>
          <div runat="server" id="DvPreparado" visible="false"  style=" width: 30%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <i id="I2" runat="server" class="fa fa-exclamation-circle fa-2x"></i>
                <asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="Lbmensaje" BorderStyle="None" border="0" Width="100%" Text=" Se eliminarán los registros seleccionados ¿Desea continuar?"  />
                <div class="row" id="cuestion" visible="true" runat="server">
                    <div class="col-lg-6">
                        <asp:Button runat="server" ID="BtnAcepta" Visible="true" tooltip="Aceptar" CssClass="btn btn-success btn-block" Width="100%"  Text="Aceptar" OnClick="checkSi_Click"/>                                                    
                    </div>
                    <div class="col-lg-6">
                        <asp:Button runat="server" ID="BTnNoAcepta" Visible="true" tooltip="Cancelar" CssClass="btn btn-success btn-block" Width="100%"  Text="Cancelar" OnClick="checkNo_Click"/>                 
                    </div>
                </div>
                 <div class="row" id="Asume" visible="false" runat="server">
                    <div class="col-lg-12">
                        <asp:Button runat="server" ID="btnasume" Visible="true" tooltip="Aceptar" CssClass="btn btn-success btn-block" Width="100%"  Text="Aceptar" OnClick="checkOk_Click"/>
                    </div>
                </div>
            </div>

          <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="Lberror"  Text="" > </asp:Label>
          <div class="row">
              <div class="col-lg-12">
                    <div class="col-lg-10" >
                        <h3 id="H3Titulo" runat="server" visible="true"> Revisión de lotes <i class="fa fa-long-arrow-right"></i> <small> “Revisión lotes y preparación importación a GoldenSoft”  </small>              
                        </h3>
                         <h3 id="H3Desarrollo" runat="server"  style="color:red;" visible="false">(DESARROLLO) --> Revisión de lotes <i class="fa fa-long-arrow-right"></i> <small> “Revisión lotes y preparación importación a GoldenSoft”  </small></h3>
                     </div>
                    <div id="dvPrinters" runat="server" style="text-align:right;" visible="true" class="col-lg-2" >
                    <br />
                      <button id="BtPrinters" type="button" runat="server" class="text-muted"  onserverClick="btPrinter_Click"><i title="Selección otra Impresora"  class="fa fa-qrcode"></i></button>                                      
                     </div>
                  <div id="dvDrlist" runat="server" style="text-align:right;" Visible="false" class="col-lg-2" >
                      <br />
                            <%--<asp:DropDownList runat="server"  CssClass="form-control"  Width="100%"  ID="DrPrinters" tooltip="Contiene las distintas impresoras configuradas" OnSelectedIndexChanged="DrPrinters_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />--%>
                      </div>
            
                </div>
        </div><!-- /.row -->


<%--        <div class="row">
            <div class="col-lg-6">
                <div class="panel panel-default">
                    <div class="panel-heading">                    
                        <h3 class="panel-title"><i class="fa fa-long-arrow-right"></i>Listas para la búsqueda de QR
                                                                  
                        </h3>
                    </div>
                    <div class="panel-body">
                        
                    </div>
                </div>
            </div>

            <div class="col-lg-6">
                <div class="panel panel-default">
                    <div class="panel-heading">                    
                        <h3 class="panel-title"><i class="fa fa-long-arrow-right"></i>Procesado de Lotes
                                                                               
                        </h3>
                    </div>
                    <div class="panel-body">
 
                    </div>
                </div>
        </div>
   </div>--%>

    <div runat="server" id="alerta" visible="false" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <%--<a href="#" id="alerT" runat="server" class="alert alert-info alert-dismissable">--%>
                    <i id="IAlert" runat="server" class="fa fa-exclamation-circle"></i>
                    <asp:Label runat="server" class="alert alert-info alert-dismissable" ID="TextAlerta" BorderStyle="None" border="0" Width="100%" Text=""  />
                <%--</a> --%> 
                <%--<asp:Button runat="server" ID="btProcesa" tooltip="Busca si el Lote tiene registro de Formulario Scan-IT" visible="false" CssClass="btn btn-success btn-block" Width="100%"  Text="Procesar Lote" OnClick="btnPorcesa_Click"/>--%>
                <asp:Button ID="btnPrint2" runat="server" tooltip="Lote con Formulario Scan-IT para imprimir con el total de su información" visible="false" CssClass="btn btn-success " Width="100%" Text="Imprimir código QR" OnClientClick="return PrintPanel();" />



    </div>
        <div runat="server" id="alertaLog" visible="false" class="alert alert-warning alert-dismissable">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <%--<a href="#" id="a1" runat="server" class="alert alert-warning alert-dismissable">--%>
                    <i id="I1" runat="server" class="fa fa-exclamation-circle"></i>
                    <asp:Label runat="server" class="alert alert-warning alert-dismissable" ID="TextAlertaLog" BorderStyle="None" border="0" Width="100%" Text=""  />
                <%--</a>--%>  
                <%--<asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label9"  Text="Usuario:" > </asp:Label>
                <asp:TextBox ID="TextUser"  style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label10"  Text="Password:" > </asp:Label>
                <asp:TextBox ID="TextPass"  TextMode="Password" style="text-align:center; font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>--%>
            <br />      
            <asp:Button runat="server" ID="Btvalidauser" tooltip="Se eliminará el registro seleccionado" CssClass="btn btn-danger btn-block" Width="100%"  Text="¿Está seguro de eliminar este registro?" OnClick="btnValidaUser_Click"/>
    </div>

    <div runat="server" id="alertaErr" visible="false" class="alert alert-danger alert-dismissable">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <%--<a href="#" id="alerTErr" runat="server" class="alert alert-danger alert-dismissable">--%>
                <i id="IAlertErr" runat="server" class="fa fa-exclamation-circle"></i>
                <asp:Label runat="server" class="alert alert-danger alert-dismissable" ID="TextAlertaErr" BorderStyle="None" border="0" Width="100%" Text=""  />
                <%--</a>--%>   
                <%--<asp:Button runat="server" ID="btPorcesa" tooltip="Busca si el Lote tiene registro de Formulario Scan-IT" visible="false" CssClass="btn btn-success btn-block" Width="100%"  Text="Procesar Lote" OnClick="btnPorcesa_Click"/>--%>
        </div>





                        <%--aqui iban los lotes escaneados y procesados --%> 





        <div class="row">
            <div class="col-lg-12">
                  <div class="panel panel-default">
                    <div class="panel-heading" style="height:50px;">
                    <h3 class="panel-title" ><i class="fa fa-long-arrow-right"></i> Entradas del Formulario seleccionado:
                         <button id="btPrintCabecera" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Exporta a Excel la vista previa presentada en pantalla según Filtro" class="fa fa-file-excel-o fa-2x"></i></button>
                    <%--<button id="BiRestore" type="button" runat="server" class="pull-right text-muted "  onserverClick="btnRestore_Click"><i title="El Lote seleccionado se restaura a su estado anterior, de finalizado a procesado" class="fa fa-undo"></i></button>--%>
                                
                    </h3>                               
                    </div>
                    <div class="panel-body">
            
                     <div class="row">       
                            <div class="col-lg-2" >
                                <asp:Label  type="text" Width="100%" visible="false" style=" font-weight: bold;"  runat="server" ID="LbIDLote"  Text="" > </asp:Label>
                                <asp:Label  type="text" Width="80%" style=" font-weight: bold;"  runat="server" ID="LbLote"  Text="Tipo Formulario:" > </asp:Label>
                                   
                            </div>
                            <div class="col-lg-3" >
                                <%--<asp:UpdatePanel ID="Update" runat="server" UpdateMode="Conditional">      
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="DrVariedad" EventName="selectedindexchanged" />
                                    </Triggers>
                                    <ContentTemplate>--%>
                                            <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Lote para filtrar Lotes"  ID="DrVariedad"  OnSelectedIndexChanged="DrVariedad_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                   <%-- </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </div>
                            
                            <div class="col-sm-4" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowEntrada"  Text="" > </asp:Label>
                            </div>
                             <div class="col-lg-1" >
                                    <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align: right; padding:6px;"  runat="server" ID="Label1"  Text="Estados:" > </asp:Label>
                             </div>
                            <div class="col-sm-2" >
                                <%--<asp:UpdatePanel ID="Update1" runat="server" UpdateMode="Conditional">      
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="dtEntrada" EventName="selectedindexchanged" />
                                    </Triggers>
                                    <ContentTemplate>--%>
                                        <asp:DropDownList ID="dtEntrada" style=" position:absolute;" CssClass="form-control" Width="90%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dtEntrada_SelectedIndexChanged"></asp:DropDownList> 
                               <%--     </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </div> 
                             <%--<div class="col-sm-1" >    
                               <div data-tip="Muestra los enviados a GoldenSoft con Estado a 2">
                                    <label runat="server" visible="true" tooltip="Muestra los enviados a GoldenSoft" id="LBCheck" class="switch pull-right">
                                        <input runat="server" onclick="submitit()" id="chkOnOff" data-toggle="tooltip" data-placement="top"  title="Muestra los enviados a GoldenSoft con Estado a 2"  type="checkbox"/><span class="slider round"></span>
                                        <asp:Button ID="btn" runat="server" OnClick="checkListas_Click" Height="0" Width="0" CssClass="hidden" />
                                    </label> 
                                </div>
                            </div>    --%>                                
                    
                     </div>
                     <br />
                     <div class="row">  

                         <div class="col-sm-1" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label19"  Text="Filas:" > </asp:Label>
                        </div>
                        <div class="col-sm-1" >                              
                            <asp:DropDownList ID="ddEntradaPageSize" style=" position:absolute;" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvEntrada_PageSize_Changed">  
                            </asp:DropDownList>  
                        </div>
                        <%--boton con solo  border color Style=" background-color:#ffffff;border-radius: 6px;border: 2px solid #12be20;"--%>
                         <div class="col-lg-2" >
                         </div>
                        <%--<div class="col-lg-2" >
                             <asp:Button runat="server" ID="BtFinalizalote" Visible="true" tooltip="Finaliza sólo este Lote del formulario seleccionado" CssClass="btn btn-success btn-block" Width="100%"  Text="Finaliza este Lote" OnClick="BtFinalizalote_Click"/>  CssClass="btn btn-warning btn-block"                              
                         </div>--%>
                         <div class="col-lg-2" >
                              <asp:Button runat="server" ID="BtFinalizaTodos" Visible="true" tooltip="Finaliza Lotes seleccionados con el check " CssClass="btn btn-warning btn-block"  Width="100%"  Text="Finalizar Lotes" OnClick="BtFinalizaTodos_Click"/>                              
                               <asp:Button runat="server" ID="BtEnviaFinalizados" Visible="false" tooltip="Envia Lotes seleccionados finalizados a GoldenSoft." CssClass="btn btn-info btn-block"  Width="100%"  Text="Enviar Lotes seleccionados" OnClick="BtEnviaFinalizados_Click"/>                              
                         </div>
                        <div class="col-lg-1" >
                        </div>
                         <div class="col-lg-2" >
                               <asp:Button runat="server" ID="BTerminado" visible="false" tooltip="Revertir él o los Lotes seleccionados con el check a estado anterior" CssClass="btn btn-success btn-block"  Width="100%"  Text="Revertir Lotes" OnClick="BTerminado_Click"/>
                        </div>
                        <div class="col-lg-1" >
                        </div>
                        <div class="col-lg-2" >
                             <asp:Button runat="server" ID="Btfin" visible="true" tooltip="Elimina él o los Lotes seleccionados con el check" CssClass="btn btn-danger btn-block" Width="100%"  Text="Eliminar Lotes" OnClick="BTfin_Click"/>
                        </div>
                         
                    </div>
                    <br />

                    <div class="row" id="DivGrid" style="width: 100%; height: 600px; overflow: auto;" runat="server">
                        <div class="col-lg-12" style="overflow: auto;">

  <%--                          <div id="DivRoot" align="left">
                                <div style="overflow: hidden;" id="DivHeaderRow">
                                </div>

                                    <div style="overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">--%>



                                        <asp:GridView ID="gvEntrada"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                                AllowSorting="true" OnSorting="gvEntrada_OnSorting" 
                                                CellPadding="4"  GridLines="None"  OnRowDataBound="gvEntrada_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="10" OnRowCommand="gvEntrada_RowCommand" DataKeyNames="ID"
                                                oonselectedindexchanged="gvEntrada_SelectedIndexChanged"  OnRowEditing="gvEntrada_RowEditing" OnRowCancelingEdit="gvEntrada_RowCancelingEdit" OnRowUpdating="gvEntrada_RowUpdating" 
                                                OnRowDeleting="gvEntrada_RowDeleting" onpageindexchanging="gvEntrada_PageIndexChanging"  >


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

                                               <%-- <asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                        <ItemTemplate>
                                                           <asp:ImageButton ID="ibtSubeCarga" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/sendUp20x20.png"
                                                           CommandName="SubeCarga" ToolTip="Devuelve la línea a órdenes de Carga" Width="20px" Height="20px"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Height="8px"></ItemStyle>                                
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Selección Todos">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkb1" runat="server" OnCheckedChanged="sellectAll"
                                                        AutoPostBack="true" />
                                                </HeaderTemplate>
                                                 <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                    <itemtemplate>
                                                        <asp:CheckBox ID="chbItem" runat="server" />
                                                    </itemtemplate>
                                                    <itemstyle horizontalalign="center" />
                                                </asp:TemplateField>


                                              <%--  <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chbItem" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

        <%--                                        <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                        <asp:ImageButton ID="ibtCamion" runat="server" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' CommandName="CargaCamion" 
		                                                    ToolTip="Tramita la línea directamente al Camión" Visible="true" ImageUrl="~/Images/etiqueta25x25.png" Width="20px" Height="20px"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Height="8px"></ItemStyle>                                
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="ID" Visible="false" >
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabID" Visible="false" runat="server" Text='<%# Eval("ID") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabID" Visible="false" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo Formulario" SortExpression="TIPO_FORM">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabTIPOFORM" runat="server" Text='<%# Eval("TIPO_FORM") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabTIPOFORM" runat="server" Text='<%# Bind("TIPO_FORM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha" SortExpression="FECHA">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabFECHA" runat="server" Text='<%# Eval("FECHA") %>' class="gridListDinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabFECHA" runat="server" Text='<%# Bind("FECHA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo Planta" SortExpression="TIPO_PLANTA">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabTIPOPLANTA" runat="server" Text='<%# Eval("TIPO_PLANTA") %>' class="gridListDinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabTIPOPLANTA" runat="server" Text='<%# Bind("TIPO_PLANTA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Variedad" SortExpression="VARIEDAD">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabVARIEDAD" runat="server" Text='<%# Eval("VARIEDAD") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabVARIEDAD" runat="server" Text='<%# Bind("VARIEDAD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lote" SortExpression="LOTE">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabLOTE" runat="server" Text='<%# Eval("LOTE") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabLOTE" runat="server" Text='<%# Bind("LOTE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lote Destino" SortExpression="LOTEDESTINO">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabLOTEDESTINO" runat="server" Text='<%# Eval("LOTEDESTINO") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabLOTEDESTINO" runat="server" Text='<%# Bind("LOTEDESTINO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unidades" SortExpression="UNIDADES">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabUNIDADES" runat="server" Text='<%# Eval("UNIDADES") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabUNIDADES" runat="server" Text='<%# Bind("UNIDADES") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Num. Unidades" SortExpression="NUM_UNIDADES">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabNUM_UNIDADES" runat="server" Text='<%# Eval("NUM_UNIDADES") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabNUM_UNIDADES" runat="server" Text='<%# Bind("NUM_UNIDADES") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Manojos" SortExpression="MANOJOS">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabMANOJOS" runat="server" Text='<%# Eval("MANOJOS") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabMANOJOS" runat="server" Text='<%# Bind("MANOJOS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Desde" SortExpression="DESDE">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabDESDE" runat="server" Text='<%# Eval("DESDE") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabDESDE" runat="server" Text='<%# Bind("DESDE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hasta" SortExpression="HASTA">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabHASTA" Width="100%" runat="server" Text='<%# Eval("HASTA") %>' class="gridListDinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabHASTA" Width="100%" runat="server" Text='<%# Bind("HASTA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        

                                                <asp:TemplateField HeaderText="Etiq. desde" SortExpression="ETDESDE">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabETDESDE" runat="server" Text='<%# Eval("ETDESDE") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabETDESDE" runat="server" Text='<%# Bind("ETDESDE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Etiq. hasta" SortExpression="ETHASTA">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabLETHASTA" Width="100%" runat="server" Text='<%# Eval("ETHASTA") %>' class="gridListDinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabLETHASTA" Width="100%" runat="server" Text='<%# Bind("ETHASTA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        
                                                <asp:TemplateField HeaderText="Tuneles" SortExpression="TUNELES">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabTUNELES" runat="server" Text='<%# Eval("TUNELES") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabTUNELES" runat="server" Text='<%# Bind("TUNELES") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pasillos" SortExpression="PASILLOS">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabPASILLOS" runat="server" Text='<%# Eval("PASILLOS") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabPASILLOS" runat="server" Text='<%# Bind("PASILLOS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OK" SortExpression="OK">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabOK" runat="server" Text='<%# Eval("OK") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabOK" runat="server" Text='<%# Bind("OK") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Observaciones" SortExpression="OBSERVACIONES">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabOBSERVACIONES" runat="server" Text='<%# Eval("OBSERVACIONES") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabOBSERVACIONES" runat="server" Text='<%# Bind("OBSERVACIONES") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Id. Dispositivo" >
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabDeviceID" runat="server" Text='<%# Eval("DeviceID") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabDeviceID" runat="server" Text='<%# Bind("DeviceID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Dispositivo" SortExpression="DeviceName" >
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabDeviceName" runat="server" Text='<%# Eval("DeviceName") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabDeviceName" runat="server" Text='<%# Bind("DeviceName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Fecha Envío" SortExpression="SendTime">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabSendTime" runat="server" Text='<%# Eval("SendTime") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabSendTime" runat="server" Text='<%# Bind("SendTime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha recepción" SortExpression="ReceiveTime">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabReceiveTime" runat="server" Text='<%# Eval("ReceiveTime") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabReceiveTime" runat="server" Text='<%# Bind("ReceiveTime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <%--<asp:TemplateField HeaderText="Código" >
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabBarcode" runat="server" Text='<%# Eval("Barcode") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabBarcode" runat="server" Text='<%# Bind("Barcode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Exportación" >
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabFECHAEXP" runat="server" Text='<%# Eval("FECHAEXP") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabFECHAEXP" runat="server" Text='<%# Bind("FECHAEXP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Id. Secuencia" >
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabID_SECUENCIA" runat="server" Text='<%# Eval("ID_SECUENCIA") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabID_SECUENCIA" runat="server" Text='<%# Bind("ID_SECUENCIA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Prueba" >
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabPRUEBA" runat="server" Text='<%# Eval("PRUEBA") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabPRUEBA" runat="server" Text='<%# Bind("PRUEBA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <%--<asp:TemplateField HeaderText="Fecha Sistema" >
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabSYSDATE" runat="server" Text='<%# Eval("ZSYSDATE") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabSYSDATE" runat="server" Text='<%# Bind("ZSYSDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Estado" SortExpression="ESTADO" >
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabLEstado" runat="server" Text='<%# Eval("ESTADO") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabLEstado" runat="server" Text='<%# Bind("ESTADO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                            <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
                                            <EditRowStyle BackColor="#eaf5dc" />   
                                            <rowstyle Height="20px" />
                                        </asp:GridView>  


                      <%--             </div>
                                    <div id="DivFooterRow" style="overflow:hidden">
                                    </div>
                                </div>--%>
                                
                        </div>
                     </div>
                  </div>
               </div>
            </div>
        </div>
        </div><!-- /#page-wrapper -->
        
        
        </div>
    </form>
    <!-- /#wrapper -->

    <!-- jQuery Version 1.11.0 -->
    <script src="js/jquery-1.11.0.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="js/plugins/metisMenu/metisMenu.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="js/sb-admin-2.js"></script>

           <script type="text/javascript">
               <%--function PrintPanel() {
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
               }--%>

               function submitit() {
                   document.getElementById('btn').click();
               }

           </script>

</body>
</html>
