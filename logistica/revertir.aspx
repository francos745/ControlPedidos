<%@ Page culture="es-MX" UICulture="es-MX"  Language="VB" AutoEventWireup="false" CodeFile="revertir.aspx.vb" Inherits="logistica_revertir" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reversión de Pedidos</title>
       <meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
    <meta charset="utf-8"/>
    <link rel="shortcut icon" href="../img/clever.png" />
    <meta name="generator" content="Bootply" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"/>
    
    
    <link href="../css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../css/responsive.bootstrap.min.css" rel="stylesheet" />

    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
    <link href="../css/styles.css" rel="stylesheet"/>
    <link href="../css/sumoselect.css" rel="stylesheet" />
    <link href="../css/Estilos.css" rel="stylesheet" />
    <!-- Include Date Range Picker -->
    <link href="../css/daterangepicker.css" rel="stylesheet"  />
    
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery.dataTables.min.js"></script> 
    <script src="../js/jquery.sumoselect.js"></script>
    <script src="../js/dataTables.bootstrap.min.js"></script>
    <script src="../js/moment.min.js"></script>
    <!-- Include Date Range Picker -->
    <script src="../js/daterangepicker.js"></script>

    
    <script type="text/javascript">
        function pageLoad(sender, args) {
            window.asd = $('.SlectBox').SumoSelect({ csvDispCount: 6 });
            window.test = $('.testsel').SumoSelect({okCancelInMulti:true });
            window.testSelAll = $('.testSelAll').SumoSelect({okCancelInMulti:true, selectAll:true });
            window.testSelAlld = $('.SlectBox-grp').SumoSelect({okCancelInMulti:true, selectAll:true });

            window.testSelAll2 = $('.testSelAll2').SumoSelect({selectAll:true });
           

            window.Search = $('.search-box').SumoSelect({ csvDispCount: 3, search: true, searchText:'Ingrese aquí.' });
            window.searchSelAll = $('.search-box-sel-all').SumoSelect({ csvDispCount: 3, selectAll:true, search: true, searchText:'Ingrese aquí.', okCancelInMulti:true });
            window.searchSelAll = $('.search-box-open-up').SumoSelect({ csvDispCount: 3, selectAll:true, search: true, searchText:'Ingrese aquí.', up:true });

            window.groups_eg_g = $('.groups_eg_g').SumoSelect({selectAll:true, search:true });
       

            $('input[name="txtFecha"]').daterangepicker(
      {
          locale: {
              format: 'DD-MM-YYYY'
          },
          showDropdowns: true,
          singleDatePicker: true

      }

      );

            $(document).ready(function () {
                $('#dtgDetalle').dataTable({

                    "lengthMenu": [[-1, 5, 10, 15], ["Todos", 5, 10, 15]],
                    "responsive": true,
                    "stateSave": true,
                    "paging": true,
                    "ordering": true,
                    "info": true,

                    "language": {
                        "emptyTable": "Sin información disponible",
                        "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
                        "infoEmpty": "Mostrando 0 to 0 de 0 registros",
                        "infoFiltered": "(filtered from _MAX_ total entries)",
                        "lengthMenu": "Mostrar _MENU_ registros",
                        "loadingRecords": "Cargando...",
                        "processing": "Procesando...",
                        "search": "Buscar:",
                        "zeroRecords": "No se encontraron registros",
                        "paginate": {
                            "first": "Primera",
                            "last": "Última",
                            "next": "Siguiente",
                            "previous": "Anterior"
                        },
                    }
                });
            });
        }
    </script>

    <style type="text/css">
        body{font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;color:#444;font-size:13px;}
        p,div,ul,li{padding:0px; margin-right: 0px;
            margin-top: 0px;
            margin-bottom: 0px;
        }
    </style>


  
    <%--<link href="../css/highlight.css" rel="stylesheet"/>
    <link href="../css/bootstrap-switch.css" rel="stylesheet"/>
    <link href="http://getbootstrap.com/assets/css/docs.min.css" rel="stylesheet"/>
    <link href="../css/main.css" rel="stylesheet"/>

    <script type="text/javascript" src="js/highlight.js">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent)
    </script>
    
    <script src="../js/bootstrap-switch.js">Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent)</script>
    <script src="../js/main.js">Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent)</script>--%>

    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            font-family: Arial;
        }
        .modals
        {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
        .centers
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        .center img
        {
            height: 128px;
            width: 128px;
        }
    </style>

    

</head>


<body>
    
    <div class="container-fluid text-center">
        <form class="form-horizontal" role="form" runat="server">

           <div id="contNavbar" runat="server">
    <nav class="navbar navbar-default" style="background-color: #fff;" role="navigation">
    <!-- El logotipo y el icono que despliega el menú se agrupan
       para mostrarlos mejor en los dispositivos móviles -->
        <div class="navbar-header">
            
            <a class="navbar-brand" href="#"><strong>HISTÓRICO</strong></a>         
        </div>
 
  <!-- Agrupar los enlaces de navegación, los formularios y cualquier
       otro elemento que se pueda ocultar al minimizar la barra -->
        <div class="collapse navbar-collapse navbar-ex1-collapse">
            



            
            <ul class="nav navbar-nav navbar-right">
                
                
                <li ><a href="#"><strong>Usuario: <br /><asp:Label ID="lblUsuario" runat="server" Text="" Font-Size="Small"></asp:Label></strong></a></li>
                <li><a href="#">
                    <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="btn btn-vitalicia btn-md"/></a>
                </li>
            </ul>
        </div>
    </nav>
    </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                 <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modals">
                        <div class="centers">
                            <img alt="" src="../img/ajax_loader.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>



    




             <!---------------INICIO TITULOS----------------------->
    <div id="contTitulos" runat="server">
        <div class="text-center" >
                
                
                <h4 class="text-center"><asp:Label ID="lblCodigoProyecto" style="display:none;" runat="server" Text="Label"></asp:Label></h4>
                <div class="container">
                    <div class="row">
                        <div class="col-sm-3" >
                            
                        </div>
                        <div class="col-sm-4" >
                            <div class="control-group">
                                <label for="nueva" class="control-label"> Número de Solicitud: </label>
                                <div class="controls" style="text-align:left;">
                                    <asp:DropDownList ID="cmbCodigoProyecto" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                                
                                </div>
                            </div>
                            <h4> <strong><span runat="server" id="lblMensaje3S" class=""><asp:Label ID="lblMensaje3" runat="server" Text=""></asp:Label></span></strong></h4>
                            <h6 class="text-center"><asp:Label ID="lblProyecto" runat="server" Text="Label"></asp:Label></h6>
                        </div>
                        <div class="col-sm-2" >
                            <br />
                            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                <div class="btn-group" role="group">
                                    <input id="btnActualizar" type="button" value="Actualizar" class="btn btn-default btn-md"  runat="server"/>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3" >
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-3" >
                        </div>

                        <div class="col-sm-6" >
                            <div runat="server" id="Div1" class="">
                            <strong><asp:Label ID="Label1" runat="server" Text=""></asp:Label></strong>
                        </div>
                        </div>

                        <div class="col-sm-3" >
                        </div>
                        
                    </div>
                </div>
                
                <div class="container">
                    <div class="row">
                        <div class="col-sm-3" >
                            
                        </div>

                        <div class="col-sm-6" >
                            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                <div class="btn-group" role="group">
                                    <input id="btnRevertir" type="button" value="Revertir Solicitud" data-target="#modalConfirmarAprobar" class="btn btn-vitalicia btn-md"  runat="server" data-toggle="modal" data-backdrop="static" />
                                </div>                        
                                
                                <div class="btn-group" role="group">
                                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-default btn-md" />
                                </div>
                                
                            </div>
                        </div>

                        <div class="col-sm-3" >
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-2" >
                        </div>

                        <div class="col-sm-8" >
                            
                                <div runat="server" id="lblMensajeS" class="" >
                                    <strong><asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></strong>
                                </div>
                            
                        </div>

                        <div class="col-sm-2" >
                        </div>
                        
                    </div>
                </div>
                
            </div>
    </div>
             <!---------------FIN TITULOS----------------------->


        <div class="row" >
        <div class="col-sm-4">
       
            <div id="contObservaciones" runat="server"> 
                <div class="control-group">
                    <label for="nueva" class="control-label">Observaciones: </label>
                    <div class="controls text-right ">
                        <textarea name="txtObservaciones" id="txtObservaciones" onkeyup="contarCaracteres(this)" onkeydown="contarCaracteres(this)" onkeypress="contarCaracteres(this)" runat="server" maxlength="1500" cols="40" rows="4" class="form-control input-md" placeholder="Observaciones" disabled="disabled"></textarea>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-4" >
            <fieldset class="row" id="contTextos" runat="server" disabled="disabled" >
                <div class="col-sm-6" >
                                <label for="nueva" class="control-label">Archivos txt Generados:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtGenerados" maxlength="50"  runat="server" class="form-control input-md" placeholder="Generados"></asp:TextBox>
                                </div>
                                <div class="form-group has-success has-feedback" id="txtEstadoS" runat="server">
                                <label for="nueva" class="control-label">Estado Solicitud:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtEstado" maxlength="50"  runat="server" class="form-control input-md" placeholder="Estado"></asp:TextBox>
                                </div>
                                </div>
                            </div>

                            <div class="col-sm-6" >
                                <label for="actual" class="control-label">Bodega Destino: </label>
                                <div class="controls">
                                    <asp:TextBox ID="txtBodDest" runat="server" class="form-control input-md" placeholder="Bodega"></asp:TextBox>
                                </div>
                                <label for="actual" class="control-label">Localización Destino: </label>
                                <div class="controls">
                                    <asp:TextBox ID="txtLocDest" runat="server" class="form-control input-md" placeholder="Localización"></asp:TextBox>
                                </div>
                            </div>              
                        </fieldset>
        </div>
        
        <div class="col-sm-4" >
            
            <fieldset class="row" id="contTextos2" runat="server">
                <fieldset class="col-sm-6" disabled="disabled">
                    <label for="actual" class="control-label">Fecha Solicitud: </label>
                    <div class="controls">
                        <asp:TextBox ID="txtFechaSol" runat="server" class="form-control input-md" placeholder="Fecha"></asp:TextBox>
                    </div>
                    
                    <label for="nueva" class="control-label">Fecha Requerida:</label>
                    <div class="controls">
                        <asp:TextBox ID="txtFechaReq" maxlength="50"  runat="server" class="form-control input-md" placeholder="Fecha Requerida"></asp:TextBox>
                    </div>
                    
                </fieldset>

                <fieldset class="col-sm-6" >
                     <fieldset disabled="disabled">   
                        <label for="actual" class="control-label">Fecha Aprobación Ingeniería: </label>
                        <div class="controls">
                            <asp:TextBox ID="txtFechaIng" runat="server" class="form-control input-md" placeholder="Fecha"></asp:TextBox>
                        </div>
                    
                    <label for="nueva" class="control-label"> Fecha Proceso de Logística: </label>
                    <div class="controls" style="text-align:left;">
                        <asp:TextBox ID="txtFechaLog" runat="server" class="form-control input-md" placeholder="Fecha"></asp:TextBox>
                    </div>
                         </fieldset>
                </fieldset>              
            </fieldset>
        </div>
    </div>

        <div class="row" >
        <div class="col-sm-4">
        <div id="contNuevoArticulo" runat="server" style="display:none;">
            
            <div class="control-group">
                <label for="nueva" class="control-label"> Actividad: </label>
                <div class="controls" style="text-align:left;">
                    <asp:DropDownList ID="cmbActividad2" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                    <asp:Label ID="lblActividad2" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                </div>
            </div>
            <div class="control-group">
                <label for="nueva" class="control-label"> Material: </label>
                <div class="controls" style="text-align:left;">
                    <asp:DropDownList ID="cmbMaterial2" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                    <asp:Label ID="lblMaterial2" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                </div>
            </div> 
            <div class="control-group">
                <label for="nueva" class="control-label"> Artículo: </label>
                <div class="controls" style="text-align:left;">
                    <asp:DropDownList ID="cmbArticulo2" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                    <asp:Label ID="lblArticulo2" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                </div>
            </div>
            <div class="row">   
                <div class="col-sm-4" >
                    <div class="control-group" runat="server" id="Div6">
                        <label for="nueva" class="control-label">Cantidad: </label>
                        <div class="controls" >
                            <asp:TextBox ID="txtCantidad2" autocomplete="off" style="text-align:center;" runat="server" onkeyup="mascara2(this)" onkeypress="ValidaSoloNumeros()" class="form-control input-md" AutoPostBack="false" placeholder=""></asp:TextBox>
                            <asp:Label ID="lblCantEq2" runat="server" Text="" Font-Bold="True" Font-Size="X-Large" ></asp:Label>
                            <asp:Label ID="lblFactor2" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                             
                        </div>
                    </div>
                </div>
                <div class="col-sm-2" >
                    <div class="control-group" runat="server" id="Div7">
                        <label for="nueva" class="control-label">UM: </label>
                        <fieldset id="Fieldset1" runat="server" disabled="disabled">
                            <asp:Label ID="lblUM2" runat="server" Text="" Font-Bold="True" Font-Size="X-Large" ></asp:Label><br />
                            <asp:Label ID="lblUMEq2" runat="server" Text="KG" Font-Bold="True" Font-Size="X-Large" ></asp:Label>
                            <asp:Label ID="lblUMAux2" runat="server" Text="KG" Font-Bold="True" Font-Size="X-Large" style="display:none;"  ></asp:Label>
                        </fieldset>
                    </div>
                </div>
                <div class="col-sm-6" >
                    <div class="control-group" runat="server" id="Div8">
            <br/>
                        <div class="controls">
                            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                <div class="btn-group" role="group">
                                    <asp:Button ID="btnAcepAgre" runat="server" Text="Aceptar" CssClass="btn btn-vitalicia btn-md" causesvalidation="true" validationgroup="GrupoValidador" />
                                </div>                        
                                
                                <div class="btn-group" role="group">
                                    <asp:Button ID="btnCancAgre" runat="server" Text="Cancelar" CssClass="btn btn-default btn-md" causesvalidation="true" validationgroup="GrupoValidador" />
                                </div>
                            </div>
                            
                            
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
            
        </div>
        <div class="col-sm-4" >
            
        </div>
        
        <div class="col-sm-4" >
            
        </div>
    </div>
 







































   
    <br/>
    
    <div class="row">
        <div class="col-sm-12" >

            
                    
                

            <div class="table" >
                <div class="testgrid2">
                <div id="contDtgDetalle" style="" runat="server">
                <asp:GridView ID="dtgDetalle" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="ID" 
                        DataSourceID="Detalles" CssClass="testgrid2" selectedindex="1"
                        EnableModelValidation="True" Width="100%" Visible="true" OnRowDataBound="OnRowDataBound">
                    <Columns>
                        <asp:CommandField ButtonType="Button" SelectText ="Editar" ShowSelectButton="True" ControlStyle-CssClass="buttonControl"  >
                        <ControlStyle CssClass="btn btn-vitalicia btn-md" />
                        </asp:CommandField>
                        
                        <asp:CommandField ButtonType="Button" DeleteText="A/P" ShowDeleteButton="True" ControlStyle-CssClass="buttonControl" HeaderText="Aceptar / Posponer"  >
                            <ControlStyle CssClass="btn btn-vitalicia btn-md" />
                        </asp:CommandField>

                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="true" SortExpression="ID" />

                        <asp:BoundField DataField="ACTIVIDAD" HeaderText=" CÓDIGO ACTIVIDAD" ReadOnly="True" SortExpression="ACT" />

                        <asp:BoundField DataField="NOM_ACTIVIDAD" HeaderText="NOMBRE ACTIVIDAD" ReadOnly="True" SortExpression="ACT" />

                        <asp:BoundField DataField="MATERIAL" HeaderText="CÓDIGO MATERIAL" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="NOM_MATERIAL" HeaderText="NOMBRE MATERIAL" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="ARTICULO" HeaderText="CÓDIGO ARTÍCULO" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="NOM_ARTICULO" HeaderText="NOMBRE ARTÍCULO" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="UM_A" HeaderText="UM" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_SOL_A" HeaderText="CANT SOL"  SortExpression="SOL" ReadOnly="True" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="UM_P" HeaderText="UM" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_SOL_P" HeaderText="CANT SOL"  SortExpression="SOL" ReadOnly="True" DataFormatString="{0:N}" />

                        <asp:TemplateField HeaderText = "BODEGA ORIGEN" ControlStyle-CssClass="search-box">
                            <ItemTemplate>
                                <asp:Label ID="lblBodegas" runat="server" Text='<%# Eval("Bodega_o") %>' Visible = "false" />
                                <asp:DropDownList ID="cmbBodegas" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ESTADO_LIN" HeaderText="ESTADO" ReadOnly="True" SortExpression="ESTADO" />
                        <asp:BoundField DataField="CANT_SOL_A" HeaderText="CANT SOL"  SortExpression="SOL" ReadOnly="True"  />
                        <asp:BoundField DataField="CANT_SOL_P" HeaderText="CANT SOL"  SortExpression="SOL" ReadOnly="True" />
                        <asp:BoundField DataField="BODEGA_O" HeaderText="BODEGA ORIGEN" ReadOnly="True" SortExpression="BO" />    
                        <asp:BoundField DataField="CONSEC_TXT" HeaderText="CONSECUTIVO TXT" ReadOnly="True" SortExpression="CON" />    
                        <asp:BoundField DataField="LINEA" HeaderText="LINEA" ReadOnly="True" SortExpression="LIN" />   
                    </Columns>
                </asp:GridView>
                <div style="display:none;">
                <asp:GridView ID="dtgBodegas" runat="server" 
                        AutoGenerateColumns="False" 
                        DataSourceID="dsBodegas" CssClass="testgrid3" selectedindex="1"
                        EnableModelValidation="True" Width="100%" Visible="true" >
                    <Columns>
                        
                        <asp:BoundField DataField="BODEGA_O" HeaderText="B O" ReadOnly="True" SortExpression="BO" />           
                    </Columns>
                </asp:GridView>

                <asp:SqlDataSource ID="dsBodegas" runat="server" ProviderName="System.Data.SqlClient" >
                         <SelectParameters>
                            <asp:ControlParameter ControlID="lblCodigoProyecto" DefaultValue="ninguno" Name="CODIGO_PROYECTO" PropertyName="Text" Type="String" />
                        </SelectParameters>

                        
                    </asp:SqlDataSource>
                </div>

                </div>
                
               

                    <asp:SqlDataSource ID="Detalles" runat="server" ProviderName="System.Data.SqlClient" >
                         <SelectParameters>
                            <asp:ControlParameter ControlID="lblCodigoProyecto" DefaultValue="ninguno" Name="CODIGO_PROYECTO" PropertyName="Text" Type="String" />
                        </SelectParameters>

                        
                    </asp:SqlDataSource>
                </div>
            </div>
            
        </div>
    </div>

    


    <!-- INICIO CONFIRMACION DESCARTE DE DATOS-->
    <div class="modal fade" id="modalConfirmarAprobar" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Cuadro de confirmación</h4>
                </div>
                <div class="modal-body">
                    <p>¿Seguro que desea revertir la Solicitud seleccionada?</p>
                    
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="...">
                        <div class="btn-group" role="group">
                            <input id="btnRevertirSi" type="button" value="Si" runat="server" data-dismiss="modal" class="btn btn-vitalicia btn-md" />
                        </div>
  
                        <div class="btn-group" role="group">
                            <input id="btnRevertirNo" type="button" value="No" runat="server" data-dismiss="modal" class="btn btn-default btn-md" />
                        </div>
                    </div>                  
                </div>
            </div>
        </div>
    </div>
    <!-- FIN CONFIRMACION DESCARTE DE DATOS-->

    <!-- INICIO CONFIRMACION DESCARTE DE DATOS-->
    <div class="modal fade" id="modalConfirmarRechazar" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Cuadro de confirmación</h4>
                </div>
                <div class="modal-body">
                    <p>¿Seguro que desea rechazar la Solicitud seleccionada?</p>
                    
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="...">
                        <div class="btn-group" role="group">
                            <input id="btnRechazarSi" type="button" value="Si" runat="server" data-dismiss="modal" class="btn btn-vitalicia btn-md" />
                        </div>
  
                        <div class="btn-group" role="group">
                            <input id="btnRechazarNo" type="button" value="No" runat="server" data-dismiss="modal" class="btn btn-default btn-md" />
                        </div>
                    </div>                  
                </div>
            </div>
        </div>
    </div>
    <!-- FIN CONFIRMACION DESCARTE DE DATOS-->

<script type="text/javascript">
    
        //function validarCantidad(){
        //    var cantidadOriginal = document.getElementById('lblCantOriginal').value();
        //    var cantidadEq = document.getElementById('lblCantEq').value();
        //    alert("fdaffafd");
        //    if (cantidadEq > cantidadOriginal) {
        //        alert("fdaffafd");

        //    }
             <%--//alert(txtNit);
             
             if (txtNit=="ND"){
                 document.getElementById("txtNitAdicional").style = "";
                 document.getElementById("txtImpCredFiscS").style = "display:none;";
                 document.getElementById('<%= lblFac.ClientID %>').innerHTML="Número de Recibo";
                 document.getElementById('<%= txtNroFac.ClientID %>').innerHTML="0";
                 document.getElementById('txtCodControl').value = "-";
                 document.getElementById('txtAutoriza').value = "-";
                 
                 document.getElementById("fsCodAut").disabled=true;
                 document.getElementById("fsCodAut").style = "display:none;";
                 //alert("holandas");
                }
             else{
                
                 document.getElementById('<%= lblFac.ClientID %>').innerHTML="Número de Factura";
                 document.getElementById("fsCodAut").disabled=false;
                 document.getElementById("fsCodAut").style = "";
                 document.getElementById("txtNitAdicional").style = "display:none;";
                 document.getElementById("txtImpCredFiscS").style = "";
                 
                 if (document.getElementById('txtAutoriza').value == "-") {
                     document.getElementById('txtCodControl').value = "";
                     document.getElementById('txtAutoriza').value = "";
                 }

                 //alert("holandas2");--%>
                
             //}
    //}  

    

    
    
        
   
   
    
   <%--function contarCaracteres(d)
    {
        val = d.value
        
        cuenta = val.length;
        cuenta = 1500 - cuenta
        if (cuenta > 750) {
            document.getElementById("formato").className = "label label-success";
        }
        else {
            if (cuenta > 50) {
                document.getElementById("formato").className = "label label-warning";
            }
            else {
                document.getElementById("formato").className = "label label-danger";
            }
        }
       
             
       document.getElementById('<%= txtCaracter.ClientID %>').innerHTML = "Caracteres restantes: " + cuenta;  

       
   }--%>

      <%-- function escribir() {
           var obs;
           var sol;
           var fecha;
           obs = document.getElementById('<%= txtObservacionesAux.ClientID %>').value;
           sol = document.getElementById('<%= txtSolicitanteAux.ClientID %>').value;
           fecha = document.getElementById('<%= txtFechaAux.ClientID %>').value;
           document.getElementById('<%= txtObservaciones.ClientID %>').value = obs;
           document.getElementById('<%= txtSolicitante.ClientID %>').value=sol;
           document.getElementById('<%= txtFecha.ClientID %>').value=fecha;
       }--%>

               
    </script>
<!------------------------------ Inicio pie de página------------------------------>
            <div id="footer">
                
                    <div class="text-right" >
		                <a href="http://www.clever.bo/"> <img src="../img/clever.png" class="" alt="Cinque Terre" width="2%" height="2%"/></a>
                    </div>
                
            </div>
<!------------------------------ Fin pie de página  ------------------------------>

         </ContentTemplate>
    </asp:UpdatePanel>

               <!-- INICIO MODAL OBSERVACIONES-->
    <div class="modal fade" id="modalObservaciones" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header text-center">
                   <h2>Ingrese la información para continuar: <span class="extra-title muted"></span></h2>
                </div>
                <div class="modal-body form-horizontal">
                    <div class="control-group">
                        
                        
                        
                        
                    </div>
                    
                    
                    
                    
                   
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="...">
                        <div class="btn-group" role="group">                            
                            <input id="btnGenerarAceptar" type="button" value="Aceptar" runat="server" data-dismiss="modal" class="btn btn-vitalicia btn-md" />
                        </div>
  
                        <div class="btn-group" role="group">
                            <input id="btnGenerarCancelar" type="button" value="Cancelar" runat="server" data-dismiss="modal" class="btn btn-default btn-md" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <!-- FIN MODAL OBSERVACIONES -->
        </form>
    </div>


</body>
</html>
