<%@ Page culture="es-MX" UICulture="es-MX"  Language="VB" AutoEventWireup="false" CodeFile="nuevaSolicitud.aspx.vb" Inherits="nuevaSolicitud" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nueva Solicitud</title>
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

    <link href="../css/alertify.core.css" rel="stylesheet" />
    <link href="../css/alertify.bootstrap.css" rel="stylesheet" />    
    <script src="../js/alertify.min.js"></script>
    
    
    
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

                    "lengthMenu": [[10, 15, 20, -1], [10, 15, 20, "Todos"]],
                    "responsive": true,
                    "stateSave": true,
                    "paging": true,
                    "ordering": true,
                    "info": true,
                    "searching": true,
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
            <a class="navbar-brand" href="#"><strong>NUEVA SOLICITUD</strong></a>
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
                      Unidad de Medida <b class="caret"></b> <br />
                    Actual:<strong> <asp:Label ID="lblUMActual" runat="server" Text="Presupuesto"></asp:Label></strong>
                    </a>
                    <ul class="dropdown-menu">
                        
                        <li><a href="#">
                            <asp:Button ID="btnPresupuesto" runat="server" Text="Presupuesto" CssClass="btn btn-default btn-md" /></a></li>
                        <li><a href="#"><asp:Button ID="btnAlmacenes" runat="server" Text="&nbsp;&nbsp;&nbsp;Almacén&nbsp;&nbsp;&nbsp;" CssClass="btn btn-default btn-md" /></a></li>
                    </ul>
                </li>
                <li ><a href="#"><strong>Usuario/Proyecto: <br /><asp:Label ID="lblUsuario" runat="server" Text="" Font-Size="Small"></asp:Label></strong></a></li>
                <li><a href="#">
                    <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="btn btn-vitalicia btn-md"/></a>
                </li>
            </ul>
        </div>
    </nav>










            <!---------------INICIO CABECERA----------------------->
            <div id="page-header">
                <div class="container">
                   <div class="text-right" >
					    
					</div>
                </div>
            </div>
             <!---------------FIN CABECERA----------------------->

             <!---------------INICIO TITULOS----------------------->
            <div class="text-center" >
                
                <h5 class="text-center"><strong><asp:Label ID="lblCodigoProyecto" runat="server" Text="Label"></asp:Label></strong></h5>
                <asp:Label ID="lblId" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                
                <div class="container">
                    <div class="row">
                        <div class="col-sm-3" >
                            
                        </div>

                        <div class="col-sm-6" >
                            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                <div class="btn-group" role="group">
                                    <input id="btnGenerar" type="button" value="Generar Solicitud" data-target="#modalObservaciones" class="btn btn-vitalicia btn-md"  runat="server" data-toggle="modal" data-backdrop="static" />
                                </div>                        
                                <div class="btn-group" role="group">
                                    <input id="btnCancelar" type="button" value="Cancelar Solicitud" data-target="#modalConfirmarDescartar" class="btn btn-default btn-md" runat="server" data-toggle="modal" data-backdrop="static" />
                                </div>
                                <div class="btn-group" role="group">
                                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-default btn-md" causesvalidation="true" validationgroup="GrupoValidador" />
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
                            <div runat="server" id="lblMensajeS" class="">
                            <strong><asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></strong>
                        </div>
                        </div>

                        <div class="col-sm-3" >
                        </div>
                        
                    </div>
                </div>
                
            </div>

             <!---------------FIN TITULOS----------------------->

    


    <div class="row">
        <div class="col-sm-4" >
            <div class="control-group">
                <label for="nueva" class="control-label"> Actividad: </label>
                <div class="controls" style="text-align:left;">
                    <asp:DropDownList ID="cmbActividad" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-sm-4" >
            <div class="control-group">
                <label for="nueva" class="control-label"> Material: </label>
                <div class="controls" style="text-align:left;">
                    <asp:DropDownList ID="cmbMaterial" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                </div>
            </div>   
        </div>
        <div class="col-sm-4" >
             <div class="control-group">
                <label for="nueva" class="control-label"> Artículo: </label>
                <div class="controls" style="text-align:left;">
                    <asp:DropDownList ID="cmbArticulo" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                </div>
            </div>

            <div class="table-responsive" >
                <div class="control-group" id="dtgArticuloDetalleS" runat="server">

                    <asp:GridView ID="dtgArticuloDetalle" runat="server" DataSourceID="datosArticulo" CssClass="testgrid2" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True">                   
                        <Columns>
                            <asp:BoundField DataField="ARTICULO" HeaderText="ARTICULO" ReadOnly="True" SortExpression="ARTICULO" />
                        
                            <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" ReadOnly="True" SortExpression="DESCRIPCION" />
                        
                            <asp:BoundField DataField="UNIDAD_ALMACEN" HeaderText="U.M." ReadOnly="True" SortExpression="UNIDAD_ALMACEN" />
                        
                            <asp:BoundField DataField="CANT_DISP_P" HeaderText="CANT. DISP." ReadOnly="True" SortExpression="CANTIDAD_DISPONIBLE" DataFormatString="{0:N}" />
                        </Columns>
                    </asp:GridView>

                    <asp:SqlDataSource ID="datosArticulo" runat="server" ProviderName="System.Data.SqlClient">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="cmbArticulo" DefaultValue="ninguno" Name="ARTICULO" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource> 
                </div> 
            </div>

            <div class="row" id="mensaje2" runat="server" style="display:none;">
                <div class="col-sm-12" >
                    <div runat="server" id="lblMensaje2S" class="alert alert-danger" >
                        <strong><asp:Label ID="lblMensajes2" runat="server" Text="" CssClass="alert alert-error"></asp:Label></strong>                        
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-4" >
            <div class="control-group" runat="server" id="txtActividadS">
                <label for="nueva" class="control-label">Ingrese una nueva Actividad: </label>
                <div class="controls">
                    <div class="input-group">
                          <asp:TextBox ID="txtActividad" runat="server" onchange="validarEspeciales(this)" onkeypress="validarLetras()" class="form-control input-md" AutoPostBack="false" placeholder="Actividad"></asp:TextBox>
                          <div class="input-group-btn">
                            <button type="button" class="btn btn-default dropdown-toggle"
                                    data-toggle="dropdown">
                              α <span class="caret"></span>
                            </button>
 
                            <ul class="dropdown-menu pull-right" role="menu">
                              
                              <li><input id="Button2" type="button" value="Ver caracteres inválidos" data-target="#modalCaracteres" class="btn btn-default btn-md"  runat="server" data-toggle="modal" data-backdrop="static" /></li>
                              
                            </ul>
                          </div>
                        </div>                    
                </div>
            </div>  
        </div>
        <div class="col-sm-4" >
            <div class="control-group" runat="server" id="txtMaterialS">
                <label for="nueva" class="control-label">Ingrese un nuevo material: </label>
                <div class="controls">                    
                        <div class="input-group">
                          <asp:TextBox ID="txtMaterial" runat="server" onchange="validarEspeciales(this)"  onkeypress="validarLetras()" class="form-control input-md" AutoPostBack="false" placeholder="Material"></asp:TextBox>
 
                          <div class="input-group-btn">
                            <button type="button" class="btn btn-default dropdown-toggle"
                                    data-toggle="dropdown">
                              α <span class="caret"></span>
                            </button>
 
                            <ul class="dropdown-menu pull-right" role="menu">
                              
                              <li><input id="Button1" type="button" value="Ver caracteres inválidos" data-target="#modalCaracteres" class="btn btn-default btn-md"  runat="server" data-toggle="modal" data-backdrop="static" /></li>
                              
                            </ul>
                          </div>
                        </div>                     
                </div>
            </div>       
        </div>
        <div class="col-sm-3" >
            

            <div class="control-group" runat="server" id="cmbArticuloAuxS">
                <label for="nueva" class="control-label">Otros Artículos: </label>
                <div class="control-group">
                    <div class="controls" style="text-align:left;">
                        <asp:DropDownList ID="cmbArticuloAux" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-sm-1" >
            <div class="control-group" runat="server" id="cmbUMS">
                <label for="nueva" class="control-label">UM: </label>
                <div class="controls">
                    <asp:DropDownList ID="cmbUM" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-8">
        </div>

        <div class="col-sm-3" >
            <div class="control-group" runat="server" id="txtArticuloS">
                <label for="nueva" class="control-label">Ingrese un nuevo Artículo: </label>
                <div class="controls">
                    <div class="input-group">
                          <asp:TextBox ID="txtArticulo" runat="server" onchange="validarEspeciales(this)" onkeypress="validarLetras()" class="form-control input-md" AutoPostBack="false" placeholder="Artículo"></asp:TextBox>
                          <div class="input-group-btn">
                            <button type="button" class="btn btn-default dropdown-toggle"
                                    data-toggle="dropdown">
                              α <span class="caret"></span>
                            </button>
 
                            <ul class="dropdown-menu pull-right" role="menu">
                              
                              <li><input id="Button3" type="button" value="Ver caracteres inválidos" data-target="#modalCaracteres" class="btn btn-default btn-md"  runat="server" data-toggle="modal" data-backdrop="static" /></li>
                              
                            </ul>
                          </div>
                        </div>



                    
                </div>
            </div>
        </div>
            
    </div>
    <div class="row">
        <div class="col-sm-8" >
        </div>    
        
        
        
        <div class="col-sm-4" >
            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                <div class="btn-group" role="group">
                <label for="nueva" class="control-label">Cantidad: </label>
                    <div class="controls" >
                        <asp:TextBox ID="txtCantidad" autocomplete="off" style="text-align:center;" runat="server" onkeyup="mascara(this)" onkeypress="ValidaSoloNumeros()" class="form-control input-md" AutoPostBack="false" placeholder=""></asp:TextBox>
                        <asp:Label ID="lblCantEq" runat="server" Text="" Font-Bold="True" Font-Size="X-Large" ></asp:Label>
                        <asp:Label ID="lblFactor" runat="server" Text="" Font-Bold="True" Font-Size="X-Large" style="display:none;"></asp:Label>
                    </div>
                        
                
                </div>
                <div class="btn-group" role="group">
                    <label for="nueva" class="control-label">UM: </label>
                    <div class="controls" >
                        <asp:Label ID="lblUM" runat="server" Text="" Font-Bold="True" Font-Size="X-Large" ></asp:Label></br>
                        <asp:Label ID="lblUMEq" runat="server" Text="KG" Font-Bold="True" Font-Size="X-Large" ></asp:Label>
                    </div>
                </div>
                <div class="btn-group" role="group">
                    <label for="nueva" class="control-label"> </label>
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-vitalicia btn-md" causesvalidation="true" validationgroup="GrupoValidador" />
                </div>                        
                <div class="btn-group" role="group">
                    <label for="nueva" class="control-label"></label>
                    <asp:Button ID="btnCancelarEdit" runat="server" Text="Cancelar" CssClass="btn btn-default btn-md" causesvalidation="true" validationgroup="GrupoValidador" />
                </div>
                                
            </div>
        </div>
    </div>     
              
            

            
            
            


       

    <div class="row">
        <div class="col-sm-12" >
            <div class="table-responsive" >
                <div class="testgrid1">
                    <div id="contDtgDetalle" style="" runat="server">
                <asp:GridView ID="dtgDetalle" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="ID" 
                        DataSourceID="Detalles" CssClass="testgrid2" selectedindex="1"
                        EnableModelValidation="True" Width="100%" Visible="true">
                    <Columns>
                        <asp:CommandField ButtonType="Link" SelectText ="Editar" ShowSelectButton="True" ControlStyle-CssClass="buttonControl"  >
                            <ControlStyle CssClass="label label-warning" />
                        </asp:CommandField>
                        
                        <asp:CommandField ButtonType="Link" DeleteText="Borrar" ShowDeleteButton="True" ControlStyle-CssClass="buttonControl"  >
                            <ControlStyle CssClass="label label-warning" />
                        </asp:CommandField>
                            
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="True" SortExpression="ID" />

                        <asp:BoundField DataField="NOM_ACTIVIDAD" HeaderText="ACTIVIDAD" ReadOnly="True" SortExpression="ACT" />

                        <asp:BoundField DataField="NOM_MATERIAL" HeaderText="MATERIAL" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="NOM_ARTICULO" HeaderText="ARTÍCULO" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="UM_P" HeaderText="UM" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_SOL_P" HeaderText="CANT SOL"  SortExpression="SOL" DataFormatString="{0:N}" ApplyFormatInEditMode="true"  />

                        <asp:BoundField DataField="CANT_PRESUP_P" HeaderText="CANT PRESUP" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_ACTAS_P" HeaderText="CANT ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />
                        
                        <asp:BoundField DataField="CANT_DEV_P" HeaderText="CANT DEV" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_APROB_P" HeaderText="CANT EJEC" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_APROB_ACTAS_P" HeaderText="CANT EJEC ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_DISP3_P" HeaderText="CANT DISP" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Left" />

                        <asp:BoundField DataField="ANT_SIN_APROB_P" HeaderText="ANT SIN APROB" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                         <asp:BoundField DataField="PRESUPUESTADO_ACT" HeaderText="ACT PRESUP" ReadOnly="True" SortExpression="ART" />

                         <asp:BoundField DataField="PRESUPUESTADO" HeaderText="MAT PRESUP" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="ESTADO_LIN" HeaderText="ESTADO LINEA" ReadOnly="True" SortExpression="ART"  Visible="true" />

                        <asp:BoundField DataField="ACTIVIDAD" HeaderText="ACTIVIDAD" ReadOnly="True" Visible="TRUE" SortExpression="ACT" HeaderStyle-Width="10px" />

                        <asp:BoundField DataField="MATERIAL" HeaderText="MATERIAL" ReadOnly="True" Visible="true" SortExpression="ART" />
         
                        <asp:BoundField DataField="ARTICULO" HeaderText="ARTICULO" ReadOnly="True" SortExpression="ART" />   
                        
                        <asp:BoundField DataField="UM_A" HeaderText="UM" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_SOL_A" HeaderText="CANT SOL"  SortExpression="SOL" ReadOnly="True" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_PRESUP_A" HeaderText="CANT PRESUP" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_ACTAS_A" HeaderText="CANT ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />
                        
                        <asp:BoundField DataField="CANT_DEV_A" HeaderText="CANT DEV" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_APROB_A" HeaderText="CANT EJEC" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_APROB_ACTAS_A" HeaderText="CANT EJEC ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_DISP3_A" HeaderText="CANT DISP" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Left" />

                        <asp:BoundField DataField="ANT_SIN_APROB_A" HeaderText="ANT SIN APROB" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="PRESUPUESTADO_ACT" HeaderText="ACT PRESUP" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="PRESUPUESTADO" HeaderText="MAT PRESUP" ReadOnly="True" SortExpression="ART" />
        
                        <asp:BoundField DataField="CANT_PRESUP_ACTAS_P" HeaderText="CANT PRESUP + ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_DISP3_P" HeaderText="CANT DISP" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Left" />


                    </Columns>

                    <selectedrowstyle font-bold="true" Font-Size="Medium"/>  
                </asp:GridView>
                
                    <asp:SqlDataSource ID="Detalles" runat="server" ProviderName="System.Data.SqlClient" >
                         <SelectParameters>
                            <asp:ControlParameter ControlID="lblCodigoProyecto" DefaultValue="ninguno" Name="CODIGO_PROYECTO" PropertyName="Text" Type="String" />

                        </SelectParameters>

                        <DeleteParameters>
                            <asp:Parameter Name="CODIGO_SOLICITUD" Type="String" />
                        </DeleteParameters>
    
                        <InsertParameters>
                            <asp:Parameter Name="FASE" Type="String" />
                            <asp:Parameter Name="ARTICULO" Type="String" />
                            <asp:Parameter Name="CANTIDAD" Type="Decimal" />
                            <asp:Parameter Name="CODIGO_SOLICITUD" Type="String" />
                        </InsertParameters>

                        <UpdateParameters>
                            <asp:Parameter Name="PROYECTO" Type="String" />
                            <asp:Parameter Name="FASE" Type="String" />
                            <asp:Parameter Name="ARTICULO" Type="String" />
                            <asp:Parameter Name="CANTIDAD_SOLICITADA" Type="Decimal" />
                            <asp:Parameter Name="CODIGO_SOLICITUD" Type="String" />
                        </UpdateParameters>

                    </asp:SqlDataSource>
                
                    </div>
                </div>
            </div>
            
        </div>
    </div>

    



    <!-- INICIO CONFIRMACION DESCARTE DE DATOS-->
    <div class="modal fade" id="modalConfirmarDescartar" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Cuadro de confirmación</h4>
                </div>
                <div class="modal-body">
                    <p>¿Seguro que desea eliminar la solicitud? Perderá todos los cambios realizados.</p>
                    
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="...">
                        <div class="btn-group" role="group">
                            <input id="btnDescartar" type="button" value="Si" runat="server" data-dismiss="modal" class="btn btn-vitalicia btn-md" />
                        </div>
  
                        <div class="btn-group" role="group">
                            <input id="btnDescartarCancelar" type="button" value="No" runat="server" data-dismiss="modal" class="btn btn-default btn-md" />
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
        var factor = 0;
        cuenta = 0;
        posicion = val.indexOf(".");
        while (posicion != -1) {
            cuenta++;
            posicion = val.indexOf(".", posicion + 1);
        }
        
        if (cuenta > 1) {
            d.value = d.value.substring(0, d.value.length - cuenta+1);
           // document.getElementById('<%= lblCantEq.ClientID %>').InnerText = resultado.substring(0, resultado.length - 1)+"ggg";
        }
        
       
        val = d.value
       
        factor=document.getElementById('<%= lblFactor.ClientID %>').innerHTML;
        
        resultado=val/factor
        resultado = Math.round(resultado * 100) / 100;
        document.getElementById('<%= lblCantEq.ClientID %>').innerHTML = resultado;
        document.getElementById('<%= lblCantEq.ClientID %>').innerHTML = resultado;
        
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

        function validarEspeciales(d) {
            val = getCleanedString(d.value)
            d.value = val
        }
    
        function getCleanedString(cadena) {
            // Definimos los caracteres que queremos eliminar
            var specialChars = "'ÇüâäàåçêëèïîìÄÅæÆôöòûùÿÖÜø£Ø×ƒªº¿®¬½¼¡«»░▒▓│┤ÂÀ©╣║╗╝¢¥┐└┴┬├─┼ãÃ╚╔╩╦╠═╬¤ðÐÊËÈıÎÏ┘┌█▄¦Ì▀ßÔÒõÕµþÞÛÙýÝ¯´≡±‗¾¶§÷¸°¨·¹³²■";
            
            // Los eliminamos todos
            for (var i = 0; i < specialChars.length; i++) {
                cadena = cadena.replace(new RegExp("\\" + specialChars[i], 'gi'), '');
            }
                       
            // Quitamos acentos y "ñ". Fijate en que va sin comillas el primer parametro
            cadena = cadena.replace(/á/gi, "a");
            cadena = cadena.replace(/é/gi, "e");
            cadena = cadena.replace(/í/gi, "i");
            cadena = cadena.replace(/ó/gi, "o");
            cadena = cadena.replace(/ú/gi, "u");
            cadena = cadena.replace(/ñ/gi, "n");
            cadena = cadena.replace(/Á/gi, "A");
            cadena = cadena.replace(/É/gi, "E");
            cadena = cadena.replace(/Í/gi, "I");
            cadena = cadena.replace(/Ó/gi, "O");
            cadena = cadena.replace(/Ú/gi, "U");
            cadena = cadena.replace(/Ñ/gi, "N");
            return cadena;
        }

        function validarLetras()
        
        {
            
            
            if ((event.keyCode < 32) || (event.keyCode > 126)) {
                if ((event.keyCode == 46)) {
                    event.returnValue = true;
                    
                }
                else {
                    event.returnValue = false;
                    alertify.error("El caracter es inválido" );

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




       function redirect() {
       window.location = "Principal.aspx";
       }

               
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
                        <div class="row">
                            <div class="col-sm-6" >
                                <label for="actual" class="control-label">Fecha: </label>
                                <div class="controls">
                                    <asp:TextBox ID="txtFecha" runat="server" class="form-control input-md" placeholder="Fecha"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-6" >
                                <label for="nueva" class="control-label">Solicitante:</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtSolicitante" runat="server" maxlength="50" class="form-control input-md" placeholder="Solicitante"></asp:TextBox>
                                </div>
                            </div>              
                        </div>
                        
                        
                        
                    </div>
                    
                    <div class="control-group">
                        <label for="nueva" class="control-label">Observaciones: </label>
                        <div class="controls text-right">
                            <h4><span class="label label-success" id="formato"><asp:Label ID="txtCaracter" runat="server" Text=""></asp:Label></span></h4>
                            <textarea name="txtObservaciones" id="txtObservaciones" onkeyup="contarCaracteres(this)" onkeydown="contarCaracteres(this)" onkeypress="contarCaracteres(this)" runat="server" maxlength="1500" cols="40" rows="5" class="form-control input-md" placeholder="Observaciones"></textarea>
                            
                        </div>
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



             <!-- Modal -->
  <div class="modal fade" id="modalCaracteres" role="modal">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Lista de Caracteres no Permitidos</h4>
        </div>
        <div class="modal-body">
          <div class="row">
        <div class="col-sm-12" >
            <div class="table-responsive" >
                <div class="testgrid2">
                    <div id="Div1" style="" runat="server">



                        <table class="testgrid2" id="dtgCaracteres">
                            <thead>
                                <tr role="row" class="odd">
                                    <th>.</th>
                                    <th>.</th>
                                    <th>.</th>
                                    <th>.</th>
                                    <th>.</th>
                                    <th>.</th>
                                    <th>.</th>
                                    <th>.</th>
                                    <th>.</th>
                                    <th>.</th>
                                    <th>.</th>
                                    <th>.</th>
                                    <th>.</th>
                                    <th>.</th>
                                    <th>.</th>
                                    <th>.</th>
                                    
                                </tr>
                            </thead>
                            <tbody>
                                <tr role="row" class="odd">
                                    <td></td>
                                    <td>☺</td>
                                    <td>☻</td>
                                    <td>♥</td>
                                    <td>♦</td>
                                    <td>♣</td>
                                    <td>♠</td>
                                    <td>•</td>
                                    <td>◘</td>
                                    <td>○</td>
                                    <td>◙</td>
                                    <td>♂</td>
                                    <td>♀</td>
                                    <td>♪</td>
                                    <td>♫</td>
                                    <td>☼</td>
                                </tr>
                                <tr>
                                    <td>Ç</td>
                                    <td>ü</td>
                                    <td>é</td>
                                    <td>â</td>
                                    <td>ä</td>
                                    <td>à</td>
                                    <td>å</td>
                                    <td>ç</td>
                                    <td>ê</td>
                                    <td>ë</td>
                                    <td>è</td>
                                    <td>ï</td>
                                    <td>î</td>
                                    <td>ì</td>
                                    <td>Ä</td>
                                    <td>Å</td>
                                </tr>
                                <tr>
                                    <td>É</td>
                                    <td>æ</td>
                                    <td>Æ</td>
                                    <td>ô</td>
                                    <td>ö</td>
                                    <td>ò</td>
                                    <td>û</td>
                                    <td>ù</td>
                                    <td>ÿ</td>
                                    <td>Ö</td>
                                    <td>Ü</td>
                                    <td>¢</td>
                                    <td>£</td>
                                    <td>¥</td>
                                    <td>₧</td>
                                    <td>ƒ</td>
                                </tr>
                                <tr>
                                    <td>á</td>
                                    <td>í</td>
                                    <td>ó</td>
                                    <td>ú</td>
                                    <td>ñ</td>
                                    <td>Ñ</td>
                                    <td>ª</td>
                                    <td>º</td>
                                    <td>¿</td>
                                    <td>⌐</td>
                                    <td>¬</td>
                                    <td>½</td>
                                    <td>¼</td>
                                    <td>¡</td>
                                    <td>«</td>
                                    <td>»</td>
                                </tr>
                                <tr>
                                    <td>░</td>
                                    <td>▒</td>
                                    <td>▓</td>
                                    <td>│</td>
                                    <td>┤</td>
                                    <td>╡</td>
                                    <td>╢</td>
                                    <td>╖</td>
                                    <td>╕</td>
                                    <td>╣</td>
                                    <td>║</td>
                                    <td>╗</td>
                                    <td>╝</td>
                                    <td>╜</td>
                                    <td>╛</td>
                                    <td>┐</td>
                                </tr>
                                <tr>
                                    <td>└</td>
                                    <td>┴</td>
                                    <td>┬</td>
                                    <td>├</td>
                                    <td>─</td>
                                    <td>┼</td>
                                    <td>╞</td>
                                    <td>╟</td>
                                    <td>╚</td>
                                    <td>╔</td>
                                    <td>╩</td>
                                    <td>╦</td>
                                    <td>╠</td>
                                    <td>═</td>
                                    <td>╬</td>
                                    <td>╧</td>
                                </tr>
                                <tr>
                                    <td>╨</td>
                                    <td>╤</td>
                                    <td>╥</td>
                                    <td>╙</td>
                                    <td>╘</td>
                                    <td>╒</td>
                                    <td>╓</td>
                                    <td>╫</td>
                                    <td>╪</td>
                                    <td>┘</td>
                                    <td>┌</td>
                                    <td>█</td>
                                    <td>▄</td>
                                    <td>▌</td>
                                    <td>▐</td>
                                    <td>▀</td>
                                </tr>
                                <tr>
                                    <td>α</td>
                                    <td>ß</td>
                                    <td>Γ</td>
                                    <td>π</td>
                                    <td>Σ</td>
                                    <td>σ</td>
                                    <td>µ</td>
                                    <td>τ</td>
                                    <td>Φ</td>
                                    <td>Θ</td>
                                    <td>Ω</td>
                                    <td>δ</td>
                                    <td>∞</td>
                                    <td>φ</td>
                                    <td>ε</td>
                                    <td>∩</td>
                                </tr>
                                <tr>
                                    <td>≡</td>
                                    <td>±</td>
                                    <td>≥</td>
                                    <td>≤</td>
                                    <td>⌠</td>
                                    <td>⌡</td>
                                    <td>÷</td>
                                    <td>≈</td>
                                    <td>°</td>
                                    <td>∙</td>
                                    <td>·</td>
                                    <td>√</td>
                                    <td>ⁿ</td>
                                    <td>²</td>
                                    <td>■</td>
                                    <td></td>
                                </tr>
                            </tbody>
                            
                    


                    
                    
                    

                   
                    
                    

                   

                </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>


             
        </form>
    </div>





    

</body>
</html>
