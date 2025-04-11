<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportTabla.aspx.cs" Inherits="QRCode_Demo.ImportTabla" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

    <head>

    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>

    <title>Viveros Rio Eresma</title>

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
                            <a class="active" href="#"><i class="fa fa-bar-chart-o fa-fw"></i> Importaciones<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level in">
                                <li>
                                    <a class="active" href="ImportTabla.aspx">Desde Tablas</a>
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
        <form id="form1" runat="server">
        <div id="page-wrapper">
  
              <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Importación desde la Tabla seleccionada:  <asp:DropDownList runat="server" class="form-control" ID="DrArchivos" Width="330px" Height="30px" AppendDataBoundItems="true" AutoPostBack="True"  OnSelectedIndexChanged="DrCategoria_SelectedIndexChanged" />
                        <asp:ListBox ID="LBcampos" style="height:345px;" CssClass="form-control" Visible="false" runat="server" SelectionMode="Multiple"></asp:ListBox>
                        </div>
                        <!-- .panel-heading -->
                        <div class="panel-body">
                            <div class="panel-group" id="accordion">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Desde Ficha</a>
                                        </h4>
                                    </div>
                                    <div id="collapseOne" class="panel-collapse collapse in">
                                        <div class="panel-body">


                                            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">

                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <!-- <div class="table-responsive">-->
                                




                            <div id="dataTables-example_wrapper" class="dataTables_wrapper form-inline" role="grid">
								<div class="row">
									<div class="col-sm-6">
										<div class="dataTables_length" id="dataTables-example_length">
											<label>
											<select name="dataTables-example_length" aria-controls="dataTables-example" class="form-control input-sm">
											<option value="10" selected="selected">10</option>
											<option value="25">25</option><option value="50">50</option>
											<option value="100">100</option>
											</select> Resultados por página
											</label>
										</div>
									</div>
									<div class="col-sm-6">
                                        <%--<div class="input-group custom-search-form">--%>
                                        <%--<input type="text" class="form-control" placeholder="Search..." />--%>
                                            <asp:TextBox id="TxtBusca" runat="server" class="form-control" placeholder="Texto a buscar..." Width="80%"></asp:TextBox>
                                            <%--<span class="input-group-btn">--%>
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-search"></i>
                                            </button>
                                            <%--</span>--%>
                                        <%--</div>--%>


	<%--									<div id="dataTables-example_filter" class="dataTables_filter">
										<label>Buscar:</label>
                                        <asp:TextBox id="TxtBusca" runat="server" class="form-control" placeholder="Texto a buscar..." Width="80%"></asp:TextBox>
										
										</div>--%>
									</div>
								</div>









                                <table class="table table-striped table-bordered table-hover dataTable no-footer" id="dataTables-example" aria-describedby="dataTables-example_info">
                                    <thead>
                                        <tr role="row"><th class="sorting_asc" tabindex="0" aria-controls="dataTables-example" rowspan="1" colspan="1" style="width: 299px;" 
											aria-sort="ascending" aria-label="Rendering engine: activate to sort column ascending">ID</th>
											<th class="sorting" tabindex="0" aria-controls="dataTables-example" rowspan="1" colspan="1" style="width: 376px;" aria-label="Browser: activate to sort column ascending">
											ALMACEN</th>
											<th class="sorting" tabindex="0" aria-controls="dataTables-example" rowspan="1" colspan="1" style="width: 346px;" aria-label="Platform(s): activate to sort column ascending">
											LOTES</th>
											<th class="sorting" tabindex="0" aria-controls="dataTables-example" rowspan="1" colspan="1" style="width: 259px;" aria-label="Engine version: activate to sort column ascending">
											FECHALOTE</th>
											<th class="sorting" tabindex="0" aria-controls="dataTables-example" rowspan="1" colspan="1" style="width: 190px;" aria-label="CSS grade: activate to sort column ascending">
											CULTIVO</th>
										</tr>
                                    </thead>
                                    <tbody>
                                        <tr class="odd gradeX">
                                            <td>Trident</td>
                                            <td>Internet Explorer 4.0</td>
                                            <td>Win 95+</td>
                                            <td class="center">4</td>
                                            <td class="center">X</td>
                                        </tr>
                                        <tr class="even gradeC">
                                            <td>Trident</td>
                                            <td>Internet Explorer 5.0</td>
                                            <td>Win 95+</td>
                                            <td class="center">5</td>
                                            <td class="center">C</td>
                                        </tr>
                                        <tr class="odd gradeA">
                                            <td>Trident</td>
                                            <td>Internet Explorer 5.5</td>
                                            <td>Win 95+</td>
                                            <td class="center">5.5</td>
                                            <td class="center">A</td>
                                        </tr>
                                        <tr class="even gradeA">
                                            <td>Trident</td>
                                            <td>Internet Explorer 6</td>
                                            <td>Win 98+</td>
                                            <td class="center">6</td>
                                            <td class="center">A</td>
                                        </tr>

                                        <tr class="odd gradeA">
                                            <td>Trident</td>
                                            <td>Internet Explorer 7</td>
                                            <td>Win XP SP2+</td>
                                            <td class="center">7</td>
                                            <td class="center">A</td>
                                        </tr>
                                        <tr class="even gradeA">
                                            <td>Trident</td>
                                            <td>AOL browser (AOL desktop)</td>
                                            <td>Win XP</td>
                                            <td class="center">6</td>
                                            <td class="center">A</td>
                                        </tr>
                                        <tr class="gradeA">
                                            <td>Gecko</td>
                                            <td>Firefox 1.0</td>
                                            <td>Win 98+ / OSX.2+</td>
                                            <td class="center">1.7</td>
                                            <td class="center">A</td>
                                        </tr>
                                       
                                    </tbody>
                                </table>
                            </div>
                            <!-- /.table-responsive -->
                            <div class="row">
                                <div class="col-sm-6">
								Vista 1 de 10 con 57 entradas
								</div>
								<div class="col-sm-6">
								    <div class="dataTables_paginate paging_simple_numbers" id="dataTables-example_paginate">
								            <ul class="pagination">
								            <li class="paginate_button previous disabled" aria-controls="dataTables-example" tabindex="0" id="dataTables-example_previous">
								            <a href="#">Anterior</a></li><li class="paginate_button active" aria-controls="dataTables-example" tabindex="0">
								            <a href="#">1</a></li><li class="paginate_button " aria-controls="dataTables-example" tabindex="0">
								            <a href="#">2</a></li><li class="paginate_button " aria-controls="dataTables-example" tabindex="0">
								            <a href="#">3</a></li><li class="paginate_button " aria-controls="dataTables-example" tabindex="0">
								            <a href="#">4</a></li><li class="paginate_button " aria-controls="dataTables-example" tabindex="0">
								            <a href="#">5</a></li><li class="paginate_button " aria-controls="dataTables-example" tabindex="0">
								            <a href="#">6</a></li><li class="paginate_button next" aria-controls="dataTables-example" tabindex="0" id="dataTables-example_next">
								            <a href="#">Siguiente</a>
								            </li>
								        </ul>
								    </div>
								</div>
							</div>

                                        <div class="panel-body">

                                            <div class="col-lg-6">
                                                <button id="bi1" type="button" runat="server" class="btn btn-danger btn-circle" visible="false" onServerClick="BtnActivate_click"><i id="ti1" runat="server" class="fa fa-thumbs-o-down"></i></button>&nbsp;&nbsp;&nbsp;<label>ID:</label>
                                                <asp:TextBox id="TxtIdentificador" runat="server" ReadOnly="true" class="form-control" placeholder="Introduzca su identificador"></asp:TextBox>
                                                <button id="bi2" type="button" runat="server" class="btn btn-danger btn-circle" visible="false" onServerClick="BtnActivate_click"><i id="ti2" runat="server" class="fa fa-thumbs-o-down"></i></button>&nbsp;&nbsp;&nbsp;<label>ALMACEN:</label>
                                                <asp:TextBox id="txFechaAlta" runat="server" ReadOnly="true" class="form-control" placeholder="Introduzca un Almacen"></asp:TextBox>
                                                <button id="bi3" runat="server" type="button" class="btn btn-success btn-circle" visible="false" onServerClick="BtnActivate_click"><i id="ti3" runat="server" class="fa fa-thumbs-o-up"></i></button>&nbsp;&nbsp;&nbsp;<label>LOTES:</label>   
                                                <asp:TextBox id="txtNombre" runat="server" ReadOnly="true" class="form-control" placeholder="Introduzca un Lote"></asp:TextBox> 
                                             <br />
                                                <button type="button" visible="true" id="btHAE" runat="server" onServerClick="BtnActivate_click" class="btn btn-primary">Nuevos Datos</button>
                                                <button type="button" visible="true" id="btHAC" runat="server" onServerClick="GuardaP4_Click" class="btn btn-warning disabled">Modificar Datos</button>
                                            </div>   
                                            <div class="col-lg-6">
                                                <button id="bi8" type="button" runat="server" class="btn btn-danger btn-circle" visible="false" onServerClick="BtnActivate_click"><i id="ti8" runat="server" class="fa fa-thumbs-o-down"></i></button>&nbsp;&nbsp;&nbsp;<label>FECHA LOTE:</label>
                                                <asp:DropDownList runat="server" class="form-control" ID="dlPais" AppendDataBoundItems="true" AutoPostBack="False" />
                                                <button id="bi9" type="button" runat="server" class="btn btn-danger btn-circle" visible="false" onServerClick="BtnActivate_click"><i id="ti9" runat="server" class="fa fa-thumbs-o-down"></i></button>&nbsp;&nbsp;&nbsp;<label>CULTIVO:</label>
                                                <asp:DropDownList runat="server" class="form-control" ID="dlProvincia" AppendDataBoundItems="true" AutoPostBack="False" />
                                                <button id="bi10" type="button" runat="server" class="btn btn-danger btn-circle" visible="false" onServerClick="BtnActivate_click"><i id="ti10" runat="server" class="fa fa-thumbs-o-down"></i></button>&nbsp;&nbsp;&nbsp;<label>OBSERVACIONES:</label>
                                                <asp:DropDownList runat="server" class="form-control" ID="DlMunicipio" AppendDataBoundItems="true" AutoPostBack="False" />
                                                <br />
                                                <button type="button" visible="true" id="EditPublicaPersonal" runat="server" onServerClick="BtnActivate_click" class="btn btn-primary">Guardar Datos</button>
                                                <button type="button" visible="true" id="GuardaPublicaPersonal" runat="server" onServerClick="BtnGuardaMascaraPer_click" class="btn btn-primary">Eliminar Datos</button>
                                                
                                            </div>
                                         </div>
                                            <!-- /.panel-body -->
                                    </div>
                                </div>
                        </div>
                        <!-- .panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
            </div>
                <!-- /.row -->
            </div>

        </div>
        <!-- /#page-wrapper -->
        </form>
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

</body>
</html>
