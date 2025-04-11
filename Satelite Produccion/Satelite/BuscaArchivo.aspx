<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuscaArchivo.aspx.cs" Inherits="Satelite.BuscaArchivo"  %>

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

    <!-- Bootstrap Core CSS MaintainScrollPositionOnPostback="true"-->
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

       <style id="gridStyles" runat="server" type="text/css">
            body
            {
                font-family: Arial;
                font-size: 10pt;
            }
            table
            {
                border: 1px solid #efefef;
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
                border: 1px solid #efefef;
            }
            table, table table td
            {
                border: 0px solid #efefef;
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
              background-color: #efefef;
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
            .juntos{
                height:4px;
                padding:-10px;
                top:-5px;
            }
                        .file-upload {
                display: inline-block;
                overflow: hidden;
                text-align: center;
                vertical-align: middle;
                font-family: Arial;
                /*border: 1px solid #124d77;*/
                background: #d5e450;
                color: #fff;
                border-radius: 6px;
                -moz-border-radius: 6px;
                cursor: pointer;
                text-shadow: #000 1px 1px 2px;
                -webkit-border-radius: 4px;
            }

            .file-upload:hover {
                    background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #0061a7), color-stop(1, #007dc1));
                    background: -moz-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                    background: -webkit-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                    background: -o-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                    background: -ms-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                    background: linear-gradient(to bottom, #0061a7 5%, #007dc1 100%);
                    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0061a7', endColorstr='#007dc1',GradientType=0);
                    background-color: #0061a7;
            }

            /* The button size */
            .file-upload {
                height: 35px;
            }

            .file-upload, .file-upload span {
                    width: 100%;
            }

            .file-upload input {
                        top: 0;
                        left: 0;
                        margin: 0;
                        font-size: 11px;
                        font-weight: bold;
                        /* Loses tab index in webkit if width is set to 0 */
                        opacity: 0;
                        filter: alpha(opacity=0);
            }

            .file-upload strong {
                        font: normal 12px Tahoma,sans-serif;
                        text-align: center;
                        vertical-align: middle;
            }

            .file-upload span {
                        top: 0;
                        left: 0;
                        display: inline-block;
                        /* Adjust button text vertical alignment */
                        padding-top: 5px;
            }

    </style>

</head>

<body>
  <form id="form1" runat="server">
    <div id="wrapper">

        <!-- Navigation -->
        <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="index.html">Viveros de tododododododo Rio Eresma</a>
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
            <div class="navbar-default sidebar" role="navigation">
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
                            <a href="Principal.aspx"><i class="fa fa-dashboard fa-fw"></i> Panel de Control</a>
                        </li>
                        <li>
                            <a class="active" href="#"><i class="fa fa-bar-chart-o fa-fw"></i> Importaciones<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level in">
                                <li>
                                    <a href="ImportTabla.aspx">Desde Tablas</a>
                                </li>
                                <li>
                                    <a class="active" href="ImportArchivo.aspx">Desde Archivo</a>
                                </li>
                            </ul>
                            <!-- /.nav-second-level -->
                        </li>
                        <li>
                            <a  href="#"><i class="fa fa-bar-chart-o fa-fw"></i> Estructura Documental<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level ">
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
                                    <a href="LoteManu.aspx"><i class="fa fa-long-arrow-right"></i> Generación lotes manuales (para lotes externos)</a>
                                    <!-- /.nav-third-level -->
                                </li>
                            </ul>
                            <!-- /.nav-second-level -->
                        </li>
                        
                    </ul>
                </div>
                <!-- /.sidebar-collapse -->
            </div>
            <!-- /.navbar-static-side -->


        </nav>

                     
        
            <div id="page-wrapper">
 

                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">      
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnAcepta" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>

                           


                        <div runat="server" id="DvPreparado" visible="false"  style="height: 20%; min-height:100px; width: 30%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid white; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="centrado">
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


                <asp:Label  type="text"  Width="100%" Visible="false" style=" font-weight: bold;text-align:left;"  runat="server" ID="Lberror"  Text="" > </asp:Label>

                <div class="row">
                  <div class="col-lg-12">
                        <div class="col-lg-8" >
                                <h3 id="H3Titulo" runat="server" visible="true"> Archivos Documentales <i class="fa fa-long-arrow-right"></i> <small> “ Gestión de Tablas del sistema ”  </small>
                                    <asp:Label  type="text"  style=" font-weight: bold; font-size:14px;"  runat="server" ID="Lbhost1"  Text="" > </asp:Label>
                                </h3>
                                <h3 id="H3Desarrollo" runat="server" style="color:red;" visible="false"> DESARROLLO --> Archivos Documentales <i class="fa fa-long-arrow-right"></i> <small> “ Gestión de Tablas del sistema ”  </small>
                                    <asp:Label  type="text"  style=" font-weight: bold; font-size:14px; color:black;"  runat="server" ID="Lbhost2"  Text="" > </asp:Label>
                                </h3>
                        </div>

                        <div runat="server" class="col-lg-1" style=" top:16px;">
                                <input type="image"  class="pull-right text-muted " title="Limpia el filtro general seleccionado a la derecha" src="images/ordencarga25x25.png" onserverclick="ImgOrdenMin_Click"  runat="server" id="ImgOrdenMin" style="border: 0px; " />    
                        </div>


                           
                    </div>
              </div><!-- /.row data-parent="#accordion"-->



 




              <!-- /.row -->
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="font-weight:bold; font-size:18px;" >
                                Importación desde la Tabla seleccionada:  
                            </div>
                            <!-- .panel-heading -->
                            <div class="panel-body">                           
							    <div class="row">
                                    <div class="col-sm-1" >
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label19"  Text="Filas:" > </asp:Label>
                                    </div>
                                    <div class="col-sm-1" >                              
                                        <asp:DropDownList ID="ddControlPageSize" style=" position:absolute;" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvControl_PageSize_Changed">  
                                        </asp:DropDownList>  
                                    </div>
                                    <div class="col-sm-1" >
                                        <button id="btTree" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnTree_Click"><i title="Estructura Documental con Jerarquía" class="fa fa-sort-amount-asc fa-2x"></i></button>
                                    </div>
                                    <div class="col-sm-2" >
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowControl"  Text="Registros:" > </asp:Label>
                                    </div>
                                    <div class="col-sm-2" >
                                        <asp:Label  type="text"  Width="90%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label1"  Text="Archivo Documental:" > </asp:Label>
                                    </div>
                                    <div class="col-sm-2" >
                                          <asp:DropDownList runat="server" class="form-control" ID="DrArchivos" Width="100%"  AppendDataBoundItems="true" AutoPostBack="True"  OnSelectedIndexChanged="DrArchivos_SelectedIndexChanged" />
                                    </div>
                                    <div class="col-sm-1" >
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right;padding:6px; "  runat="server" ID="Label2"  Text="Tabla:" > </asp:Label>
                                    </div>
                                    <div class="col-sm-2" >
                                        <asp:DropDownList runat="server" class="form-control" ID="DrCampos" Width="100%"  AppendDataBoundItems="true" AutoPostBack="True"  OnSelectedIndexChanged="DrCampos_SelectedIndexChanged" />
                                        <%--<asp:ListBox ID="LBcampos" style="height:345px;" CssClass="form-control" Visible="true" runat="server" SelectionMode="Multiple"></asp:ListBox>--%>
                                    </div>
							    </div>

                                    <%--Grid general--%>
                                <div class="row" runat="server" id="divTree" visible="true">
                                     <div class="panel-body">
                                           <div class="col-lg-10" >
                                                <div class="myTreeScroll"  runat="server" >
                                                    <asp:TreeView ID="treeUser" ShowLines="true" runat="server"  ImageSet="XPFileExplorer" NodeIndent="15" OnSelectedNodeChanged="treeUser_SelectedNodeChanged" >
                                                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA"   />
                                                       <%-- <NodeStyle CssClass="Juntos" Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                                                        <ParentNodeStyle Font-Bold="False" />--%>
                                                        <%--<SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="false" HorizontalPadding="0px" VerticalPadding="0px" />--%>

                                                    </asp:TreeView>
                                                </div> 
                                           </div>
                                    </div>
                                </div>


                                <div class="row">
<%--                                    <div id="Div2" runat="server" visible="false" style="overflow:auto;" class="panel-body"> <%-- AutoGenerateSelectButton="True"
                                        <asp:GridView ID="gvCabecera"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                            AllowSorting="true" OnSorting="gvCabecera_OnSorting"
                                            CellPadding="4"  GridLines="None"  OnRowDataBound="gvCabecera_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="15" OnRowCommand="gvCabecera_RowCommand" DataKeyNames="ZID"
                                            oonselectedindexchanged="gvCabecera_SelectedIndexChanged"  OnRowEditing="gvControl_RowEditing" OnRowCancelingEdit="gvCabecera_RowCancelingEdit" OnRowUpdating="gvCabecera_RowUpdating" 
                                            OnRowDeleting="gvCabecera_RowDeleting" onpageindexchanging="gvCabecera_PageIndexChanging"  >
                                        <RowStyle />    
                                            <Columns>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
                                            <EditRowStyle BackColor="#eaf5dc" />                    
                                        </asp:GridView>                                  
                                    </div>--%>
                                    <div id="DivGrid" runat="server" visible="false" class="panel-body"> <%-- style="overflow:auto; height:800px;" AutoGenerateSelectButton="True"--%>  
                                        <asp:GridView ID="gvControl"  runat="server" AutoGenerateColumns="true" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                            AllowSorting="true" OnSorting="gvControl_OnSorting"
                                            CellPadding="4"  GridLines="None"  OnRowDataBound="gvControl_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="10" OnRowCommand="gvControl_RowCommand" DataKeyNames="ZID"
                                            oonselectedindexchanged="gvControl_SelectedIndexChanged"  OnRowEditing="gvControl_RowEditing" OnRowCancelingEdit="gvControl_RowCancelingEdit" OnRowUpdating="gvControl_RowUpdating" 
                                            OnRowDeleting="gvControl_RowDeleting" onpageindexchanging="gvControl_PageIndexChanging"  >
                                        <RowStyle />    
                                            <%--Columnas en modo ejecución--%>
                                            <Columns>
<%--                                                <asp:CommandField ButtonType="Image" 
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
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
                                            <EditRowStyle BackColor="#eaf5dc" />                    
                                        </asp:GridView>                                  
                                    </div>


                                  <div runat="server" id="DivCampos0" visible="false"  style=" width: 90%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
                 <%--<div class="centrado" visible="false" runat="server" id="DivCampos0"  height: 700px; max-height: 700px;>--%>
                                    <div class="row" runat="server" id="DivReg0" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA0">
		                                    <asp:TextBox id="lbL0" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB0">
		                                    <asp:TextBox id="TxL0" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD0">
		                                    <asp:TextBox id="lbD0" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE0">
		                                    <asp:TextBox id="TxD0" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg1" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA1">
		                                    <asp:TextBox id="lbL1" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB1">
		                                    <asp:TextBox id="TxL1" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD1">
		                                    <asp:TextBox id="lbD1" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE1">
		                                    <asp:TextBox id="TxD1" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg2" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA2">
		                                    <asp:TextBox id="lbL2" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB2">
		                                    <asp:TextBox id="TxL2" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD2">
		                                    <asp:TextBox id="lbD2" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE2">
		                                    <asp:TextBox id="TxD2" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg3" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA3">
		                                    <asp:TextBox id="lbL3" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB3">
		                                    <asp:TextBox id="TxL3" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD3">
		                                    <asp:TextBox id="lbD3" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE3">
		                                    <asp:TextBox id="TxD3" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg4" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA4">
		                                    <asp:TextBox id="lbL4" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB4">
		                                    <asp:TextBox id="TxL4" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD4">
		                                    <asp:TextBox id="lbD4" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE4">
		                                    <asp:TextBox id="TxD4" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg5" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA5">
		                                    <asp:TextBox id="lbL5" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB5">
		                                    <asp:TextBox id="TxL5" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD5">
		                                    <asp:TextBox id="lbD5" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE5">
		                                    <asp:TextBox id="TxD5" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg6" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA6">
		                                    <asp:TextBox id="lbL6" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB6">
		                                    <asp:TextBox id="TxL6" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD6">
		                                    <asp:TextBox id="lbD6" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE6">
		                                    <asp:TextBox id="TxD6" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg7" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA7">
		                                    <asp:TextBox id="lbL7" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB7">
		                                    <asp:TextBox id="TxL7" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD7">
		                                    <asp:TextBox id="lbD7" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE7">
		                                    <asp:TextBox id="TxD7" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg8" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA8">
		                                    <asp:TextBox id="lbL8" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB8">
		                                    <asp:TextBox id="TxL8" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD8">
		                                    <asp:TextBox id="lbD8" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE8">
		                                    <asp:TextBox id="TxD8" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg9" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA9">
		                                    <asp:TextBox id="lbL9" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB9">
		                                    <asp:TextBox id="TxL9" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD9">
		                                    <asp:TextBox id="lbD9" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE9">
		                                    <asp:TextBox id="TxD9" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg10" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA10">
		                                    <asp:TextBox id="lbL10" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB10">
		                                    <asp:TextBox id="TxL10" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD10">
		                                    <asp:TextBox id="lbD10" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE10">
		                                    <asp:TextBox id="TxD10" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg11" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA11">
		                                    <asp:TextBox id="lbL11" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB11">
		                                    <asp:TextBox id="TxL11" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD11">
		                                    <asp:TextBox id="lbD11" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE11">
		                                    <asp:TextBox id="TxD11" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg12" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA12">
		                                    <asp:TextBox id="lbL12" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB12">
		                                    <asp:TextBox id="TxL12" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD12">
		                                    <asp:TextBox id="lbD12" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE12">
		                                    <asp:TextBox id="TxD12" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg13" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA13">
		                                    <asp:TextBox id="lbL13" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB13">
		                                    <asp:TextBox id="TxL13" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD13">
		                                    <asp:TextBox id="lbD13" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE13">
		                                    <asp:TextBox id="TxD13" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg14" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA14">
		                                    <asp:TextBox id="lbL14" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB14">
		                                    <asp:TextBox id="TxL14" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD14">
		                                    <asp:TextBox id="lbD14" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE14">
		                                    <asp:TextBox id="TxD14" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg15" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA15">
		                                    <asp:TextBox id="lbL15" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB15">
		                                    <asp:TextBox id="TxL15" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD15">
		                                    <asp:TextBox id="lbD15" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE15">
		                                    <asp:TextBox id="TxD15" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg16" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA16">
		                                    <asp:TextBox id="lbL16" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB16">
		                                    <asp:TextBox id="TxL16" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD16">
		                                    <asp:TextBox id="lbD16" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE16">
		                                    <asp:TextBox id="TxD16" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg17" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA17">
		                                    <asp:TextBox id="lbL17" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB17">
		                                    <asp:TextBox id="TxL17" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD17">
		                                    <asp:TextBox id="lbD17" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE17">
		                                    <asp:TextBox id="TxD17" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg18" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA18">
		                                    <asp:TextBox id="lbL18" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB18">
		                                    <asp:TextBox id="TxL18" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD18">
		                                    <asp:TextBox id="lbD18" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE18">
		                                    <asp:TextBox id="TxD18" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg19" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA19">
		                                    <asp:TextBox id="lbL19" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB19">
		                                    <asp:TextBox id="TxL19" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD19">
		                                    <asp:TextBox id="lbD19" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE19">
		                                    <asp:TextBox id="TxD19" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg20" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA20">
		                                    <asp:TextBox id="lbL20" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB20">
		                                    <asp:TextBox id="TxL20" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD20">
		                                    <asp:TextBox id="lbD20" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE20">
		                                    <asp:TextBox id="TxD20" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg21" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA21">
		                                    <asp:TextBox id="lbL21" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB21">
		                                    <asp:TextBox id="TxL21" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD21">
		                                    <asp:TextBox id="lbD21" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE21">
		                                    <asp:TextBox id="TxD21" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg22" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA22">
		                                    <asp:TextBox id="lbL22" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB22">
		                                    <asp:TextBox id="TxL22" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD22">
		                                    <asp:TextBox id="lbD22" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE22">
		                                    <asp:TextBox id="TxD22" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg23" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA23">
		                                    <asp:TextBox id="lbL23" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB23">
		                                    <asp:TextBox id="TxL23" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD23">
		                                    <asp:TextBox id="lbD23" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE23">
		                                    <asp:TextBox id="TxD23" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg24" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA24">
		                                    <asp:TextBox id="lbL24" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB24">
		                                    <asp:TextBox id="TxL24" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD24">
		                                    <asp:TextBox id="lbD24" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE24">
		                                    <asp:TextBox id="TxD24" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg25" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA25">
		                                    <asp:TextBox id="lbL25" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB25">
		                                    <asp:TextBox id="TxL25" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD25">
		                                    <asp:TextBox id="lbD25" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE25">
		                                    <asp:TextBox id="TxD25" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg26" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA26">
		                                    <asp:TextBox id="lbL26" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB26">
		                                    <asp:TextBox id="TxL26" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD26">
		                                    <asp:TextBox id="lbD26" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE26">
		                                    <asp:TextBox id="TxD26" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg27" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA27">
		                                    <asp:TextBox id="lbL27" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB27">
		                                    <asp:TextBox id="TxL27" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD27">
		                                    <asp:TextBox id="lbD27" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE27">
		                                    <asp:TextBox id="TxD27" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg28" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA28">
		                                    <asp:TextBox id="lbL28" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB28">
		                                    <asp:TextBox id="TxL28" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD28">
		                                    <asp:TextBox id="lbD28" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE28">
		                                    <asp:TextBox id="TxD28" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg29" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA29">
		                                    <asp:TextBox id="lbL29" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB29">
		                                    <asp:TextBox id="TxL29" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD29">
		                                    <asp:TextBox id="lbD29" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE29">
		                                    <asp:TextBox id="TxD29" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg30" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA30">
		                                    <asp:TextBox id="lbL30" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB30">
		                                    <asp:TextBox id="TxL30" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD30">
		                                    <asp:TextBox id="lbD30" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE30">
		                                    <asp:TextBox id="TxD30" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg31" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA31">
		                                    <asp:TextBox id="lbL31" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB31">
		                                    <asp:TextBox id="TxL31" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD31">
		                                    <asp:TextBox id="lbD31" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE31">
		                                    <asp:TextBox id="TxD31" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg32" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA32">
		                                    <asp:TextBox id="lbL32" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB32">
		                                    <asp:TextBox id="TxL32" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD32">
		                                    <asp:TextBox id="lbD32" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE32">
		                                    <asp:TextBox id="TxD32" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg33" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA33">
		                                    <asp:TextBox id="lbL33" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB33">
		                                    <asp:TextBox id="TxL33" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD33">
		                                    <asp:TextBox id="lbD33" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE33">
		                                    <asp:TextBox id="TxD33" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg34" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA34">
		                                    <asp:TextBox id="lbL34" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB34">
		                                    <asp:TextBox id="TxL34" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD34">
		                                    <asp:TextBox id="lbD34" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE34">
		                                    <asp:TextBox id="TxD34" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg35" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA35">
		                                    <asp:TextBox id="lbL35" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB35">
		                                    <asp:TextBox id="TxL35" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD35">
		                                    <asp:TextBox id="lbD35" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE35">
		                                    <asp:TextBox id="TxD35" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg36" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA36">
		                                    <asp:TextBox id="lbL36" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB36">
		                                    <asp:TextBox id="TxL36" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD36">
		                                    <asp:TextBox id="lbD36" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE36">
		                                    <asp:TextBox id="TxD36" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg37" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA37">
		                                    <asp:TextBox id="lbL37" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB37">
		                                    <asp:TextBox id="TxL37" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD37">
		                                    <asp:TextBox id="lbD37" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE37">
		                                    <asp:TextBox id="TxD37" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg38" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA38">
		                                    <asp:TextBox id="lbL38" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB38">
		                                    <asp:TextBox id="TxL38" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD38">
		                                    <asp:TextBox id="lbD38" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE38">
		                                    <asp:TextBox id="TxD38" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg39" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA39">
		                                    <asp:TextBox id="lbL39" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB39">
		                                    <asp:TextBox id="TxL39" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD39">
		                                    <asp:TextBox id="lbD39" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE39">
		                                    <asp:TextBox id="TxD39" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg40" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA40">
		                                    <asp:TextBox id="lbL40" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB40">
		                                    <asp:TextBox id="TxL40" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD40">
		                                    <asp:TextBox id="lbD40" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE40">
		                                    <asp:TextBox id="TxD40" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg41" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA41">
		                                    <asp:TextBox id="lbL41" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB41">
		                                    <asp:TextBox id="TxL41" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD41">
		                                    <asp:TextBox id="lbD41" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE41">
		                                    <asp:TextBox id="TxD41" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg42" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA42">
		                                    <asp:TextBox id="lbL42" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB42">
		                                    <asp:TextBox id="TxL42" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD42">
		                                    <asp:TextBox id="lbD42" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE42">
		                                    <asp:TextBox id="TxD42" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg43" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA43">
		                                    <asp:TextBox id="lbL43" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB43">
		                                    <asp:TextBox id="TxL43" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD43">
		                                    <asp:TextBox id="lbD43" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE43">
		                                    <asp:TextBox id="TxD43" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg44" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA44">
		                                    <asp:TextBox id="lbL44" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB44">
		                                    <asp:TextBox id="TxL44" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD44">
		                                    <asp:TextBox id="lbD44" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE44">
		                                    <asp:TextBox id="TxD44" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg45" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA45">
		                                    <asp:TextBox id="lbL45" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB45">
		                                    <asp:TextBox id="TxL45" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD45">
		                                    <asp:TextBox id="lbD45" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE45">
		                                    <asp:TextBox id="TxD45" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg46" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA46">
		                                    <asp:TextBox id="lbL46" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB46">
		                                    <asp:TextBox id="TxL46" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD46">
		                                    <asp:TextBox id="lbD46" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE46">
		                                    <asp:TextBox id="TxD46" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg47" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA47">
		                                    <asp:TextBox id="lbL47" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB47">
		                                    <asp:TextBox id="TxL47" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD47">
		                                    <asp:TextBox id="lbD47" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE47">
		                                    <asp:TextBox id="TxD47" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg48" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA48">
		                                    <asp:TextBox id="lbL48" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB48">
		                                    <asp:TextBox id="TxL48" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD48">
		                                    <asp:TextBox id="lbD48" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE48">
		                                    <asp:TextBox id="TxD48" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg49" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA49">
		                                    <asp:TextBox id="lbL49" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB49">
		                                    <asp:TextBox id="TxL49" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD49">
		                                    <asp:TextBox id="lbD49" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE49">
		                                    <asp:TextBox id="TxD49" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>


                                    <div class="row" runat="server" id="DivReg50" visible="false">
	                                    <div class="col-lg-2" runat="server" id="DivColumA50">
		                                    <asp:TextBox id="lbL50" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumB50">
		                                    <asp:TextBox id="TxL50" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-2" runat="server" id="DivColumD50">
		                                    <asp:TextBox id="lbD50" runat="server" style="width:100%;border-style:none;background-color:#ffffff;" ReadOnly="true" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
	                                    <div class="col-lg-4" runat="server" id="DivColumE50">
		                                    <asp:TextBox id="TxD50" runat="server" style="width:100%;border-style:inset;background-color:#efefef;" class="form-control" placeholder=""></asp:TextBox>
	                                    </div>
                                    </div>

                                    <div class="row" runat="server" id="DivFicheros" visible="true" style="width:100%; height:180px;" >
                                        
                                        <div id="Div8" runat="server" style=" height:180px;overflow:auto;" class="col-lg-8"> <%-- AutoGenerateSelectButton="True"--%>                                  
                                            <br />
                                            <asp:GridView ID="gvLista"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                                AllowSorting="true" OnSorting="gvLista_OnSorting"
                                                CellPadding="4"  GridLines="None"  OnRowDataBound="gvLista_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="10" OnRowCommand="gvLista_RowCommand" DataKeyNames="ZID"
                                                oonselectedindexchanged="gvLista_SelectedIndexChanged"  OnRowEditing="gvLista_RowEditing" OnRowCancelingEdit="gvLista_RowCancelingEdit" OnRowUpdating="gvLista_RowUpdating" 
                                                OnRowDeleting="gvLista_RowDeleting" onpageindexchanging="gvLista_PageIndexChanging" EnablePersistedSelection="True"  >
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

                                                    <asp:TemplateField HeaderText="Nombre"  SortExpression="NOMBRE">
                                                        <EditItemTemplate>
                                                                <asp:TextBox ID="TabLNumLinea"  runat="server" Text='<%# Eval("NOMBRE") %>' class="gridListAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                                <asp:Label ID="LabLNumLinea"  runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Ruta" SortExpression="RUTA">
                                                        <EditItemTemplate>
                                                                <asp:TextBox ID="TabLEmpresa" runat="server" Text='<%# Eval("RUTA") %>' class="gridListAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                                <asp:Label ID="LabLLEmpresa" runat="server" Text='<%# Bind("RUTA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Fecha" SortExpression="FECHA">
                                                        <EditItemTemplate>
                                                                <asp:TextBox ID="TabLProveedor" runat="server" Text='<%# Eval("FECHA") %>' class="gridListDinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                                <asp:Label ID="LabLLProveedor" runat="server" Text='<%# Bind("FECHA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Categoria" SortExpression="CATEGORIA">
                                                        <EditItemTemplate>
                                                                <asp:TextBox ID="TabLFiscal" runat="server" Text='<%# Eval("CATEGORIA") %>' class="gridListDinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                                <asp:Label ID="LabLLFiscal" runat="server" Text='<%# Bind("CATEGORIA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Peso" SortExpression="PESO">
                                                        <EditItemTemplate>
                                                                <asp:TextBox ID="TabLpeso" runat="server" Text='<%# Eval("PESO") %>' class="gridListDinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                                <asp:Label ID="LabLLpesol" runat="server" Text='<%# Bind("PESO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Estado" SortExpression="ESTADO">
                                                        <EditItemTemplate>
                                                                <asp:TextBox ID="TabLRuta" runat="server" Text='<%# Eval("ESTADO") %>' class="gridListAinput"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                                <asp:Label ID="LabLRuta" runat="server" Text='<%# Bind("ESTADO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>    --%>

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
                                        <div class="col-lg-4" style="margin-top: 15px;" >
                                            <iframe style="width:100%; height:170px; border:none;" scrolling="no" sandbox="allow-same-origin allow-forms allow-scripts" src="importFiles.aspx"></iframe>
                                            <asp:Label ID="Label9" Style="color:olivedrab ;text-align:right;" runat="server" Text=""></asp:Label>
                                        </div>


                                    </div>



                                    <div class="row">
                                        <br />
                                        <div class="col-lg-1">
                                        </div>
                                        <div class="col-lg-2">
                                            <button type="button" visible="true" id="BtnNewDato" style="width:100%;" runat="server" onServerClick="BtnNewDato_click" class="btn btn-primary">Nuevos Datos</button>
                                        </div>
                                        <div class="col-lg-2">
                                           <button type="button" visible="true" id="BtnModificaDato" style="width:100%;" runat="server" onServerClick="BtnModificaDato_Click" class="btn btn-warning disabled">Modificar Datos</button>
                                        </div>
                                        <div class="col-lg-2" style="text-align:center;">
                                             <button id="btOpenFiles" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="btnOpenFiles_Click"><i title="Muestra los documentos asociados a este registro" class="fa fa-archive fa-2x"></i></button>
                                        </div>
                                        <div class="col-lg-2">
                                           <button type="button" visible="true" id="BtnGuardaDato" style="width:100%;" runat="server" onServerClick="BtnGuardaDato_click" class="btn btn-primary">Guardar Datos</button>
                                        </div>
                                        <div class="col-lg-2">
                                           <button type="button" visible="true" id="BtnEliminaDato" style="width:100%;" runat="server" onServerClick="BtnEliminaDato_click" class="btn btn-primary">Eliminar Datos</button>
                                        </div>
                                         <div class="col-lg-1">
                                        </div>
                                    </div>

                                </div>     <!-- /.panel-body -->
                                </div>
                        </div>


  



                        <!-- /.panel -->
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
            </div>

        </div>
        <!-- /#page-wrapper -->
        
    </div>
</form>
    <!-- /#wrapper -->

    <!-- jQuery Version 1.11.0 -->
    <script src="js/jquery-1.11.0.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="js/plugins/metisMenu/metisMenu.min.js"></script>

    <!-- Morris Charts JavaScript -->
    <script src="js/plugins/morris/raphael.min.js"></script>
    <script src="js/plugins/morris/morris.min.js"></script>
    <script src="js/plugins/morris/morris-data.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="js/sb-admin-2.js"></script>

</body>
</html>
