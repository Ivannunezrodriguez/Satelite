<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RioFiles.aspx.cs" Inherits="QRCode_Demo.RioFiles" MaintainScrollPositionOnPostback="true" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

    <head>

    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <title>Viveros Rio Eresma</title>

    <link href="css/Container.css" rel="stylesheet" />

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

            <style type="text/css">
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

            <style id="gridStyles" runat="server" type="text/css">
            body
            {
                font-family: Arial;
                font-size: 10pt;
            }
            table
            {
                /*border: 1px solid #ccc;*/
                 /*border: 1px solid #ccc;*/
                border-top: 0px solid #ccc;
                font-size: 10pt;
            }

            .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
                 border: 0px solid #ddd; 
            }

            table th
            {
                color: #0090CB;
                font-weight: bold;
            }
            table th, table td
            {
                padding: 5px;
               /* border: 1px solid #ccc;*/
            }
            table, table table td
            {
                /*border: 0px solid #ccc;*/
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

    </style>

        <script src="js/jquery-1.11.0.js"></script>
<%--        <script>
            var counter;
            function UploadFile() {
                var files = $("#<%=file1.ClientID%>").get(0).files;
                counter = 0;

                // Loop through files
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    var formdata = new FormData();
                    formdata.append("file1", file);
                    var ajax = new XMLHttpRequest();

                    ajax.upload.addEventListener("progress", progressHandler, false);
                    ajax.addEventListener("load", completeHandler, false);
                    ajax.addEventListener("error", errorHandler, false);
                    ajax.addEventListener("abort", abortHandler, false);
                    ajax.open("POST", "fileuploadhandler.ashx");
                    ajax.send(formdata);
                }
            }

            function progressHandler(event) {
                $("#loaded_n_total").html("Uploaded " + event.loaded + " bytes of " + event.total);
                var percent = (event.loaded / event.total) * 100;
                $("#progressBar").val(Math.round(percent));
                $("#status").html(Math.round(percent) + "% uploaded... please wait");
            }

            function completeHandler(event) {
                counter++
                $("#status").html(counter + " " + event.target.responseText);
            }

            function errorHandler(event) {
                $("#status").html("Upload Failed");
            }

            function abortHandler(event) {
                $("#status").html("Upload Aborted");
            }
        </script>--%>



</head>

