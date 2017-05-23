<%@ Language="VB" AutoEventWireup="false" CodeFile="presupuestoMateriales.aspx.vb" Inherits="ingenieria_presupuestoMateriales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Análisis de Precio Unitario</title>
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
                $('#dtgMateriales').dataTable({
                    "lengthMenu": [[5, 10, 15, -1], [5, 10, 15, "Todos"]],
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

            $(document).ready(function () {
                $('#dtgDetalleAct').dataTable({
                    "lengthMenu": [[5, 10, 15, -1], [5, 10, 15, "Todos"]],
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

            $(document).ready(function () {
                $('#dtgSolicitud').dataTable({
                    "lengthMenu": [[5, 10, 15, -1], [5, 10, 15, "Todos"]],
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

            $(document).ready(function () {
                $('#dtgActas').dataTable({
                    "lengthMenu": [[5, 10, 15, -1], [5, 10, 15, "Todos"]],
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

    <script type="text/javascript">
        function MostrarLabel() {
            setTimeout("OcultarLabel()", 3000);
            var msj = document.getElementById("lblMensajeSS");
            msj.style.visibility = "visible";
            }

        function OcultarLabel() {
            var msj = document.getElementById("lblMensajeSS");
            msj.style.visibility = "hidden";
            }
    </script>

</head>


<body>
    
    <div class="container-fluid text-center">
        <form class="form-horizontal" role="form" runat="server">



    <nav class="navbar navbar-default" style="background-color: #fff;" role="navigation">
    <!-- El logotipo y el icono que despliega el menú se agrupan
       para mostrarlos mejor en los dispositivos móviles -->
        <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-ex1-collapse">
              <span class="sr-only">Desplegar navegación</span>
              <span class="icon-bar"></span>
              <span class="icon-bar"></span>
              <span class="icon-bar"></span>
            </button>
      
           
            <a class="navbar-brand" href="#"><strong>PRESUPUESTO DE MATERIALES</strong></a>
        </div>
 
  <!-- Agrupar los enlaces de navegación, los formularios y cualquier
       otro elemento que se pueda ocultar al minimizar la barra -->
        <div class="collapse navbar-collapse navbar-ex1-collapse">
            <!-- <ul class="nav navbar-nav">
                <li class="active"><a href="#">Solicitudes <br/>Aprobadas</a></li>
                <li><a href="#">Solicitudes<br/> Cursadas</a></li>
                <li class="active"><a href="#">Materiales<br/> Rechazados</a></li>
                <li><a href="#">Presupuesto</a></li>
                <li class="active"><a href="#">Cambiar<br/> Contraseña</a></li>
            </ul>-->
            
            <ul class="nav navbar-nav navbar-right">
                <li class="dropdown ">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                      Opciones adicionales <b class="caret"></b> <br />
                       Seleccione una:
                    </a>
                    <ul class="dropdown-menu">
                        
                        <li><a href="#"><asp:Button ID="btnCuadroCom" runat="server" Text="Generar Excel Desglose" CssClass="btn btn-default btn-md" /></a></li>
                        
                    </ul>
                </li>
                <li class="dropdown" style="display:none;">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                      Unidad de Medida <b class="caret"></b> <br />
                    Actual:<strong> <asp:Label ID="lblUMActual" runat="server" Text="Presupuesto"></asp:Label></strong>
                    </a>
                    <ul class="dropdown-menu">
                        
                        <li><a href="#">
                            <asp:Button ID="btnPresupuesto" runat="server" Text="Presupuesto" CssClass="btn btn-default btn-md" /></a></li>
                        <li><a href="#"><asp:Button ID="btnAlmacenes" runat="server" Text="&nbsp;&nbsp;&nbsp;Almacén&nbsp;&nbsp;&nbsp;" CssClass="btn btn-default btn-md" /></a></li>
                    </ul>
                </li>
                <li class="active" ><a href="#"><strong>Usuario: <br /><asp:Label ID="lblUsuario" runat="server" Text="" Font-Size="Small"></asp:Label></strong></a></li>
                <li class="" style="display:none;"><a href="#">
                    <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="btn btn-vitalicia btn-md"/></a>
                </li>
            </ul>
        </div>
    </nav>


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
                        <div class="col-sm-6" >
                            <div class="control-group">
                                <label for="nueva" class="control-label"> Proyecto: </label>
                                <div class="controls" style="text-align:left;">
                                    <asp:DropDownList ID="cmbProyecto" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                                
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

                        <div class="col-sm-6" style="display:none;" >
                            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                <div class="btn-group" role="group">
                                    <input id="btnGenerarExcel" type="button" value="Generar Excel" data-target="#modalConfirmarAprobar" class="btn btn-vitalicia btn-md"  runat="server" data-toggle="modal" data-backdrop="static" />
                                </div>                        
                                <div class="btn-group" role="group">
                                    <input id="btnSolicitud" type="button" value="Ver Solicitud" data-target="#modalConfirmarRechazar" class="btn btn-default btn-md"  runat="server" data-toggle="modal" data-backdrop="static" />
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
                            
                                <div runat="server" id="lblMensajeS" class="" >
                                    <strong><asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></strong>
                                </div>
                            
                        </div>

                        <div class="col-sm-3" >
                        </div>
                        
                    </div>
                </div>
                
            </div>
    </div>
             <!---------------FIN TITULOS----------------------->


        
 
   
    <br/>
    
    <div class="row">
        <div class="col-sm-12">
            
            
            <div class="table-responsive" >
                <div class="testgrid1">
                     
                <div id="contDtgMateriales" style="" runat="server">
                <asp:GridView ID="dtgMateriales" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="NOM_MATERIAL" 
                        DataSourceID="Materiales" CssClass="testgrid1" selectedindex="1"
                        EnableModelValidation="True" Width="100%" Visible="true">
                    <Columns>
                        <asp:CommandField ButtonType="Link" SelectText ="Desglose" ShowSelectButton="True" ControlStyle-CssClass="buttonControl"  >
                            <ControlStyle CssClass="label label-warning" />
                        </asp:CommandField>
                        
                        
                        <asp:BoundField DataField="NOM_MATERIAL" HeaderText="MATERIAL" ReadOnly="True" SortExpression="ART" />

                       <asp:BoundField DataField="UM_P" HeaderText="UNIDAD MEDIDA" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_SOL_P" HeaderText="CANTIDAD SOLICITADA"  SortExpression="SOL" DataFormatString="{0:N}" />

                       <asp:BoundField DataField="CANT_PRESUP_P" HeaderText="CANTIDAD PRESUPUESTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_ACTAS_P" HeaderText="CANTIDAD ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_PRESUP_ACTAS_P" HeaderText="CANTIDAD PRESUPUESTADA MAS ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />
                        
                        <asp:BoundField DataField="CANT_DEV_P" HeaderText="CANTIDAD DEVUELTA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />
                        
                        <asp:BoundField DataField="CANT_SOL_APROB_P" HeaderText="CANTIDAD EJECUTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_DISP_P" HeaderText="CANTIDAD DISPONIBLE" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />


                        <asp:BoundField DataField="UM_P" HeaderText="UNIDAD MEDIDA" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_SOL_A" HeaderText="CANTIDAD SOLICITADA"  SortExpression="SOL" DataFormatString="{0:N}" />

                       <asp:BoundField DataField="CANT_PRESUP_A" HeaderText="CANTIDAD PRESUPUESTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_ACTAS_A" HeaderText="CANTIDAD ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_PRESUP_ACTAS_A" HeaderText="CANTIDAD PRESUPUESTADA MAS ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />
                        
                        <asp:BoundField DataField="CANT_DEV_A" HeaderText="CANTIDAD DEVUELTA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />
                        
                        <asp:BoundField DataField="CANT_SOL_APROB_A" HeaderText="CANTIDAD EJECUTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_DISP_A" HeaderText="CANTIDAD DISPONIBLE" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />
                        
                       </Columns>
                </asp:GridView>
                </div>
                
                    <asp:SqlDataSource ID="Materiales" runat="server" ProviderName="System.Data.SqlClient" >
                         
                    </asp:SqlDataSource>
                </div>
            </div>
            
        </div>
        <div class="col-sm-12">
            <h4><span class="label alert-info" id="lblActividadDetalleS" runat="server"><asp:Label ID="lblActividadDetalle" runat="server" Text=""></asp:Label></span></h4>
            <div style="display:none;"><asp:Label ID="lblActividad" runat="server" Text="" ></asp:Label></div>
           
            <div class="table-responsive" >
                <div class="testgrid1">
                     
                <div id="contDtgDetalleAct" style="" runat="server">
                <asp:GridView ID="dtgDetalleAct" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="NOM_MATERIAL" 
                        DataSourceID="detalleActividad" CssClass="testgrid1" selectedindex="1"
                        EnableModelValidation="True" Width="100%" Visible="true">
                    <Columns>
                        <asp:CommandField ButtonType="Link" SelectText ="Ver Detalle" ShowSelectButton="True" ControlStyle-CssClass="buttonControl"  >
                            <ControlStyle CssClass="label label-warning" />
                        </asp:CommandField>
                        
                        <asp:BoundField DataField="NOM_MATERIAL" HeaderText="MATERIAL" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="UM_P" HeaderText="UNIDAD MEDIDA" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CODIGO_SOLICITUD" HeaderText="CODIGO SOLICITUD" ReadOnly="True" SortExpression="UMP" />
                        
                        <asp:BoundField DataField="CANT_SOL_APROB_P" HeaderText="CANTIDAD EJECUTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_APROB_ACTAS_P" HeaderText="CANTIDAD EJECUTADA EN ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_PEND_P" HeaderText="CANTIDAD SOLICITADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_RECH_P" HeaderText="CANTIDAD RECHAZADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_DISP_P" HeaderText="CANTIDAD DISPONIBLE" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="FECHA_APROBACION" HeaderText="FECHA APROBACIÓN INGENIERIA" ReadOnly="True" SortExpression="SOL" />


                        <asp:BoundField DataField="UM_P" HeaderText="UNIDAD MEDIDA" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CODIGO_SOLICITUD" HeaderText="CODIGO SOLICITUD" ReadOnly="True" SortExpression="UMP" />
                        
                        <asp:BoundField DataField="CANT_SOL_APROB_P" HeaderText="CANTIDAD EJECUTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_APROB_ACTAS_P" HeaderText="CANTIDAD EJECUTADA EN ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_PEND_P" HeaderText="CANTIDAD SOLICITADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_RECH_P" HeaderText="CANTIDAD RECHAZADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_DISP_P" HeaderText="CANTIDAD DISPONIBLE" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="FECHA_APROBACION" HeaderText="FECHA APROBACIÓN INGENIERIA" ReadOnly="True" SortExpression="SOL" />
                                                
                       </Columns>
                </asp:GridView>
                </div>
                
                    <asp:SqlDataSource ID="detalleActividad" runat="server" ProviderName="System.Data.SqlClient" >
                         <SelectParameters>
                            <asp:ControlParameter ControlID="lblActividad" DefaultValue="ninguno" Name="DETALLE_ACTIVIDAD" PropertyName="text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
           
        </div>

        <div class="col-sm-12">
            <h4><span class="label alert-info" id="lblDevolucionDetalleS" runat="server"><asp:Label ID="lblDevolucionDetalle" runat="server" Text=""></asp:Label></span></h4>
            <div style="display:none;"><asp:Label ID="Label3" runat="server" Text="" ></asp:Label></div>
           
            <div class="table-responsive" >
                <div class="testgrid1">
                     
                <div id="Div2" style="" runat="server">
                <asp:GridView ID="dtgDevolucion" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="NOM_MATERIAL" 
                        DataSourceID="detalleDevolucion" CssClass="testgrid1" selectedindex="1"
                        EnableModelValidation="True" Width="100%" Visible="true">
                    <Columns>
                        <asp:CommandField ButtonType="Link" SelectText ="Ver Detalle" ShowSelectButton="True" ControlStyle-CssClass="buttonControl"  >
                            <ControlStyle CssClass="label label-warning" />
                        </asp:CommandField>
                         <asp:BoundField DataField="FECHA" HeaderText="FECHA" ReadOnly="True" SortExpression="ART" />

                         <asp:BoundField DataField="PROYECTO" HeaderText="PROYECTO" ReadOnly="True" SortExpression="ART" />

                         <asp:BoundField DataField="NOM_ACTIVIDAD" HeaderText="ACTIVIDAD" ReadOnly="True" SortExpression="ART" />
                        
                        <asp:BoundField DataField="NOM_MATERIAL" HeaderText="MATERIAL" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="UM_P" HeaderText="UNIDAD MEDIDA" ReadOnly="True" SortExpression="UMP" />

                       <asp:BoundField DataField="CANT_P" HeaderText="CANTIDAD DEVUELTA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        
                                                
                       </Columns>
                </asp:GridView>
                </div>
                
                    <asp:SqlDataSource ID="detalleDevolucion" runat="server" ProviderName="System.Data.SqlClient" >
                         <SelectParameters>
                            <asp:ControlParameter ControlID="lblActividad" DefaultValue="ninguno" Name="DETALLE_ACTIVIDAD" PropertyName="text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
           
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <h4><span class="label alert-info" id="lblSolicitudDescS" runat="server"><asp:Label ID="lblSolicitudDesc" runat="server" Text=""></asp:Label></span></h4>
            <div style="display:none;"><asp:Label ID="lblCodSolicitud" runat="server" Text=""></asp:Label></div>
            
            <div class="table-responsive" >
                <div class="testgrid1">
                     
                <div id="contDtgSolicitud" style="" runat="server">
                <asp:GridView ID="dtgSolicitud" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="MATERIAL" 
                        DataSourceID="detalleSolicitudes" CssClass="testgrid1" selectedindex="1"
                        EnableModelValidation="True" Width="100%" Visible="true">
                    <Columns>
                        
                        
                        <asp:BoundField DataField="NUMERO" HeaderText="NO." ReadOnly="True" Visible="True" SortExpression="ID" />

                        <asp:BoundField DataField="NOM_ACTIVIDAD" HeaderText="ACTIVIDAD" ReadOnly="True" SortExpression="ACT" />

                        <asp:BoundField DataField="NOM_MATERIAL" HeaderText="MATERIAL" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="NOM_ARTICULO" HeaderText="ARTÍCULO" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="UM_P" HeaderText="UNIDAD MEDIDA" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_SOL_P" HeaderText="CANTIDAD SOLICITADA"  SortExpression="SOL" ReadOnly="True" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_PRESUP_P" HeaderText="CANTIDAD PRESUPUESTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_ACTAS_P" HeaderText="CANTIDADD ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_APROB_P" HeaderText="CANTIDAD EJECUTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_APROB_ACTAS_P" HeaderText="CANTIDAD EJECUTADA ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_DISP2_P" HeaderText="CANTIDAD DISPONIBLE" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_PEND_P" HeaderText="ANTERIORES SIN APROBAR" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="ESTADO_LIN" HeaderText="ESTADO LINEA" ReadOnly="True" SortExpression="ART"  Visible="true" />


                        <asp:BoundField DataField="UM_P" HeaderText="UNIDAD MEDIDA" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_SOL_A" HeaderText="CANTIDAD SOLICITADA"  SortExpression="SOL" ReadOnly="True" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_PRESUP_A" HeaderText="CANTIDAD PRESUPUESTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_ACTAS_A" HeaderText="CANTIDADD ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_APROB_A" HeaderText="CANTIDAD EJECUTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_APROB_ACTAS_A" HeaderText="CANTIDAD EJECUTADA ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_DISP2_A" HeaderText="CANTIDAD DISPONIBLE" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_PEND_A" HeaderText="ANTERIORES SIN APROBAR" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="ESTADO_LIN" HeaderText="ESTADO LINEA" ReadOnly="True" SortExpression="ART"  Visible="true" />

                       </Columns>
                </asp:GridView>
                </div>
                
                    <asp:SqlDataSource ID="detalleSolicitudes" runat="server" ProviderName="System.Data.SqlClient" >
                         <SelectParameters>
                            <asp:ControlParameter ControlID="lblCodSolicitud" DefaultValue="ninguno" Name="CODIGO_SOLICITUD" PropertyName="text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
            
        </div>
        <div class="col-sm-12">
            <h4><span class="label alert-info" id="lblSolicitudDescS2" runat="server"><asp:Label ID="lblSolicitudDesc2" runat="server" Text=""></asp:Label></span></h4>
           
            <div class="table-responsive" >
                <div class="testgrid1">
                     
                <div id="contDtgActas" style="" runat="server">
                <asp:GridView ID="dtgActas" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="NOM_MATERIAL" 
                        DataSourceID="detallesActa" CssClass="testgrid1" selectedindex="1"
                        EnableModelValidation="True" Width="100%" Visible="true">
                    <Columns>
                        
                        
                        <asp:BoundField DataField="COD_ACTA" HeaderText="CORRELATIVO" ReadOnly="True" Visible="TRUE" SortExpression="ID" />

                        <asp:BoundField DataField="ACTA_ING" HeaderText="CODIGO ACTA" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="NOM_ACTIVIDAD" HeaderText="ACTIVIDAD" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="NOM_MATERIAL" HeaderText="MATERIAL" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_P" HeaderText="CANTIDAD ACTA"  SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="FECHA" HeaderText="FECHA" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_A" HeaderText="CANTIDAD ACTA"  SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="FECHA" HeaderText="FECHA" ReadOnly="True" SortExpression="UMP" />

                       </Columns>
                </asp:GridView>
                </div>
                
                    <asp:SqlDataSource ID="detallesActa" runat="server" ProviderName="System.Data.SqlClient" >
                         <SelectParameters>
                            <asp:ControlParameter ControlID="lblCodSolicitud" DefaultValue="ninguno" Name="CODIGO_SOLICITUD" PropertyName="text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
            
        </div>
    </div>

<!------------------------------ Inicio pie de página------------------------------>
            <div id="footer">
                
                    <div class="text-right" >
		                <a href="http://www.clever.bo/"> <img src="../img/clever.png" class="" alt="Cinque Terre" width="2%" height="2%"/></a>
                    </div>
                
            </div>
<!------------------------------ Fin pie de página  ------------------------------>

         </ContentTemplate>
    </asp:UpdatePanel>
        </form>
    </div>


</body>
</html>
