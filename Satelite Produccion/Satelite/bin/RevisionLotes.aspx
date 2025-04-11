<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="RevisionLotes.aspx.cs" Inherits="Satelite.RevisionLotes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div id="pagevistaform" runat="server"><!-- /#page-wrapper -->

<%--            <asp:UpdateProgress ID="Progress" runat="server" AssociatedUpdatePanelID="Update">
                <ProgressTemplate>
                    <div class="centrado" style="z-index:1100;">
                        <img src="images/loading-buffering.gif" alt="placeholder" style="border: 0px; overflow:auto;" /> 
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>

                 <div class="windowmessaje" visible="false" runat="server" id="windowmessaje">
			        <div class="contenedormessaje">
				        <div class="content-text">
				        <asp:Label runat="server"  ID="Lbmensaje" BorderStyle="None" border="0" Width="100%" Text=""/>
				        <%-- <a href="#">Leer más.</a>--%>

				        </div>
				        <%--<div class="content-buttons"><a href="#" id="close-button">Aceptar</a></div>--%>
				        <br />
				        <br />
					        <div class="row" id="Asume" visible="true" runat="server">
						        <div class="col-lg-3">
					        </div>
						        <div class="col-lg-6">
						        <asp:Button runat="server" ID="btnasume" Visible="true" tooltip="Aceptar" CssClass="btn btn-success btn-block" Width="100%"  Text="Aceptar" OnClick="checkOk_Click"/>
						        </div>
						        <div class="col-lg-3">
						        </div>
					        </div>
					        <div class="row" id="cuestion" visible="false" runat="server">
						        <div class="col-lg-3">
						        </div>
					        <div class="col-lg-6">
						        <asp:Button runat="server" ID="BtnAcepta" Visible="true" tooltip="Aceptar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Aceptar" OnClick="checkSi_Click"/>                                                    
						        </div>
					        <div class="col-lg-6">
						        <asp:Button runat="server" ID="BTnNoAcepta" Visible="true" tooltip="Cancelar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Cerrar" OnClick="checkNo_Click"/>                 
					        </div>
						        <div class="col-lg-3">
					        </div>
				        </div>
			        </div>
		        </div>