<body>

     <form id="formCode" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  

         <div runat="server" id="DvPreparado" visible="false"  style="height: 20%; width: 30%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
            <button type="button" class="close" data-dismiss="alert">&times;</button> 
            <i id="I2" runat="server" class="fa fa-exclamation-circle fa-2x"></i>
            <asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="Lbmensaje" BorderStyle="None" border="0" Width="100%" Text=" El registro seleccionado se eliminará y las existencias volverán a Listas de Pedidos pendientes ¿Desea continuar?"  />
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


       

        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="Lberror"  Text="" > </asp:Label>

        <div id="page-wrapper"><!-- /#page-wrapper -->

            <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="Update">
                <ProgressTemplate>
                    <div class="centrado" style="z-index:1100;">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

 

  

            <div class="row">
                <div class="col-lg-10">
                <%--<h3> Generación de lotes automáticos <i class="fa fa-long-arrow-right"></i> <small> “Para generar lotes de forma automática e imprimir código QR”  </small></h3>--%>
                <h3 id="H3Titulo" runat="server" visible="true"> Servidor de ficheros <i class="fa fa-long-arrow-right"></i> <small> “Para generar rutas virtuales sobre ruta físicas”  </small></h3>
                <h3 id="H3Desarrollo" runat="server" style="color:red;" visible="false">(DESARROLLO) --> Ficheros RioEresma <i class="fa fa-long-arrow-right"></i> <small> “Para vincular posteriormente a Registros o bien, unificar la información electrónica diseminada” </small></h3>
                </div>
                <div id="dvPrinters" runat="server" style="text-align:right;" visible="true" class="col-lg-2" >
                <%--   <br />
                    <button id="BtPrinters" type="button" runat="server" class="text-muted"  onserverClick="btPrinter_Click"><i title="Selección otra Impresora"  class="fa fa-qrcode"></i></button>   --%>                                   
                    </div>
                <div id="dvDrlist" runat="server" style="text-align:right;" Visible="false" class="col-lg-2" >
                    <%--<br />
                        <asp:DropDownList runat="server"  CssClass="form-control"  Width="100%"  ID="DrPrinters" tooltip="Contiene las distintas impresoras configuradas" OnSelectedIndexChanged="DrPrinters_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />--%>
                        <%--<i title="Selección otra Impresora" class="fa fa-undo"></i>--%>
                </div>
            </div><!-- /.row -->

           
            <div class="row">
                <div class="col-lg-12">
                    <div class="bs-example">
                    <ul class="nav nav-tabs" style="margin-bottom: 15px;">
                        <li id="Menu0" class="active" runat="server" ><asp:LinkButton ID="aMenu0" runat="server" OnClick="HtmlAnchor_Click" >FICHEROS PUBLICADOS</asp:LinkButton></li>                        
                        <li id="Menu1" class="" runat="server" ><asp:LinkButton ID="aMenu1" runat="server" OnClick="HtmlAnchor_Click" >MIS FICHEROS CATALOGADOS</asp:LinkButton></li>
                        <li id="Menu2" class="" runat="server" ><asp:LinkButton ID="aMenu2" runat="server" OnClick="HtmlAnchor_Click" >SUBIR FICHEROS</asp:LinkButton></li>
                    </ul>
                </div>
                </div>
            </div>

           <div class="tab-pane fade active in" visible="true" runat="server" id="accordion2">

                <div class="row" id="BodyAll"  runat="server">
                <%--<div class="col-lg-5">--%>
                    <div class="panel panel-default">
                        <div class="panel-heading">                    
                            <h3 class="panel-title" id="H3PanelTitle" runat="server" ><i class="fa fa-long-arrow-right"></i>accordion0 Publicar ficheros
                        </h3>
                        </div>
                        <div class="panel-body" id="BodyLote" style="height:150px;" runat="server" >
                            <div class="col-sm-1" >                              
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label2"  Text="Categoria:" > </asp:Label>                                   
                            </div> 
                            <div class="col-sm-2" >                              
                            <asp:DropDownList ID="DrCategoria" CssClass="form-control" Width="100%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvLista_PageSize_Changed">  
                            <asp:ListItem Text="5" Value="5" />    
                            </asp:DropDownList>
                                   
                            </div>   
                            <div class="col-sm-1" >                              
                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label3"  Text="SubCategoria:" > </asp:Label>
                            </div> 
                            <div class="col-sm-2" >                              
                            <asp:DropDownList ID="DrSubCategoria" CssClass="form-control" Width="90%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvLista_PageSize_Changed">  
                            <asp:ListItem Text="5" Value="5" />    
                            </asp:DropDownList>                               
                            </div>  
                            <div class="col-sm-2" >  
                                <asp:Button ID="FiUpload" CssClass="btn btn-default" Visible="true"  runat="server" Width="90%" Text="Actualizar" OnClick="Files_SaveBtn_Click" />
                            </div>  
                            <div class="col-lg-4" >
                                <iframe style="width:100%; height:110px; border:none;" scrolling="no" sandbox="allow-same-origin allow-forms allow-scripts" src="importFiles.aspx"></iframe>
                                <asp:Label ID="LbRowLista" Style="color:olivedrab ;margin-top:-10px;text-align:right;" runat="server" Text=""></asp:Label>
                            </div>
      
                        </div>
                    </div>
                </div>

                <div class="row" id="PNGridLista" runat="server" visible="true">
                        <div class="panel panel-default">
                        <div class="panel-heading">                    
                            <h3 class="panel-title" id="H1" runat="server" ><i class="fa fa-long-arrow-right"></i>Listas de ficheros publicados
                        </h3>
                        </div>
                        <div class="panel-body" id="Div1" runat="server" >
                               
                        <div class="row" id="Div2" runat="server" visible="true">
                            <div class="col-sm-1" >                              
                                    <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label21"  Text="Filas:" > </asp:Label>
                                </div> 
                                <div class="col-sm-2" >                              
                                        <asp:DropDownList ID="ddListaPageSize" CssClass="form-control" Width="100%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvLista_PageSize_Changed">  
                                <asp:ListItem Text="5" Value="5" />    
                                </asp:DropDownList>
                                </div>                              
                        </div>

                        <div id="ContainDiv2" runat="server" style="overflow:auto;" class="panel-body"> <%-- AutoGenerateSelectButton="True"--%>                                  
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

                                        

                                        

                                    <asp:TemplateField HeaderText="Cabecera" SortExpression="ID_CABECERA">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="TabLCabecera" runat="server" Text='<%# Eval("ID_CABECERA") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="LabLCabecera" runat="server" Text='<%# Bind("ID_CABECERA") %>'></asp:Label>
                                        </ItemTemplate>
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
                                        
                                    <asp:TemplateField HeaderText="Subcategoria" SortExpression="SUBCATEGORIA">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="TabLRoot" runat="server" Text='<%# Eval("SUBCATEGORIA") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="LabLRoot" runat="server" Text='<%# Bind("SUBCATEGORIA") %>'></asp:Label>
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
                                                CommandName="SubeCarga" ToolTip="Devuelve la línea a órdenes de Carga" Width="30px" Height="30px"></asp:ImageButton>
                                            </ItemTemplate>
                                            <ItemStyle Height="8px"></ItemStyle>                                
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

            <div class="tab-pane fade active" visible="false" runat="server" id="accordion">

                <div class="row" id="Div3"  runat="server">
                <%--<div class="col-lg-5">--%>
                    <div class="panel panel-default">
                        <div class="panel-heading">                    
                            <h3 class="panel-title" id="H2" runat="server" ><i class="fa fa-long-arrow-right"></i>Ficheros catalogados
                        </h3>
                        </div>
                        <div class="panel-body" id="Div4" style="height:150px;" runat="server" >
                            <div class="row" id="Div15"  runat="server">
                                <div class="col-sm-1" >                              
                                    <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label1"  Text="Categoria:" > </asp:Label>                                   
                                </div> 
                                <div class="col-sm-2" >                              
                                <asp:DropDownList ID="DropDownList1" CssClass="form-control" Width="100%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvLista_PageSize_Changed">  
                                <asp:ListItem Text="5" Value="5" />    
                                </asp:DropDownList>
                                   
                                </div>   
                                <div class="col-sm-1" >                              
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label4"  Text="SubCategoria:" > </asp:Label>
                                </div> 
                                <div class="col-sm-2" >                              
                                <asp:DropDownList ID="DropDownList2" CssClass="form-control" Width="90%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvLista_PageSize_Changed">  
                                <asp:ListItem Text="5" Value="5" />    
                                </asp:DropDownList>                               
                                </div>  
                                <div class="col-sm-2" >  
                                    <asp:Button ID="Button1" CssClass="btn btn-default" Visible="true"  runat="server" Width="90%" Text="Actualizar" OnClick="Files_SaveBtn_Click" />
                                </div>  
                                <div class="col-lg-4" >
                                    <iframe style="width:100%; height:110px; border:none;" scrolling="no" sandbox="allow-same-origin allow-forms allow-scripts" src="importFiles.aspx"></iframe>
                                    <asp:Label ID="Label5" Style="color:olivedrab ;margin-top:-10px;text-align:right;" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" id="Div5" runat="server" visible="true">
                        <div class="panel panel-default">
                        <div class="panel-heading">                    
                            <h3 class="panel-title" id="H3" runat="server" ><i class="fa fa-long-arrow-right"></i>Listas de ficheros catalogados
                        </h3>
                        </div>
                        <div class="panel-body" id="Div6" runat="server" >
                               
                        <div class="row" id="Div7" runat="server" visible="true">
                            <div class="col-sm-1" >                              
                                    <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label6"  Text="Filas:" > </asp:Label>
                                </div> 
                                <div class="col-sm-2" >                              
                                        <asp:DropDownList ID="DropDownList3" CssClass="form-control" Width="100%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvLista_PageSize_Changed">  
                                <asp:ListItem Text="5" Value="5" />    
                                </asp:DropDownList>
                                </div>                              
                        </div>

                        <div id="Div8" runat="server" style="overflow:auto;" class="panel-body"> <%-- AutoGenerateSelectButton="True"--%>                                  
                            <asp:GridView ID="GridView1"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
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

                                        

                                        

                                    <asp:TemplateField HeaderText="Cabecera" SortExpression="ID_CABECERA">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="TabLCabecera" runat="server" Text='<%# Eval("ID_CABECERA") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="LabLCabecera" runat="server" Text='<%# Bind("ID_CABECERA") %>'></asp:Label>
                                        </ItemTemplate>
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
                                        
                                    <asp:TemplateField HeaderText="Subcategoria" SortExpression="SUBCATEGORIA">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="TabLRoot" runat="server" Text='<%# Eval("SUBCATEGORIA") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="LabLRoot" runat="server" Text='<%# Bind("SUBCATEGORIA") %>'></asp:Label>
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
                                                CommandName="SubeCarga" ToolTip="Devuelve la línea a órdenes de Carga" Width="30px" Height="30px"></asp:ImageButton>
                                            </ItemTemplate>
                                            <ItemStyle Height="8px"></ItemStyle>                                
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

            <div class="tab-pane fade active" visible="false" runat="server" id="accordion0">

                <div class="row" id="Div9"  runat="server">
                <%--<div class="col-lg-5">--%>
                    <div class="panel panel-default">
                        <div class="panel-heading">                    
                            <h3 class="panel-title" id="H4" runat="server" ><i class="fa fa-long-arrow-right"></i> accordion2 Mis ficheros publicados
                        </h3>
                        </div>
                        <div class="panel-body" id="Div10" style="height:150px;" runat="server" >
                            <div class="col-sm-1" >                              
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label7"  Text="Categoria:" > </asp:Label>                                   
                            </div> 
                            <div class="col-sm-2" >                              
                            <asp:DropDownList ID="DropDownList4" CssClass="form-control" Width="100%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvLista_PageSize_Changed">  
                            <asp:ListItem Text="5" Value="5" />    
                            </asp:DropDownList>
                                   
                            </div>   
                            <div class="col-sm-1" >                              
                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label8"  Text="SubCategoria:" > </asp:Label>
                            </div> 
                            <div class="col-sm-2" >                              
                            <asp:DropDownList ID="DropDownList5" CssClass="form-control" Width="90%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvLista_PageSize_Changed">  
                            <asp:ListItem Text="5" Value="5" />    
                            </asp:DropDownList>                               
                            </div>  
                            <div class="col-sm-2" >  
                                <asp:Button ID="Button2" CssClass="btn btn-default" Visible="true"  runat="server" Width="90%" Text="Actualizar" OnClick="Files_SaveBtn_Click" />
                            </div>  
                            <div class="col-lg-4" >
                                <iframe style="width:100%; height:110px; border:none;" scrolling="no" sandbox="allow-same-origin allow-forms allow-scripts" src="importFiles.aspx"></iframe>
                                <asp:Label ID="Label9" Style="color:olivedrab ;margin-top:-10px;text-align:right;" runat="server" Text=""></asp:Label>
                            </div>
      
                        </div>
                    </div>
                </div>

                <div class="row" id="Div11" runat="server" visible="true">
                        <div class="panel panel-default">
                        <div class="panel-heading">                    
                            <h3 class="panel-title" id="H5" runat="server" ><i class="fa fa-long-arrow-right"></i>Listas de ficheros publicados
                        </h3>
                        </div>
                        <div class="panel-body" id="Div12" runat="server" >
                               
                        <div class="row" id="Div13" runat="server" visible="true">
                            <div class="col-sm-1" >                              
                                    <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label10"  Text="Filas:" > </asp:Label>
                                </div> 
                                <div class="col-sm-2" >                              
                                        <asp:DropDownList ID="DropDownList6" CssClass="form-control" Width="100%"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvLista_PageSize_Changed">  
                                <asp:ListItem Text="5" Value="5" />    
                                </asp:DropDownList>
                                </div>                              
                        </div>

                        <div id="Div14" runat="server" style="overflow:auto;" class="panel-body"> <%-- AutoGenerateSelectButton="True"--%>                                  
                            <asp:GridView ID="GridView2"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
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

                                        

                                        

                                    <asp:TemplateField HeaderText="Cabecera" SortExpression="ID_CABECERA">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="TabLCabecera" runat="server" Text='<%# Eval("ID_CABECERA") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="LabLCabecera" runat="server" Text='<%# Bind("ID_CABECERA") %>'></asp:Label>
                                        </ItemTemplate>
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
                                        
                                    <asp:TemplateField HeaderText="Subcategoria" SortExpression="SUBCATEGORIA">
                                        <EditItemTemplate>
                                                <asp:TextBox ID="TabLRoot" runat="server" Text='<%# Eval("SUBCATEGORIA") %>' class="gridListAinput"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                                <asp:Label ID="LabLRoot" runat="server" Text='<%# Bind("SUBCATEGORIA") %>'></asp:Label>
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
                                                CommandName="SubeCarga" ToolTip="Devuelve la línea a órdenes de Carga" Width="30px" Height="30px"></asp:ImageButton>
                                            </ItemTemplate>
                                            <ItemStyle Height="8px"></ItemStyle>                                
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

           <%-- <div class="row"><!-- /.row -->
                <asp:Panel ID="pnlContents" runat="server"></asp:Panel>   
            </div><!-- /.row -->--%>
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
                function PrintPanel() {

                  <%-- if ($('#pnlContents').css('display') == 'none') {
                       // Acción si el elemento no es visible
                       var panel = document.getElementById("<%=pnlContentsQR.ClientID %>");
                   } else {
                       // Acción si el elemento es visible
                       var panel = document.getElementById("<%=pnlContents.ClientID %>");
                   }--%>


                function submitit() {
                    document.getElementById('btn').click();
                }

            </script>


</body>
</html>
