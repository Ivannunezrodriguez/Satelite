<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="FlujosDatosCopia.aspx.cs" Inherits="Satelite.FlujosDatosCopia" EnableEventValidation="false" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/tree.css" rel="stylesheet"/>
    
    <meta http-equiv="Expires" content="0"> 
    <meta http-equiv="Last-Modified" content="0">
    <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"> 
    <meta http-equiv="Pragma" content="no-cache">


     <!-- Bootstrap Core CSS
    <link href="css/bootstrap.css" rel="stylesheet" />
    <!-- Bootstrap Core CSS
    <link href="css/bootstrap.min.css" rel="stylesheet" /> -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">      
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnAcepta" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>                        
                            <div runat="server" id="DvPreparado" visible="false"  style=" width: 30%;z-index: 888;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155); overflow:auto; position: fixed;" class="alert alert-grey centrado position-fixed">
                                <%--<button type="button" class="close" data-dismiss="alert">&times;</button>--%> 
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



            <asp:UpdateProgress ID="Progress3" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                <ProgressTemplate>
                    <div class="centrado position-fixed" style="z-index:1100;position: fixed;">
                        <img src="images/rastreando.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdateMenu">
                <ProgressTemplate>
                    <div class="centrado position-fixed" style="z-index:1100;position: fixed;">
                        <img src="images/rastreando.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
               <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdateCampos">
                <ProgressTemplate>
                    <div class="centrado position-fixed" style="z-index:1100;position: fixed;">
                        <img src="images/rastreando.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

    

              <%--Segundos Mensajes --%>
            <div runat="server" id="DivCuestion" visible="false"  style=" width: 30%;z-index: 1999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155); overflow:auto;position: fixed;" class="alert alert-grey centrado">
                <%--<button type="button" class="close" data-dismiss="alert">&times;</button>--%>
                <i id="I3" runat="server" class="fa fa-exclamation-circle fa-2x"></i>
                <asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="lbCuestion" BorderStyle="None" border="0" Width="100%" Text=" Se eliminarán los registros actuales con una nueva consulta ¿Desea continuar?"  />
                <div class="row" id="Div4" visible="true" runat="server">
                    <div class="col-lg-4">
                     </div>
                    <div class="col-lg-4">
                    <asp:Button runat="server" ID="Button2" Visible="true" tooltip="Aceptar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Aceptar" OnClick="Aceptar_Click"/>                                                    
                    </div>
                    <div class="col-lg-4">
                    </div>
                </div>
            </div>

     <%-- DivCampos0--%>
                <asp:UpdatePanel ID="UpdateCampos" runat="server" UpdateMode="Conditional">      
                    <ContentTemplate>
			          <div runat="server" id="DivCampos0" visible="false"  style=" height:560px; width: 80%;z-index: 1000;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black;position: fixed; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
                         <%--<div class="centrado" visible="false" runat="server" id="DivCampos0"  height: 700px; max-height: 700px;>--%>
					        <div runat="server" id="DivFirma" visible="false"  style=" width: 60%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155); overflow:auto;" class="alert alert-grey centrado">
                                <%--<button type="button" class="close" data-dismiss="alert">&times;</button>--%> 
                                <i id="I2" runat="server" class="fa fa-exclamation-circle fa-2x"></i>
                                <asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="Lbmensfirma" BorderStyle="None" border="0" Width="100%" Text=" Se eliminarán los registros actuales con una nueva consulta ¿Desea continuar?"  />
  
                                 <div class="row" id="Divmens" visible="true" runat="server">
                                     <div class="col-lg-5">
                                     </div>
                                    <div class="col-lg-2">
                                        <asp:Button runat="server" ID="Button3" Visible="true" tooltip="Aceptar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Aceptar" OnClick="checkYa_Click"/>
                                    </div>
                                     <div class="col-lg-5">
                                     </div>
                                </div>
                            </div>
                  
                  
                           <button id="BtnRegistro" type="button" runat="server" visible="true" style=" text-align:right; position:absolute; top:10; left:97%; border-style:none; background-color:transparent;" class="pull-center text-muted "  onserverClick="CierraRegistro_Click"><i title="Cierra la ficha Catalográfica" class="fa fa-times fa-2x"></i></button>
                          <h3 id="H3Nombre" runat="server" style="text-align:center;font-size:18pt;font-weight: bold;" visible="true"> Documentación adjunta 
					        </h3>
                  	        <h3 id="H3Titulo" runat="server" style="text-align:center;font-size:16pt;" visible="true"> Documentación adjunta 
					        </h3>
                            <h3 id="H3TituFicha" runat="server" style="text-align:center;font-size:16pt;" visible="false"> Documentación adjunta 
					        </h3>


						        <%-- Grid Fichros
						        <br />--%> 
                                <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;text-align:left; padding:6px; font-size:18px;"  runat="server" ID="Lbusuario"  Text="Usuario:" > </asp:Label>
                                <div class="row" runat="server" id="DivDispositivos" visible="true"  >
                                    <div id="Div3" class="col-lg-4">
                                        
                                    </div>
                                    <div id="Diva" class="col-lg-2">
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:left; padding:6px; font-size:18px;"  runat="server" ID="Label3"  Text="Dispositivo de Firma:" > </asp:Label>
                                    </div>
                                    <div id="Divs" class="col-lg-2">
                                        <asp:DropDownList ID="DrDispositivos" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrDispositivo_Changed"></asp:DropDownList>
                                    </div>
                                    <div id="Divd" class="col-lg-4">
                                    </div>
                                </div>


                            <div class="row" runat="server" id="DivFicheros" visible="false"  >
						        <div id="DivTreeDoc" visible="true" runat="server" style=" overflow:auto;" class="col-lg-8">
							        <div class="panel-body">
								
									        <asp:TreeView ID="TreeDoc" class="tabla1" runat="server" ImageSet="XPFileExplorer" NodeIndent="15" NodeStyle-CssClass="treeNode"
									        RootNodeStyle-CssClass="rootNode" LeafNodeStyle-CssClass="leafNode" OnSelectedNodeChanged="TreeDoc_SelectedNodeChanged" >
										        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
										        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
											        NodeSpacing="0px" VerticalPadding="2px"></NodeStyle>
										        <ParentNodeStyle Font-Bold="False" />
										        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
											        VerticalPadding="0px" />
									        </asp:TreeView> 

							        </div>
						        </div>
                            </div>

                          <div class="row" runat="server" id="DivFicha" visible="false"  >
						        <div id="Div7" visible="true" runat="server" style=" overflow:auto;" class="col-lg-12">
							        <div class="panel-body">
                                    <asp:GridView ID="gvCuestion"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                        AllowSorting="true" OnSorting="gvCuestion_OnSorting"
                                        CellPadding="4"  GridLines="None"  OnRowDataBound="gvCuestion_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="10" OnRowCommand="gvCuestion_RowCommand" DataKeyNames="ZID"
                                        oonselectedindexchanged="gvCuestion_SelectedIndexChanged"  OnRowEditing="gvCuestion_RowEditing" OnRowCancelingEdit="gvCuestion_RowCancelingEdit" OnRowUpdating="gvCuestion_RowUpdating" 
                                        onpageindexchanging="gvCuestion_PageIndexChanging" EnablePersistedSelection="True"  >
                                    <RowStyle />                     
                                        <Columns>
                                            <asp:TemplateField HeaderText="Ver" visible="true"  SortExpression="ZID">
                                               <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                        <itemtemplate>
                                                        <a id="linkLee" href= "Download.ashx?file=images/cuestion.png">
                                                            <asp:ImageButton ID="ibtLeeCuestion" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/cuestion.png"         
                                                            CommandName="LeeCuestion" ToolTip="Apertura de Ayuda" Width="30px" Height="30px"></asp:ImageButton>
                                                        </a>
                                                    </ItemTemplate>
                                               <ItemStyle Height="8px"></ItemStyle>   
                                            </asp:TemplateField>
                                     
									        <asp:TemplateField HeaderText="Identificador" visible="false" SortExpression="ZID">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="Tabid"  runat="server" Text='<%# Eval("ZID") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="Labid"  runat="server" Text='<%# Bind("ZID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Plantilla Maestra" visible="true" SortExpression="ZTEMPLATE">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="Tabplt"  runat="server" Text='<%# Eval("ZTEMPLATE") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="Labplt"  runat="server" Text='<%# Bind("ZTEMPLATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre" visible="true"  SortExpression="ZDESCRIPCION">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabNombre"  runat="server" Text='<%# Eval("ZDESCRIPCION") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabNombre"  runat="server" Text='<%# Bind("ZDESCRIPCION") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--Ocultos--%>
                                            <asp:TemplateField HeaderText="Fecha" Visible="false" SortExpression="ZFECHA">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabLFecha" runat="server" Text='<%# Eval("ZFECHA") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabFecha" runat="server" Text='<%# Bind("ZFECHA") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dato" Visible="false"  SortExpression="ZID_DATO">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabDato"  runat="server" Text='<%# Eval("ZID_DATO") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabDato"  runat="server" Text='<%# Bind("ZID_DATO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dato" Visible="false"  SortExpression="ZID_CUESTION">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="Tabcuestion"  runat="server" Text='<%# Eval("ZID_CUESTION") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="Labcuestion"  runat="server" Text='<%# Bind("ZID_CUESTION") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dato" Visible="false"  SortExpression="ZID_TEMPLATE">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabTemplate"  runat="server" Text='<%# Eval("ZID_TEMPLATE") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabTemplate"  runat="server" Text='<%# Bind("ZID_TEMPLATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dato" Visible="false"  SortExpression="ZID_ARCHIVO">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="Tabarchivo"  runat="server" Text='<%# Eval("ZID_ARCHIVO") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="Labarchivo"  runat="server" Text='<%# Bind("ZID_ARCHIVO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dato" Visible="false"  SortExpression="ZID_FLUJO">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="Tabflujo"  runat="server" Text='<%# Eval("ZID_FLUJO") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="Labflujo"  runat="server" Text='<%# Bind("ZID_FLUJO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dato" Visible="false"  SortExpression="ZID_ESTADO">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="Tabestado"  runat="server" Text='<%# Eval("ZID_ESTADO") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="Labestado"  runat="server" Text='<%# Bind("ZID_ESTADO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dato" Visible="false"  SortExpression="ZID_REGISTRO">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="Tabregistro"  runat="server" Text='<%# Eval("ZID_REGISTRO") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="Labregistro"  runat="server" Text='<%# Bind("ZID_REGISTRO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--Ocultos--%>

                                            <asp:TemplateField HeaderText="SI">
                                                <HeaderTemplate>
                                                    <asp:Label ID="LabDatoSi" visible="true"  runat="server" Text='Aceptar'></asp:Label>
                                                    <asp:CheckBox ID="chkSI" ToolTip="Selecciona o no todos los SI de esta Ficha." runat="server" OnCheckedChanged="sellectSiAll"
                                                        AutoPostBack="true" />
                                                </HeaderTemplate>
                                                    <itemstyle horizontalalign="Right" verticalalign="Middle" />
                                                    <itemtemplate>
                                                        <asp:CheckBox ID="chbItemSI" runat="server" OnCheckedChanged="sellectSiCK"  AutoPostBack="true"  />
                                                    </itemtemplate>
                                                    <itemstyle horizontalalign="center" />
                                                </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="NO">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkNO" ToolTip="Selecciona o no todos los NO de esta Ficha." runat="server" OnCheckedChanged="sellectNoAll"
                                                        AutoPostBack="true" />
                                                    <asp:Label ID="LabDatoNo"  runat="server" Text='NO'></asp:Label>
                                                </HeaderTemplate>
                                                    <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                    <itemtemplate>
                                                        <asp:CheckBox ID="chbItemNO" runat="server" OnCheckedChanged="sellectNoCK"  AutoPostBack="true" />
                                                    </itemtemplate>
                                                    <itemstyle horizontalalign="center" />
                                                </asp:TemplateField>--%>
 
                                        </Columns>
                                        <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
                                        <EditRowStyle BackColor="#eaf5dc" />   
                                        <rowstyle Height="20px" />
                                    </asp:GridView> 
                                    <br />

							        </div>
						        </div>
                            </div>


                           <div id="DivGridDoc" visible="true" runat="server" style=" height:300px;overflow:auto;"  class="position-fixed"> <%-- AutoGenerateSelectButton="True"--%>                                  
                                    <br />
                                   <%-- <div class="row"> 
                                        <div class="col-sm-4" >
                                        </div>
                                        <div class="col-sm-2" >
                                            <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label3"  Text="Estado Actual:" > </asp:Label>
                                        </div>
                                        <div class="col-sm-2" >
                                            <asp:DropDownList ID="DrFindEstado" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrFlujoEstadoFile_Changed"></asp:DropDownList>
                                        </div>
                                         <div class="col-sm-4" >
                                        </div>
                                    </div>--%>
                                    <br />
                                    <asp:GridView ID="gvLista"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                        AllowSorting="true" OnSorting="gvLista_OnSorting"
                                        CellPadding="4"  GridLines="None"  OnRowDataBound="gvLista_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="10" OnRowCommand="gvLista_RowCommand" DataKeyNames="ZID"
                                        oonselectedindexchanged="gvLista_SelectedIndexChanged"  OnRowEditing="gvLista_RowEditing" OnRowCancelingEdit="gvLista_RowCancelingEdit" OnRowUpdating="gvLista_RowUpdating" 
                                        onpageindexchanging="gvLista_PageIndexChanging" EnablePersistedSelection="True"  >
                                    <RowStyle />                     
                                        <Columns>
                                            <asp:TemplateField HeaderText="Ver"  SortExpression="ZID">
                                               <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                        <itemtemplate>
                                                        <a id="linkLee" href= "Download.ashx?file=images/cat1.jpg">
                                                            <asp:ImageButton ID="ibtveDoc" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/leer.png"         
                                                            CommandName="LveDoc" ToolTip="Apertura del Registro" Width="30px" Height="30px"></asp:ImageButton>
                                                        </a>
                                                    </ItemTemplate>
                                               <ItemStyle Height="8px"></ItemStyle>   
                                            </asp:TemplateField>
                                     
        <%--                                    <asp:CommandField ButtonType="Image" 
                                                        ItemStyle-HorizontalAlign="Center"
                                                        EditImageUrl="~/Images/leer.png" 
                                                        ShowEditButton="True" 
                                                        CancelImageUrl="~/Images/cancelar20x20.png" 
                                                        CancelText="" 
                                                        DeleteText="" 
                                                        UpdateImageUrl="~/Images/guardar18x18.png"          
                                                        UpdateText="" />      --%>    

        <%--                                     <asp:TemplateField HeaderText="Anterior" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lbAnterior"  runat="server" Text='Anterior'></asp:Label>
                                                </HeaderTemplate>
                                                 <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                        <itemtemplate>
                                                        <a id="linkAnterior" href= "Download.ashx?file=images/cat1.jpg">
                                                            <asp:ImageButton ID="ibtAnterior" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/leer.png"         
                                                            CommandName="Anterior" ToolTip="Envia el registro a su estado anterior" Width="30px" Height="30px"></asp:ImageButton>
                                                        </a>
                                                    </ItemTemplate>
                                                    <ItemStyle Height="8px"></ItemStyle>                                
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Siguiente" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lbSiguiente"  runat="server" Text='Siguiente'></asp:Label>
                                                </HeaderTemplate>
                                                 <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                        <itemtemplate>
                                                        <a id="linkSiguiente" href= "Download.ashx?file=images/cat1.jpg">
                                                            <asp:ImageButton ID="ibtSiguiente" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/leer.png"         
                                                            CommandName="Anterior" ToolTip="Envia el registro a su estado siguiente" Width="30px" Height="30px"></asp:ImageButton>
                                                        </a>
                                                    </ItemTemplate>
                                                    <ItemStyle Height="8px"></ItemStyle>                                
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Rechazado" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lbRechazado"  runat="server" Text='Rechazado'></asp:Label>
                                                </HeaderTemplate>
                                                 <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                        <itemtemplate>
                                                        <a id="linkRechazado" href= "Download.ashx?file=images/cat1.jpg">
                                                            <asp:ImageButton ID="ibtRechazado" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/leer.png"         
                                                            CommandName="Anterior" ToolTip="Envia el registro a su estado siguiente" Width="30px" Height="30px"></asp:ImageButton>
                                                        </a>
                                                    </ItemTemplate>
                                                    <ItemStyle Height="8px"></ItemStyle>                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Final" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lbFinal"  runat="server" Text='Finalizado'></asp:Label>
                                                </HeaderTemplate>
                                                 <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                        <itemtemplate>
                                                        <a id="linkFinal" href= "Download.ashx?file=images/cat1.jpg">
                                                            <asp:ImageButton ID="ibtFinal" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/leer.png"         
                                                            CommandName="Final" ToolTip="Envia el registro a su estado final" Width="30px" Height="30px"></asp:ImageButton>
                                                        </a>
                                                    </ItemTemplate>
                                                    <ItemStyle Height="8px"></ItemStyle>                                
                                            </asp:TemplateField>--%>

        <%--                                    <asp:TemplateField HeaderText="Selección Todos" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkbB" ToolTip="Actuación sobre TODOS los registros de esta columna." runat="server" OnCheckedChanged="sellectAllB"
                                                        AutoPostBack="true" />
                                                </HeaderTemplate>
                                                 <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                    <itemtemplate>
                                                        <a id="linkDown" href= "Download.ashx?file=images/cat1.jpg">
                                                            <asp:ImageButton ID="ibtSubeCarga" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/descargar.png"         
                                                            CommandName="SubeCarga" ToolTip="Descarga el documento en carpeta Donwload del usuario conectado" Width="30px" Height="30px"></asp:ImageButton>
                                                        </a>
                                                   
                                                    </ItemTemplate>
                                                    <ItemStyle Height="8px"></ItemStyle>                                
                                            </asp:TemplateField>--%>

        <%--                                    <asp:TemplateField HeaderText="Selección Todos" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkbC" ToolTip="Actuación sobre TODOS los registros de esta columna." runat="server" OnCheckedChanged="sellectAllC"
                                                        AutoPostBack="true" />
                                                </HeaderTemplate>
                                                 <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                     <itemtemplate>
                                                        <a id="linkPrint" href= "Download.ashx?file=images/cat1.jpg">
                                                            <asp:ImageButton ID="ibtPrintDoc" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/printer.png"         
                                                            CommandName="SubePrint" ToolTip="Imprime el Documento seleccionado en el equipo LOCAL desde donde se esté ejecutando esta página" Width="30px" Height="30px"></asp:ImageButton>
                                                        </a>
                                                   
                                                    </ItemTemplate>
                                                    <ItemStyle Height="8px"></ItemStyle>                                
                                            </asp:TemplateField>--%>

                                            <%--<asp:TemplateField HeaderText="Selección Todos" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkbD" ToolTip="Actuación sobre TODOS los registros de esta columna." runat="server" OnCheckedChanged="sellectAllD"
                                                        AutoPostBack="true" />
                                                </HeaderTemplate>
                                                 <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                     <itemtemplate>
                                                        <a id="linkUp" href= "Download.ashx?file=images/cat1.jpg">
                                                            <asp:ImageButton ID="ibtUpDoc" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/hands.png"         
                                                            CommandName="SubeUpload" ToolTip="Confirma la lectura y acuerdo de este Documento por parte del usuario para ser firmado" Width="30px" Height="30px"></asp:ImageButton>
                                                        </a>
                                                   
                                                    </ItemTemplate>
                                                    <ItemStyle Height="8px"></ItemStyle>                                
                                            </asp:TemplateField>--%>

                                            <%--<asp:TemplateField HeaderText="Selección Todos" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkbE" ToolTip="Actuación sobre TODOS los registros de esta columna." runat="server" OnCheckedChanged="sellectAllE"
                                                        AutoPostBack="true" />
                                                </HeaderTemplate>
                                                 <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                     <itemtemplate>
                                                        <a id="linkFirma" href= "Download.ashx?file=images/cat1.jpg">
                                                            <asp:ImageButton ID="ibtFirma" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/firma.png"         
                                                            CommandName="Firma" ToolTip="Apertura de Firma del Documento" Width="30px" Height="30px"></asp:ImageButton>
                                                        </a>
                                                   
                                                    </ItemTemplate>
                                                    <ItemStyle Height="8px"></ItemStyle>                                
                                            </asp:TemplateField>--%>

                                           <%-- 5--%>
        <%--                                    <asp:TemplateField HeaderText="Selección Todos" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkbF" ToolTip="Actuación sobre TODOS los registros de esta columna." runat="server" OnCheckedChanged="sellectAllF"
                                                        AutoPostBack="true" />
                                                </HeaderTemplate>
                                                 <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                     <itemtemplate>
                                                        <a id="linkComparte" href= "Download.ashx?file=images/cat1.jpg">
                                                            <asp:ImageButton ID="ibtComparte" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/ejec.png"         
                                                            CommandName="Comparte" ToolTip="Inicia proceso para la creación de Documentos para su posterior lectura" Width="30px" Height="30px"></asp:ImageButton>
                                                        </a>
                                                   
                                                    </ItemTemplate>
                                                    <ItemStyle Height="8px"></ItemStyle>                                
                                            </asp:TemplateField>--%>

        <%--                                    <asp:TemplateField HeaderText="Selección Todos" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkbG" ToolTip="Actuación sobre TODOS los registros de esta columna." runat="server" OnCheckedChanged="sellectAllG"
                                                        AutoPostBack="true" />
                                                </HeaderTemplate>
                                                 <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                   <itemtemplate>
                                                        <a id="linkpki" href= "Download.ashx?file=images/cat1.jpg">
                                                            <asp:ImageButton ID="ibtpki" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/pki.png"         
                                                            CommandName="SubePki" ToolTip="Firmar el Documento con Certificado electrónico" Width="30px" Height="30px"></asp:ImageButton>
                                                        </a>
                                                   
                                                    </ItemTemplate>
                                                    <ItemStyle Height="8px"></ItemStyle>                                
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Selección Todos">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkb1" ToolTip="Selecciona o no todos los Documentos leídos por el Empleado." runat="server" OnCheckedChanged="sellectAll"
                                                        AutoPostBack="true" />
                                                </HeaderTemplate>
                                                    <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                    <itemtemplate>
                                                        <asp:CheckBox ID="chbItem" runat="server"   />
                                                    </itemtemplate>
                                                    <itemstyle horizontalalign="center" />
                                                </asp:TemplateField>

        <%--                                    <asp:TemplateField HeaderText="Selección Todos" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkbH" ToolTip="Actuación sobre TODOS los registros de esta columna." runat="server" OnCheckedChanged="sellectAllH"
                                                        AutoPostBack="true" />
                                                </HeaderTemplate>
                                                 <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                        <itemtemplate>
                                                            <a id="linkError" href= "Download.ashx?file=images/cat1.jpg">
                                                                <asp:ImageButton ID="ibtError" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/docError.png"         
                                                                CommandName="SubeError" ToolTip="Notifica que este Documento electrónico no es correcto." Width="30px" Height="30px"></asp:ImageButton>
                                                            </a>
                                                        </ItemTemplate>
                                                        <ItemStyle Height="8px"></ItemStyle>                                
                                                </asp:TemplateField>--%>

        <%--									<asp:TemplateField HeaderText="Selección Todos" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkb1" ToolTip="Aceptación de TODOS los Documentos y activación de Flujo para Firma." runat="server" OnCheckedChanged="sellectAll"
                                                                AutoPostBack="true" />
                                                        </HeaderTemplate>
                                                         <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                            <itemtemplate>
                                                                <asp:CheckBox ID="chbItem" ToolTip="Aceptación del Documento de este registro y activación de Flujo para Firma." runat="server" />
                                                            </itemtemplate>
                                                            <itemstyle horizontalalign="center" />
                                            </asp:TemplateField>--%>
                                            <%-- 3--%>
									        <asp:TemplateField HeaderText="Identificador" visible="false" SortExpression="ZID">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="Tabid"  runat="server" Text='<%# Eval("ZID") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="Labid"  runat="server" Text='<%# Bind("ZID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 4--%>
                                            <asp:TemplateField HeaderText="Nombre"  SortExpression="ZDESCRIPCION">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabNombre"  runat="server" Text='<%# Eval("ZDESCRIPCION") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabNombre"  runat="server" Text='<%# Bind("ZDESCRIPCION") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha" SortExpression="ZFECHA">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabLFecha" runat="server" Text='<%# Eval("ZFECHA") %>' class="gridListDinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabFecha" runat="server" Text='<%# Bind("ZFECHA") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Categoria" SortExpression="ZCATEGORIA">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabCategoria" runat="server" Text='<%# Eval("ZCATEGORIA") %>' class="gridListDinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabCategoria" runat="server" Text='<%# Bind("ZCATEGORIA") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 8--%>
                                                <asp:TemplateField HeaderText="Peso" SortExpression="ZPESO">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabPeso" runat="server" Text='<%# Eval("ZPESO") %>' class="gridListDinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabPeso" runat="server" Text='<%# Bind("ZPESO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Usuario" SortExpression="ZUSER">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="Tabuser" runat="server" Text='<%# Eval("ZUSER") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabLuser" runat="server" Text='<%# Bind("ZUSER") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>     
                                             <asp:TemplateField HeaderText="firma" Visible="false" SortExpression="ZFIRMA">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="Tabfirma" runat="server" Text='<%# Eval("ZFIRMA") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabLfirma" runat="server" Text='<%# Bind("ZFIRMA") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>     
                                            <%-- 12--%>
                                            <asp:TemplateField HeaderText="Llave" Visible="false" SortExpression="ZLLAVE">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="Tabllave" runat="server" Text='<%# Eval("ZLLAVE") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabLllave" runat="server" Text='<%# Bind("ZLLAVE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField> 


        <%--                                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImprimeDoc" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/printer.png"
                                                        CommandName="ImprimirDoc" ToolTip="imprimir el Documento" Width="30px" Height="30px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Height="8px"></ItemStyle>                                
                                            </asp:TemplateField>--%>
                                    
                                           <%-- <asp:TemplateField  ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="EliminaCarga" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/elimina.png"
                                                        CommandName="EliminaCarga" ToolTip="Elimina el fichero del Servidor" Width="30px" Height="30px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Height="8px"></ItemStyle>                                
                                            </asp:TemplateField>--%>

                                        </Columns>
                                        <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
                                        <EditRowStyle BackColor="#eaf5dc" />   
                                        <rowstyle Height="20px" />
                                    </asp:GridView> 
                                    <br />


                           </div>  
                          <br />
                              <div class="row"> 
                                         <div class="col-sm-2" >
                                             <asp:Button runat="server" ID="BtdocAdjunto" Visible="false"  BorderStyle="None" border="0" CssClass="btn btn-info btn-block" Width="100%"  tooltip="Lista los documentos adjuntos vinculados al registro"  Text="Documentación Adjunta" OnClick="BtnCambioficha_Click"/>                      
                                        </div>
                                        <div class="col-sm-3" >
                                        </div>
                                        <div class="col-sm-2" >
                                            <asp:Button runat="server" ID="BtOpenFicha" Visible="false"  BorderStyle="None" border="0" CssClass="btn btn-success btn-block" Width="100%"  tooltip="Acepta los cambios que actualizaran el Documento vinculado para firmar"  Text="Grabar Autorización" OnClick="BtnOpenFicha_Click"/>                      
                                            <asp:Button runat="server" ID="BtOpenFirma" Visible="false"  BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  tooltip="Puede abrir la tableta de firmas desde este mismo puesto donde verá al usuario en tramite en la vantana de la tableta WACOM"  Text="Firmar Documentos" OnClick="BtnOpenFirma_Click"/>                      
                                            <asp:Button runat="server" ID="BtModeFirma" Visible="false"  BorderStyle="None" border="0" CssClass="btn btn-warning btn-block" Width="100%"  tooltip="Puede modificar la tableta de firmas para que otro puesto pueda ver al usuario en tramite en su vantana de la tableta WACOM"  Text="Modificar Dispositivo de Firma" OnClick="BtnCambiaFirma_Click"/>                      
                                        </div>
                                         <div class="col-sm-3" >
                                        </div>
                                        <div class="col-sm-2" >
                                        </div>
                              </div>
                          <br />
                     </div>     
                    </ContentTemplate> 
               </asp:UpdatePanel>      
             
		      <%-- Fin DivCampos0--%>


      <div id="page_wrapper" runat="server" ><!-- /#page-wrapper  class="portada"-->
          <asp:Timer ID="Timer1" runat="server" Enabled="false" Interval="10000" OnTick="Timer1_Tick"></asp:Timer> 





              
            <div runat="server" id="alerta" visible="false" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <i id="IAlert" runat="server" class="fa fa-exclamation-circle"></i>
                <asp:Label runat="server" class="alert alert-info alert-dismissable" ID="TextAlerta" BorderStyle="None" border="0" Width="100%" Text=""  />
            </div>

            <asp:Label  type="text" Visible="false"  Width="100%" style=" font-weight: bold;"  runat="server" ID="Lberror"  Text="" > </asp:Label>

            <div class="row" id="DivArchivos" runat="server" visible="true">
                    <div class="col-lg-8">
                    </div>

            </div><!-- /.row data-parent="#accordion"-->
            <br />
            <div class="row" >
                <div class="col-lg-12"  >
                    <div class="bs-example" id="MisMenus" runat="server">
                    <ul class="nav nav-tabs" style="margin-bottom: 15px;" >
                        <li id="Menu0" class="active" runat="server" ><asp:LinkButton ID="aMenu0" runat="server" OnClick="HtmlAnchor_Click" >FLUJOS DE TRABAJOS GENERALES</asp:LinkButton></li>                        
                        <%--<li id="Menu1" class="" runat="server" ><asp:LinkButton ID="aMenu1" runat="server" OnClick="HtmlAnchor_Click" >EMPLEADOS</asp:LinkButton></li>--%>
                    </ul>
                </div>
                </div>
            </div>

            <%--Menus--%>
            <div id="PanelGralFiltro" Enabled="true" visible="true" runat="server" class="panel panel-default">
                <div class="panel-heading" runat="server" id="PanelFiltro" >
                    <div class="row">
                            <div class="col-lg-12"> 
                            <h4 class="panel-title">
                                <a data-toggle="collapse" style=" font-weight: bold;"> Recogida muestra datos desde Flujo seleccionado
                                    <%--fa-box-open             <asp:Label  type="text"  Width="90%" style=" font-weight: bold; color:green;"  runat="server" ID="LbCabecera"  Text="" > </asp:Label>--%>                           
                                </a>                            
                            </h4>
                        </div>                            
                    </div>
                </div>

                <div id="Div5" runat="server"  class="panel-collapse collapse in">
                     <%--filtros Fecha--%>
                    <div class="panel panel-default">           
                        <div class="panel-body" id="Div6" visible="true" runat="server">
                                <div class="row" runat="server" id="DivGralConsulta">                                                                       
                                    <div class="col-sm-1" >
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label7"  Text="Fecha:" > </asp:Label>
                                            
                                    </div>
                                    <div class="col-sm-1" >
                                        <asp:TextBox runat="server" CssClass="form-control" Enabled="false" Visible="true"  Width="100%" tooltip="Introduzca Fecha Desde para filtrar datos"  ID="TxtDesde"  Font-Bold="True" />
                                    </div>
                                    <div class="col-sm-1" >
                                        <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label8"  Text="Fecha:" > </asp:Label>
                                    </div>
                                    <div class="col-sm-1" >
                                        <asp:TextBox runat="server" CssClass="form-control" Enabled="false" Visible="true"  Width="100%" tooltip="Introduzca Fecha Desde para filtrar datos"  ID="TxtHasta"  Font-Bold="True" />
                                    </div>
                                    <div class="col-sm-3" >

                                        <div class="col-sm-1" >
                                            <asp:ImageButton id="ImgFechamenos" Enabled="false" visible="true" runat="server" ToolTip="Atrás Meses" ImageAlign="right" Width="25px" Height="30px" ImageUrl="images/menos.png" OnClick="BtFechaMas_Click"/>
                                            </div>
                                        <div class="col-sm-5" >
                                            <asp:DropDownList ID="DrAno" CssClass="form-control" Enabled="false" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrMeses_Changed"></asp:DropDownList>
                                            </div>
                                        <div class="col-sm-5" >
                                            <asp:DropDownList ID="DrMeses" CssClass="form-control" Enabled="false" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrMeses_Changed"></asp:DropDownList>
                                            </div>
                                        <div class="col-sm-1" >
                                            <asp:ImageButton id="ImgFechamas" visible="true" Enabled="false" runat="server" ToolTip="Adelante Meses" ImageAlign="left" Width="25px" Height="30px" ImageUrl="images/mas.png" OnClick="BtFechaMas_Click"/>
                                        </div>
                                    </div>

                                    <div class="col-sm-3" >
                                            <asp:UpdatePanel ID="Update2" runat="server" UpdateMode="Conditional">      
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btUltimo" EventName="Click" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <%--<a data-toggle="collapse" onclick="submitit8()" href="#"></a>--%>
                                                <asp:Button ID="btUltimo"  runat="server" OnClick="Ultima_Click" Height="0" Width="0" CssClass="hidden" />
                                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="LbUltConsulta"  Text="" > </asp:Label>
                                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="LdDia" Text="" > </asp:Label>
                                                </ContentTemplate>
                                        </asp:UpdatePanel> 
                                        <%--<asp:LinkButton id="Lanza80" type="button" runat="server" style="width:40%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onClick="Lanza80_Click" Text='<i title="Consulta General con filtro o sin filtro según selección sobre la base de datos de RecoDat. Elimina la consulta actual visualizada en pantalla." class="fa fa-ban fa-2x"></i>'></asp:LinkButton>--%>    
                                    </div>
                                    <div class="col-lg-2" >
                                        <asp:LinkButton id="BtGralConsulta" type="button" runat="server" style="width:40%; text-align:right; border-style:none; background-color:transparent;" class="pull-right text-muted "  onClick="BtCuestionGralConsulta_Click" Text='<i title="Consulta General con filtro o sin filtro según selección sobre la base de datos de RecoDat. Elimina la consulta actual visualizada en pantalla." class="fa fa-outdent fa-2x"></i>'></asp:LinkButton>    
                                    </div>
                                </div>

                                 <div class="row">      
                                    <div runat="server" class="col-lg-1" >
                                        <input type="image"  class="pull-right text-muted " title="Limpia el filtro general seleccionado a la derecha" src="images/ordencarga25x25.png" onserverclick="ImgOrdenMin_Click"  runat="server" id="ImgOrdenMin" style="border: 0px; " />    
                                        <%--<img src="https://drive.google.com/open?id=1OqE1KIvtsNDjmtwMJpCxY0Bz19ALiuQE" alt="placeholder" style="border: 0px; overflow:auto;" />--%> 

                                    </div>
                                    <div  runat="server" class="col-lg-2" > 
                                            <asp:DropDownList runat="server" Enabled="false"  CssClass="form-control" style="position:relative; background-color:#fff; font-size:14px;"  Width="100%"  ID="DrVistaEmpleado" tooltip="Contiene la orden de cabecera seleccionada" OnSelectedIndexChanged="DrVistaEmpleado_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                    </div>
                                     <div class="col-sm-1" >
                                         <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label2"  Text="Flujos:" > </asp:Label>
                                     </div>
                                     <div class="col-sm-2" >
                                         <asp:DropDownList ID="DrFlujo" Enabled="false" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrFlujo_Changed"></asp:DropDownList>
                                     </div>
                                     <div class="col-sm-2" >
                                         <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label1"  Text="Estado del Flujo:" > </asp:Label>
                                     </div>
                                     <div class="col-sm-2" >
                                         <asp:DropDownList Enabled="false" ID="DrFlujoEstado" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrFlujoEstado_Changed"></asp:DropDownList>
                                     </div>
                                     <div class="col-sm-2" >
                                     </div>
                                </div>


                            <br />
                            <br />


                            <div class="row">
                                <div class="col-lg-10"> 
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" style=" font-weight: bold; background-color:#c1c1c1"> Para el Archivo documental seleccionado:
                                        </a> 
                                        &nbsp&nbsp<label runat="server" visible="true" style="color:olivedrab ;"  id="LbArchivoDoc" title="Archivo documental seleccionado"></label>
                                    </h4>
                                    <br />
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" style=" font-weight: bold; background-color:#c1c1c1"> Estás en el Estado:
                                        </a> 
                                    </h4>
                                </div>   
                                <%--<div class="col-lg-1">
                                    <label runat="server" visible="true"  id="LabelAltas" title="Todos los registros con/sin condición filtro">Aplicar :</label>
                                </div>
                                <div class="col-lg-1" style="text-align:center;"> 
                                    <asp:ImageButton id="ImgAbiertos" visible="true" runat="server" ToolTip="Todos los Empleados con y sin alta" ImageAlign="right" Width="30px" Height="30px" ImageUrl="images/pnegro.png" OnClick="checkTodo_Click"/>
                                    <asp:ImageButton id="ImgCerrados" visible="false" runat="server" ToolTip="Todos los Empleados con alta" ImageAlign="right" Width="30px" Height="30px" ImageUrl="images/pazul.png" OnClick="checkTodo_Click"/>

                                </div>--%>
                            </div>
                            <br />
                            <div class="row"> 
                                <asp:UpdatePanel ID="UpdateMenu" runat="server" UpdateMode="Conditional">      
                                    <ContentTemplate>
                                        <div class="row"> 
                                            <div class="col-lg-2" style="text-align:center;"> 
                                                 <asp:ImageButton id="ImageEjecucion" Enabled="false" visible="false" runat="server" ToolTip="Estado Ejecutandose"  Width="40px" Height="40px" ImageUrl="images/loading-buffering.gif" OnClick="checknada_Click"/>
                                                 <asp:ImageButton id="ImagenEstado" Enabled="false" visible="false" runat="server" ToolTip="Estado del Flujo Seleccionado"  Width="40px" Height="40px" ImageUrl="images/pnegro.png" OnClick="checkTodo_Click"/>
                                            </div>
                                            <div class="col-lg-2" style="text-align:center;"> 
                                                  <asp:ImageButton id="ImgDetiene" Enabled="false" visible="false" runat="server" ToolTip="Detiene el Flujo en el Archivo documental seleccionado"  Width="40px" Height="40px" ImageUrl="images/boxcloseorange.png" OnClick="btFinFlujo_Click"/>
                                                  <asp:ImageButton id="ImgInicia" Enabled="false" visible="false" runat="server" ToolTip="Inicia el Flujo en el Archivo documental seleccionado"  Width="40px" Height="40px" ImageUrl="images/box-opengreen.png" OnClick="btIniFlujo_Click"/>
                                            </div>
                                            <div class="col-sm-1" >  
                                                  <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowEmpleado"  Text="" > </asp:Label>
                                            </div>
                                            <div class="col-sm-1" >
                                                <asp:LinkButton id="LinkFlujo" Enabled="false" type="button" runat="server" style="width:40%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onClick="BtiniciaFlujo_Click" Text='<i title="Inicia el Flujo de trabajo seleccionado." class="fa fa-address-card-o fa-2x"></i>'></asp:LinkButton>    
                                            </div>
                                
                                            <%--<div class="col-lg-2" style="text-align:center;"> 
                                                <asp:Button runat="server" ID="BtAtras" Enabled="false" Visible="false" tooltip="Aceptar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Rechazar Seleccionados" OnClick="RetrocedeEstado_Click"/>
                                             </div>
                                            <div class="col-sm-1" >
                                            </div>
                                            <div class="col-lg-2" style="text-align:center;"> 
                                                   <asp:Button runat="server" ID="Button1"  Enabled="false" Visible="true" tooltip="Actualiza sobre pantalla lo procesado hasta el momento por el Servicio Procesos RioEresma" BorderStyle="None" border="0" CssClass="btn btn-success btn-block" Width="100%" AutoPostBack="true"  Text="Actualizar" OnClick="ActualizaDatos_Click"/>
                                            </div>--%>
                                        </div>
                                       <%-- <div class="col-lg-1" style="text-align:center;"> 
                                             <asp:ImageButton id="ImgAyuda" visible="true" Enabled="false" runat="server" ToolTip="Visualiza en pantalla la descripcion del Estado en que se encuentra" ImageAlign="right" Width="30px" Height="30px" ImageUrl="images/ayuda.png" OnClick="BtAyuda_Click"/>
                                        </div> --%>



                                        <%-- <div class="col-lg-1" style="text-align:center;"> 
                                              <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowEmpleado"  Text="" > </asp:Label>
                                         </div>--%>
                                        <%--<div class="col-lg-1" style="text-align:center;"> 
                                        </div>--%>
                                        <div class="row"> 
                                             <div class="col-lg-2" style="text-align:center;"> 
                                            </div>
                                             <div class="col-lg-2" style="text-align:center;"> 
                                            </div>
                                            <div class="col-lg-1" style="text-align:center;"> 
                                                <asp:Button runat="server" ID="BtAtras" Enabled="false" Visible="true" tooltip="Envía al Estado definido cómo anterior los registros seleccionados" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Atrás" OnClick="RetrocedeEstado_Click"/>
                                             </div>
                                            <div class="col-lg-1" style="text-align:center;"> 
                                                <asp:Button runat="server" ID="BtSiguiente" Enabled="false" Visible="true" tooltip="Envía al Estado definido cómo siguiente los registros seleccionados" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Siguiente" OnClick="RetrocedeEstado_Click"/>
                                             </div>
                                            <div class="col-lg-1" style="text-align:center;"> 
                                                <asp:Button runat="server" ID="BtAlternate" Enabled="false" Visible="true" tooltip="Envía al Estado definido cómo rechazado los registros seleccionados" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Rechazado" OnClick="RetrocedeEstado_Click"/>
                                             </div>
                                            <div class="col-lg-1" style="text-align:center;"> 
                                                <asp:Button runat="server" ID="BtFinal" Enabled="false" Visible="true" tooltip="Envía al Estado definido cómo finalizado los registros seleccionados" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Finalizar" OnClick="RetrocedeEstado_Click"/>
                                             </div>
                                            <div class="col-lg-3" style="text-align:center;"> 
                                            </div>
                                             <div class="col-lg-1" style="text-align:center;"> 
                                                 <asp:ImageButton id="ImgAyuda" visible="true" Enabled="false" runat="server" ToolTip="Visualiza en pantalla la descripcion del Estado en que se encuentra" ImageAlign="right" Width="30px" Height="30px" ImageUrl="images/ayuda.png" OnClick="BtAyuda_Click"/>
                                             </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>



                            </div>
                            <div class="row"> 
                                 <asp:UpdatePanel ID="UpdatePanelEje" runat="server" UpdateMode="Conditional">      
                                <%--<Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="BtnAcepta" EventName="Click" />
                                </Triggers>--%>
                                    <ContentTemplate>
                                        <div class="col-lg-2" style="text-align:center;"> 
                                            <label runat="server" visible="false"  id="LbimgEstado" title="">Estado</label>
                                            <label runat="server" visible="false"  id="Lbejecutando" title="">Ejecutando...</label>
                                        </div>
                                        <div class="col-lg-2" style="text-align:center;"> 
                                              <label runat="server" visible="false"  id="LbAutomatico" title="">Inicio de proceso automático</label>
                                        </div>
                                        <div class="col-sm-1" >  

                                        </div>
                                        <div class="col-sm-1" >
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                   </div>
               </div>





        

                    <%--Fin filtros--%>

              
               <%-- <div class="panel panel-default">     
                    <div class="panel-heading" runat="server" id="Div21" >

                    </div>
                    <%--filtros Columnas--%>
                   <%-- <div class="panel-body" id="Div35" visible="true" runat="server">
                            <div class="row"> 
                                <div class="col-sm-1" style="text-align:right;" >
                                    <asp:linkbutton id="lbFilCodigo" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Código:"></asp:linkbutton>

                                    <button id="BtCodigo" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="IContent" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2" >
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca uno o más Códigos de Empleado separados por coma. Ejemplo: 00000001, 000000232 "  ID="TxtCodigo"  Font-Bold="True" />
                                </div>

                                <div class="col-sm-2" style="text-align:right;">
                                    <asp:linkbutton id="LinkNombre" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Nombre:"></asp:linkbutton>
                                    <button id="BtNombre" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="INombre" style="color:darkred;" runat="server" title="No deberá contener estos Datos." class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2" >
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca un nombre de trabajador. Ejemplo: Rosa "  ID="TxtNombre"  Font-Bold="True" />
                                </div>
                                <div class="col-sm-2" style="text-align:right;">
                                    <asp:linkbutton id="LinkApellidos" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Apellidos:"></asp:linkbutton>
                                    <button id="BtApellido" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="IApellido" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2" >
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca un apellido de trabajador. Ejemplo: Sanchez"  ID="TxtApellidos"  Font-Bold="True" />
                                </div>
                                    <div class="col-sm-1" >
                                </div>
                            </div>
                        </div>--%>
                    <%--Fin filtros Columnas--%>

