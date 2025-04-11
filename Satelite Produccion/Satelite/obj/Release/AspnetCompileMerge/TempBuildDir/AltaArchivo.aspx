<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaArchivo.aspx.cs" Inherits="QRCode_Demo.AltaArchivo" %>

<%--<%@ Register TagPrefix="MsgBox" Src="UCMessageBox.ascx" TagName="UCMessageBox" %>--%>
<!DOCTYPE html>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Viveros Rio Eresma - Archivos</title>

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
    
    <%--nueva seccion--%>
    <script src="js/jquery.js"></script>
    <script src="js/jquery-ui-1.8rc3.custom.min.js"></script>
            <script>
        
        $(function () {

            //Array para dar formato en español
            $.datepicker.regional['es'] =
            {
                closeText: 'Cerrar',
                prevText: 'Previo',
                nextText: 'Próximo',

                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
                'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun',
                'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                monthStatus: 'Ver otro mes', yearStatus: 'Ver otro año',
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sáb'],
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
                dateFormat: 'dd/mm/yy', firstDay: 0,
                initStatus: 'Selecciona la fecha', isRTL: false
            };
            $.datepicker.setDefaults($.datepicker.regional['es']);

            $("#txtFechaAlta").datepicker({
                dateFormat: 'dd/mm/yy', showOn: 'button', buttonImage: 'images/Calendar_scheduleHS.png', buttonImageOnly: true, changeMonth: true,
                changeYear: true, gotoCurrent: true
            });

             $("#txtFechaBaja").datepicker({
                dateFormat: 'dd/mm/yy', showOn: 'button', buttonImage: 'images/Calendar_scheduleHS.png', buttonImageOnly: true, changeMonth: true,
                changeYear: true, gotoCurrent: true
            });

            $("#TxtFechaTemporal").datepicker({
                dateFormat: 'dd/mm/yy', showOn: 'button', buttonImage: 'images/Calendar_scheduleHS.png', buttonImageOnly: true, changeMonth: true,
                changeYear: true, gotoCurrent: true
            });
        });


    </script>


    <style type="text/css">
        body {
            BACKGROUND-COLOR: #fff
        }

        .auto-style1 {
            height: 26px;
        }

    </style>
