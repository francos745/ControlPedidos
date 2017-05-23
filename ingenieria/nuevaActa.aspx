<%@ Page culture="es-MX" UICulture="es-MX"  Language="VB" AutoEventWireup="false" CodeFile="nuevaActa.aspx.vb" Inherits="ingenieria_nuevaActa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nueva Acta</title>
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



<link rel="stylesheet" type="text/css" href="../css/daterangepicker.css" />
    
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

    <script type="text/javascript" src="../js/highlight.js">
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
            <a class="navbar-brand" href="#"><strong>NUEVA ACTA</strong></a>         
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
                <li class="dropdown " style="display:none;">
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
                
				<h4 class="text-center"><asp:Label ID="lblCodigoActa" runat="server" Text="Label"></asp:Label></h4>
                <asp:Label ID="lblId" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                <div class="container">
                    <div class="row">
                        <div class="col-sm-3" >
                           
                        </div>

                        

                        <div class="col-sm-6" >
                            

                            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                <div class="btn-group" role="group">
                                    <input id="btnGenerar" type="button" value="Generar Acta" data-target="#modalObservaciones" class="btn btn-vitalicia btn-md"  runat="server" data-toggle="modal" data-backdrop="static" />
                                </div>                        
                                <div class="btn-group" role="group">
                                    <input id="btnCancelar" type="button" value="Cancelar Acta" data-target="#modalConfirmarDescartar" class="btn btn-default btn-md" runat="server" data-toggle="modal" data-backdrop="static" />
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
        <div class="col-sm-3" >
            
            
           <div class="control-group">
                <label for="nueva" class="control-label"> Código de Proyecto: </label>
                    <div class="controls" style="text-align:left;">
                        <asp:DropDownList ID="cmbProyecto" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                    </div>
            </div>
            
        </div>
        <div class="col-sm-3" >
            
            
            <div class="control-group">
                <label for="nueva" class="control-label"> Actividad: </label>
                <div class="controls" style="text-align:left;">
                    <asp:DropDownList ID="cmbActividad" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                </div>
            </div>
            
        </div>
        
        <div class="col-sm-3" >
            
             <div class="control-group">
                <label for="nueva" class="control-label"> Material: </label>
                <div class="controls" style="text-align:left;">
                    <asp:DropDownList ID="cmbMaterial" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                </div>
            </div>   
                 
        </div>
        
        <div class="col-sm-3" >
            
            <div class="control-group">
                <label for="nueva" class="control-label"> Artículo: </label>
                <div class="controls" style="text-align:left;">
                    <asp:DropDownList ID="cmbArticulo" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                </div>
            </div>

            
            <div class="control-group" runat="server" id="cmbArticuloAuxS">
                <label for="nueva" class="control-label">Otros Artículos: </label>
                <div class="control-group">
                    <div class="controls" style="text-align:left;">
                        <asp:DropDownList ID="cmbArticuloAux" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                    </div>
                </div>
            </div>  
            <div class="col-sm-9" >
                <div class="control-group" runat="server" id="txtArticuloS">
                    <label for="nueva" class="control-label">Ingrese un nuevo Artículo: </label>
                    <div class="controls">
                        <asp:TextBox ID="txtArticulo" runat="server" class="form-control input-md" AutoPostBack="false" placeholder="Artículo"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-3" >
                <div class="control-group" runat="server" id="cmbUMS">
                    <label for="nueva" class="control-label">UM: </label>
                    <div class="controls">
                        <asp:DropDownList ID="cmbUM" runat="server" class="search-box" AutoPostBack="true" placeholder="No hay elementos disponibles" ></asp:DropDownList>
                    </div>
                </div>
            </div>            
        </div>
    </div>
    <div class="row">
        <div class="col-sm-9" >
        </div>    
        
        
        
        <div class="col-sm-3" >
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
               <asp:GridView ID="dtgDetalle" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="ID" 
                        DataSourceID="Detalles" CssClass="testgrid1" 
                        EnableModelValidation="True" Width="100%" Visible="true">
                    <Columns>
                        <asp:CommandField ButtonType="button" SelectText ="Editar" ShowSelectButton="True" ControlStyle-CssClass="buttonControl"  >
                            <ControlStyle CssClass="btn btn-vitalicia btn-xs" />
                        </asp:CommandField>
                        
                        <asp:CommandField ButtonType="button" DeleteText="Borrar" ShowDeleteButton="True" ControlStyle-CssClass="buttonControl"  >
                            <ControlStyle CssClass="btn btn-vitalicia btn-xs" />
                        </asp:CommandField>
                           
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />

                        <asp:BoundField DataField="PROYECTO" HeaderText="PROYECTO" ReadOnly="True" SortExpression="ACT" />

                        <asp:BoundField DataField="NOM_ACTIVIDAD" HeaderText="ACTIVIDAD" ReadOnly="True" SortExpression="ACT" />

                        <asp:BoundField DataField="NOM_MATERIAL" HeaderText="MATERIAL" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="NOM_ARTICULO" HeaderText="ARTÍCULO" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="UM_P" HeaderText="UM" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_ACT_P" HeaderText="CANTIDAD ACTAS"  SortExpression="SOL" DataFormatString="{0:N}" />

                         <asp:BoundField DataField="UM_A" HeaderText="UM" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_ACT_A" HeaderText="CANTIDAD ACTAS"  SortExpression="SOL" DataFormatString="{0:N}" />
                        
                        <asp:BoundField DataField="ACTIVIDAD" HeaderText="ACTIVIDAD" ReadOnly="True" SortExpression="ACT" />

                        <asp:BoundField DataField="MATERIAL" HeaderText="MATERIAL" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="ARTICULO" HeaderText="ARTÍCULO" ReadOnly="True" SortExpression="ART" />                            
                    </Columns>
                   <selectedrowstyle font-bold="true" Font-Size="Medium"/>  
                </asp:GridView>
                
               

               
                    <asp:SqlDataSource ID="Detalles" runat="server" ProviderName="System.Data.SqlClient" >
                         <SelectParameters>
                            <asp:ControlParameter ControlID="lblCodigoActa" DefaultValue="ninguno" Name="CODIGO_ACTA" PropertyName="Text" Type="String" />
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
                                <label for="nueva" class="control-label">Código de Acta:</label>
                                <div class="controls">
                                    <input id="txtCodigoActa" type="text" runat="server" maxlength="50" class="form-control input-md" placeholder="Código de Acta" />
                                </div>
                            </div> 
                            <div class="col-sm-6" >
                                <label for="actual" class="control-label">Fecha de Acta: </label>
                                <div class="controls">
                                    <asp:TextBox ID="txtFecha" runat="server" class="form-control input-md" placeholder="Fecha"></asp:TextBox>
                                </div>
                            </div>

                                         
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
        </form>
    </div>


</body>
</html>