<%--                    <div class="panel-body" id="Div36" visible="true" runat="server">
                            <div class="row"> 
                                    
                                <div class="col-sm-1" style="text-align:right;">
                                    <asp:linkbutton id="LinkCentro" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Centro:"></asp:linkbutton>
                                    <button id="BtCentro" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="ICentro" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2" >
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca uno o más Centros de Trabajo separados por coma. Ejemplo: 001, 002 "  ID="TxtCentro"  Font-Bold="True" />
                                </div>
                                <div class="col-sm-2" style="text-align:right;">
                                    <asp:linkbutton id="LinkCategoria" ForeColor="Black"  onClick="lbFilClose_Click" runat="server" Text="Categoría:"></asp:linkbutton>
                                    <button id="BtCategoria" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="ICategoria" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2" >
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca una o más Categorías separadas por coma. Ejemplo: S1, S2, H1"  ID="TxtCategoria"  Font-Bold="True" />
                                </div>
                                <div class="col-sm-2" style="text-align:right;">
                                    <asp:linkbutton id="LinkVivienda" ForeColor="Black" onClick="lbFilClose_Click" runat="server" Text="Vivienda:"></asp:linkbutton>
                                    <button id="BtVivienda" type="button" runat="server" style="width:10%; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="BtContiene_Click"><i id="IVivienda" style="color:darkred;" runat="server" title="Deberá contener estos Datos" class="fa fa-hand-o-up fa-2x"></i></button> 
                                </div>
                                <div class="col-sm-2" >
                                    <asp:TextBox runat="server" CssClass="form-control" Visible="true"  Width="100%" tooltip="Introduzca uno o más Códigos de Empleado separados por coma. Ejemplo: Propia, Casa1 "  ID="TxtVivienda"  Font-Bold="True" />
                                </div>
                                    <div class="col-sm-1" >
                                    </div>
                                </div>
                        </div>
                </div> --%> 

              </div>  

              <%--Menus usuario--%>
            <div id="DivUser" visible="false" runat="server" style="height:600px; width:100%;" class="panel panel-default">
                <br />

                <br />
            </div>  

			 







          
            <%--gvEmpleado--%>
            <div class="panel-heading" runat="server" visible="true" id="PanelCabecera" >

                  <div id="DivAyuda" visible="false" runat="server" style=" height:300px; width:100%; overflow:auto;"  class="col-lg-12">
                        <asp:TextBox ID="LbAyuda" Width="100%" Height="100%"  TextMode="MultiLine" style="Font-Size:20px;" Enabled="false" Columns="50" Rows="100" runat="server"
                            Text="-Flujos y Estados de Trabajo: Un flujo contiene una serie de Estados por los que pasa un registro documental en su ciclo de vida documental.

