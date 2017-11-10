<%@ Page Language="VB" AutoEventWireup="false" CodeFile="presupuestoMateriales.aspx.vb" Inherits="ingenieria_presupuestoMateriales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Presupuesto de Materiales</title>
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
    <link href="../css/select2.min.css" rel="stylesheet" /> 
   <!-- botones de exportacion a excel en tablas -->
    <link href="../css/buttons.dataTables.min.css" rel="stylesheet" /> 

    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery.dataTables.min.js"></script> 
    <script src="../js/select2.full.js"></script>
    <script src="../js/dataTables.bootstrap.min.js"></script>
    <script src="../js/moment.min.js"></script>
    <!-- Include Date Range Picker -->
    <script src="../js/daterangepicker.js"></script>
    <!-- botones de exportacion a excel en tablas -->
    <script src="../js/dataTables.buttons.min.js"></script>
    <script src="../js/jszip.min.js"></script>    
    <script src="../js/buttons.html5.min.js"></script>
      
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

                     <div class="modals" id="carga" style="display:none;">
                        <div class="centers">
                            <img alt="" src="../img/ajax_loader.gif" />
                        </div>
                    </div>
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
                                    <asp:DropDownList ID="cmbProyecto" runat="server" class="mySelect" Width="100%"></asp:DropDownList>
                                
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
                <div class="testgrid4">                     
                    <div id="contDtgMateriales" style="" runat="server">
                        <table id="dtgMateriales" class="testgrid4" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>ACCIONES</th>
                                    <th>MATERIAL</th>
                                    <th>UNIDAD MEDIDA</th>
                                    <th>CANTIDAD SOLICITADA</th>
                                    <th>CANTIDAD PRESUPUESTADA</th>
                                    <th>CANTIDAD ACTAS</th>
                                    <th>CANTIDAD PRESUPUESTADA MAS ACTAS</th>
                                    <th>CANTIDAD DEVUELTA</th>
                                    <th>CANTIDAD EJECUTADA</th>
                                    <th>CANTIDAD DISPONIBLE CON ACTAS</th>
                                    <th>CANTIDAD DISPONIBLE</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
            
        </div>
        <div class="col-sm-12">
           
            
           
            <div class="table-responsive" >
                <div class="testgrid4">                     
                    <div id="dtgDetallesS" style="">
                         <h4><span class="label alert-info" id="lblDetalleS" runat="server"><asp:Label ID="lblDetalle" runat="server" Text=""></asp:Label></span></h4>
                        <table id="dtgDetalles" class="testgrid4" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>ACCIONES</th>
                                    <th>MATERIAL</th>
                                    <th>UNIDAD MEDIDA</th>
                                    <th>CODIGO SOLICITUD</th>
                                    <th>CANTIDAD EJECUTADA</th>
                                    <th>CANTIDAD EJECUTADA EN ACTAS</th>
                                    <th>CANTIDAD SOLICITADA</th>
                                    <th>CANTIDAD RECHAZADA</th>
                                    <th>CANTIDAD DISPONIBLE</th>
                                    <th>FECHA APROBACIÓN INGENIERÍA</th>
                                    
                                    
                                </tr>
                            </thead>
                        </table>
                    
                        
                    </div>
                </div>
            </div>           
        </div>

        <div class="col-sm-12">
            <h4><span class="label alert-info" id="lblDevolucionesS" runat="server"><asp:Label ID="lblDevoluciones" runat="server" Text=""></asp:Label></span></h4>
            
            <div class="table-responsive" >
                <div class="testgrid4">                     
                    <div id="Div2" style="" runat="server">
                        <table id="dtgDevoluciones" class="testgrid4" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>FECHA</th>
                                    <th>PROYECTO</th>
                                    <th>ACTIVIDAD</th>
                                    <th>MATERIAL</th>
                                    <th>UNIDAD MEDIDA</th>
                                    <th>CANTIDAD DEVUELTA</th>
                                   
                                    
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>   
        </div>

        <div class="col-sm-12">
           <h4><span class="label alert-info" id="lblSolicitudesS" runat="server"><asp:Label ID="lblSolicitudes" runat="server" Text=""></asp:Label></span></h4>
            
            <div class="table-responsive" >
                <div class="testgrid4">                     
                    <div id="Div4" style="" runat="server">
                        <table id="dtgSolicitudes" class="testgrid4" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>NO.</th>
                                    <th>ACTIVIDAD</th>
                                    <th>MATERIAL</th>
                                    <th>ARTÍCULO</th>
                                    <th>UNIDAD MEDIDA</th>
                                    <th>CANTIDAD SOLICITADA</th>
                                    <th>CANTIDAD PRESUPUESTADA</th>
                                    <th>CANTIDAD ACTAS</th>
                                    <th>CANTIDAD EJECUTADA</th>
                                    <th>CANTIDAD EJECUTADA ACTAS</th>
                                    <th>CANTIDAD DISPONIBLE</th>
                                    <th>ANTERIORES SIN APROBAR</th>
                                    <th>ESTADO LINEA</th>                                 

                                   
                                    
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>   
        </div>

        <div class="col-sm-12">
           <h4><span class="label alert-info" id="Span1" runat="server"><asp:Label ID="lblActas" runat="server" Text=""></asp:Label></span></h4>
           
            <div class="table-responsive" >
                <div class="testgrid4">                     
                    <div id="Div5" style="" runat="server">
                        <table id="dtgActas" class="testgrid4" cellspacing="0" width="100%">
                            <thead>
                                <tr>
                                    <th>CORRELATIVO</th>
                                    <th>CODIGO ACTA</th>
                                    <th>ACTIVIDAD</th>
                                    <th>MATERIAL</th>
                                    <th>CANTIDAD ACTA</th>
                                    <th>FECHA</th>
                                   
                                    
                                </tr>
                            </thead>
                        </table>
                    </div>
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

     <script type="text/javascript">
         function pageLoad(sender, args) {

             var currentTr = null;
             var currentTr1 = null;
             $(".mySelect").select2();
       

            $('input[name="txtFecha"]').daterangepicker(
      {
          locale: {
              format: 'DD-MM-YYYY'
          },
          showDropdowns: true,
          singleDatePicker: true

      }

             );
          



            var table;
            var table1;

            $(document).ready(function () {
                $(document).ajaxStart(function () {
                    document.getElementById("carga").style = "";
                });
                $(document).ajaxStop(function () {
                    document.getElementById("carga").style = "display:none;";
                });


                llenarComboProyectos();
                table=$('#dtgMateriales').DataTable({

                    "lengthMenu": [[-1,5, 10, 15], ["Todos",5, 10, 15]],
                    "responsive": true,
                    "stateSave": true,
                    "paging": true,
                    "ordering": true,
                    "info": true,
                    "searching": true,
                    "columnDefs": [
                        {
                             "className": 'text-left',
                             "visible": true,
                             "targets": [1]
                         }, {
                             "className": 'text-left',
                             "visible": true,
                             "targets": [2]
                         }, {
                             "className": 'text-right',
                             "visible": true,
                             "targets": [3]
                         }, {
                             "className": 'text-right',
                             "visible": true,
                             "targets": [4]
                         }, {
                             "className": 'text-right',
                             "visible": true,
                             "targets": [5]
                         }, {
                             "className": 'text-right',
                             "visible": true,
                             "targets": [6]
                         }, {
                             "className": 'text-right',
                             "visible": true,
                             "targets": [7]
                         }, {
                             "className": 'text-right',
                             "visible": true,
                             "targets": [8]
                         }, {
                             "className": 'text-right',
                             "visible": true,
                             "targets": [9]
                         }, {
                             "className": 'text-right',
                             "visible": true,
                             "targets": [10]
                         }, {
                             "targets": [0],
                             "data": null,
                             "defaultContent": "<span class='label label-warning edit'  data-toggle='modal'><i class='fa fa-edit'>Detalle</i></span>"
                         }
                    ],
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
           
                table1=$('#dtgDetalles').DataTable({

                    "lengthMenu": [[-1, 5, 10, 15], ["Todos", 5, 10, 15]],
                    "responsive": true,
                    "stateSave": true,
                    "paging": false,
                    "ordering": true,
                    "info": true,
                    "searching": true,
                    dom: 'Bfrtip',
                    buttons: [
                        {
                            text: 'Exportar a Excel',
                            extend: 'excelHtml5',
                            title: 'Detalle de Materiales',
                        }
                    ],

                   
                    "columnDefs": [
                        {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [1]
                        }, {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [2]
                        }, {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [3]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [4]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [5]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [6]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [7]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [8]
                        }, {
                            
                            "className": 'text-right',
                            "visible": true,
                            "targets": [9]
                        }, {
                            "targets": [0],
                            "data": null,
                            "defaultContent": "<span class='label label-warning edit'  data-toggle='modal'>Detalles<i class='fa fa-edit'></i></span>"
                        }
                    ],
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

               

                $('#dtgDevoluciones').DataTable({

                    "lengthMenu": [[-1,5, 10, 15], ["Todos",5, 10, 15]],
                    "responsive": true,
                    //"stateSave": true,
                    "paging": false,
                    "ordering": true,
                    "info": true,
                    "searching": true,
                    "columnDefs": [
                        {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [0]
                        }, {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [1]
                        }, {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [2]
                        }, {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [3]
                        }, {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [4]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [5]
                        }
                    ],
                    dom: 'Bfrtip',
                    buttons: [
                        {
                            text: 'Exportar a Excel',
                            extend: 'excelHtml5',
                            title: 'Detalle de Devoluciones',
                        }
                    ],
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
           
          
                
            
                $('#dtgSolicitudes').DataTable({

                    "lengthMenu": [[-1,5, 10, 15], ["Todos",5, 10, 15]],
                    "responsive": true,
                    //"stateSave": true,
                    "paging": true,
                    "ordering": true,
                    "info": true,
                    "searching": true,
                    "columnDefs": [
                        {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [0]
                        }, {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [1]
                        }, {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [2]
                        }, {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [3]
                        }, {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [4]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [5]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [6]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [7]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [8]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [9]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [10]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [11]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [12]
                        }
                    ],
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



                $('#dtgActas').DataTable({

                    "lengthMenu": [[-1,5, 10, 15], ["Todos",5, 10, 15]],
                    "responsive": true,
                    //"stateSave": true,
                    "paging": true,
                    "ordering": true,
                    "info": true,
                    "searching": true,
                    "columnDefs": [
                        {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [0]
                        }, {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [1]
                        }, {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [2]
                        }, {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [3]
                        }, {
                            "className": 'text-right',
                            "visible": true,
                            "targets": [4]
                        }, {
                            "className": 'text-left',
                            "visible": true,
                            "targets": [5]
                        }
                    ],
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





             //funcion para llenar el combo box de proyectos
            function llenarComboProyectos() {
               
                $.ajax({
                    type: "POST",
                    url: "presupuestoMateriales.aspx/llenarComboProyectos",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var count = 0;
                        $.each(response.d, function () {
                           
                            if (count == 0) {
                                $('#cmbProyecto').append($("<option selected></option>").val(this['Value']).html(this['Text']));
                            } else {
                                $('#cmbProyecto').append($("<option></option>").val(this['Value']).html(this['Text']));
                            }
                            count++;
                        });
                        
                        llenarTablaMateriales();
                    }

                });
                
            }


            $('#cmbProyecto').on('change', function () {

                llenarTablaMateriales();
                llenarTablaDetalles("NA");
                llenarTablaDevoluciones("NA");
                llenarTablaSolicitudes("NA");
                llenarTablaActas("NA");

                //alert($('#cmbProyecto option:selected').text());
            });

             function llenarTablaMateriales(material) {
                 var proyecto = $('#<%= cmbProyecto.ClientID %>').val();
                 
                
                 var t1 = $('#dtgMateriales').DataTable();
                 t1.clear().draw();
                $.ajax({
                    type: "POST",
                    url: "presupuestoMateriales.aspx/obtenerTablaMateriales",
                    data: '{proyecto: "' + proyecto + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                       
                        $.each(response.d, function () {
                          
                            
                            t1.row.add([
                                "",
                            this['c1'],
                            this['c2'],
                            this['c3'],
                            this['c4'],
                            this['c5'],
                            this['c6'],
                            this['c7'],
                            this['c8'],
                            this['c9'],
                            this['c10']                                                        
                            ]).draw(false);
                                
                        });
                        
                        
                    }
                });
             }


             //función para el botón de edición de comprobante
            $('#dtgMateriales tbody').on('click', 'span.edit', function () {
                currentTr = this;
                var data = table.row($(this).parents('tr')).data();
                
                llenarTablaDetalles(data[1]);
                llenarTablaDevoluciones(data[1]);
                llenarTablaSolicitudes("NA");
                llenarTablaActas("NA");
            });



             function llenarTablaDetalles(material) {
                 var proyecto = $('#<%= cmbProyecto.ClientID %>').val();
                 document.getElementById('<%= lblDetalle.ClientID %>').innerHTML = "Detalles del material: " + material;
                 console.log("Detalles del material: " + material);
                 var a = 0;
                 var b = 0;
                 var c = 0;
                 var d = 0;
                 var e = 0;
                 var t2 = $('#dtgDetalles').DataTable();
                 //var t2aux = $('#dtgDetalles2').DataTable();
                 var obj = {};
                 obj.proyecto = proyecto;
                 obj.material = material;
                 t2.clear().draw();
                // t2aux.clear().draw();
                $.ajax({
                    type: "POST",
                    url: "presupuestoMateriales.aspx/obtenerTablaDetalles",
                    data: JSON.stringify(obj),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                       
                        $.each(response.d, function () {
                            a = parseFloat(this['c4']);
                            b = parseFloat(this['c5']);
                            c = parseFloat(this['c6']);
                            d = parseFloat(this['c7']);
                            e = a + b + c + d;
                           
                            if (e > 0) {
                                t2.row.add([
                               "",
                               this['c1'],
                               this['c2'],
                               this['c3'],
                               this['c4'],
                               this['c5'],
                               this['c6'],
                               this['c7'],
                               this['c8'],
                               "<p style='display:none;'>"+this['c10']+"---</p>" + this['c9']
                               
                                ]).draw(false);

                             //   t2aux.row.add([
                             //this['c1'],
                             //this['c2'],
                             //this['c3'],
                             //this['c4'],
                             //this['c5'],
                             //this['c6'],
                             //this['c7'],
                             //this['c8'],
                             //this['c9'],
                             //this['c10']
                             //   ]).draw(false);

                            } 
                                
                        });
                        
                        
                    }
                });
             }

             //función para el botón de edición de detalles
             $('#dtgDetalles tbody').on('click', 'span.edit', function () {
                 currentTr1 = this;
                 var data = table1.row($(this).parents('tr')).data();
                 llenarTablaSolicitudes(data[3]);
                 llenarTablaActas(data[3]);
                 
               
             });



             //llenar tabla de devoluciones
             function llenarTablaDevoluciones(material) {
                 var proyecto = $('#<%= cmbProyecto.ClientID %>').val();
                 document.getElementById('<%= lblDevoluciones.ClientID %>').innerHTML = "Detalles de devolución del material: " + material;
                 console.log("Detalles de devolución del material: " + material);
                 var obj = {};
                 obj.proyecto = proyecto;
                 obj.material = material;
                 var t3 = $('#dtgDevoluciones').DataTable();
                 t3.clear().draw();
                $.ajax({
                    type: "POST",
                    url: "presupuestoMateriales.aspx/obtenerTablaDevoluciones",
                    data: JSON.stringify(obj),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                       
                        $.each(response.d, function () {
                            
                            
                                t3.row.add([
                                this['c1'],
                                this['c2'],
                                this['c3'],
                                this['c4'],
                                this['c5'],
                                this['c6']
                                 ]).draw(false);

                           
                                
                        });
                        
                        
                    }
                });
             }


              //llenar tabla de Solicitudes
             function llenarTablaSolicitudes(codigoSol) {
                 
                 document.getElementById('<%= lblSolicitudes.ClientID %>').innerHTML = "Detalles de la Solicitud: " + codigoSol;
                 var t4 = $('#dtgSolicitudes').DataTable();
                 t4.clear().draw();
                $.ajax({
                    type: "POST",
                    url: "presupuestoMateriales.aspx/obtenerTablaSolicitudes",
                    data: '{codigoSolicitud: "' + codigoSol + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                       
                        $.each(response.d, function () {
                            
                            
                                t4.row.add([
                                this['c1'],
                                this['c2'],
                                this['c3'],
                                this['c4'],
                                this['c5'],
                                this['c6'],
                                this['c7'],
                                this['c8'],
                                this['c9'],
                                this['c10'],
                                this['c11'],
                                this['c12'],
                                this['c13']
                                 ]).draw(false);

                           
                                
                        });
                        
                        
                    }
                });
             }


              //llenar tabla de Actas
             function llenarTablaActas(codigoSol) {
                 
                 document.getElementById('<%= lblActas.ClientID %>').innerHTML = "Detalles de Actas de la Solicitud: " + codigoSol;
                 var t5 = $('#dtgActas').DataTable();
                 t5.clear().draw();
                $.ajax({
                    type: "POST",
                    url: "presupuestoMateriales.aspx/obtenerTablaActas",
                    data: '{codigoSolicitud: "' + codigoSol + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                       
                        $.each(response.d, function () {
                            
                            
                                t5.row.add([
                                this['c1'],
                                this['c2'],
                                this['c3'],
                                this['c4'],
                                this['c5'],
                                this['c6'],
                                this['c7'],
                                this['c8'],
                                 ]).draw(false);

                           
                                
                        });
                        
                        
                    }
                });
             }

            

             
        }
    </script>
</body>
</html>
