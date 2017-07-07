<%@ Page Language="VB" AutoEventWireup="false" CodeFile="conversionUM.aspx.vb" Inherits="logistica_conversionUM" %>

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
                    //var arr = $('#dtgDetalle').dataTable().fnGetData($(this));
                    var proyecto = $('td', this).eq(0).text();
                    var articulo = $('td', this).eq(1).text();
                    //var desc = arr[2];
                    var desc = $('td', this).eq(2).text();
                    var unidad = $('td', this).eq(3).text();
                    var factor = $('td', this).eq(5).text();
                    var color = $(this).css("background-color");
                    $("tr").css("font-weight", "normal"); // filas impares
                   
                    $(this).css('font-weight', 'bold');
                    
                   
                //alert('Código del cliente ' + unidad + ' ' + 'Nombre ' + factor);
                    //$("[id*=txtNombre]") = nombre;
                    $('#<%= txtProyecto.ClientID %>').val(proyecto);
                    $('#<%= txtArticulo.ClientID %>').val(articulo);
                    $('#<%= txtDesc.ClientID %>').val(desc);
                    $('#<%= txtFactor.ClientID %>').val(factor);
                    $('#<%= cmbUM.ClientID %>').val(unidad);
                    
                    sugerencias(articulo);
                    $("#modalActas").modal({ show: true });

                    if (color == 'rgb(255, 255, 0)') { //comparamos con el color amarillo
                        
                        $('#<%= btnCambiarSi.ClientID %>').attr('class', "btn btn-vitalicia btn-md disabled");
                        alertify.error("No se puede cambiar el factor de conversión de un artículo con orden de cambio 1");
                        
                    } else {
                        
                        $('#<%= btnCambiarSi.ClientID %>').attr('class', "btn btn-vitalicia btn-md");
                    }
                    //$("[id*=cmbUM]").val("M3");
                    //$('#cmbUM > [value=' + unidad + ']').attr('selected', 'true');
                //var as = $('#<%= cmbUM.ClientID %>').val();
               // alert(as);
                //$('select').val(codigo);
                });

                
            });

            function sugerencias(articulo) {
                $.ajax({
                    type: "POST",
                    url: "conversionUM.aspx/obtenerSugerencia",
                    data: '{articulo: "' + articulo + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }

            function OnSuccess(response) {
                document.getElementById('<%= lblSugerencia.ClientID %>').innerHTML = response.d;
                
            }

            function actualizar() {
                var factor = $("#txtFactor").val();
                var um = $("#cmbUM").val();
                var proyecto = $("#txtProyecto").val();
                var articulo = $("#txtArticulo").val();
                var usuario = document.getElementById('<%= lblUsuario.ClientID %>').innerHTML
                var oc = document.getElementById('<%= lblOC.ClientID %>').innerText

                $.ajax({
                    type: "POST",
                    url: "conversionUM.aspx/actualizaFactores",
                    data: '{factor: "' + factor + '", um: "' + um + '", proyecto: "' + proyecto + '", articulo: "' + articulo + '", usuario: "' + usuario + '", oc: "' + oc + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: refrescarValores,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }

            function refrescarValores(response) {
                
                if (currentTr != null) {
                    $(currentTr).find('td').eq(5).html(Math.round($("#txtFactor").val() * 100000000) / 100000000);
                    $(currentTr).find('td').eq(3).html($("#cmbUM").val());
                    alertify.success( $(currentTr).find('td').eq(2).text() + " actualizado(a) exitosamente");
                }
            }

            $('#btnCambiarSi').on('click', function () {
                actualizar();
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
                 
             <a class="navbar-brand" href="#"><strong>UNIDADES DE CONVERSIÓN</strong></a>         
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
                 <li><div id="div2" runat="server">

                    <asp:Button ID="btnSinc" runat="server" Text="Sincronizar Base de Datos" CssClass="btn btn-default btn-md" /></div>
                </li>
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
                          
                        <asp:BoundField DataField="ARTICULO" HeaderText="ARTÍCULO" ReadOnly="True" SortExpression="ART" />
                         
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCIÓN" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="UM_P" HeaderText="UNIDAD DE PRESUPUESTO" ReadOnly="True" SortExpression="UMP" />
                        
                        <asp:BoundField DataField="UM_A" HeaderText="UNIDAD DE ALMACEN" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="FACTOR" HeaderText="FACTOR DE CONVERSIÓN"  SortExpression="SOL" DataFormatString="{0:N8}" />

                        <asp:BoundField DataField="OC" HeaderText="OC" ReadOnly="True" SortExpression="OC" />
                                                 
                    </Columns>
                </asp:GridView>
                
               
               
                    <asp:SqlDataSource ID="Detalles" runat="server" ProviderName="System.Data.SqlClient" >
                                           

                    </asp:SqlDataSource>
                </div>
            </div>
            
        </div>
    </div>

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
           
        }
        
       
        val = d.value
       
        
        
    }

    //Función que permite solo Números
    function ValidaSoloNumeros()
        
    {
        var de = document.getElementById('<%= txtFactor.ClientID %>').innerHTML;
        
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

                }
            }
        
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
    
    <!-- FIN MODAL OBSERVACIONES -->

                           <!-- INICIO MODAL OBSERVACIONES-->
    <div class="modal fade" id="modalActas" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header text-center">
                   <h2>Cambio de datos: <span class="extra-title muted"></span></h2>
                </div>
                <div class="modal-body form-horizontal">
                    
                    <div class="control-group">
                        <fieldset class="row" id="contTextos" runat="server" disabled="disabled" >
                        <div class="row">
                            <div class="col-sm-2">                                
                            </div> 
                            <div class="col-sm-8" >
                                <label for="nueva" class="control-label">Proyecto:</label>
                                <div class="controls">
                                     <asp:TextBox ID="txtProyecto" maxlength="25"  runat="server" class="form-control input-md disabled" placeholder="Codigo de Acta"></asp:TextBox>                                    
                                </div>
                            </div> 
                            <div class="col-sm-2">                                
                            </div> 
                        </div>
                        <div class="row">
                            <div class="col-sm-2">                                
                            </div> 
                            <div class="col-sm-8" >
                                <label for="nueva" class="control-label">Artículo:</label>
                                <div class="controls">
                                     <asp:TextBox ID="txtArticulo" maxlength="25"  runat="server" class="form-control input-md disabled" placeholder="Codigo de Acta"></asp:TextBox>                                    
                                </div>
                            </div> 
                            <div class="col-sm-2">                                
                            </div> 
                        </div>
                        <div class="row">
                            <div class="col-sm-2">                                
                            </div> 
                            <div class="col-sm-8" >
                                <label for="nueva" class="control-label">Descripción Artículo:</label>
                                <div class="controls">
                                     <asp:TextBox ID="txtDesc" maxlength="50"  runat="server" class="form-control input-md disabled" placeholder="Codigo de Acta"></asp:TextBox>                                    
                                </div>
                            </div> 
                            <div class="col-sm-2">                                
                            </div> 
                        </div>
                        </fieldset>
                        <div class="row">
                            <div class="col-sm-2">                                
                            </div> 
                            <div class="col-sm-4" >
                                <label for="nueva" class="control-label">Factor:</label>
                                <div class="controls">
                                     <asp:TextBox ID="txtFactor" maxlength="25"  runat="server" class="form-control input-md" autocomplete="off" style="text-align:center;" runat="server" onkeyup="mascara(this)" onkeypress="ValidaSoloNumeros()" placeholder="Factor"></asp:TextBox>                                    
                                </div>
                            </div> 
                            
                        
                            <div class="col-sm-4" >
                            <label for="actual" class="control-label">Unidad de Medida: </label>
                            <div class="select-style"  >
                                
                                <div class="controls">
                                   <asp:DropDownList ID="cmbUM" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            </div>
                            <div class="col-sm-2">                                
                            </div> 
                                         
                        </div>
                        <div class="row">
                            <div class="col-sm-2">                                
                            </div> 
                            <div class="col-sm-8" >
                                <div class="alert alert-success" >
                                    <strong><asp:Label ID="lblSugerencia" runat="server" Text=""></asp:Label></strong>
                                </div>
                            </div>
                            <div class="col-sm-2">                                
                            </div> 
                        </div>
                        
                        
                    </div>
                    
                    
                    
                    
                   
                </div>
                <div class="modal-footer">
                    <div class="btn-group btn-group-justified" role="group" aria-label="...">
                        <div class="btn-group" role="group">                            
                            <input id="btnCambiarSi" type="button" value="Aceptar" runat="server" data-dismiss="modal" class="btn btn-vitalicia btn-md" />
                        </div>
  
                        <div class="btn-group" role="group">
                            <input id="btnCambiarNo" type="button" value="Cancelar" runat="server" data-dismiss="modal" class="btn btn-default btn-md" />
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
