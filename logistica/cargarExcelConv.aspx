<%@ Page culture="es-MX" UICulture="es-MX"  Language="VB" AutoEventWireup="false" CodeFile="cargarExcelConv.aspx.vb" Inherits="logistica_cargarExcelConv" %>

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
                <h4 class="text-center"><asp:Label ID="lblUsuario" style="display:none;" runat="server" Text="Label"></asp:Label></h4>
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
                    
                  
                </div>
            </div>

             <!---------------FIN TITULOS----------------------->


           
 
   
               
        
        
       
    

    <div class="row" style="display:none;">
        <div class="col-sm-12" >
            <div class="table-responsive" >
                <div class="testgrid1">
               
               <asp:GridView ID="dtgExcel" runat="server"></asp:GridView>
                    
                   
                    <asp:Label ID="lblCarpetaFinal" runat="server" Text=""></asp:Label>
                    
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

