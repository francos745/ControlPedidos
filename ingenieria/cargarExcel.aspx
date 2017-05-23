<%@ Page culture="es-MX" UICulture="es-MX"  Language="VB" AutoEventWireup="false" CodeFile="cargarExcel.aspx.vb" Inherits="cargarExcel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Principal</title>
       <meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
    <meta charset="utf-8"/>
    <link rel="shortcut icon" href="../img/clever.png" />
    <meta name="generator" content="Bootply" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"/>
    
    
    
    
    
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
    <!--[if lt IE 9]>
        <script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <link href="../css/styles.css" rel="stylesheet"/>
    <!-- script references -->
    <script type="text/jscript" src="../js/jquery.min.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    




    <script type="text/jscript" src="../js/jquery.min.js"></script>
    <script type="text/jscript" src="../js/jquery.sumoselect.min.js"></script>
    <link href="../css/sumoselect.css" rel="stylesheet" />
    <link href="../css/Estilos.css" rel="stylesheet" />

    
    

      <!-- Include Required Prerequisites -->
    <%--<script type="text/javascript" src="../js/jquery.min.js"></script>--%>
<script type="text/javascript" src="../js/moment.min.js"></script>

 
<!-- Include Date Range Picker -->
<script type="text/javascript" src="../js/daterangepicker.js"></script>
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
    <link href="css/main.css" rel="stylesheet"/>

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
                
				<h3 class="text-center">Carga de documentos Excel (*.xls)</h3>
               
                
                <h4 class="text-center"><asp:Label ID="lblCodigoProyecto" style="display:none;" runat="server" Text="Label"></asp:Label></h4>
                <div class="container">
                    <div class="row">
                        <div class="col-sm-3" >
                            
                        </div>
                        <div class="col-sm-6" >
                            <h4 class="text-center">Seleccione el archivo que desea cargar</h4>
                            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                <div class="btn-group" role="group">
                                    <asp:FileUpload ID="fileUpload"  CssClass="btn btn-primary btn-md"   runat="server" class="form-control input-md" />
                                </div>
                            </div>
                            
                          
                        </div>

                        <div class="col-sm-3" >
                        </div>
                    </div>

                    
                </div>
                <br/><br/>
                <div class="container">
                    <div class="row">
                        <div class="col-sm-3" >
                            
                        </div>

                        <div class="col-sm-6" >
                            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                <div class="btn-group" role="group">
                                    <asp:Button ID="btnCargar" runat="server" Text="Cargar Archivo" CssClass="btn btn-vitalicia btn-md" causesvalidation="true" validationgroup="GrupoValidador" />
                                
                                    
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
                
                                <div class="container">
                    <div class="row">
                        <div class="col-sm-3" >
                            
                        </div>

                        <div class="col-sm-6" id="btnConfirmar" runat="server">
                            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                <div class="btn-group" role="group">
                                    <asp:Button ID="btnSi" runat="server" Text="Si" CssClass="btn btn-vitalicia btn-md" causesvalidation="true" validationgroup="GrupoValidador" />
                                </div>                        
                                
                                <div class="btn-group" role="group">
                                    <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-default btn-md" causesvalidation="true" validationgroup="GrupoValidador" />
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
            </div>

             <!---------------FIN TITULOS----------------------->


           
 
   
               
        
        
       
    

    <div class="row" style="display:none;">
        <div class="col-sm-12" >
            <div class="table-responsive" >
                <div class="testgrid1">
                <asp:GridView ID="dtgDetalle" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="ID" 
                        DataSourceID="Detalles" CssClass="testgrid1" 
                        EnableModelValidation="True" Width="100%" Visible="true">
                    <Columns>
                        <asp:CommandField ButtonType="Button" EditText ="Modificar" ShowEditButton="True" CancelText="Cancelar" UpdateText=" Aceptar " ControlStyle-CssClass="buttonControl"  >
                        <ControlStyle CssClass="btn btn-vitalicia btn-md" />
                        </asp:CommandField>
                        
                        <asp:CommandField ButtonType="Button" DeleteText="Borrar" ShowDeleteButton="True" ControlStyle-CssClass="buttonControl"  >
                            <ControlStyle CssClass="btn btn-vitalicia btn-md" />
                        </asp:CommandField>
                            
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False" SortExpression="ID" />

                        <asp:BoundField DataField="NOM_ACTIVIDAD" HeaderText="ACTIVIDAD" ReadOnly="True" SortExpression="ACT" />

                        <asp:BoundField DataField="NOM_MATERIAL" HeaderText="MATERIAL" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="NOM_ARTICULO" HeaderText="ARTÍCULO" ReadOnly="True" SortExpression="ART" />

                        <asp:BoundField DataField="UM_P" HeaderText="UM" ReadOnly="True" SortExpression="UMP" />

                        <asp:BoundField DataField="CANT_SOL_P" HeaderText="CANTIDAD SOLICITADA"  SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_PRESUP_P" HeaderText="CANTIDAD PRESUPUESTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_ACTAS_P" HeaderText="CANTIDAD CON ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_APROB_P" HeaderText="CANTIDAD EJECUTADA" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_APROB_ACTAS_P" HeaderText="CANTIDAD EJECUTADA CON ACTAS" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_DISP2_P" HeaderText="CANTIDAD DISPONIBLE" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                        <asp:BoundField DataField="CANT_SOL_PEND_P" HeaderText="ANTERIORES SIN APROBAR" ReadOnly="True" SortExpression="SOL" DataFormatString="{0:N}" />

                         <asp:BoundField DataField="PRESUPUESTADO_ACT" HeaderText="ACTIVIDAD PRESUP" ReadOnly="True" SortExpression="ART" />

                         <asp:BoundField DataField="PRESUPUESTADO" HeaderText="MATERIAL PRESUP" ReadOnly="True" SortExpression="ART" />

                        


                                                
                    </Columns>
                </asp:GridView>
                
               <asp:GridView ID="dtgExcel" runat="server"></asp:GridView>
                    <asp:GridView ID="dtgObsRes" runat="server"></asp:GridView>
                    <asp:GridView ID="dtgObsSup" runat="server"></asp:GridView>
                    <asp:GridView ID="dtgCabecera" runat="server"></asp:GridView>
                    <asp:Label ID="lblCodigoSolicitud" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblCarpetaFinal" runat="server" Text=""></asp:Label>
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

    



   
                 
<!------------------------------ Inicio pie de página------------------------------>
            <div id="footer">
                
                    <div class="text-right" >
		                <a href="http://www.clever.bo/"> <img src="../img/clever.png" class="" alt="Cinque Terre" width="2%" height="2%"/></a>
                    </div>
                
            </div>
<!------------------------------ Fin pie de página  ------------------------------>

        
    
                                
   
    <!-- FIN MODAL OBSERVACIONES -->
        </form>
    </div>


</body>
</html>