Los Estados son momentos de ciclo de vida de un registro por el cual debe pasar según el criterio personal de un Usuario o Proceso. Las posibles opciones que tiene cada Estado son las siguientes:

Atrás. Devuelve un registro a su estado anterior.
Adelante: Avanza el registro a su Estado siguiente.
Alternativo: Contiene cualquier otro Estado que se haya definido como una alternativa a un supuesto contemplado incluyendo el salto a otro Flujo de Trabajo diferente.
Fin: Envía el registro a su Estado final en el Flujo de Trabajo
"/>
                    </div> 

                <div id="VistaGrid" visible="true" runat="server"  class="row">
                    <%--gvEmpleado--%>
                    <div class="panel panel-default">  
                                        <br />
                            <div class="row">       
                                 <div class="col-sm-1" >
                                      <asp:Label  type="text"  Width="20%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label4"  Text="Orden:" > </asp:Label>
                                      <button id="BtnAsc" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnAsc_Click"><i title="Orden ascendente de la columna seleccionada" class="fa fa-sort-alpha-asc fa-2x"></i></button>
                                 </div>
                                 <div class="col-lg-1" >
                                     <asp:DropDownList ID="DrListaColumna" style=" position:absolute;" CssClass="form-control" Width="85%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListaColumnas_SelectedIndexChanged"> </asp:DropDownList>
                                 </div>

                                 <div class="col-sm-1" style="text-align:right;">
                                        <button id="BtnDesc" type="button" runat="server" visible="true" style=" position: absolute; border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnDesc_Click"><i title="Orden descendente de la columna seleccionada" class="fa fa-sort-alpha-desc fa-2x"></i></button>
                                        <asp:Label  type="text"  Width="100%" style=" text-align:right; font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label5"  Text="Filas:" > </asp:Label>
                                </div>



