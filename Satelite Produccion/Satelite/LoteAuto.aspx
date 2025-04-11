<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoteAuto.aspx.cs" Inherits="Satelite.LoteAuto" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

    <head>

    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <title>Viveros Rio Eresma</title>

    <link href="css/Container.css" rel="stylesheet"  title="Estilo 1" />

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" title="Estilo 1" />
    <!-- MetisMenu CSS -->
    <link href="css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet" title="Estilo 1"/>
    <!-- Timeline CSS -->
    <link href="css/plugins/timeline.css" rel="stylesheet" title="Estilo 1"/>
    <!-- Custom CSS -->
    <link href="css/sb-admin-2.css" rel="stylesheet" title="Estilo 1"/>
    <!-- Morris Charts CSS -->
    <link href="css/plugins/morris.css" rel="stylesheet" title="Estilo 1"/>
    <!-- Custom Fonts -->
    <link href="font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" title="Estilo 1"/>

    <link href="css/Container.css" rel="alternate stylesheet"  title="Estilo 2" />

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

    </style>


</head>

<body>

    <div id="wrapper">
      <form id="formCode" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

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
        </div>

        
        <nav runat="server" id="Menus" visible="true" class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0"><!-- Navigation Menu Cabecera e izquierda-->
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="index.html">Viveros Rio Eresma</a>
                
            </div>

           <div class="navbar-header" style="text-align:center; width:60%;">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <h3 id="H3Titulo" runat="server" visible="true"> Generación de lotes automáticos <i class="fa fa-long-arrow-right"></i> <small> “Para generar lotes de forma automática e imprimir código QR”  </small></h3>
                <h3 id="H3Desarrollo" runat="server" style="color:red;" visible="false">(DESARROLLO) --> Generación de lotes automáticos <i class="fa fa-long-arrow-right"></i> <small> “Para generar lotes de forma automática e imprimir código QR” </small></h3>

                
            </div>
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                
                 <div id="dvPrinters" runat="server" style="text-align:right;" visible="true" >
                    <button id="BtPrinters" type="button" runat="server" class="text-muted" style=" border-style:none; background-color:transparent; padding-top:15px;"  onserverClick="btPrinter_Click"><i title="Selección otra Impresora"  class="fa fa-qrcode fa-2x"></i></button>                                      
                </div>
                <div id="dvDrlist" runat="server" style="text-align:right;padding-top:15px;" Visible="false" >
                   <asp:DropDownList runat="server"  CssClass="form-control"  Width="80%"  ID="DrPrinters" tooltip="Contiene las distintas impresoras configuradas" OnSelectedIndexChanged="DrPrinters_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                </div>
                
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


 
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>  

        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="Lberror"  Text="" > </asp:Label>
        <div id="page-wrapper"><!-- /#page-wrapper -->

            <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="Update">
                <ProgressTemplate>
                    <div class="centrado" style="z-index:1100;">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>



             <div class="row" id="CabeceraLotes" visible="true" runat="server">
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
                            <div class="panel-heading">
                            <h3 class="panel-title"> <i class="fa fa-long-arrow-right"></i> 
                            <asp:Label  type="text"  style="font-weight: bold; font-size:14px;"  runat="server" ID="lbtitleLote"  Text="Seleccionar código lote. Existen Duplicados:" > </asp:Label>
                            <asp:Label  type="text"  style=" font-weight: bold; font-size:14px;"  runat="server" ID="LbDuplicados"  Text="No" > </asp:Label>
                            <label runat="server" visible="true" tooltip="Muestra los enviados a GoldenSoft" id="LBCheck" class="switch pull-right">
                                      <input runat="server" onclick="submitit()" id="chkOnOff" data-toggle='tooltip' data-original-title='Muestra los enviados a GoldenSoft con Estado a 2' type="checkbox"/><span class="slider round"></span>
                                       <asp:Button ID="btn" runat="server" OnClick="checkListas_Click" Height="0" Width="0" CssClass="hidden" />
                                    </label>
                            </h3>                               
                            </div>
                           <div class="panel-body" id="BodyLotes" runat="server">
            
                               <div class="col-lg-12"  >
                                <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="lbBuscaCod"  Text="Códigos QR recibidos / finalizados:" > </asp:Label>
                                
                                   <asp:DropDownList runat="server" CssClass="form-control"  Width="100%"  tooltip="Contiene los código QR dados de alta y que aún no contienen registro desde formulario Scan-IT" ID="DrLotes"  OnSelectedIndexChanged="DrLotes_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                <br />
                               </div>

                                <div class="col-lg-12" >
                                <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server"  ID="Label2"  Text="Códigos QR creados / escaneados:" > </asp:Label>
                                
                                   <asp:DropDownList runat="server" CssClass="form-control"  Width="100%"  ID="DrScaneados" tooltip="Contiene los código QR pendientes y finalizados pero no importados" OnSelectedIndexChanged="DrScaneados_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                </div>
                               <div class="col-lg-6" > 
                                   
                                 </div>
                               <div class="col-lg-6" >
                                   
                                </div>
                             <%--  <div class="col-lg-12" >
                                   <asp:DropDownList runat="server" CssClass="form-control" Visible="true"  Width="100%"  ID="DrDuplicados"  OnSelectedIndexChanged="DrDuplicados_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                               </div>--%>
                            </div>
                        </div>
                        <%-- Fin Lotes creados y escaneados--%>


                        <div runat="server" id="alerta" visible="false" class="alert alert-info alert-dismissable">
                                    <button type="button" class="close" data-dismiss="alert">&times;</button> 
                                    <%--<a href="#" id="alerT" runat="server" class="alert alert-info alert-dismissable">--%>
                                        <i id="IAlert" runat="server" class="fa fa-exclamation-circle"></i>
                                        <asp:Label runat="server" class="alert alert-info alert-dismissable" ID="TextAlerta" BorderStyle="None" border="0" Width="100%" Text=""  />
                                    <%--</a> OnClientClick="return PrintPaletAlv();" --%> 
                                    <asp:Button runat="server" ID="btProcesa" tooltip="Busca si el Lote tiene registro de Formulario Scan-IT" visible="false" CssClass="btn btn-success btn-block" Width="100%"  Text="Procesar Lote" OnClick="btnPorcesa_Click"/>
                                    <asp:Button ID="btnPrint2" runat="server" tooltip="Lote con Formulario Scan-IT para imprimir con el total de su información" visible="false" CssClass="btn btn-success " Width="100%" Text="Imprimir código QR" OnClientClick="return PrintPanel();" />
                                    <asp:Button ID="btnPrintPaletAlv" runat="server" tooltip="Lote con Formulario Scan-IT para imprimir con el total de su información" visible="false" CssClass="btn btn-success " Width="100%" Text="Imprimir código QR" OnClick="PrintCurrentPage"   />



                        </div>
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

                        <div runat="server" id="alertaErr" visible="false" class="alert alert-danger alert-dismissable">
                                    <button type="button" class="close" data-dismiss="alert">&times;</button> 
                                    <%--<a href="#" id="alerTErr" runat="server" class="alert alert-danger alert-dismissable">--%>
                                    <i id="IAlertErr" runat="server" class="fa fa-exclamation-circle"></i>
                                    <asp:Label runat="server" class="alert alert-danger alert-dismissable" ID="TextAlertaErr" BorderStyle="None" border="0" Width="100%" Text=""  />
                                    <%--</a>--%>   
                                    <asp:Button runat="server" ID="btPorcesa" tooltip="Busca si el Lote tiene registro de Formulario Scan-IT" visible="false" CssClass="btn btn-success btn-block" Width="100%"  Text="Procesar Lote" OnClick="btnPorcesa_Click"/>
                         </div>
                       

                       <%-- <div class="panel-group" id="accordion">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Configuración Códigos QR</a>
                                    </h4>
                                </div>
                                <div id="collapseOne" class="panel-collapse collapse">
                                    <div class="panel-body">
                                       
                                        <div class="col-lg-6" >
                                         <label>Código QR Lote</label>
                                        <br />
                                        <label>Alto:</label><asp:TextBox ID="TxAlto" runat="server" Text="200"  CssClass="form-control"></asp:TextBox>
                                        <label>Ancho:</label><asp:TextBox ID="TxAncho" runat="server" Text="200"  CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-6" >
                                            <label>Código QR Total</label>
                                            <br />
                                            <label>Alto:</label><asp:TextBox ID="TxAltoT" runat="server" Text="150"  CssClass="form-control"></asp:TextBox>
                                            <label>Ancho:</label><asp:TextBox ID="TxAnchoT" runat="server" Text="150"  CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>         
                            </div>
                            <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">Configuración Tabla Formularios Scan-IT</a>
                                </h4>
                            </div>
                            <div id="collapseTwo" class="panel-collapse collapse">
                                <div class="panel-body">
                                    Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                                </div>
                            </div>
                            </div>
                        </div>--%>

                    </div>
                                    



                   

                    <div class="col-lg-7" >
                        <div class="panel panel-default" style="min-height:420px;">

                            <%--<div id="DivPrinters" runat="server" visible="true" class="panel-heading">
                                <h3 class="panel-title" runat="server" id="panelPrinter" style="color: black; font-weight:bold;"><i class="fa fa-long-arrow-right"></i> Seleccione un Lote                               
                                    <button id="btnPrintA1" type="button" runat="server" visible="false" class="pull-right text-muted "  onclick="return PrintPanel();"><i title="Imprime la vista previa presentada en pantalla" class="fa fa-print"></i></button>
                                </h3>                              
                            </div>    --%>
                            
                            <div id="H1Normal" runat="server" visible="true" class="panel-heading">
                                <h3 class="panel-title" style="color: black; font-weight:bold;"><i class="fa fa-long-arrow-right"></i> Seleccione un Lote    
                                    <%--<asp:DropDownList runat="server"  CssClass="form-control"  Width="20%"  ID="DrCodeQR" tooltip="Contiene las distintas APIS QR" OnSelectedIndexChanged="DrPrintQR_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />--%>
                                     <label runat="server" visible="true" tooltip="Selección de otra impresora QR" id="LbDrCodeQRA" class="switch pull-right">
                                      <input runat="server" onclick="submititA()" id="ChecQRA" data-toggle='tooltip' data-original-title='Selecciona distinta impresora QR' type="checkbox"/><span class="slider round"></span>
                                       <asp:Button ID="btnQRA" runat="server" OnClick="checkQR_Click" Height="0" Width="0" CssClass="hidden" />
                                    </label>
                                    <button id="btnPrintA1" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintPanel();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPanel()" class="fa fa-print fa-2x"></i></button>
                                    <button id="btnPrintB1" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintQR();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintQR()" class="fa fa-print fa-2x"></i></button>   
                                    <button id="btnPrintC1" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintFT();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintFT()" class="fa fa-print fa-2x"></i></button>
                                    <button id="btnPrintD1" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;" onserverClick="PrintCurrentPage"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPaletAlv()" class="fa fa-print fa-2x"></i></button>
                                </h3>                              
                            </div>    
                            <div id="H1Seleccion" runat="server" visible="false" class="panel-heading">
                                <h3 class="panel-title" style="color: black; font-weight:bold;"><i class="fa fa-long-arrow-right"></i> Lote con Código QR seleccionado
                                     <%--<asp:DropDownList runat="server"  CssClass="form-control"  Width="20%"  ID="DrCodeQR1" tooltip="Contiene las distintas APIS QR" OnSelectedIndexChanged="DrPrintQR_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />--%>
                                    <label runat="server" visible="true" tooltip="Selección de otra impresora QR" id="LbDrCodeQRB" class="switch pull-right">
                                      <input runat="server" onclick="submititB()" id="ChecQRB" data-toggle='tooltip' data-original-title='Selecciona distinta impresora QR' type="checkbox"/><span class="slider round"></span>
                                       <asp:Button ID="btnQRB" runat="server" OnClick="checkQR_Click" Height="0" Width="0" CssClass="hidden" />
                                    </label>
                                    <button id="btnPrintA2" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintPanel();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPanel()" class="fa fa-print fa-2x"></i></button>
                                    <button id="btnPrintB2" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintQR();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintQR()" class="fa fa-print fa-2x"></i></button>
                                    <button id="btnPrintC2" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintFT();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintFT()" class="fa fa-print fa-2x"></i></button>
                                    <button id="btnPrintD2" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onserverClick="PrintCurrentPage"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPaletAlv()" class="fa fa-print fa-2x"></i></button>
                                </h3>                              
                            </div>    
                             <div id="H1Red" runat="server" visible="false" class="panel-heading">
                                <h3 class="panel-title" style="color: red; font-weight:bold;"><i class="fa fa-long-arrow-right"></i> Código QR YA ASIGNADO, PENDIENTE PROCESAR
                                      <%--<asp:DropDownList runat="server"  CssClass="form-control"  Width="20%"  ID="DrCodeQR2" tooltip="Contiene las distintas APIS QR" OnSelectedIndexChanged="DrPrintQR_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />--%>
                                     <label runat="server" visible="true" tooltip="Selección de otra impresora QR" id="LbDrCodeQRC" class="switch pull-right">
                                      <input runat="server" onclick="submititC()" id="ChecQRC" data-toggle='tooltip' data-original-title='Selecciona distinta impresora QR' type="checkbox"/><span class="slider round"></span>
                                       <asp:Button ID="btnQRC" runat="server" OnClick="checkQR_Click" Height="0" Width="0" CssClass="hidden" />
                                    </label>
                                    <button id="btnPrintA3" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintPanel();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPanel()" class="fa fa-print fa-2x"></i></button>
                                    <button id="btnPrintB3" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintQR();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintQR()" class="fa fa-print fa-2x"></i></button>
                                    <button id="btnPrintC3" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintFT();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintFT()" class="fa fa-print fa-2x"></i></button>
                                    <button id="btnPrintD3" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;" onserverClick="PrintCurrentPage"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPaletAlv()" class="fa fa-print fa-2x"></i></button>
                                </h3>                              
                            </div>    
                             <div id="H1Green" runat="server" visible="false" class="panel-heading">
                                <h3 class="panel-title" style="color: LimeGreen; font-weight:bold;"><i class="fa fa-long-arrow-right"></i> Código QR PROCESADO
                                      <%--<asp:DropDownList runat="server"  CssClass="form-control"  Width="20%"  ID="DrCodeQR3" tooltip="Contiene las distintas APIS QR" OnSelectedIndexChanged="DrPrintQR_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />--%>
                                    <label runat="server" visible="true" tooltip="Selección de otra impresora QR" id="LbDrCodeQRD" class="switch pull-right">
                                      <input runat="server" onclick="submititD()" id="ChecQRD" data-toggle='tooltip' data-original-title='Selecciona distinta impresora QR' type="checkbox"/><span class="slider round"></span>
                                       <asp:Button ID="btnQRD" runat="server" OnClick="checkQR_Click" Height="0" Width="0" CssClass="hidden" />
                                    </label>
                                    <button id="btnPrintA4" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintPanel();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPanel()" class="fa fa-print fa-2x"></i></button>
                                    <button id="btnPrintB4" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintQR();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintQR()" class="fa fa-print fa-2x"></i></button>
                                    <button id="btnPrintC4" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;"  onclick="return PrintFT();"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintFT()" class="fa fa-print fa-2x"></i></button>
                                    <button id="btnPrintD4" type="button" runat="server" visible="false" class="pull-right text-muted " style=" border-style:none; background-color:transparent;" onserverClick="PrintCurrentPage"><i style="margin-top:-8px;" title="Imprime la vista previa presentada en pantalla PrintPaletAlv()" class="fa fa-print fa-2x"></i></button>
                                </h3>                              
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
                                            <div class="col-lg-4"  alig="left">
                                                
                                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:48px;"  runat="server" ID="LbCodeQRPalteAlv"  Text="SIN CÓDIGO LOTE" > </asp:Label>        
                                            </div>
                                            <div class="col-lg-8" alig="left">

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
                                <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:12px;"  runat="server" ID="LBReadQR"  Text="" > </asp:Label> 
                           </div> 
                      </div>
                 </div> 

            <div class="row" id="Div1" runat="server">
                              <%-- Ficha Campos--%>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                            <h3 class="panel-title"><i class="fa fa-long-arrow-right"></i>
                                <asp:Label  type="text" Width="70%" visible="true" style=" font-weight: bold;"  runat="server" ID="Label12"  Text="Formulario de entrada de datos desde Móvil" > </asp:Label>
                            </h3>                              
                            </div>
                           <div class="panel-body" id="BodyCampos" runat="server">
            
                              <div class="col-lg-6"  >
                                  <div class="col-lg-3"  >
                                     <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="LbForm"  Text="Tipo Formulario:" > </asp:Label>
                                  </div>
                                   <div class="col-lg-8"  >
                                        <asp:TextBox ID="TxtForm" Enabled="false" style="text-align:center; font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                  </div>
                                 <div class="col-lg-3"  >
                                    <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label5"  Text="Fecha:" > </asp:Label>
                                 </div>
                                  <div class="col-lg-8"  >
                                    <asp:TextBox ID="TxtFecha"  Enabled="false" style="text-align:center;  font-weight: bold;" tooltip="El formato requerido para la Fecha: dd-MM-yyyy"  Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                 </div>
                                 <div class="col-lg-3"  >
                                       <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label4"  Text="Tipo Planta:" > </asp:Label>
                                </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtCampo"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label6"  Text="Variedad:" > </asp:Label>
                                </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtVariedad"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label3"  Text="Lote Destino:" > </asp:Label>
                                 </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtLoteDestino"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label7"  Text="Unidades:" > </asp:Label>
                                </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtCajas"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="LbnumeroPlantas"  Text="Número de Unidades:" > </asp:Label>
                                 </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtPlantas"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                 </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text" Enabled="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="LbManojo"  Text="Número Manojos:" > </asp:Label>
                                </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtManojos" Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>  
                                </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="LbDesde"  Text="Movimiento Desde:" > </asp:Label>
                                </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtDesde" Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="LbHasta"  Text="Movimiento Hasta:" > </asp:Label>
                                </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtHasta"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                               </div>
                            </div>
                              <div class="col-lg-6"  >
                                  <div class="col-lg-3"  >
                                    <asp:Label  type="text"   Width="100%" style=" font-weight: bold;"  runat="server" ID="LbETDesde"  Text="Etiqueta Desde:" > </asp:Label>
                                  </div>
                                  <div class="col-lg-8"  >
                                    <asp:TextBox ID="TxtETDesde"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="LbETHasta"  Text="Etiqueta Hasta:" > </asp:Label>
                                </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtETHasta"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text"   Width="100%" style=" font-weight: bold;"  runat="server" ID="LbTuneles"  Text="Tuneles:" > </asp:Label>
                                 </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtTuneles"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text"   Width="100%" style=" font-weight: bold;"  runat="server" ID="LbPasillos"  Text="Pasillos:" > </asp:Label>
                                </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtPasillos"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text"   Width="100%" style=" font-weight: bold;"  runat="server" ID="LbObservaciones"  Text="Observaciones:" > </asp:Label>
                                </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtObservaciones"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                 </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text"   Width="100%" style=" font-weight: bold;"  runat="server" ID="LbOK"  Text="OK:" > </asp:Label>
                                 </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtOK"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text"   Width="100%" style=" font-weight: bold;"  runat="server" ID="Label8"  Text="Estado:" > </asp:Label>
                                </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtEstado"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                 </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text"   Width="100%" style=" font-weight: bold;"  runat="server" ID="Label11"  Text="Dispositivo Móvil:" > </asp:Label>
                                </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtDispositivo"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                <%--Ocultos  font-size:36px;--%> 
                                </div>
                                 <div class="col-lg-3"  >
                                      <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="LbID"  Text="Identificador:" > </asp:Label>
                                </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TxtID"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                                 </div>
                                  <div class="col-lg-3"  >
                                      <asp:Label  type="text"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Label13"  Text="Nuevo Campo:" > </asp:Label>
                                </div>
                                  <div class="col-lg-8"  >
                                     <asp:TextBox ID="TextBox1"  Enabled="false" style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
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
            <!-- /#fin row -->

           <%-- <div class="row"><!-- /.row -->
                <asp:Panel ID="pnlContents" runat="server"></asp:Panel>   
            </div><!-- /.row -->--%>
        </div><!-- /#page-wrapper -->
        
        </form>
    </div>

    <!-- /#wrapper -->

    <script src="js/jquery-1.11.0.js"></script>

    <script src="js/bootstrap.min.js"></script>

    <script src="js/plugins/metisMenu/metisMenu.min.js"></script>

    <script src="js/sb-admin-2.js"></script>

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
 
</body>
</html>