<%--          <div runat="server" id="DvPreparado" visible="false"  style=" width: 30%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);" class="alert alert-grey centrado">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <i id="I2" runat="server" class="fa fa-exclamation-circle fa-2x"></i>
                <asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="Lbmensaje" BorderStyle="None" border="0" Width="100%" Text=" Se eliminarán los registros seleccionados ¿Desea continuar?"  />
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
            </div>--%>

          <asp:Label  type="text" Width="100%" style=" font-weight: bold; font-size:24px;"  runat="server" ID="Lberror"  Text="" > </asp:Label>


    <div runat="server" id="alerta" visible="false" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <%--<a href="#" id="alerT" runat="server" class="alert alert-info alert-dismissable">--%>
                    <i id="IAlert" runat="server" class="fa fa-exclamation-circle"></i>
                    <asp:Label runat="server" class="alert alert-info alert-dismissable" ID="TextAlerta" BorderStyle="None" border="0" Width="100%" Text=""  />
                <%--</a> --%> 
                <%--<asp:Button runat="server" ID="btProcesa" tooltip="Busca si el Lote tiene registro de Formulario Scan-IT" visible="false" CssClass="btn btn-success btn-block" Width="100%"  Text="Procesar Lote" OnClick="btnPorcesa_Click"/>--%>
                <asp:Button ID="btnPrint2" runat="server" tooltip="Lote con Formulario Scan-IT para imprimir con el total de su información" visible="false" CssClass="btn btn-success " Width="100%" Text="Imprimir código QR" OnClientClick="return PrintPanel();" />



    </div>
        <div runat="server" id="alertaLog" visible="false" class="alert alert-warning alert-dismissable">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <%--<a href="#" id="a1" runat="server" class="alert alert-warning alert-dismissable">--%>
                    <i id="I1" runat="server" class="fa fa-exclamation-circle"></i>
                    <asp:Label runat="server" class="alert alert-warning alert-dismissable" ID="TextAlertaLog" BorderStyle="None" border="0" Width="100%" Text=""  />
                <%--</a>--%>  
                <%--<asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label9"  Text="Usuario:" > </asp:Label>
                <asp:TextBox ID="TextUser"  style="text-align:center;  font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>
                <asp:Label  type="text" Width="100%" style=" font-weight: bold;"  runat="server" ID="Label10"  Text="Password:" > </asp:Label>
                <asp:TextBox ID="TextPass"  TextMode="Password" style="text-align:center; font-weight: bold;" Text="" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox>--%>
            <br />      
            <asp:Button runat="server" ID="Btvalidauser" tooltip="Se eliminará el registro seleccionado" CssClass="btn btn-danger btn-block" Width="100%"  Text="¿Está seguro de eliminar este registro?" OnClick="btnValidaUser_Click"/>
    </div>

    <div runat="server" id="alertaErr" visible="false" class="alert alert-danger alert-dismissable">
                <button type="button" class="close" data-dismiss="alert">&times;</button> 
                <%--<a href="#" id="alerTErr" runat="server" class="alert alert-danger alert-dismissable">--%>
                <i id="IAlertErr" runat="server" class="fa fa-exclamation-circle"></i>
                <asp:Label runat="server" class="alert alert-danger alert-dismissable" ID="TextAlertaErr" BorderStyle="None" border="0" Width="100%" Text=""  />
                <%--</a>--%>   
                <%--<asp:Button runat="server" ID="btPorcesa" tooltip="Busca si el Lote tiene registro de Formulario Scan-IT" visible="false" CssClass="btn btn-success btn-block" Width="100%"  Text="Procesar Lote" OnClick="btnPorcesa_Click"/>--%>
        </div>





                        <%--aqui iban los lotes escaneados y procesados --%> 





        <div class="row">
            <div class="col-lg-12">
                  <div class="panel panel-default">
                    <div class="panel-heading" style="height:50px;">
                    <h3 class="panel-title" ><i class="fa fa-long-arrow-right"></i> Entradas del Formulario seleccionado:
                         <button id="btPrintCabecera" type="button" runat="server" visible="true" style=" border-style:none; background-color:transparent;" class="pull-right text-muted "  onserverClick="btnPrint_Click"><i title="Exporta a Excel la vista previa presentada en pantalla según Filtro" class="fa fa-file-excel-o fa-2x"></i></button>
                    <%--<button id="BiRestore" type="button" runat="server" class="pull-right text-muted "  onserverClick="btnRestore_Click"><i title="El Lote seleccionado se restaura a su estado anterior, de finalizado a procesado" class="fa fa-undo"></i></button>--%>
                                
                    </h3>                               
                    </div>
                    <div class="panel-body">
            
                     <div class="row">       
                            <div class="col-lg-2" >
                                <asp:Label  type="text" Width="100%" visible="false" style=" font-weight: bold;"  runat="server" ID="LbIDLote"  Text="" > </asp:Label>
                                <asp:Label  type="text" Width="80%" style=" font-weight: bold;"  runat="server" ID="LbLote"  Text="Tipo Formulario:" > </asp:Label>
                                   
                            </div>
                            <div class="col-lg-3" >
                                <%--<asp:UpdatePanel ID="Update" runat="server" UpdateMode="Conditional">      
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="DrVariedad" EventName="selectedindexchanged" />
                                    </Triggers>
                                    <ContentTemplate>--%>
                                            <asp:DropDownList runat="server" CssClass="form-control"  Width="100%" tooltip="Seleccione un Tipo de Lote para filtrar Lotes"  ID="DrVariedad"  OnSelectedIndexChanged="DrVariedad_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="True" Font-Bold="True" />
                                   <%-- </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </div>
                            
                            <div class="col-sm-4" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:center; color:olivedrab ; padding:6px;"  runat="server" ID="lbRowEntrada"  Text="" > </asp:Label>
                            </div>
                             <div class="col-lg-1" >
                                    <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align: right; padding:6px;"  runat="server" ID="Label1"  Text="Estados:" > </asp:Label>
                             </div>
                            <div class="col-sm-2" >
                                <%--<asp:UpdatePanel ID="Update1" runat="server" UpdateMode="Conditional">      
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="dtEntrada" EventName="selectedindexchanged" />
                                    </Triggers>
                                    <ContentTemplate>--%>
                                        <asp:DropDownList ID="dtEntrada" style=" position:absolute;" CssClass="form-control" Width="90%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dtEntrada_SelectedIndexChanged"></asp:DropDownList> 
                               <%--     </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </div> 
                             <%--<div class="col-sm-1" >    
                               <div data-tip="Muestra los enviados a GoldenSoft con Estado a 2">
                                    <label runat="server" visible="true" tooltip="Muestra los enviados a GoldenSoft" id="LBCheck" class="switch pull-right">
                                        <input runat="server" onclick="submitit()" id="chkOnOff" data-toggle="tooltip" data-placement="top"  title="Muestra los enviados a GoldenSoft con Estado a 2"  type="checkbox"/><span class="slider round"></span>
                                        <asp:Button ID="btn" runat="server" OnClick="checkListas_Click" Height="0" Width="0" CssClass="hidden" />
                                    </label> 
                                </div>
                            </div>    --%>                                
                    
                     </div>
                     <br />
                     <div class="row">  

                         <div class="col-sm-1" >
                                <asp:Label  type="text"  Width="100%" style=" font-weight: bold;text-align:right; padding:6px;"  runat="server" ID="Label19"  Text="Filas:" > </asp:Label>
                        </div>
                        <div class="col-sm-1" >                              
                            <asp:DropDownList ID="ddEntradaPageSize" style=" position:absolute;" CssClass="form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="gvEntrada_PageSize_Changed">  
                            </asp:DropDownList>  
                        </div>
                        <%--boton con solo  border color Style=" background-color:#ffffff;border-radius: 6px;border: 2px solid #12be20;"--%>
                         <div class="col-lg-2" >
                         </div>
                        <%--<div class="col-lg-2" >
                             <asp:Button runat="server" ID="BtFinalizalote" Visible="true" tooltip="Finaliza sólo este Lote del formulario seleccionado" CssClass="btn btn-success btn-block" Width="100%"  Text="Finaliza este Lote" OnClick="BtFinalizalote_Click"/>  CssClass="btn btn-warning btn-block"                              
                         </div>--%>
                         <div class="col-lg-2" >
                              <asp:Button runat="server" ID="BtFinalizaTodos" Visible="true" tooltip="Finaliza Lotes seleccionados con el check " CssClass="btn btn-warning btn-block"  Width="100%"  Text="Finalizar Lotes" OnClick="BtFinalizaTodos_Click"/>                              
                               <asp:Button runat="server" ID="BtEnviaFinalizados" Visible="false" tooltip="Envia Lotes seleccionados finalizados a GoldenSoft." CssClass="btn btn-info btn-block"  Width="100%"  Text="Enviar Lotes seleccionados" OnClick="BtEnviaFinalizados_Click"/>                              
                         </div>
                        <div class="col-lg-1" >
                        </div>
                         <div class="col-lg-2" >
                               <asp:Button runat="server" ID="BTerminado" visible="false" tooltip="Revertir él o los Lotes seleccionados con el check a estado anterior" CssClass="btn btn-success btn-block"  Width="100%"  Text="Revertir Lotes" OnClick="BTerminado_Click"/>
                        </div>
                        <div class="col-lg-1" >
                        </div>
                        <div class="col-lg-2" >
                             <asp:Button runat="server" ID="Btfin" visible="true" tooltip="Elimina él o los Lotes seleccionados con el check" CssClass="btn btn-danger btn-block" Width="100%"  Text="Eliminar Lotes" OnClick="BTfin_Click"/>
                        </div>
                         
                    </div>
                    <br />

                    <div class="row" id="DivGrid" style="width: 100%; height: 600px; overflow: auto;" runat="server">
                        <div class="col-lg-12" style="overflow: auto;">

                                        <asp:GridView ID="gvEntrada"  runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-condensed table-responsive table-hover " 
                                                AllowSorting="true" OnSorting="gvEntrada_OnSorting" 
                                                CellPadding="4"  GridLines="None"  OnRowDataBound="gvEntrada_OnRowDataBound"  width="100%" AllowPaging="True" PageSize="10" OnRowCommand="gvEntrada_RowCommand" DataKeyNames="ID"
                                                oonselectedindexchanged="gvEntrada_SelectedIndexChanged"  OnRowEditing="gvEntrada_RowEditing" OnRowCancelingEdit="gvEntrada_RowCancelingEdit" OnRowUpdating="gvEntrada_RowUpdating" 
                                                OnRowDeleting="gvEntrada_RowDeleting" onpageindexchanging="gvEntrada_PageIndexChanging"  >


                                        <RowStyle /> 
                                            <Columns>
                                     
                                             <asp:CommandField ButtonType="Image" 
                                                            EditImageUrl="~/Images/editar20x20.png" 
                                                            ShowEditButton="True" 
                                                            CancelImageUrl="~/Images/cancelar20x20.png" 
                                                            CancelText="" 
                                                            DeleteText="" 
                                                            UpdateImageUrl="~/Images/guardar18x18.png"          
                                                            UpdateText="" />          

                                                <asp:TemplateField HeaderText="Selección Todos">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkb1" ToolTip="Actualiza la visualización de todos los registros para poder seleccionarlos desde pantalla." runat="server" OnCheckedChanged="sellectAll"
                                                        AutoPostBack="true" />
                                                </HeaderTemplate>
                                                 <itemstyle horizontalalign="Center" verticalalign="Middle" />
                                                    <itemtemplate>
                                                        <asp:CheckBox ID="chbItem" runat="server" />
                                                    </itemtemplate>
                                                    <itemstyle horizontalalign="center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ID" Visible="false" >
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabID" Visible="false" runat="server" Text='<%# Eval("ID") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabID" Visible="false" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo Formulario" SortExpression="TIPO_FORM">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabTIPOFORM" runat="server" Text='<%# Eval("TIPO_FORM") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabTIPOFORM" runat="server" Text='<%# Bind("TIPO_FORM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha" SortExpression="FECHA">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabFECHA" runat="server" Text='<%# Eval("FECHA") %>' class="gridListDinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabFECHA" runat="server" Text='<%# Bind("FECHA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo Planta" SortExpression="TIPO_PLANTA">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabTIPOPLANTA" runat="server" Text='<%# Eval("TIPO_PLANTA") %>' class="gridListDinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabTIPOPLANTA" runat="server" Text='<%# Bind("TIPO_PLANTA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Variedad" SortExpression="VARIEDAD">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabVARIEDAD" runat="server" Text='<%# Eval("VARIEDAD") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabVARIEDAD" runat="server" Text='<%# Bind("VARIEDAD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lote" SortExpression="LOTE">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabLOTE" runat="server" Text='<%# Eval("LOTE") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabLOTE" runat="server" Text='<%# Bind("LOTE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lote Destino" SortExpression="LOTEDESTINO">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabLOTEDESTINO" runat="server" Text='<%# Eval("LOTEDESTINO") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabLOTEDESTINO" runat="server" Text='<%# Bind("LOTEDESTINO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unidades" SortExpression="UNIDADES">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabUNIDADES" runat="server" Text='<%# Eval("UNIDADES") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabUNIDADES" runat="server" Text='<%# Bind("UNIDADES") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Num. Unidades" SortExpression="NUM_UNIDADES">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabNUM_UNIDADES" runat="server" Text='<%# Eval("NUM_UNIDADES") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabNUM_UNIDADES" runat="server" Text='<%# Bind("NUM_UNIDADES") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Manojos" SortExpression="MANOJOS">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabMANOJOS" runat="server" Text='<%# Eval("MANOJOS") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabMANOJOS" runat="server" Text='<%# Bind("MANOJOS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Desde" SortExpression="DESDE">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabDESDE" runat="server" Text='<%# Eval("DESDE") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabDESDE" runat="server" Text='<%# Bind("DESDE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hasta" SortExpression="HASTA">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabHASTA" Width="100%" runat="server" Text='<%# Eval("HASTA") %>' class="gridListDinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabHASTA" Width="100%" runat="server" Text='<%# Bind("HASTA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        

                                                <asp:TemplateField HeaderText="Etiq. desde" SortExpression="ETDESDE">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabETDESDE" runat="server" Text='<%# Eval("ETDESDE") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabETDESDE" runat="server" Text='<%# Bind("ETDESDE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Etiq. hasta" SortExpression="ETHASTA">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabLETHASTA" Width="100%" runat="server" Text='<%# Eval("ETHASTA") %>' class="gridListDinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabLETHASTA" Width="100%" runat="server" Text='<%# Bind("ETHASTA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                        
                                                <asp:TemplateField HeaderText="Tuneles" SortExpression="TUNELES">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabTUNELES" runat="server" Text='<%# Eval("TUNELES") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabTUNELES" runat="server" Text='<%# Bind("TUNELES") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pasillos" SortExpression="PASILLOS">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabPASILLOS" runat="server" Text='<%# Eval("PASILLOS") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabPASILLOS" runat="server" Text='<%# Bind("PASILLOS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OK" SortExpression="OK">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabOK" runat="server" Text='<%# Eval("OK") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabOK" runat="server" Text='<%# Bind("OK") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Observaciones" SortExpression="OBSERVACIONES">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabOBSERVACIONES" runat="server" Text='<%# Eval("OBSERVACIONES") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabOBSERVACIONES" runat="server" Text='<%# Bind("OBSERVACIONES") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dispositivo" SortExpression="DeviceName" >
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabDeviceName" runat="server" Text='<%# Eval("DeviceName") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabDeviceName" runat="server" Text='<%# Bind("DeviceName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Fecha Envío" SortExpression="SendTime">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabSendTime" runat="server" Text='<%# Eval("SendTime") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabSendTime" runat="server" Text='<%# Bind("SendTime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha recepción" SortExpression="ReceiveTime">
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabReceiveTime" runat="server" Text='<%# Eval("ReceiveTime") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabReceiveTime" runat="server" Text='<%# Bind("ReceiveTime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Estado" SortExpression="ESTADO" >
                                                    <EditItemTemplate>
                                                            <asp:TextBox ID="TabLEstado" runat="server" Text='<%# Eval("ESTADO") %>' class="gridListAinput"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                            <asp:Label ID="LabLEstado" runat="server" Text='<%# Bind("ESTADO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>

                                            <SelectedRowStyle BackColor="#eaf5dc" Font-Bold="true" />
                                            <EditRowStyle BackColor="#eaf5dc" />   
                                            <rowstyle Height="20px" />
                                        </asp:GridView>  


                      <%--             </div>
                                    <div id="DivFooterRow" style="overflow:hidden">
                                    </div>
                                </div>--%>
                                
                        </div>
                     </div>
                  </div>
               </div>
            </div>
        </div>
    </div><!-- /#page-wrapper -->
        
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
               <%--function PrintPanel() {
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
               }--%>

               function submitit() {
                   document.getElementById('btn').click();
               }

           </script>
</asp:Content>