<%--                                <div class="col-sm-1" >
                                    <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label19"  Text="Filas:" > </asp:Label>
                                </div>--%>
                                <div class="col-sm-1" >                              
                                    <asp:DropDownList ID="ddCabeceraPageSize" style=" position:absolute;" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvEmpleado_PageSize_Changed">  
                                    </asp:DropDownList>  
                                    </div>
                                <div class="col-sm-5" >
                                </div>

                                <div class="col-sm-2" >
                                </div>
                                <div class="col-sm-1"> 
                                    <button id="BtPrintReport" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted " onserverClick="btDriver_Click">
                                        <i title="Descarga el driver para tabletas de firma WACOM" class="fa fa-cog fa-2x"></i>
                                    </button>
                                    <button id="btPrintCabecera" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnSDK_Click"><i title="Descarga SDK para tabletas de firma WACOM" class="fa fa-cogs fa-2x"></i></button>
                                    <button id="BtnDescarFirma" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnSing_Click"><i title="Descarga formulario de firma para tabletas WACOM" class="fa fa-tablet fa-2x"></i></button>

                                </div>
                            </div>
                            <div class="row">

                            <asp:UpdatePanel ID="UpdatePanelGV" runat="server" UpdateMode="Conditional">      
                            <%--<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnAcepta" EventName="Click" />
                            </Triggers>--%>
                            <ContentTemplate>
                                    <div id="Div1" runat="server" style="overflow:auto;" class="panel-body"> <%-- AutoGenerateSelectButton="True"--%> 
                                        <%--Automatico--%>
                                        <asp:GridView ID="gvEmpleado"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                            AllowSorting="true" OnSorting="gvEmpleado_OnSorting"
                                            CellPadding="4"  GridLines="None"  OnRowDataBound="gvEmpleado_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="5" OnRowCommand="gvEmpleado_RowCommand" DataKeyNames="ZID"
                                            oonselectedindexchanged="gvEmpleado_SelectedIndexChanged"  OnRowEditing="gvEmpleado_RowEditing" OnRowCancelingEdit="gvEmpleado_RowCancelingEdit" OnRowUpdating="gvEmpleado_RowUpdating" 
                                            OnRowDeleting="gvEmpleado_RowDeleting" onpageindexchanging="gvEmpleado_PageIndexChanging"  >
                                        <RowStyle />                     
                                            <Columns>

                                             <asp:TemplateField HeaderText="Todos">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkb1Empleado" ToolTip="Selecciona o no todos los Documentos leídos por el Empleado." runat="server" OnCheckedChanged="sellectAllEmpleado"
                                                        AutoPostBack="true" />
                                                </HeaderTemplate>
                                                    <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                    <itemtemplate>
                                                        <asp:CheckBox ID="chbItemEmpleado" runat="server" />
                                                    </itemtemplate>
                                                    <itemstyle horizontalalign="center" />
                                                </asp:TemplateField>

                                          <%-- <asp:TemplateField HeaderText="Estados Flujo"  SortExpression="ZID">
                                               <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                        <itemtemplate>
                                                        <a id="linkLee" href= "Download.ashx?file=images/cat1.jpg">
                                                            <asp:ImageButton ID="ibtLeeDoc" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/leer.png"         
                                                            CommandName="LeeDoc" ToolTip="Apertura del Registro" Width="30px" Height="30px"></asp:ImageButton>
                                                        </a>
                                                    </ItemTemplate>
                                               <ItemStyle Height="8px"></ItemStyle>   
                                            </asp:TemplateField>--%>

                                   


                                            </Columns>
                                            <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
                                            <EditRowStyle BackColor="#eaf5dc" />                    
                                        </asp:GridView>                    
                                


                                        <%--Manual--    %>
                                      <%--   <asp:GridView ID="gvEmpleado"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                            AllowSorting="true" OnSorting="gvEmpleado_OnSorting"
                                            CellPadding="4"  GridLines="None"  OnRowDataBound="gvEmpleado_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="15" OnRowCommand="gvEmpleado_RowCommand" DataKeyNames="ZID"
                                            oonselectedindexchanged="gvEmpleado_SelectedIndexChanged"  OnRowEditing="gvEmpleado_RowEditing" OnRowCancelingEdit="gvEmpleado_RowCancelingEdit" OnRowUpdating="gvEmpleado_RowUpdating" 
                                            OnRowDeleting="gvEmpleado_RowDeleting" onpageindexchanging="gvEmpleado_PageIndexChanging"  >
                                        <RowStyle />                     
                                            <Columns>
                                           <asp:TemplateField HeaderText="Estados Flujo"  SortExpression="ZID">
                                               <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                        <itemtemplate>
                                                        <a id="linkLee" href= "Download.ashx?file=images/cat1.jpg">
                                                            <asp:ImageButton ID="ibtLeeDoc" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' runat="server" ImageUrl="~/Images/leer.png"         
                                                            CommandName="LeeDoc" ToolTip="Apertura del Documento para ser leído" Width="30px" Height="30px"></asp:ImageButton>
                                                        </a>
                                                    </ItemTemplate>
                                               <ItemStyle Height="8px"></ItemStyle>   
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Identificador" visible="true" SortExpression="ZID">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="Tabid"  runat="server" Text='<%# Eval("ZID") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="Labid"  runat="server" Text='<%# Bind("ZID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Codigo Trabajador" visible="true" SortExpression="CodigoTrabajador">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabCodigoTrabajador"  runat="server" Text='<%# Eval("CodigoTrabajador") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabCodigoTrabajador"  runat="server" Text='<%# Bind("CodigoTrabajador") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nombre" visible="true" SortExpression="Nombre">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabNombre"  runat="server" Text='<%# Eval("Nombre") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabNombre"  runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Apellido1" visible="true" SortExpression="Apellido1">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabApellido1"  runat="server" Text='<%# Eval("Apellido1") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabApellido1"  runat="server" Text='<%# Bind("Apellido1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Apellido2" visible="true" SortExpression="Apellido2">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabApellido2"  runat="server" Text='<%# Eval("Apellido2") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabApellido2"  runat="server" Text='<%# Bind("Apellido2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Abreviatura" visible="false" SortExpression="Abreviatura">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabAbreviatura"  runat="server" Text='<%# Eval("Abreviatura") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabAbreviatura"  runat="server" Text='<%# Bind("Abreviatura") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="NIF" visible="true" SortExpression="NIF">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabNIF"  runat="server" Text='<%# Eval("NIF") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabNIF"  runat="server" Text='<%# Bind("NIF") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Foto" visible="false" SortExpression="Foto">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabFoto"  runat="server" Text='<%# Eval("Foto") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabFoto"  runat="server" Text='<%# Bind("Foto") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email" visible="true" SortExpression="Email">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabEmail"  runat="server" Text='<%# Eval("Email") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabEmail"  runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contador" visible="false" SortExpression="Contador">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabContador"  runat="server" Text='<%# Eval("Contador") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabContador"  runat="server" Text='<%# Bind("Contador") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FechaInicio" visible="true" SortExpression="FechaInicio">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabFechaInicio"  runat="server" Text='<%# Eval("FechaInicio") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabFechaInicio"  runat="server" Text='<%# Bind("FechaInicio") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                                 <asp:TemplateField HeaderText="FechaFin" visible="true" SortExpression="FechaFin">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabFechaFin"  runat="server" Text='<%# Eval("FechaFin") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabFechaFin"  runat="server" Text='<%# Bind("FechaFin") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="CodigoContrato" visible="true" SortExpression="CodigoContrato">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabCodigoContrato"  runat="server" Text='<%# Eval("CodigoContrato") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabCodigoContrato"  runat="server" Text='<%# Bind("CodigoContrato") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="EsIndefinido" visible="false" SortExpression="EsIndefinido">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabEsIndefinido"  runat="server" Text='<%# Eval("EsIndefinido") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabEsIndefinido"  runat="server" Text='<%# Bind("EsIndefinido") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="EsTiempoParcial" visible="false" SortExpression="EsTiempoParcial">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabEsTiempoParcial"  runat="server" Text='<%# Eval("EsTiempoParcial") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabEsTiempoParcial"  runat="server" Text='<%# Bind("EsTiempoParcial") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="ModeloContrato" visible="true" SortExpression="ModeloContrato">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabModeloContrato"  runat="server" Text='<%# Eval("ModeloContrato") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabModeloContrato"  runat="server" Text='<%# Bind("ModeloContrato") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        
                                                 <asp:TemplateField HeaderText="DatosPDF" visible="false" SortExpression="DatosPDF">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabDatosPDF"  runat="server" Text='<%# Eval("DatosPDF") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabDatosPDF"  runat="server" Text='<%# Bind("DatosPDF") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="DocumentosAdjuntos" visible="true" SortExpression="DocumentosAdjuntos">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabDocumentosAdjuntos"  runat="server" Text='<%# Eval("DocumentosAdjuntos") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabDocumentosAdjuntos"  runat="server" Text='<%# Bind("DocumentosAdjuntos") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Estado Flujo" visible="true" SortExpression="ZID_ESTADO">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabZESTADO"  runat="server" Text='<%# Eval("ZID_ESTADO") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabZESTADO"  runat="server" Text='<%# Bind("ZID_ESTADO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Ruta Empleado" visible="true" SortExpression="ZRUTA">
                                                <EditItemTemplate>
                                                        <asp:TextBox ID="TabRuta"  runat="server" Text='<%# Eval("ZRUTA") %>' class="gridListAinput"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                        <asp:Label ID="LabRuta"  runat="server" Text='<%# Bind("ZRUTA") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            </Columns>
                                            <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
                                            <EditRowStyle BackColor="#eaf5dc" />                    
                                        </asp:GridView>                  --%>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>      
                        </div>
                    </div>

                    </div>
                </div>
                <%--final gvEmpleado--%>
          </div>
        <%--Final page_wrapper--%>



                    

             <script type="text/javascript">
              function drag(ev) {
                  ev.dataTransfer.setData("text", ev.target.id);
              }

              function allowDrop(ev) {
                  ev.preventDefault();
              }

              function drop(ev) {
                  ev.preventDefault();
                  var data = ev.dataTransfer.getData("text");
                  ev.target.appendChild(document.getElementById(data));
                  //document.getElementById('Btn' + data.id).click();
              }

              function submdrag0() {
                  document.getElementById("LbPosicionCamion").textContent = 'drag0';
                  var elem = document.getElementById('drag0');
                  document.getElementById('Btndrag0').click();
              }
              function submdrag1() {
                  document.getElementById("LbPosicionCamion").textContent = 'drag1';
                  var elem = document.getElementById('drag1');
                  document.getElementById('Btndrag1').click();
              }
              function submdrag2() {
                  document.getElementById("LbPosicionCamion").textContent = 'drag2';
                  var elem = document.getElementById('drag2');
                  document.getElementById('Btndrag2').click();
              }
              function submdrag3() {
                  document.getElementById("LbPosicionCamion").textContent = 'drag3';
                  var elem = document.getElementById('drag3');
                  document.getElementById('Btndrag3').click();
              }

              function submitit() {
                  document.getElementById('btn').click();
              }
              function submititTodo() {
                  document.getElementById('Button1').click();
              }

              function submititCab() {
                  document.getElementById('btnCaCheck').click();
              }           
     
              function submitit1() {
                  document.getElementById('btn1').click();
              }

              function submitit2() {
                  document.getElementById('btn2').click();
              }

              function submitit3() {
                  document.getElementById('btn3').click();
              }

              function submiti4() {
                  document.getElementById('btn4').click();
              }

              function submitit5() {
                  document.getElementById('btn5').click();
              }

              function submitit6() {
                  document.getElementById('btn6').click();
              }

              function submitAnchor() {
                  document.getElementById('btnAnchor').click();
              }
              function submitit8() {
                  document.getElementById('btUltimo').click();
              }

              function PrintGrid(html, css) {
                  var printWin = window.open('', '', 'left=0,top=0,width=400,height=300,scrollbars=1');
                  printWin.document.write('<style type = "text/css">' + css + '</style>');
                  printWin.document.write(html);
                  printWin.document.close();
                  printWin.focus();
                  printWin.print();
                  printWin.close();
              }

              function doPrintGRCabecera() {
                  var prtContent = document.getElementById('<%= gvEmpleado.ClientID %>');
                  //prtContent.border = 0; //set no border here
                  var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
                  WinPrint.document.write(prtContent.outerHTML);
                  WinPrint.document.close();
                  WinPrint.focus();
                  WinPrint.print();
                  WinPrint.close();
              }

              <%--function doPrintGRControl() {
                  var prtContent = document.getElementById('<%= gvProduccion.ClientID %>');
                  //prtContent.border = 0; //set no border here
                  var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
                  WinPrint.document.write(prtContent.outerHTML);
                  WinPrint.document.close();
                  WinPrint.focus();
                  WinPrint.print();
                  WinPrint.close();
              }

              function doPrintGRLista() {
                  var prtContent = document.getElementById('<%= gvJornada.ClientID %>');
                  //prtContent.border = 0; //set no border here
                  var WinPrint = window.open('', '', 'left=100,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
                  WinPrint.document.write(prtContent.outerHTML);
                  WinPrint.document.close();
                  WinPrint.focus();
                  WinPrint.print();
                  WinPrint.close();
              }--%>

              function Cargando() {
                  document.getElementById("carga").style.display = "";
              }
              windows.onbeforeunload = Cargando;

             </script>

        <!-- jQuery Version 1.11.0 -->
    <script src="js/jquery-1.11.0.js"></script>
 
    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>
    
    <!-- Metis Menu Plugin JavaScript -->
    <script src="js/plugins/metisMenu/metisMenu.min.js"></script>

    <!-- Morris Charts JavaScript -->
 <%--     <script src="js/plugins/morris/raphael.min.js"></script>
  <script src="js/plugins/morris/morris.min.js"></script>
    <script src="js/plugins/morris/morris-data.js"></script>--%>

    <!-- Custom Theme JavaScript Menu derecha correcto -->
    <script src="js/sb-admin-2.js"></script>
</asp:Content>
