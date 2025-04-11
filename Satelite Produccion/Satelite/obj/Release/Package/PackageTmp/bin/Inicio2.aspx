<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Inicio2.aspx.cs" Inherits="Satelite.Inicio2" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%--<%@ MasterType VirtualPath="~/Default.Master" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="css/bootstrap.min.css" rel="stylesheet" />




      <style>
    .container-main {
      position: relative
    }
    
    .container-main video {
      width: 100%;
      height:120%;
      display: block;
      min-height: 600px;
      filter:opacity(60%);
    }

    
    .container-main .img {
      position: absolute;
      top: 40%;
      max-width: 600px
    }
    
    .img-1 {
        left:30%;
       
        height:250px;
        width:600px;
    }
    
  </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Chart ID="Chart1" runat="server">
        <Series>
            <asp:Series Name="Series1"></asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
        </ChartAreas>
    </asp:Chart>


     <div id="page_wrapper" runat="server" ><!-- /#page-wrapper  class="portada"-->
        <%--<asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="Update">
            <ProgressTemplate>--%>
               <%-- <div class="centrado" visible="false" style="z-index:1100;">
                    <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                </div>--%>
 <%--           </ProgressTemplate>
        </asp:UpdateProgress>--%>

           <div id="barchart_material" style="width: 900px; height: 500px;"></div>
  


         <div class="row container-main" runat="server"   id="DivVideo">
              <video autoplay="autoplay" loop="loop" id="video_background" preload="auto" muted="muted" poster="Images/Morangos">
                <source src="Images/Water.mp4" type="video/mp4">
            </video>
            <img src="Images/Logo_Rio_Eresma_Transparente.png" class="img img-1" />
            <%--<img src="Images/logo.gif" style="left:40%;top:50%;height:40%;width:100%" />--%>
         </div>
        <div class="row" runat="server"  id="PendienteImportacion">
            <div class="col-lg-12">
                <h1 class="page-header"> Panel de control principal</h1>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <!-- /.row -->
        <div class="row" runat="server" id="formularios">
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
                        <asp:Repeater id="RpImportacion0"  runat="server">
                            <itemtemplate>
                               <a  href="#" style="font-size:11pt; width:100%; top:0px;" runat="server" onserverclick="ARepeater_clik"><i class="fa fa-circle">&nbsp;&nbsp;</i><%# DataBinder.Eval(Container.DataItem, "TIPO_FORM") %> contiene <%# DataBinder.Eval(Container.DataItem, "CUANTOS") %> Lotes</a>
                               <br />
                            </itemtemplate>
                        </asp:Repeater>
                        <br/>
                        <br/>
                        <u style="width:100%;">Tipo de formularios finalizados:</u><i id="Rpt1" runat="server"></i>
                        <br/>
                        <br/>                      
                        <asp:Repeater id="RpImportacion1" runat="server">
                            <itemtemplate>
                               <a  href="#" style="font-size:11pt; width:100%; top:0px;" runat="server" onserverclick="BRepeater_clik"><i class="fa fa-circle">&nbsp;&nbsp;</i><%# DataBinder.Eval(Container.DataItem, "TIPO_FORM") %> contiene <%# DataBinder.Eval(Container.DataItem, "CUANTOS") %> Lotes</a>
                               <br />
                            </itemtemplate>
                            <%--<itemtemplate>
                                <b>
                                    <i class="fa fa-circle">&nbsp;&nbsp;</i><%# DataBinder.Eval(Container.DataItem, "TIPO_FORM") %> contiene <%# DataBinder.Eval(Container.DataItem, "CUANTOS") %> Lotes
                                </b>
                                <br>
                            </itemtemplate>--%>
                              
                        </asp:Repeater>
                        <br/>
                        <br/>
                        <u>Tipo de formularios procesados:</u><i id="Rpt2" runat="server"></i>
                        <br/>
                        <br/>                   
                        <asp:Repeater id="RpImportacion2" runat="server">
                             <itemtemplate>
                               <a  href="#" style="font-size:11pt; width:100%; top:0px;" runat="server" onserverclick="CRepeater_clik"><i class="fa fa-circle">&nbsp;&nbsp;</i><%# DataBinder.Eval(Container.DataItem, "TIPO_FORM") %> contiene <%# DataBinder.Eval(Container.DataItem, "CUANTOS") %> Lotes</a>
                               <br />
                            </itemtemplate>
                           <%-- <itemtemplate>
                                <b>
                                    <i class="fa fa-circle">&nbsp;&nbsp;</i><%# DataBinder.Eval(Container.DataItem, "TIPO_FORM") %> contiene <%# DataBinder.Eval(Container.DataItem, "CUANTOS") %> Lotes
                                </b>
                                <br>
                            </itemtemplate>--%>
                              
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
                               <a  href="#" style="font-size:11pt; color:olivedrab; width:100%; top:0px;" runat="server" onserverclick="DRepeater_clik"><i class="fa fa-circle">&nbsp;&nbsp;</i><%# DataBinder.Eval(Container.DataItem, "EMPRESA") %> contiene <%# DataBinder.Eval(Container.DataItem, "CUANTOS") %> Órdenes de Carga nuevas</a>
                               <br />
                            </itemtemplate>
                              
                        </asp:Repeater>
                        <br/>
                        <br/>
                        <u>Órdenes abiertas:</u><i id="Io2" runat="server"></i>
                        <br/>
                        <br/>                      
                        <asp:Repeater id="RpTOrden1" runat="server">
                            <itemtemplate>
                               <a  href="#" style="font-size:11pt; color:olivedrab; width:100%; top:0px;" runat="server" onserverclick="ERepeater_clik"><i class="fa fa-circle">&nbsp;&nbsp;</i><%# DataBinder.Eval(Container.DataItem, "EMPRESA") %> contiene <%# DataBinder.Eval(Container.DataItem, "CUANTOS") %> Órdenes de Carga abiertas</a>
                               <br />
                            </itemtemplate>
                              
                        </asp:Repeater>
                        <br/>
                        <br/>
                        <u>Órdenes cerradas:</u><i id="Io3" runat="server"></i>
                        <br/>
                        <br/>                   
                        <asp:Repeater id="RpTOrden2" runat="server">
                            <itemtemplate>
                               <a  href="#" style="font-size:11pt; color:olivedrab; width:100%; top:0px;" runat="server" onserverclick="FRepeater_clik"><i class="fa fa-circle">&nbsp;&nbsp;</i><%# DataBinder.Eval(Container.DataItem, "EMPRESA") %> contiene <%# DataBinder.Eval(Container.DataItem, "CUANTOS") %> Órdenes de Carga cerradas</a>
                               <br />
                            </itemtemplate>
                        </asp:Repeater>                            
                        <%--<span class="pull-left">Ver Detalles</span>
                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>--%>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>


            <div class="col-lg-3 col-md-6">
                <div class="panel panel-yellow">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-3">
                                <i class="fa fa-comments fa-5x"></i>
                            </div>
                            <div class="col-xs-9 text-right">
                                <div class="huge">124</div>
                                <div> Compra de Plantas</div>
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
                                <div> Últimos Lotes/Palets</div>
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
        <div class="row" runat="server" id="Produccionanual">
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

    <!-- jQuery Version 1.11.0 -->
    <%--<script src="js/jquery-1.11.0.js"></script>--%>

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
</asp:Content>
