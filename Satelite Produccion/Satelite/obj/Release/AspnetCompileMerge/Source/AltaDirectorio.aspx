<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaDirectorio.aspx.cs" Inherits="QRCode_Demo.AltaDirectorio" %>

<%--<%@ Register TagPrefix="MsgBox" Src="UCMessageBox.ascx" TagName="UCMessageBox" %>--%>
<!DOCTYPE html>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">


<html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">

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
                            <a class="active" href="#"><i class="fa fa-sitemap fa-fw"></i> Codigos QR<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level in">
                                <li>
                                    <a href="LoteAuto.aspx"><i class="fa fa-long-arrow-right"></i> Generación e impresión de códigos de lote</a>
                                </li>
                                <li>
                                    <a href="LoteRevi.aspx" class="active "><i class="fa fa-long-arrow-right"></i> Revisión lotes y preparación importación a GoldenSoft</a>
                                    <!-- /.nav-third-level -->
                                </li>
                                <li>
                                    <a href="LoteManu.aspx"><i class="fa fa-long-arrow-right"></i> Generación lotes manuales (para lotes externos)</a>
                                    <!-- /.nav-third-level -->
                                </li>
                            </ul>
                        </li>
                        
                    </ul>
                </div>
                <!-- /.sidebar-collapse -->
            </div>
            <!-- /.navbar-static-side -->
        </nav><!-- Navigation -->









      <form id="formCode" runat="server">
                    
        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="Lberror"  Text="" > </asp:Label>
        <div id="page-wrapper"><!-- /#page-wrapper -->

            <div class="row">
              <div class="col-lg-12">
                <h3 id="H3Titulo" runat="server" visible="true"> Vista Jerárquica Documental sobre Base de Datos <i class="fa fa-long-arrow-right"></i> <small> “Para consultar por todos los repositorios de información de la estructura Documental”  </small></h3>
                <h3 id="H3Desarrollo" runat="server" style="color:red;" visible="false">(DESARROLLO) --> Vista Jerárquica Documental sobre Base de Datos <i class="fa fa-long-arrow-right"></i> <small> “Para consultar por todos los repositorios de información de la estructura Documental” </small></h3>
              </div>
            </div><!-- /.row -->

            <div class="row">
                <div class="col-lg-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">                    
                                 <h3 class="panel-title"><i class="fa fa-long-arrow-right"></i>Instancias de conexión a Servidor
                                <%--<button id="Button1" type="button" runat="server" class="pull-right text-muted "  OnServerClick="btnUser_Click"><i tooltip="Usuario valido para la edición casillas sobre esta página" class="fa fa-user"></i></button>
                                <asp:Button ID="btnPrint" runat="server"  CssClass="btn btn-success " Width="100%" Text="Imprimir código QR" OnClientClick="return PrintPanel();" />--%>
                                </h3>
                                </div>
                               <div class="panel-body">
                                   <div class="col-lg-10" >
                                        <div class="myTreeScroll"  runat="server" >
                                            <asp:TreeView ID="TreeConect" runat="server"  ImageSet="XPFileExplorer" NodeIndent="15" OnSelectedNodeChanged="treeConect_SelectedNodeChanged" >
                                                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                                                <ParentNodeStyle Font-Bold="False" />
                                                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />

                                            </asp:TreeView>
                                        </div> 
                                   </div>
                            </div>
                        </div>
                    </div>

                <div class="col-lg-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">                    
                                 <h3 class="panel-title"><i class="fa fa-long-arrow-right"></i>Bases de Datos de la conexión
                                <%--<button id="Button1" type="button" runat="server" class="pull-right text-muted "  OnServerClick="btnUser_Click"><i tooltip="Usuario valido para la edición casillas sobre esta página" class="fa fa-user"></i></button>
                                <asp:Button ID="btnPrint" runat="server"  CssClass="btn btn-success " Width="100%" Text="Imprimir código QR" OnClientClick="return PrintPanel();" />--%>
                                </h3>
                                </div>
                               <div class="panel-body">
                                   <div class="col-lg-10" >
                                        <div class="myTreeScroll"  runat="server" >
                                            <asp:TreeView ID="treeUser" runat="server"  ImageSet="XPFileExplorer" NodeIndent="15" OnSelectedNodeChanged="treeUser_SelectedNodeChanged" >
                                                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                                                <ParentNodeStyle Font-Bold="False" />
                                                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />

                                            </asp:TreeView>
                                        </div> 
                                   </div>
                            </div>
                        </div>
                    </div>

                <div class="col-lg-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">                    
                                 <h3 class="panel-title"><i class="fa fa-long-arrow-right"></i>Jerarquía en su Archivo Documental
                                <%--<button id="Button1" type="button" runat="server" class="pull-right text-muted "  OnServerClick="btnUser_Click"><i tooltip="Usuario valido para la edición casillas sobre esta página" class="fa fa-user"></i></button>
                                <asp:Button ID="btnPrint" runat="server"  CssClass="btn btn-success " Width="100%" Text="Imprimir código QR" OnClientClick="return PrintPanel();" />--%>
                                </h3>
                                </div>
                               <div class="panel-body">
                                   <div class="col-lg-10" >
                                        <div class="myTreeScroll"  runat="server" >
                                            <asp:TreeView ID="TreeBases" runat="server"  ImageSet="XPFileExplorer" NodeIndent="15" OnSelectedNodeChanged="treeBases_SelectedNodeChanged" >
                                                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                                                <ParentNodeStyle Font-Bold="False" />
                                                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />

                                            </asp:TreeView>
                                        </div> 
                                   </div>
                            </div>
                        </div>
                    </div>
            </div> 

           <div class="row">
               <div class="col-lg-5">
                    <div class="panel panel-default">
                        <div class="panel-heading">                    
                            <h3 class="panel-title"><i class="fa fa-long-arrow-right"></i>Resultados desde Tabla
                        </h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-10" >
                                <div id="Directorio" runat="server" visible ="false" >
                                    <%-- Por defecto -> </div>--%>
                                     <asp:GridView ID="gvControl" runat="server" AutoGenerateColumns="False" 
                                            CellPadding="4" ForeColor="#333333" 
                                            GridLines="None"  OnRowDataBound="gvIG_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="22"
                                            oonselectedindexchanged="gvControl_SelectedIndexChanged" OnRowEditing="gvControl_RowEditing" OnRowCancelingEdit="gvControl_RowCancelingEdit" OnRowUpdating="gvControl_RowUpdating"  >
                                       <RowStyle BackColor="#EFF3FB" />
                                         <Columns>
                                          <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/editar.png" ShowEditButton="True" CancelImageUrl="~/Images/cancelar.png" CancelText="" DeleteText="" UpdateImageUrl="~/Images/guardar.png" UpdateText="" />
                                         </Columns>
                                         <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                         <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                         <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                         <HeaderStyle BackColor="#062d5c" Font-Bold="True" ForeColor="White" />
                                         <EditRowStyle BackColor="#2461BF" />
                                         <AlternatingRowStyle BackColor="White" />
                                     </asp:GridView>
                            </div>
                         </div> 
                     </div>
                 </div>
              </div>
            </div>

            <div class="row">
                 <div class="col-lg-5">
                    <div class="panel panel-default">
                        <div class="panel-heading">                    
                            <h3 class="panel-title"><i class="fa fa-long-arrow-right"></i>Resultados desde Tabla
                        </h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-10" >
                                 <panel id="pnlControles" runat="server" visible="true" style="height:300px; width:100%;">

                                </panel>
                            </div>
                         </div> 
                     </div>
                 </div>
              </div>
        </div>
  
    </form>
</body>
</html>
