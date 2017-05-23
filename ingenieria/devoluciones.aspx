<%@ Page Language="VB" AutoEventWireup="false" CodeFile="devoluciones.aspx.vb" Inherits="ingenieria_devoluciones" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unidades de Conversión</title>
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
            

           
            var currentTr = null;
            $(document).ready(function () {
                // Setup - add a text input to each footer cell
                $('#dtgDetalle tfoot td').each(function () {
                    var title = $(this).text();
                    $(this).html('<input type="text" placeholder="Buscar ' + title + '" />');
                });
                $('#dtgDetalle').css('cursor', 'pointer');
                // DataTable
                var table = $('#dtgDetalle').DataTable({
                    "lengthMenu": [[20, 30, 40, 80, 100, -1], [20, 30, 40, 80, 100, "Todos"]],
                    "responsive": true,
                    
                    "paging": true,
                    "ordering": true,
                    "info": true,
                    "bAutoWidth": true,

                    "columnDefs": [
                        {
                            "targets": [6],
                            "visible": false,
                            "searchable": false
                        },
                        {
                            "targets": [7],
                            "visible": false,
                            "searchable": false
                        },
                        {
                            "targets": [8],
                            "visible": false,
                            "searchable": false
                        },
                        {
                            "targets": [9],
                            "visible": false,
                            "searchable": false
                        },
                        {
                            "targets": [10],
                            "visible": false,
                            "searchable": false
                        },
                        {
                            "targets": [11],
                            "visible": false,
                            "searchable": false
                        },
                        {
                            "targets": [12],
                            "visible": false,
                            "searchable": false
                        },
                        {
                            "targets": [13],
                            "visible": false,
                            "searchable": false
                        },
                        {
                            "targets": [14],
                            "visible": false,
                            "searchable": false
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

                // Apply the search
                table.columns().every(function () {
                    var that = this;

                    $('input', this.footer()).on('keyup change', function () {
                        if (that.search() !== this.value) {
                            that
                                .search(this.value)
                                .draw();
                        }
                    });
                });
                
                
                $('#dtgDetalle tbody').on('click', 'tr', function () {
                    currentTr = this;
                    var arr = $('#dtgDetalle').dataTable().fnGetData($(this));
                    var proyecto = arr[0];
                    var actividad = arr[1];
                    var material = arr[2];
                    var articulo = arr[3];
                    var codAct = arr[7];
                    var codMat = arr[8];
                    var codArt = arr[9];
                    var um = arr[4];
                    var cant = arr[5];
                    var referencia = arr[6];
                    var id = arr[13];
                    var codControl = $(this).css("color");

                    $("tr").css("font-weight", "normal"); // filas impares
                   
                    $(this).css('font-weight', 'bold');
                   
                   
                    
                    //alert('Código del cliente ' + unidad + ' ' + 'Nombre ' + factor);
                    //$("[id*=txtNombre]") = nombre;
                    $('#<%= txtProyecto.ClientID %>').val(proyecto);
                    $('#<%= txtCodActividad.ClientID %>').val(codAct);
                    $('#<%= txtActividad.ClientID %>').val(actividad);
                    $('#<%= txtCodMaterial.ClientID %>').val(codMat);
                    $('#<%= txtMaterial.ClientID %>').val(material);
                    $('#<%= txtCodArticulo.ClientID %>').val(codArt);
                    $('#<%= txtArticulo.ClientID %>').val(articulo);
                    $('#<%= txtCantidad.ClientID %>').val(cant);
                    $('#<%= txtUM.ClientID %>').val(um);
                    $('#<%= txtReferencia.ClientID %>').val(referencia);
                    document.getElementById('<%= lblId.ClientID %>').innerText = id;
                   
                    if (codControl == 'rgb(255, 0, 0)') { //comparamos con el color rojo
                        $('#<%= btnRechazar.ClientID %>').val("Quitar Rechazo");
                        $('#<%= btnAprobar.ClientID %>').attr('class', "btn btn-vitalicia btn-md disabled");
                        
                    } else {
                         $('#<%= btnRechazar.ClientID %>').val("Rechazar");
                        $('#<%= btnAprobar.ClientID %>').attr('class', "btn btn-vitalicia btn-md");
                    }

                    $("#modalActas").modal({ show: true });


                    
                });

                
            });

            
            function aprobar() {
                
                var id = document.getElementById('<%= lblId.ClientID %>').innerText;
                var usuario = document.getElementById('<%= lblUsuario.ClientID %>').innerHTML;
                alertify.set({ labels: { ok: "Si", cancel: "No" } });
                alertify.set({ buttonReverse: true });
                alertify.confirm("¿Seguro que desea aprobar la devolución?", function (e) {
                    if (e) {
                        $.ajax({
                            type: "POST",
                            url: "devoluciones.aspx/aprobarDevolucion",
                            data: '{id: "' + id + '", usuario: "' + usuario + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: quitarRegistro,
                            failure: function (response) {
                                alert(response.d);
                            }
                        });
                    } else {
                        alertify.error("Operación cancelada");
                    }
                });


               
            }

            function rechazar() {
                
                var id = document.getElementById('<%= lblId.ClientID %>').innerText;
                var usuario = document.getElementById('<%= lblUsuario.ClientID %>').innerHTML;
                var estado;
                var mensaje;

                if ($('#<%= btnRechazar.ClientID %>').val()=='Rechazar'){
                    estado = 'B';
                    mensaje = '¿Seguro que desea rechazar la devolución?';
                } else {
                    estado = 'M';
                    mensaje = '¿Seguro que desea quitar el rechazo a la devolución?';
                }

                alertify.set({ labels: { ok: "Si", cancel: "No" } });
                alertify.set({ buttonReverse: true });
                alertify.set({ delay: 10000 });
                alertify.confirm(mensaje, function (e) {
                    if (e) {
                        $.ajax({
                            type: "POST",
                            url: "devoluciones.aspx/rechazarDevolucion",
                            data: '{id: "' + id + '", estado: "' + estado + '", usuario: "' + usuario + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: quitarRegistro,
                            failure: function (response) {
                                alert(response.d);
                            }
                        });
                    } else {
                        alertify.error("Operación cancelada");
                    }
                });


               
            }

            function quitarRegistro(response) {
                
                if (currentTr != null) {
                    var articulo = $(currentTr).find('td').eq(3).html();

                    if (response.d == "a") {
                        $(currentTr).closest('tr').empty();
                    }
                    else {
                        if ($('#<%= btnRechazar.ClientID %>').val()=='Rechazar'){
                            $(currentTr).closest('tr').css({ "text-decoration": "overline underline line-through", "color": "red" });
                            
                        } else {
                            $(currentTr).closest('tr').css({ "text-decoration": "none", "color": "black" });
                            
                        }



                        

                    }
                    
                    
                    if (response.d == "a") {
                        alertify.success("Devolución de " + articulo + " aprobada exitosamente");
                        //$(currentTr).find('td').eq(3).html($("#cmbUM").val());
                    }
                    else {
                        alertify.error("Devolución de " + articulo + " rechazada exitosamente");
                        //$(currentTr).find('td').eq(3).html($("#cmbUM").val());
                    }
                    
                }
            }

            $('#btnAprobar').on('click', function () {
                aprobar();
            });

            $('#btnRechazar').on('click', function () {
                rechazar();
            });
        }
    </script>

    <style type="text/css">
        body{font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;color:#444;font-size:13px;}
        p,div,ul,li{padding:0px; margin-right: 0px;
            margin-top: 0px;
            margin-bottom: 0px;
        }
        
	tfoot {
    display: table-header-group;
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

.select-style {
    padding: 0;
    margin: 0;
    border: 1px solid #ccc;
    width: 100%;
    border-radius: 3px;
    overflow: hidden;
    background-color: #fff;

    background: #fff url("https://www.furgovw.org/Themes/default/images/sort_down.gif") no-repeat 90% 50%;
}

.select-style select {
    padding: 5px 8px;
    width: 130%;
    border: none;
    box-shadow: none;
    background-color: transparent;
    background-image: none;
    -webkit-appearance: none;
       -moz-appearance: none;
            appearance: none;
}

.select-style select:focus {
    outline: none;
}
        

    </style>

</head>


<body>
    
    <div class="container-fluid text-center">
        <form class="form-horizontal" role="form" runat="server">
            <nav class="navbar navbar-default" style="background-color: #fff;" role="navigation">
    <!-- El logotipo y el icono que despliega el menú se agrupan
       para mostrarlos mejor en los dispositivos móviles -->
        <div class="navbar-header">
                 
             <a class="navbar-brand" href="#"><strong>DEVOLUCIONES</strong></a>         
        </div>
 
  <!-- Agrupar los enlaces de navegación, los formularios y cualquier
       otro elemento que se pueda ocultar al minimizar la barra -->
        <div class="collapse navbar-collapse navbar-ex1-collapse">
                       
            <ul class="nav navbar-nav navbar-left">
                 <li><div id="div1" runat="server">

                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-default btn-md" /></div>
                </li>
                
            </ul>
            <ul class="nav navbar-nav navbar-right">
                 <li><div id="divBoton" runat="server" style="display:none;">

                    <asp:Button ID="btnExcel" runat="server" Text="Cargar Factores" CssClass="btn btn-default btn-md" /></div>
                </li>
                <li ><a href="#"><strong>Usuario: <br /><asp:Label ID="lblUsuario" runat="server" Text="" Font-Size="Small"></asp:Label></strong></a></li>
                <li style="display:none;" ><asp:Label ID="lblOC" runat="server" Text="" Font-Size="Small"></asp:Label></li>
                <li><a href="#">
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
                <asp:Label ID="lblId" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large"   style="display:none;"></asp:Label>
                <h4 class="text-center"><asp:Label ID="lblCodigoActa" style="display:none;" runat="server" Text="Label"></asp:Label></h4>
            </div>

             <!---------------FIN TITULOS----------------------->


           
 
   

   

    <div class="row">
        <div class="col-sm-12" >
            <div class="table-responsive" >
                <div class="testgrid2">
                <asp:GridView ID="dtgDetalle" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="ARTICULO" 
                        DataSourceID="Detalles" CssClass="testgrid2" cellspacing="0"
                        EnableModelValidation="True" Width="100%" Visible="true" ShowFooter="true">
                    <Columns>                                                 
                       
                        <asp:BoundField DataField="PROYECTO" HeaderText="PROYECTO" ReadOnly="True" SortExpression="ACT" />
                          
                        <asp:BoundField DataField="NOM_ACTIVIDAD" HeaderText="NOMBRE ACTIVIDAD" ReadOnly="True" SortExpression="ART" />
                         
                        <asp:BoundField DataField="NOM_MATERIAL" HeaderText="NOMBRE MATERIAL" ReadOnly="True" SortExpression="ART" />
                         
                        <asp:BoundField DataField="NOM_ARTICULO" HeaderText="NOMBRE ARTÍCULO" ReadOnly="True" SortExpression="ART" />
                         
                        <asp:BoundField DataField="UM_P" HeaderText="UNIDAD PRESUPUESTO" ReadOnly="True" SortExpression="ART" />
                         
                        <asp:BoundField DataField="CANT_P" HeaderText="CANTIDAD PRESUPUESTO"  SortExpression="SOL" DataFormatString="{0:N2}" />

                        <asp:BoundField DataField="REFERENCIA" HeaderText="REFERENCIA" ReadOnly="True" SortExpression="ART" />
                         
                        <asp:BoundField DataField="ACTIVIDAD" HeaderText="ACTIVIDAD" ReadOnly="True" SortExpression="ART" />
                         
                        <asp:BoundField DataField="MATERIAL" HeaderText="MATERIAL" ReadOnly="True" SortExpression="ART" />
                         
                        <asp:BoundField DataField="ARTICULO" HeaderText="ARTÍCULO" ReadOnly="True" SortExpression="ART" />
                         
                        <asp:BoundField DataField="UM_A" HeaderText="UNIDAD ALMACEN" ReadOnly="True" SortExpression="ART" />
                         
                        <asp:BoundField DataField="CANT_A" HeaderText="CANTIDAD ALMACEN"  SortExpression="SOL" DataFormatString="{0:N2}" />

                        <asp:BoundField DataField="ESTADO_LIN" HeaderText="ESTADO" ReadOnly="True" SortExpression="ART" />    
                        
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ART" />    
                          
                        <asp:BoundField DataField="COD_CONT_LINEA" HeaderText="COD_CONT_LINEA" ReadOnly="True" SortExpression="ART" />    
                                            
                                                 
                    </Columns>
                </asp:GridView>
                               
                    <asp:SqlDataSource ID="Detalles" runat="server" ProviderName="System.Data.SqlClient" >
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

               <!-- INICIO MODAL OBSERVACIONES-->
    
    <!-- FIN MODAL OBSERVACIONES -->

                           <!-- INICIO MODAL OBSERVACIONES-->
    <div class="modal fade" id="modalActas" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header text-center">
                   <h2>Detalles de la devolución: <span class="extra-title muted"></span></h2>
                </div>
                <div class="modal-body form-horizontal">
                    
                    <div class="btn-group btn-group-justified">
                        <fieldset class="row" id="contTextos" runat="server" disabled="disabled" >
                        <div class="row">
                            <div class="col-sm-1">                                                        
                            </div> 
                            <div class="col-sm-10" >
                                <label for="nueva" class="control-label">Proyecto:</label>
                                <div class="controls">
                                     <asp:TextBox ID="txtProyecto" maxlength="25"  runat="server" class="form-control input-md disabled" placeholder=""></asp:TextBox>                                    
                                </div>
                            </div> 
                            <div class="col-sm-1">                                                        
                            </div> 
                        </div>
                        <div class="row">
                            <div class="col-sm-1">                                                        
                            </div> 
                            <div class="col-sm-3">    
                                <label for="nueva" class="control-label">Cód. Act.:</label>
                                <div class="controls">
                                     <asp:TextBox ID="txtCodActividad" maxlength="25"  runat="server" class="form-control input-md disabled" placeholder=""></asp:TextBox>                                    
                                </div>                            
                            </div> 
                            <div class="col-sm-7" >
                                <label for="nueva" class="control-label">Actividad:</label>
                                <div class="controls">
                                     <asp:TextBox ID="txtActividad" maxlength="25"  runat="server" class="form-control input-md disabled" placeholder=""></asp:TextBox>                                    
                                </div>
                            </div> 
                            <div class="col-sm-1">                                                        
                            </div> 
                            
                        </div>
                        <div class="row">
                            <div class="col-sm-1">                                                        
                            </div> 
                            <div class="col-sm-3">    
                                <label for="nueva" class="control-label">Cód. Mat.:</label>
                                <div class="controls">
                                     <asp:TextBox ID="txtCodMaterial" maxlength="25"  runat="server" class="form-control input-md disabled" placeholder=""></asp:TextBox>                                    
                                </div>                            
                            </div> 
                            <div class="col-sm-7" >
                                <label for="nueva" class="control-label">Material:</label>
                                <div class="controls">
                                     <asp:TextBox ID="txtMaterial" maxlength="25"  runat="server" class="form-control input-md disabled" placeholder=""></asp:TextBox>                                    
                                </div>
                            </div> 
                            <div class="col-sm-1">                                                        
                            </div> 
                            
                        </div>
                        <div class="row">
                            <div class="col-sm-1">                                                        
                            </div> 
                            <div class="col-sm-3">    
                                <label for="nueva" class="control-label">Cód. Art.:</label>
                                <div class="controls">
                                     <asp:TextBox ID="txtCodArticulo" maxlength="25"  runat="server" class="form-control input-md disabled" placeholder=""></asp:TextBox>                                    
                                </div>                            
                            </div> 
                            <div class="col-sm-7" >
                                <label for="nueva" class="control-label">Artículo:</label>
                                <div class="controls">
                                     <asp:TextBox ID="txtArticulo" maxlength="25"  runat="server" class="form-control input-md disabled" placeholder=""></asp:TextBox>                                    
                                </div>
                            </div> 
                            <div class="col-sm-1">                                                        
                            </div> 
                            
                        </div>
                        <div class="row">
                            <div class="col-sm-1">                                                        
                            </div> 
                            <div class="col-sm-5">    
                                <label for="nueva" class="control-label">Cantidad:</label>
                                <div class="controls">
                                     <asp:TextBox ID="txtCantidad" maxlength="25"  runat="server" class="form-control input-md disabled" placeholder=""></asp:TextBox>                                    
                                </div>                            
                            </div> 
                            <div class="col-sm-5" >
                                <label for="nueva" class="control-label">Unidad Medida:</label>
                                <div class="controls">
                                     <asp:TextBox ID="txtUM" maxlength="25"  runat="server" class="form-control input-md disabled" placeholder=""></asp:TextBox>                                    
                                </div>
                            </div> 
                            <div class="col-sm-1">                                                        
                            </div>                             
                        </div>
                        <div class="row">  
                            <div class="col-sm-1">                                                        
                            </div>                           
                            <div class="col-sm-10" >
                                <label for="nueva" class="control-label">Referencia Devolución:</label>
                                <div class="controls">
                                     <textarea name="txtReferencia" id="txtReferencia" runat="server"  maxlength="1500" cols="40" rows="4" class="form-control input-md disabled" placeholder="Observaciones" disabled="disabled"></textarea>                                  
                                </div>
                            </div> 
                            <div class="col-sm-1">                                                        
                            </div>                             
                        </div>
                        </fieldset>
                        
                        
                        
                        
                    </div>
                    
                    
                    
                    
                   
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="...">
                        <div class="btn-group" role="group">                            
                            <input id="btnAprobar" type="button" value="Aprobar" runat="server" data-dismiss="modal" class="btn btn-vitalicia btn-md" />
                        </div>
  
                        <div class="btn-group" role="group">                            
                            <input id="btnRechazar" type="button" value="Rechazar" runat="server" data-dismiss="modal" class="btn btn-default btn-md" />
                        </div>

                        <div class="btn-group" role="group">
                            <input id="btnCancelar" type="button" value="Cancelar" runat="server" data-dismiss="modal" class="btn btn-default btn-md" />
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
