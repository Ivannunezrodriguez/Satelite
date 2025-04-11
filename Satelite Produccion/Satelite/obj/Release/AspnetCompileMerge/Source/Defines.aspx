<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Defines.aspx.cs" Inherits="QRCode_Demo.Defines" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

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
<%--       <link href="css/bootstrap.css" rel="stylesheet"/>--%>

    <!-- MetisMenu CSS -->
    <link href="css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet"/>

    <!-- Timeline CSS -->
    <link href="css/plugins/timeline.css" rel="stylesheet"/>

    <!-- Custom CSS -->
    <link href="css/sb-admin-2.css" rel="stylesheet"/>

    <!-- Morris Charts CSS -->
    <link href="css/plugins/morris.css" rel="stylesheet"/>

    <!-- Custom Fonts -->
    <link href="font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css"/

        <!-- Embed the HTMEditor --> 

    <style>@import url(http://fonts.googleapis.com/css?family=Bree+Serif);
        body, h1, h2, h3, h4, h5, h6{
        font-family: 'Bree Serif', serif;
        }
    </style>

  <%--  <script runat="server">

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ltlResult.Text = Editor1.Content;
        }
    </script>--%>


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
                            <a class="active" href="#"><i class="fa fa-sitemap fa-fw"></i> Codigos QR<span class="fa arrow"></span></a>
                            <ul class="nav nav-second-level in">
                                <li>
                                    <a href="LoteAuto.aspx"><i class="fa fa-long-arrow-right"></i> Generación de lotes automáticos</a>
                                </li>
                                <li>
                                    <a href="LoteRevi.aspx"><i class="fa fa-long-arrow-right"></i> Revisión lotes y preparación importación a GoldenSoft</a>
                                    <!-- /.nav-third-level -->
                                </li>
                                <li>
                                    <a href="LoteManu.aspx" class="active "><i class="fa fa-long-arrow-right"></i> Generación lotes manuales (para lotes externos)</a>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="Lberror"  Text="" > </asp:Label>
        <div id="page-wrapper"><!-- /#page-wrapper -->

        <div class="row">
          <div class="col-lg-12">
            <%--<h3> Generación de lotes manuales <i class="fa fa-long-arrow-right"></i> <small> “ Generación de lotes manualmente (para lotes externos) ”  </small></h3>--%>
            <h3 id="H3Titulo" runat="server" visible="true"> Generación de lotes manuales <i class="fa fa-long-arrow-right"></i> <small> “ Generación de lotes manualmente (para lotes externos) ”  </small></h3>
            <h3 id="H3Desarrollo" runat="server"  style="color:red;" visible="false">(DESARROLLO) --> Generación de lotes manuales <i class="fa fa-long-arrow-right"></i> <small> “ Generación de lotes manualmente (para lotes externos) ” </small></h3>

          </div>
        </div><!-- /.row 
            https://www.htmeditor.com/
         Parameters

        htmeditor_textarea - [The text area id name] The area you want the editor to be presented in.
        full_screen - [yes/no] Display the editor on a full screen mode. Default is “no”.
        editor_height - [number] The height size of editor.
        run_local - [yes/no] Run the script in offline mode so it can be run in your local device. Default is 'no'.

                -->

         <div class="row">      
                <div class="col-lg-12" >
                    <div class="panel panel-default">
                        <div id="Div4" runat="server" visible="true" class="panel-heading">
                            <h3 class="panel-title" style="color: black; font-weight:bold;"><i class="fa fa-long-arrow-right"></i> Plantillas para impresión Bootstrap
                            <button id="Button1" type="button" runat="server" class="pull-right text-muted " tooltip="Imprime la vista previa presentada en pantalla" onclick="return PrintPanel();"><i class="fa fa-print"></i></button>
                            </h3>                              
                        </div>    
  
                            
                        <div class="panel-body" style="height:900px;">                              
                            <textarea id="htmeditor"></textarea> 
                                 <script src="https://htmeditor.com/js/htmeditor.min.js"     
                                 htmeditor_textarea="htmeditor"      
                                     full_screen="no"     
                                     editor_height="480"    
                                     run_local="no" lang="es"> 
                                 </script> 
                        </div>     
                    </div> 
                </div>
           </div> 

         <div class="row">      
                <div class="col-lg-12" >
                    <div class="panel panel-default">
                        <div id="Div1" runat="server" visible="true" class="panel-heading">
                            <h3 class="panel-title" style="color: black; font-weight:bold;"><i class="fa fa-long-arrow-right"></i> Plantillas para impresión
                            <button id="Button5" type="button" runat="server" class="pull-right text-muted " tooltip="Imprime la vista previa presentada en pantalla" onclick="return PrintPanel();"><i class="fa fa-print"></i></button>
                            </h3>                              
                        </div>    
  
                            
                        <div class="panel-body" style="height:900px;">                             
                            <CKEditor:CKEditorControl ID="CKEditor1" BasePath="/ckeditor/" language='es' runat="server"></CKEditor:CKEditorControl>
                            
                              <script>
                                  CKEDITOR.replace('editor1', {
                                      language: 'es'
                                  });
                              </script>


                        </div>     
                    </div> 
                </div>
           </div> 

       <div class="row">      
                <div class="col-lg-12" >
                    <div class="panel panel-default">
                        <div id="Div2" runat="server" visible="true" class="panel-heading">
                            <h3 class="panel-title" style="color: black; font-weight:bold;"><i class="fa fa-long-arrow-right"></i> Vista Previa
                            <button id="Button6" type="button" runat="server" class="pull-right text-muted " tooltip="Imprime la vista previa presentada en pantalla" onclick="return PrintPanel();"><i class="fa fa-print"></i></button>
                            </h3>                              
                        </div>    
  
                            
                        <div class="panel-body" style="height:900px;">  
                            <%--<cc1:Editor 
                                ID="Editor1" 
                                Width="450px"  
                                Height="200px"
                                runat="server"/>
                            <br />--%>
                            <asp:Button
                                id="btnSubmit"
                                Text="Submit"
                                Runat="server" onclick="btnSubmit_Click" />
    
                            <hr />
                            <h1>You Entered:</h1>
        
                            <asp:Literal
                                id="ltlResult"
                                Runat="server" />
                        </div>     
                    </div> 
                </div>
           </div> 

 <%--       <div class="row">      
                <div class="col-lg-12" >
                    <div class="panel panel-default">
                        <div id="Div3" runat="server" visible="true" class="panel-heading">
                            <h3 class="panel-title" style="color: black; font-weight:bold;"><i class="fa fa-long-arrow-right"></i> Datos a guardar
                            <button id="Button7" type="button" runat="server" class="pull-right text-muted " tooltip="Imprime la vista previa presentada en pantalla" onclick="return PrintPanel();"><i class="fa fa-print"></i></button>
                            </h3>                              
                        </div>    
  
                            
                        <div class="panel-body" style="height:900px;">  
                            <asp:label runat="server" ID="lbl2"></asp:label>
                            <asp:Panel ID="pnlContents" runat="server">
                                        <br />
                                        <br />
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="LbCodigoLote"  Text="SIN CÓDIGO LOTE" > </asp:Label>
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:40px;"  runat="server" ID="LbSecuenciaLote"  Text="" > </asp:Label>
                                        <asp:PlaceHolder ID="PlaceHolder1"  runat="server"></asp:PlaceHolder>
                                        <div class="col-lg-12" >
                                            <asp:PlaceHolder ID="PlaceHolder2"  runat="server"></asp:PlaceHolder>
                                        <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Lbcompleto"  Text="" > </asp:Label>
                                        </div>
                                </asp:Panel>
                        </div>     
                    </div> 
                </div>
           </div> --%>

           <%-- <div class="row"><!-- /.row -->
                <asp:Panel ID="pnlContents" runat="server"></asp:Panel>   
            </div><!-- /.row -->--%>
        </div><!-- /#page-wrapper -->
        
        </form>
    </div>
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
 <%--              function PrintPanel() {
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
               }--%>
           </script>

</body>
</html>