</head>
<body>

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
                            <!-- /.nav-second-level -->
                        </li>
                        <li>
                            <a class="active" href="#"><i class="fa fa-bar-chart-o fa-fw"></i> Estructura Documental<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level in">
                                <li>
                                    <a class="active" href="AltaArchivo.aspx"> Archivos</a>
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







     <form id="form1"  runat="server" >
          <div id="page-wrapper"><!-- /#page-wrapper -->
                <div class="row">
                  <div class="col-lg-12">
                    <h3 id="H3Titulo" runat="server" visible="true"> Relación de Archivos documentales <i class="fa fa-long-arrow-right"></i> <small> “Crea, vincula o desvincula Tablas y columnas o campos relacionales”  </small></h3>
                    <h3 id="H3Desarrollo" runat="server"  style="color:red;" visible="false">(DESARROLLO) --> Relación de Archivos documentales <i class="fa fa-long-arrow-right"></i> <small> “Crea, vincula o desvincula Tablas y columnas o campos relacionales”  </small></h3>
                  </div>
                </div><!-- /.row -->

                <div class="row">
                  <div class="col-lg-5 col-md-6" >
                        <div class="panel panel-default">
                            <div class="panel-heading">
                               Campos disponibles
                            </div>
                           <div class="panel-body">
                              <asp:ListBox ID="ListBox1" style="height:300px;" CssClass="form-control btn-block" runat="server" SelectionMode="Multiple"></asp:ListBox>

                          </div>
                        </div>
                    </div>

                    <div class="col-lg-2 col-md-6" >
                            <div class="panel panel-default">
                                <div class="panel-heading" id="LbIDArchivo" runat="server">
                                    Relaciones 
                                </div>
                                <div class="panel-body">
                                    <%--<asp:Label  type="text" Width="100%" style="text-align:center; font-weight: bold;"  runat="server" ID="LbIDArchivo"  Text="Archivo:" > </asp:Label>--%>
                              <asp:Button ID="Button1" CssClass="btn btn-success btn-block"  runat="server" Text="Asignar Campos >>" OnClick="btnPasarSeleccionados_Click" />
                              <asp:Button ID="Button2"  CssClass="btn btn-success btn-block" runat="server" Text="<< Desasignar Campos" OnClick="btnRegresarSeleccionados_Click" />
                              <%--<asp:Button ID="Button3"  CssClass="btn btn-success btn-block" runat="server" Text="Eliminar Campos" OnClick="btnEliminarSeleccionados_Click" />--%>

                              <asp:Button ID="Button4" CssClass="btn btn-success btn-block" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
                              <asp:Button ID="Button5" CssClass="btn btn-success btn-block"  runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                    <br />
                              <asp:TextBox ID="TextBox1" Width="100%" CssClass="form-control" runat="server">Buscar...</asp:TextBox>    
                                    <br />
                              <asp:Button ID="Button6" CssClass="btn btn-success btn-block"   runat="server" Text="Subir" OnClick="Subir_Click" />
                              <asp:Button ID="Button7"  CssClass="btn btn-success btn-block" runat="server"  Text="Bajar" OnClick="Bajar_Click" />
                            </div>
                               </div>
                    </div>
                    <div class="col-lg-5 col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                         Campos seleccionados
                                </div>
                            <div class="panel-body">
                              <asp:ListBox ID="ListBox2" style="height:300px;" CssClass="form-control" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                    </div>

                    <%--</div>--%>
                    </div >

                    <div class="row">
                         <div class="col-lg-12">
                               <div class="col-lg-2">
                                  <asp:Label  Font-Bold="true" type="text" runat="server" ID="Label10" Text="Todos los Archivos:"> </asp:Label>
                                </div>
                               <div class="col-lg-3">
                                   <asp:DropDownList runat="server" CssClass="form-control"  ID="DrArchivos"  OnSelectedIndexChanged="DrArchivos_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                </div>
                                 <div class="col-lg-2">
                                 </div>
                               <div class="col-lg-2">
                                   <asp:Label  Font-Bold="true" type="text" runat="server" ID="Label12"   Text="Campos asignados a este Archivo:"> </asp:Label>
                               </div>
                               <div class="col-lg-3">
                                   <asp:DropDownList runat="server" CssClass="form-control" ID="DrCampoasig" OnSelectedIndexChanged="DrCampoasig_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                </div>
                            </div>
                    </div>
                     <br />


                    <%--Campos--%>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:Label CssClass="cajaslabel" type="text" runat="server" ID="LblIDArchivo" Width="100%"   Text="Nombre:"> </asp:Label>
                                    <asp:TextBox type="text" CssClass="form-control" runat="server" ID="TxtNombre" Width="100%"  Height="35px"  ></asp:TextBox>
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="LblIdentificacion" Width="100%"   Text="Descripción:"> </asp:Label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="TxtDescripcion" Width="100%"  ></asp:TextBox>
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label8"  Width="100%" Text="Nombre Tabla:"> </asp:Label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="TablaName" Width="100%" Height="35px" ></asp:TextBox>
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label9"  Width="100%"  Text="Tabla Objetos:"> </asp:Label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="TablaObj"  Width="100%"   Height="35px"></asp:TextBox>
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label1"  Width="100%"  Text="Conexión:"> </asp:Label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="TextConexion"  Width="100%"   Height="35px"></asp:TextBox>
                                    </div>
                            </div>
                         </div>
                           <div class="col-lg-6">
                            <div class="panel panel-default">

                                <div class="panel-body">                        
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label6"  Width="100%" Text="Nivel seguridad:"> </asp:Label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="dlNivel" Width="100%"  OnSelectedIndexChanged="dlNivel_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="LblResponsable" Width="100%"  Text="Dependiente de:"> </asp:Label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="Djerarquia" Width="100%"  OnSelectedIndexChanged="Djerarquia_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />   
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label7" Width="100%"  Text="Estado:"> </asp:Label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="dlEstado" Width="100%"  OnSelectedIndexChanged="dlEstado_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label5" Width="100%"   Text="Tipo de elemento:"> </asp:Label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="Dtipo" Width="100%"  OnSelectedIndexChanged="Dtipo_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" />   
                                    <asp:Label CssClass="cajaslabel" runat="server" ID="Label2"  Width="100%"  Text="Número registros:"> </asp:Label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="TextBox3"  Width="100%"   Height="35px"></asp:TextBox>
                                </div>
                                </div>
                         </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                                <div class="panel-body">
                          
                                        <div class="col-lg-1">
                                        </div>
                                    <div class="col-lg-2">
                                            <asp:Button runat="server" ID="btnNuevo"  CssClass="btn btn-success btn-block" Width="100%"  Text="Nuevo" OnClick="btnNuevo_Click"/>
                                        </div>
                                        <div class="col-lg-2">
                                        <asp:Button runat="server" ID="btnEditar"  CssClass="btn btn-success btn-block"  Width="100%" Text="Editar" OnClick="btnEditar_Click"/>
                                        </div>
                                        <div class="col-lg-2">
                                          </div>
                                        <div class="col-lg-2">
                                        <asp:Button runat="server" ID="btnGuardar"  CssClass="btn btn-success btn-block" Width="100%" Text="Guardar" OnClick="btnGuardar_Click"/>
                                        </div>
                                        <div class="col-lg-2">
                                         <asp:Button runat="server" ID="btnCancelar"  CssClass="btn btn-success btn-block" Width="100%" Text="Cancelar" OnClick="btnCancelar_Click"/>
                                        </div>
                                    <div class="col-lg-1">
                                    </div>
                                </div>
                        </div> 
                    </div>
            </div>

        </form>
    </div>
</body>
</html>
