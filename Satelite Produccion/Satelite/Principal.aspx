<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="Satelite.Principal" %>

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


</head>

<body>
<form id="formCode" runat="server">  
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                <li>
                                    <a href="BuscaArchivo.aspx"> Consultas Generales</a>
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
        </nav>
        <!-- Navigation -->




        <div id="page-wrapper">

           <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="Update">
                <ProgressTemplate>
                    <div class="centrado" style="z-index:1100;">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>


            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header"> Panel de control principal</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-3 col-md-6">
                    <div class="panel panel-primary" style="position: absolute; width:92%; z-index: 3;">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="fa fa-comments fa-5x"></i>
                                </div>
                                <div class="col-xs-9 text-right">
                                    <div id="T1" runat="server" class="huge">44</div>
                                    <div>Pendientes de Importación</div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <a data-toggle="collapse" style="padding:10px;" data-parent="#accordion" href="#collapseImportacion"><span class="pull-right"><i class="fa fa-arrow-circle-right"> &nbsp;&nbsp;</i></span>Ver Detalles</a>                     
                        <div class="panel-footer panel-collapse collapse" id="collapseImportacion">
                            <u>Tipo de formularios recien introducidos:</u><i id="Rpt0" runat="server"></i>
                            <br/>
                            <br/>                           
                            <asp:Repeater id="RpImportacion0" runat="server">
                                <itemtemplate>
                                    <b>
                                       <i class="fa fa-circle">&nbsp;&nbsp;</i><%# DataBinder.Eval(Container.DataItem, "TIPO_FORM") %> contiene <%# DataBinder.Eval(Container.DataItem, "CUANTOS") %> Lotes
                                    </b>
                                    <br>
                                </itemtemplate>
                              
                            </asp:Repeater>
                            <br/>
                            <br/>
                            <u>Tipo de formularios por finalizar:</u><i id="Rpt1" runat="server"></i>
                            <br/>
                            <br/>                      
                            <asp:Repeater id="RpImportacion1" runat="server">
                                <itemtemplate>
                                    <b>
                                       <i class="fa fa-circle">&nbsp;&nbsp;</i><%# DataBinder.Eval(Container.DataItem, "TIPO_FORM") %> contiene <%# DataBinder.Eval(Container.DataItem, "CUANTOS") %> Lotes
                                    </b>
                                    <br>
                                </itemtemplate>
                              
                            </asp:Repeater>
                            <br/>
                            <br/>
                            <u>Tipo de formularios procesados:</u><i id="Rpt2" runat="server"></i>
                            <br/>
                            <br/>                   
                            <asp:Repeater id="RpImportacion2" runat="server">
                                <itemtemplate>
                                    <b>
                                       <i class="fa fa-circle">&nbsp;&nbsp;</i><%# DataBinder.Eval(Container.DataItem, "TIPO_FORM") %> contiene <%# DataBinder.Eval(Container.DataItem, "CUANTOS") %> Lotes
                                    </b>
                                    <br>
                                </itemtemplate>
                              
                            </asp:Repeater>                            
                            <%--<span class="pull-left">Ver Detalles</span>
                            <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>--%>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>



                   <div class="col-lg-3 col-md-6">
                    <div class="panel panel-green" style="position: absolute; width:92%; z-index: 3;">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="fa fa-tasks fa-5x"></i>
                                </div>
                                <div class="col-xs-9 text-right">
                                    <div id="T2" runat="server" class="huge">44</div>
                                    <div>Órdenes de Carga</div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <a data-toggle="collapse" style="padding:10px;" data-parent="#accordion" href="#collapseOrden"><span class="pull-right"><i class="fa fa-arrow-circle-right"> &nbsp;&nbsp;</i></span>Ver Detalles</a>                     
                        <div class="panel-footer panel-collapse collapse" id="collapseOrden">
                            <u>Órdenes nuevas:</u><i id="Io1" runat="server"></i>
                            <br/>
                            <br/>                           
                            <asp:Repeater id="RpTOrden0" runat="server">
                                <itemtemplate>
                                    <b>
                                       <i class="fa fa-circle">&nbsp;&nbsp;</i> <%# DataBinder.Eval(Container.DataItem, "CUANTOS") %> Órdenes de Carga nuevas
                                    </b>
                                    <br>
                                </itemtemplate>
                              
                            </asp:Repeater>
                            <br/>
                            <br/>
                            <u>Órdenes abiertas:</u><i id="Io2" runat="server"></i>
                            <br/>
                            <br/>                      
                            <asp:Repeater id="RpTOrden1" runat="server">
                                <itemtemplate>
                                    <b>
                                       <i class="fa fa-circle">&nbsp;&nbsp;</i> <%# DataBinder.Eval(Container.DataItem, "CUANTOS") %> Órdenes de Carga abiertas
                                    </b>
                                    <br>
                                </itemtemplate>
                              
                            </asp:Repeater>
                            <br/>
                            <br/>
                            <u>Órdenes cerradas:</u><i id="Io3" runat="server"></i>
                            <br/>
                            <br/>                   
                            <asp:Repeater id="RpTOrden2" runat="server">
                                <itemtemplate>
                                    <b>
                                       <i class="fa fa-circle">&nbsp;&nbsp;</i>  <%# DataBinder.Eval(Container.DataItem, "CUANTOS") %> Órdenes de Carga cerradas
                                    </b>
                                    <br>
                                </itemtemplate>
                              
                            </asp:Repeater>                            
                            <%--<span class="pull-left">Ver Detalles</span>
                            <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>--%>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>




           <%--     <div class="col-lg-3 col-md-6">
                    <div class="panel panel-green">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="fa fa-tasks fa-5x"></i>
                                </div>
                                <div class="col-xs-9 text-right">
                                    <div class="huge">12</div>
                                    <div>Órdenes de Carga</div>
                                </div>
                            </div>
                        </div>
                        <a href="#">
                            <div class="panel-footer">
                                <span class="pull-left">Ver Detalles</span>
                                <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                <div class="clearfix"></div>
                            </div>
                        </a>
                    </div>
                </div>--%>
                <div class="col-lg-3 col-md-6">
                    <div class="panel panel-yellow">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="fa fa-comments fa-5x"></i>
                                </div>
                                <div class="col-xs-9 text-right">
                                    <div class="huge">124</div>
                                    <div>Tareas Codigos QR</div>
                                </div>
                            </div>
                        </div>
                        <a href="#">
                            <div class="panel-footer">
                                <span class="pull-left">Ver Detalles</span>
                                <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                <div class="clearfix"></div>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="panel panel-red">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="fa fa-support fa-5x"></i>
                                </div>
                                <div class="col-xs-9 text-right">
                                    <div class="huge">21</div>
                                    <div>Tareas Lotes/Palets</div>
                                </div>
                            </div>
                        </div>
                        <a href="#">
                            <div class="panel-footer">
                                <span class="pull-left">Ver Detalles</span>
                                <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                <div class="clearfix"></div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-8">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Producción anual
                            <div class="pull-right">
                                <div class="btn-group">
                                    <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                                        Acción
                                        <span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu pull-right" role="menu">
                                        <li><a href="#">Agrupados por cultivo</a>
                                        </li>
                                        <li><a href="#">Agrupados por Almacen</a>
                                        </li>
                                        <li><a href="#">Agrupado por plantas</a>
                                        </li>
                                        <li class="divider"></li>
                                        <li><a href="#">Agrupado en barras</a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div id="morris-area-chart"></div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Productividad mensual
                            <div class="pull-right">
                                <div class="btn-group">
                                    <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                                        Acciones
                                        <span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu pull-right" role="menu">
                                        <li><a href="#">Agrupados por cultivo</a>
                                        </li>
                                        <li><a href="#">Agrupados por Almacen</a>
                                        </li>
                                        <li><a href="#">Agrupado por plantas</a>
                                        </li>
                                        <li class="divider"></li>
                                        <li><a href="#">Agrupado en barras</a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-hover table-striped">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Fecha</th>
                                                    <th>Hora</th>
                                                    <th>Acumulado</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>3326</td>
                                                    <td>10/21/2013</td>
                                                    <td>3:29 PM</td>
                                                    <td>321.33</td>
                                                </tr>
                                                <tr>
                                                    <td>3325</td>
                                                    <td>10/21/2013</td>
                                                    <td>3:20 PM</td>
                                                    <td>234.34</td>
                                                </tr>
                                                <tr>
                                                    <td>3324</td>
                                                    <td>10/21/2013</td>
                                                    <td>3:03 PM</td>
                                                    <td>724.17</td>
                                                </tr>
                                                <tr>
                                                    <td>3323</td>
                                                    <td>10/21/2013</td>
                                                    <td>3:00 PM</td>
                                                    <td>23.71</td>
                                                </tr>
                                                <tr>
                                                    <td>3322</td>
                                                    <td>10/21/2013</td>
                                                    <td>2:49 PM</td>
                                                    <td>8345.23</td>
                                                </tr>
                                                <tr>
                                                    <td>3321</td>
                                                    <td>10/21/2013</td>
                                                    <td>2:23 PM</td>
                                                    <td>245.12</td>
                                                </tr>
                                                <tr>
                                                    <td>3320</td>
                                                    <td>10/21/2013</td>
                                                    <td>2:15 PM</td>
                                                    <td>5663.54</td>
                                                </tr>
                                                <tr>
                                                    <td>3319</td>
                                                    <td>10/21/2013</td>
                                                    <td>2:13 PM</td>
                                                    <td>943.45</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <!-- /.table-responsive -->
                                </div>
                                <!-- /.col-lg-4 (nested) -->
                                <div class="col-lg-8">
                                    <div id="morris-bar-chart"></div>
                                </div>
                                <!-- /.col-lg-8 (nested) -->
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- /.panel-body -->
                    </div>
                </div>
                <!-- /.col-lg-8 -->
                <div class="col-lg-4">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bell fa-fw"></i> Panel notificaciones
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="list-group">
                                <a href="#" class="list-group-item">
                                    <i class="fa fa-comment fa-fw"></i> Importación para GoldenSoft
                                    <span class="pull-right text-muted small"><em>Hace 4 minutos</em>
                                    </span>
                                </a>
                                <a href="#" class="list-group-item">
                                    <i class="fa fa-twitter fa-fw"></i> Preparación fichajes Recodat
                                    <span class="pull-right text-muted small"><em>Hace 12 minutos</em>
                                    </span>
                                </a>
                                <a href="#" class="list-group-item">
                                    <i class="fa fa-envelope fa-fw"></i> Proceso Paletizado
                                    <span class="pull-right text-muted small"><em>Hace 27 minutos</em>
                                    </span>
                                </a>
                                <a href="#" class="list-group-item">
                                    <i class="fa fa-tasks fa-fw"></i> Proceso etiquetado fitosanitario
                                    <span class="pull-right text-muted small"><em>Hace 43 minutos</em>
                                    </span>
                                </a>
                                <a href="#" class="list-group-item">
                                    <i class="fa fa-upload fa-fw"></i> Usuarios con acceso
                                    <span class="pull-right text-muted small"><em>11:32 AM</em>
                                    </span>
                                </a>
                                <a href="#" class="list-group-item">
                                    <i class="fa fa-bolt fa-fw"></i> Usuarios en sesión
                                    <span class="pull-right text-muted small"><em>11:13 AM</em>
                                    </span>
                                </a>
                                <a href="#" class="list-group-item">
                                    <i class="fa fa-bolt fa-fw"></i> Usuarios fuera de sesión
                                    <span class="pull-right text-muted small"><em>23:45 AM</em>
                                    </span>
                                </a>
                            </div>
                            <!-- /.list-group -->
                            <a href="#" class="btn btn-default btn-block">Ver todas las alertas</a>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Usuarios operativos
                        </div>
                        <div class="panel-body">
                            <div id="morris-donut-chart"></div>
                            <a href="#" class="btn btn-default btn-block">Ver detalles</a>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                </div>
                <!-- /.col-lg-4 -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /#page-wrapper -->

    </div>
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
</form>
</body>
</html>
