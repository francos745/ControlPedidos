<%@ Page culture="es-MX" UICulture="es-MX"  Language="VB" AutoEventWireup="false" CodeFile="solicitudesCursadasIng.aspx.vb" Inherits="ingenieria_solicitudesCursadasIng" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Solicitudes pendientes</title>
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

    

</head>


<body>
    
    <div class="container-fluid text-center">
        <form class="form-horizontal" role="form" runat="server">

           <div id="contNavbar" runat="server">
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
             <a class="navbar-brand" href="#"><strong>SOLICITUDES PENDIENTES</strong></a>         
        </div>
 
  <!-- Agrupar los enlaces de navegación, los formularios y cualquier
       otro elemento que se pueda ocultar al minimizar la barra -->
        <div class="collapse navbar-collapse navbar-ex1-collapse">
            <ul class="nav navbar-nav navbar-left">
                <li class="dropdown ">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                    Menú de navegación <b class="caret"></b> <br />
                    Seleccione una opción:
                    </a>
                    <ul class="dropdown-menu">
                        <li>
                            <a href="#">                                
                                    <asp:Button ID="btnCursadas" runat="server"   Text="Solicitudes Pendientes &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" CssClass="btn btn-default btn-md" />
                            </a>
                        </li>
                        <li>
                            <a href="#">
                                    <asp:Button ID="btnAprobadas" runat="server" Text="Solicitudes Aprobadas &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" CssClass="btn btn-default btn-md" />
                            </a>
                        </li>
                        <li>
                            <a href="#">
                                    <asp:Button ID="btnRechazadas" runat="server" Text="Solicitudes Rechazadas &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" CssClass="btn btn-default btn-md" />
                            </a>
                        </li>
                        <li>
                            <a href="#">
                                    <asp:Button ID="btnMatRech" runat="server" Text="Materiales Rechazados &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" CssClass="btn btn-default btn-md" />
                            </a>
                        </li>
                        <li>
                            <a href="#">
                                    <asp:Button ID="btnActas" runat="server" Text="Registrar Actas &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" CssClass="btn btn-default btn-md" />
                            </a>
                        </li>
                        <li>
                            <a href="#">
                                    <asp:Button ID="btnModActas" runat="server" Text="Modificar Actas &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" CssClass="btn btn-default btn-md" />
                            </a>
                        </li>
                    </ul>
                </li>
                
                
            </ul>



            
            <ul class="nav navbar-nav navbar-right">
                <li class="dropdown ">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                      Opciones adicionales <b class="caret"></b> <br />
                       Seleccione una:
                    </a>
                    <ul class="dropdown-menu">
                        
                        <li>
                            <a href="#">
                                
                                    <asp:Button ID="btnSinc" runat="server"   Text="Sincronizar Base de Datos" CssClass="btn btn-default btn-md" />
                                
                            </a>
                        </li>
                        <li><a href="#"><asp:Button ID="btnPU" runat="server" Text="Análisis de Precio Unitario" CssClass="btn btn-default btn-md" /></a></li>
                        <li><a href="#"><asp:Button ID="btnPM" runat="server" Text="Presupuesto de materiales" CssClass="btn btn-default btn-md" /></a></li>
                        <li><a href="#"><asp:Button ID="btnExcel" runat="server" Text="Cargar Solicitud" CssClass="btn btn-default btn-md" /></a></li>
                        <li><a href="#"><asp:Button ID="btnCuadroCom" runat="server" Text="Cuadro de Comunicacion" CssClass="btn btn-default btn-md" /></a></li>

                    </ul>
                </li>
                <li class="dropdown"  style="display:none;">
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
                               
                            <h5 class="text-center"><asp:Label ID="lblProyecto" runat="server" Text="Label"></asp:Label></h5>
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
                                    <input id="btnAprobar" type="button" value="Aprobar" data-target="#modalConfirmarAprobar" class="btn btn-vitalicia btn-md"  runat="server" data-toggle="modal" data-backdrop="static" />
                                </div>                        
                                <div class="btn-group" role="group">
                                    <input id="btnRechazar" type="button" value="Rechazar" data-target="#modalConfirmarRechazar" class="btn btn-default btn-md"  runat="server" data-toggle="modal" data-backdrop="static" />
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
    <div class="row">
        <div class="col-sm-12" >
            <div class="table-responsive" >
                <div class="testgrid1">
                <div id="contDtgDetalle" style="" runat="server">
                <asp:GridView ID="dtgDetalle" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="ID" 
                        DataSourceID="Detalles" CssClass="testgrid1" selectedindex="1"
                        EnableModelValidation="True" Width="100%" Visible="true">
                    <Columns>
                        <asp:CommandField ButtonType="button" SelectText ="Editar" ShowSelectButton="True" ControlStyle-CssClass="buttonControl"  >
                            <ControlStyle CssClass="btn btn-vitalicia btn-xs" />
                        </asp:CommandField>
                        
                        <asp:CommandField ButtonType="button" DeleteText="A/R" ShowDeleteButton="True" ControlStyle-CssClass="buttonControl" HeaderText="Aceptar / Rechazar"  >
                            <ControlStyle CssClass="btn btn-vitalicia btn-xs" />
                        </asp:CommandField>

                        <asp:BoundField DataField="NUMERO" HeaderText="NO." ReadOnly="True" Visible="True" SortExpression="ID" />

                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="True" SortExpression="ID" />

                        <asp:BoundField DataField="NOM_ACTIVIDAD" HeaderText="ACTIVIDAD" ReadOnly="True" SortExpression="ACT" />

                        <asp:BoundField DataField="NOM_MATERIAL" HeaderText="MATERIAL" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="NOM_ARTICULO" HeaderText="ARTÍCULO" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="UM_P" HeaderText="UNIDAD MEDIDA" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_SOL_P" HeaderText="CANTIDAD SOLICITADA"  SortExpression="SOL" ReadOnly="True" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_PRESUP_P" HeaderText="CANTIDAD PRESUPUESTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_ACTAS_P" HeaderText="CANTIDAD ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />
                        
                        <asp:BoundField DataField="CANT_DEV_P" HeaderText="CANTIDAD DEVUELTA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_APROB_P" HeaderText="CANTIDAD EJECUTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_APROB_ACTAS_P" HeaderText="CANTIDAD EJECUTADA EN ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_DISP3_P" HeaderText="CANTIDAD DISPONIBLE" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Left" />

                        <asp:BoundField DataField="ANT_SIN_APROB_P" HeaderText="ANTERIORES SIN APROBAR" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                         <asp:BoundField DataField="PRESUPUESTADO_ACT" HeaderText="ACT PRESUP" ReadOnly="True" SortExpression="ART" />

                         <asp:BoundField DataField="PRESUPUESTADO" HeaderText="MAT PRESUP" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="ESTADO_LIN" HeaderText="ESTADO LINEA" ReadOnly="True" SortExpression="ART"  Visible="true" />

                        <asp:BoundField DataField="ACTIVIDAD" HeaderText="ACTIVIDAD" ReadOnly="True" Visible="TRUE" SortExpression="ACT" HeaderStyle-Width="10px" />

                        <asp:BoundField DataField="MATERIAL" HeaderText="MATERIAL" ReadOnly="True" Visible="true" SortExpression="ART" />

                        <asp:BoundField DataField="COD_ACTA" HeaderText="ACTA ASIGNADA" ReadOnly="True" Visible="true" SortExpression="ART" />

                        <asp:BoundField DataField="CANT_ACTA_LINEA_P" HeaderText="CANTIDAD ACTA ASIGNADA" ReadOnly="True" Visible="true" SortExpression="SOL" DataFormatString="{0:N}" />
                                      
                        <asp:BoundField DataField="ARTICULO" HeaderText="ARTICULO" ReadOnly="True" SortExpression="ART" />   
                        
                        <asp:BoundField DataField="UM_A" HeaderText="UNIDAD MEDIDA" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_SOL_A" HeaderText="CANTIDAD SOLICITADA"  SortExpression="SOL" ReadOnly="True" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_PRESUP_A" HeaderText="CANTIDAD PRESUPUESTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_ACTAS_A" HeaderText="CANTIDAD ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />
                        
                        <asp:BoundField DataField="CANT_DEV_A" HeaderText="CANTIDAD DEVUELTA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_APROB_A" HeaderText="CANTIDAD EJECUTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_APROB_ACTAS_A" HeaderText="CANTIDAD EJECUTADA EN ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_DISP3_A" HeaderText="CANTIDAD DISPONIBLE" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Left" />

                        <asp:BoundField DataField="ANT_SIN_APROB_A" HeaderText="ANTERIORES SIN APROBAR" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="PRESUPUESTADO_ACT" HeaderText="ACT PRESUP" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="PRESUPUESTADO" HeaderText="MAT PRESUP" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="CANT_ACTA_LINEA_A" HeaderText="CANTIDAD ACTA ASIGNADA" ReadOnly="True" Visible="true" SortExpression="SOL" DataFormatString="{0:N}" />
                                      
                        
       
                    </Columns>
                </asp:GridView>


                    

                </div>
                
                <div style="">

                <asp:GridView ID="dtgCuadroCom1" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="ID" 
                        CssClass="testgrid1" selectedindex="1"
                        EnableModelValidation="True" Width="100%" Visible="true" >
                    <Columns>
                        
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False" SortExpression="ID" />

                        <asp:BoundField DataField="NOM_ACTIVIDAD" HeaderText="ACTIVIDAD" ReadOnly="True" SortExpression="ACT" />

                        <asp:BoundField DataField="NOM_MATERIAL" HeaderText="MATERIAL" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="UM_P" HeaderText="UM" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_SOL_P" HeaderText="CANT SOL"  SortExpression="SOL" ReadOnly="True" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="PRESUPUESTADO" HeaderText="MAT PRESUP" ReadOnly="True" SortExpression="ART" />

                                                
                    </Columns>
                </asp:GridView>
                    <asp:SqlDataSource ID="detalleCuadroCom2" runat="server" ProviderName="System.Data.SqlClient" >
                         <SelectParameters>
                            <asp:ControlParameter ControlID="lblCodigoProyecto" DefaultValue="ninguno" Name="CODIGO_PROYECTO" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                 <asp:GridView ID="dtgCuadroCom2" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="NOM_MATERIAL" 
                       CssClass="testgrid1" selectedindex="1"
                        EnableModelValidation="True" Width="100%" Visible="true" >
                    <Columns>
                        
                        
                        <asp:BoundField DataField="NOM_MATERIAL" HeaderText="MATERIAL" ReadOnly="True" SortExpression="ART" />
                                               
                        <asp:BoundField DataField="UM_P" HeaderText="UM" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_PRESUP_ACUM_P" HeaderText="CANT PRESUP"  SortExpression="SOL" ReadOnly="True" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_APROB_ACUM_P" HeaderText="CANT APROB" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_APROB_ACTAS_ACUM_P" HeaderText="CANT APROB ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_DISP_P" HeaderText="CANT DISP" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                       
                                                
                    </Columns>
                </asp:GridView>
                    <asp:SqlDataSource ID="detalleCuadroCom1" runat="server" ProviderName="System.Data.SqlClient" >
                         <SelectParameters>
                            <asp:ControlParameter ControlID="lblCodigoProyecto" DefaultValue="ninguno" Name="CODIGO_PROYECTO" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
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

        <div class="row" >
        <div class="col-sm-4">
        <div id="contEditarLinea" runat="server" style="display:none;">
            
            <div class="control-group">
                <label for="nueva" class="control-label"> Actividad: </label>
                <div class="controls" style="text-align:left;">
                    <asp:DropDownList ID="cmbActividad" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                    <asp:Label ID="lblActividad" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                </div>
            </div>
            <div class="control-group">
                <label for="nueva" class="control-label"> Material: </label>
                <div class="controls" style="text-align:left;">
                    <asp:DropDownList ID="cmbMaterial" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                    <asp:Label ID="lblMaterial" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                </div>
            </div> 
            <div class="control-group">
                <label for="nueva" class="control-label"> Artículo: </label>
                <div class="controls" style="text-align:left;">
                    <asp:DropDownList ID="cmbArticulo" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                    <asp:Label ID="lblArticulo" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                </div>
            </div>
            <div class="row"> 
                <div class="col-sm-4" >
                    <div class="control-group">
                        <label for="nueva" class="control-label"> Acta Disponible: </label>
                        <div class="controls" style="text-align:left;">
                            <asp:DropDownList ID="cmbActa" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                            <asp:Label ID="Label2" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4" >
                    <div class="control-group" runat="server" id="Div5">
                        <label for="nueva" class="control-label">Cantidad Acta: </label>
                        <div class="controls" >
                            <asp:TextBox ID="txtCantActa" autocomplete="off" style="text-align:center;" runat="server" onkeyup="validarImporte();mascara2(this);" onkeypress="ValidaSoloNumeros()" class="form-control input-md" AutoPostBack="false" placeholder=""></asp:TextBox>
                            <asp:Label ID="lblCantActaDisp" runat="server" Text="" Font-Bold="True" Font-Size="X-Large" style="display:none;"></asp:Label>
                            <asp:Label ID="lblCantidads" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                             <asp:Label ID="Label5" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">   
                <div class="col-sm-4" >
                    <div class="control-group" runat="server" id="Div4">
                        <label for="nueva" class="control-label">Cantidad: </label>
                        <div class="controls" >
                            <asp:TextBox ID="txtCantidad" autocomplete="off" style="text-align:center;" runat="server" onkeyup="mascara(this)" onkeypress="ValidaSoloNumeros()" class="form-control input-md" AutoPostBack="false" placeholder=""></asp:TextBox>
                            <asp:Label ID="lblCantEq" runat="server" Text="" Font-Bold="True" Font-Size="X-Large" ></asp:Label>
                            <asp:Label ID="lblFactor" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                             <asp:Label ID="lblId" runat="server" Text="" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2" >
                    <div class="control-group" runat="server" id="Div2">
                        <label for="nueva" class="control-label">UM: </label>
                        <fieldset id="umS" runat="server" disabled="disabled">
                            <asp:Label ID="lblUM" runat="server" Text="" Font-Bold="True" Font-Size="X-Large" ></asp:Label><br />
                            <asp:Label ID="lblUMEq" runat="server" Text="KG" Font-Bold="True" Font-Size="X-Large" ></asp:Label>
                            <asp:Label ID="lblUMAux" runat="server" Text="KG" Font-Bold="True" Font-Size="X-Large" style="display:none;"  ></asp:Label>
                        </fieldset>
                    </div>
                </div>
                <div class="col-sm-6" >
                    <div class="control-group" runat="server" id="Div3">
            <br/>
                        <div class="controls">
                            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                <div class="btn-group" role="group">
                                    <asp:Button ID="btnAceptarEdit" runat="server" Text="Aceptar" CssClass="btn btn-vitalicia btn-md" causesvalidation="true" validationgroup="GrupoValidador" />
                                </div>                        
                                
                                <div class="btn-group" role="group">
                                    <asp:Button ID="btnCancelarEdit" runat="server" Text="Cancelar" CssClass="btn btn-default btn-md" causesvalidation="true" validationgroup="GrupoValidador" />
                                </div>
                            </div>
                            
                            
                            
                        </div>
                    </div>
                </div>
            </div>
             <div class="row" id="mensaje2" runat="server" style="display:none;">

                <div class="col-sm-12" >
                    <div runat="server" id="lblMensaje2S" class="alert alert-danger" >
                        <strong><asp:Label ID="lblMensaje2" runat="server" Text="" CssClass="alert alert-error"></asp:Label></strong>
                        
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-sm-1" >
                </div>

                <div class="col-sm-8" >
                    <div runat="server" id="lblErrorArticuloS" class="" >
                        <strong><asp:Label ID="lblErrorArticulo" runat="server" Text=""></asp:Label></strong>
                    </div>
                </div>

                <div class="col-sm-1" >
                </div>
            </div>
        </div>
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
                                <label for="nueva" class="control-label">Solicitante:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtSolicitante" maxlength="50"  runat="server" class="form-control input-sm" placeholder="Solicitante"></asp:TextBox>
                                </div>
                                <label for="actual" class="control-label">Fecha Solicitud: </label>
                                <div class="controls">
                                    <asp:TextBox ID="txtFechaSol" runat="server" class="form-control input-sm" placeholder="Fecha"></asp:TextBox>
                                </div>

                            </div>

                            <div class="col-sm-6" >
                                <label for="actual" class="control-label">Fecha Aprob SO: </label>
                                <div class="controls">
                                    <asp:TextBox ID="txtFechaSO" runat="server" class="form-control input-sm" placeholder="Fecha"></asp:TextBox>
                                </div>
                                <label for="actual" class="control-label">Fecha Aprob DO: </label>
                                <div class="controls">
                                    <asp:TextBox ID="txtFechaDO" runat="server" class="form-control input-sm" placeholder="Fecha"></asp:TextBox>
                                </div>
                            </div>              
                        </fieldset>
        </div>
        
        <div class="col-sm-4" >
            
            <div class="row" id="contObsRechazadas" runat="server" style="display:none;" >
                <div class="col-sm-12" >
                    <div class="control-group">
                        <label for="nueva" class="control-label">Observaciones Ingeniería: </label>

                        <div class="controls text-right ">
                            <h4><span class="label label-success" id="formato"><asp:Label ID="txtCaracter" runat="server" Text=""></asp:Label></span></h4>
                            <textarea name="txtObservaciones" id="txtObsRechazados" runat="server" onkeyup="contarCaracteres(this)" onkeydown="contarCaracteres(this)" onkeypress="contarCaracteres(this)" maxlength="1500" cols="40" rows="2" class="form-control input-md" placeholder="Observaciones" ></textarea>
                        </div>
                    </div>
                </div>          
            </div>
        </div>
    </div>
 
   
    <br/>
    
    

    


    <!-- INICIO CONFIRMACION DESCARTE DE DATOS-->
    <div class="modal fade" id="modalConfirmarAprobar" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Cuadro de confirmación</h4>
                </div>
                <div class="modal-body">
                    <p>¿Seguro que desea aprobar la Solicitud seleccionada?</p>
                    
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="...">
                        <div class="btn-group" role="group">
                            <input id="btnAprobarSi" type="button" value="Si" runat="server" data-dismiss="modal" class="btn btn-vitalicia btn-md" />
                        </div>
  
                        <div class="btn-group" role="group">
                            <input id="btnAprobarNo" type="button" value="No" runat="server" data-dismiss="modal" class="btn btn-default btn-md" />
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
    

    
    function mascara(d)
    {
        val = d.value

        cuenta = 0;
        posicion = val.indexOf(".");
        while (posicion != -1) {
            cuenta++;
            posicion = val.indexOf(".", posicion + 1);
        }

        if (cuenta > 1) {
            d.value = d.value.substring(0, d.value.length - cuenta+1);
           // document.getElementById('<%= lblCantEq.ClientID %>').innerHTML = resultado.substring(0, resultado.length - 1)+"ggg";
        }
        
        
        val = d.value
        
        factor=document.getElementById('<%= lblFactor.ClientID %>').innerHTML;

        resultado=val/factor
        resultado = Math.round(resultado * 100) / 100;
        document.getElementById('<%= lblCantEq.ClientID %>').innerHTML = resultado;
        document.getElementById('<%= lblCantEq.ClientID %>').innerHTML = resultado;
      
    }

    function mascara2(d)
    {
        val = d.value

        cuenta = 0;
        posicion = val.indexOf(".");
        while (posicion != -1) {
            cuenta++;
            posicion = val.indexOf(".", posicion + 1);
        }

        if (cuenta > 1) {
            d.value = d.value.substring(0, d.value.length - cuenta+1);
           // document.getElementById('<%= lblCantEq.ClientID %>').innerHTML = resultado.substring(0, resultado.length - 1)+"ggg";
        }
        
        
        val = d.value
        
        factor=document.getElementById('<%= lblFactor.ClientID %>').innerHTML;

        resultado=val/factor
        resultado = Math.round(resultado * 100) / 100;
        
      
    }

    //Función que permite solo Números
    function ValidaSoloNumeros()
        
    {
        var de = document.getElementById('<%= txtCantidad.ClientID %>').innerHTML;
        
        if ((event.keyCode < 48) || (event.keyCode > 58)) {
            if ((event.keyCode == 46)) {
                event.returnValue = true;
            }
            else {
                event.returnValue = false;

            }
        }

        
    }

    function validarImporte() {
        var cantidad = 0;
        var cantPedido = 0;
        var cantDisp = 0;
        
        var txtCantidad = document.getElementById('txtCantActa').value;
        var lblCantDisp = document.getElementById('<%= lblCantActaDisp.ClientID %>').innerHTML;
        var txtCantPedido = document.getElementById('txtCantidad').value;
        
        cantidad = (txtCantidad);
        cantDisp = lblCantDisp;
        cantPedido = txtCantPedido;
       

        if (cantidad > Math.round(cantDisp * 100) / 100) {
            document.getElementById("lblErrorArticuloS").style = "";
            document.getElementById("lblErrorArticuloS").className = "alert alert-danger";
            document.getElementById('<%= lblErrorArticulo.ClientID %>').innerHTML = "No se puede ingresar una cantidad mayor a " + Math.round(cantDisp * 100) / 100 + " en actas" ;
            document.getElementById("btnAceptarEdit").className = "btn btn-vitalicia btn-md disabled";
        }
        else {
            
            if (cantidad > Math.round(cantPedido * 100) / 100) {
            document.getElementById("lblErrorArticuloS").style = "";
            document.getElementById("lblErrorArticuloS").className = "alert alert-danger";
            document.getElementById('<%= lblErrorArticulo.ClientID %>').innerHTML = "No se puede ingresar una cantidad mayor a " + Math.round(cantPedido * 100) / 100 + " en actas";
            document.getElementById("btnAceptarEdit").className = "btn btn-vitalicia btn-md disabled";
            }
            else {
            
                document.getElementById('<%= lblErrorArticulo.ClientID %>').innerHTML = "";
                document.getElementById("lblErrorArticuloS").style = "display:none;";
                document.getElementById("lblErrorArticuloS").className = "alert alert-info";
                document.getElementById("btnAceptarEdit").className = "btn btn-vitalicia btn-md";
        
                }
        
            }
       
       }

   function contarCaracteres(d)
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

       
   }

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
