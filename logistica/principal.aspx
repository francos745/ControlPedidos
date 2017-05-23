<%@ Page culture="es-MX" UICulture="es-MX"  Language="VB" AutoEventWireup="false" CodeFile="principal.aspx.vb" Inherits="logistica_principal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Principal</title>
    
    <meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
    <meta charset="utf-8"/>
    <link rel="shortcut icon" href="../img/clever.png" />
    <meta name="generator" content="Bootply" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"/>
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
    <link href="../css/Estilos.css" rel="stylesheet"/>
    <!--[if lt IE 9]>
        <script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->    
    <!-- script references -->
    <script type="text/jscript" src="../js/jquery.min.js"></script>
    <script type="text/javascript" src="../js/bootstrap.min.js"></script>
    <script type="text/jscript" src="../js/jquery.min.js"></script>
</head>

<body>
    <div class="container">
        <form class="form-horizontal" role="form" runat="server">
            <div id="page-header">
                <div class="container">
                   <div class="text-right" >
					    <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión" CssClass="btn btn-vitalicia btn-md"/><BR/>
                        <strong><asp:Label ID="lblUsuario" runat="server" Text="" Font-Size="Small"></asp:Label></strong>
					</div>
                </div>
            </div>

            <div class="text-center" >
               
                <h1 class="text-center"></h1>
                <h3 class="text-center">PEDIDO DE MATERIALES</h3>
                <h4 class="text-center"><span class="hidden-xs">PÁGINA PRINCIPAL DE LOGISTICA</span></h4>
                <asp:Label ID="lblPass"  runat="server" style="display:none;" Text="Label"></asp:Label>
                <asp:Label ID="lblFecha"  runat="server" style="display:none;" Text="Label"></asp:Label>
                <div runat="server" id="lblMensajeErrorS" class="">
                    <strong><asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label></strong>
                </div>
                <div runat="server" id="alert" class="">
                    <strong><asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></strong>
                </div>
            </div>
            
            <fieldset id="fsFormulario" runat="server">
                <fieldset id="fsOpciones" runat="server">
                <div class="row">
                    <div class="col-sm-6 center-block">
                        
                       <asp:Button ID="btnAprobIng" runat="server" Text="Solicitudes Aprobadas " class="btn btn-vitalicia btn-lg btn-block" />
                    </div>
                    <div class="col-sm-6">
                       <h3><span class="hidden-xs">Solicitudes aprobadas por el área de Ingeniería. Desde esta interfaz se generan los archivos de texto para su carga en LogFire.</span></h3> 
                    </div>
                </div>
        
               <div class="row">
                    <div class="col-sm-6">
                        <asp:Button ID="btnPospuestas" runat="server" Text="Solicitudes Pospuestas" class="btn btn-vitalicia btn-lg btn-block" />
                    </div>
                    <div class="col-sm-6">
                        <h3><span class="hidden-xs">Artículos que no se aprobaron para la generación del archivo de texto. Desde esta interfaz puede generarse una Solicitud de Compra, en caso de no existir los artículos en el inventario.</span></h3> 
                    </div>
                </div>
    
                <div class="row">
                    <div class="col-sm-6">
                        <asp:Button ID="btnRevertir" runat="server" Text="Revertir Solicitudes/Histórico" class="btn btn-vitalicia btn-lg btn-block" />
                    </div>
                    <div class="col-sm-6">
                       <h3><span class="hidden-xs">Muestra el historial de solicitudes realizadas. Desde esta pantalla es posible revertir el estado de solicitudes aprobadas o rechazadas. </span></h3>
                    </div>
                </div> 
        
                <div class="row">
                    <div class="col-sm-6">
                        <asp:Button ID="btnPresupuesto" runat="server" Text="Presupuesto de Materiales " class="btn btn-vitalicia btn-lg btn-block" />
                    </div>
                    <div class="col-sm-6">
                       <h3><span class="hidden-xs">Verificar el Presupuesto disponible.</span></h3> 
                    </div>
                </div> 
                </fieldset>
                <div class="row">
                    <div class="col-sm-6">
                        <asp:Button ID="btnConversion" runat="server" Text="Tabla de Conversiones" class="btn btn-vitalicia btn-lg btn-block" />
                    </div>
                    <div class="col-sm-6">
                       <h3><span class="hidden-xs">Modificar la tabla de Unidades y Factores de conversión.</span></h3> 
                    </div>
                </div> 
            </fieldset>
            <div class="row">
                <div class="col-sm-6">
                    <button id="btnPass" runat="server" type="button" class="btn btn-vitalicia btn-lg btn-block" data-toggle="modal" data-target="#modalPassword" data-backdrop="static">Cambiar Contraseña</button>
                </div>
                <div class="col-sm-6">
                   <h3><span class="hidden-xs">Cambiar la contraseña de acceso a la aplicación.</span></h3> 
                    
                </div>            
            </div> 
<!---------------------------- Inicio Modal Nueva Contraseña ---------------------------->
            <div class="modal fade" id="modalPassword" role="dialog">
                <div class="modal-dialog">
                <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header text-center">
                           <h2>Cambiar contraseña <span class="extra-title muted"></span></h2>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="control-group">
                                <label for="actual" class="control-label">Contraseña Actual</label>
                                <div class="controls">
                                    <input id="txtPassActual" type="password" name="actual" runat="server" onkeyup="checkPassAnterior(this.value)" class="form-control input-lg" placeholder="Contraseña Actual" />
                                    <h4><span class="" id="password_same"><asp:Label ID="txtPassSame" runat="server" Text=""></asp:Label></span></h4>
                                </div>
                            </div>
                            <div class="control-group">
                                <label for="nueva" class="control-label">Nueva Contraseña</label>
                                <div class="controls">
                                    <input id="txtPassNueva" type="password" name="nueva" runat="server" onkeyup="CheckPasswordStrength(this.value);comparePass();" class="form-control input-lg" placeholder="Nueva Contraseña" />
                                    <h4><span class="" id="password_strength"><asp:Label ID="txtPassStrength" runat="server" Text=""></asp:Label></span></h4>
                                </div>
                            </div>
                            <div class="control-group">
                                <label for="confirm" class="control-label">Confirmar Contraseña</label>
                                <div class="controls">
                                    <input id="txtPassConfirmar" name="confirm" type="password" runat="server" onkeyup="comparePass()" class="form-control input-lg" placeholder="Confirmar Contraseña"/>
                                    <h4><span class="" id="password_compare"><asp:Label ID="txtPassCompare" runat="server" Text=""></asp:Label></span></h4>
                                </div>
                            </div>
                    
                   
                        </div>
                        <div class="modal-footer">
                            <div class="btn-group btn-group-justified" role="group" aria-label="...">
                                <div class="btn-group" role="group">
                                    <input id="btnPassAceptar" type="button" value="Aceptar" runat="server" class="btn btn-vitalicia btn-lg disabled" />
                                </div>
  
                                <div class="btn-group" role="group">
                                <input id="btnPassCancelar" type="button" value="Cancelar" runat="server" data-dismiss="modal" class="btn btn-default btn-lg" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
<!------------------------------ Fin Modal Nueva Contraseña ------------------------------>

<!------------------------------ Inicio pie de página------------------------------>
            <div id="footer">
                <div class="container">
                    <div class="text-right" >
		                <a href="http://www.clever.bo/"> <img src="../img/clever.png" class="" alt="Cinque Terre" width="5%" height="5%"/></a>
                    </div>
                </div>
            </div>
<!------------------------------ Fin pie de página  ------------------------------>
        </form>

           <script type="text/javascript">

               function checkPassAnterior(password){
                   var passAnt = document.getElementById('<%= lblPass.ClientID %>').innerHTML;
                   var pass = password;
                   if (pass == passAnt) {
                       document.getElementById("password_same").className = "label label-success";
                       document.getElementById('<%= txtPassSame.ClientID %>').innerHTML = "Las contraseñas coinciden"; 
                   }
                   else {
                       document.getElementById("password_same").className = "label label-danger";
                       document.getElementById('<%= txtPassSame.ClientID %>').innerHTML = "Las contraseñas no coinciden"; 
                   }
                   var classA = document.getElementById("password_same").className;
                   var classB = document.getElementById("password_strength").className;
                   var classC = document.getElementById("password_compare").className;
                   if (classA == "label label-success" && classB == "label label-success" && classC == "label label-success") {
                        document.getElementById("btnPassAceptar").className = "btn btn-vitalicia btn-lg";
                   }
                   else {
                       document.getElementById("btnPassAceptar").className = "btn btn-vitalicia btn-lg disabled";
                   }

                   
                   


               }

       function CheckPasswordStrength(password) {
            var password_strength = document.getElementById("password_strength");

            //TextBox left blank.
            if (password.length == 0) {
                document.getElementById('<%= txtPassStrength.ClientID %>').innerHTML = ""; 
                document.getElementById("password_strength").className = "";
                return;
            }

            

            //Regular Expressions.
            var regex = new Array();
            regex.push("[A-Z]"); //Uppercase Alphabet.
            regex.push("[a-z]"); //Lowercase Alphabet.
            regex.push("[0-9]"); //Digit.
            regex.push("[$@$!%*#?&]"); //Special Character.

            var passed = 0;

            //Validate for each Regular Expression.
            for (var i = 0; i < regex.length; i++) {
                if (new RegExp(regex[i]).test(password)) {
                    passed++;
                }
            }

            //Validate for length of Password.
            if (password.length < 10 && password.length > 0) {
                
                passed = 99;

            }
            else {
                passed++;
            }
            
           

            //Display status.
            var color = "";
            var strength = "";
            switch (passed) {
                case 0:
                    
                case 1:
                    
                case 2:strength = "Muy débil: Combine mayúsculas, minúsculas y números ";
                    color = "label label-danger";
                    break;
                    
                case 3: strength = "Débil: Combine mayúsculas, minúsculas y números";
                    color = "label label-warning";
                    break;
                case 4:
                    strength = "Fuerte";
                    color = "label label-success";
                    break;
                case 5:
                    strength = "Muy Fuerte";
                    color = "label label-success";
                    break;
                case 99:
                    
                    cuenta = password.length;
                    cuenta = 10 - cuenta
                    strength = "Ingrese al menos " + cuenta +  " caracteres más";
                    color = "label label-danger";
                    break;
            }
            document.getElementById('<%= txtPassStrength.ClientID %>').innerHTML = strength; 
           document.getElementById("password_strength").className = color;

           var classA = document.getElementById("password_same").className;
           var classB = document.getElementById("password_strength").className;
           var classC = document.getElementById("password_compare").className;
           if (classA == "label label-success" && classB == "label label-success" && classC == "label label-success") {
               document.getElementById("btnPassAceptar").className = "btn btn-vitalicia btn-lg";
           }
           else {
               document.getElementById("btnPassAceptar").className = "btn btn-vitalicia btn-lg disabled";
           }
       }

               function comparePass() {
                    var passAnt = document.getElementById('<%= txtPassNueva.ClientID %>').value;
                   var pass = document.getElementById('<%= txtPassConfirmar.ClientID %>').value;
                   if (pass == passAnt) {
                       document.getElementById("password_compare").className = "label label-success";
                       document.getElementById('<%= txtPassCompare.ClientID %>').innerHTML = "Las contraseñas coinciden"; 
                   }
                   else {
                       document.getElementById("password_compare").className = "label label-danger";
                       document.getElementById('<%= txtPassCompare.ClientID %>').innerHTML = "Las contraseñas no coinciden"; 
                   }
                   var classA = document.getElementById("password_same").className;
                   var classB = document.getElementById("password_strength").className;
                   var classC = document.getElementById("password_compare").className;
                   if (classA == "label label-success" && classB == "label label-success" && classC == "label label-success") {
                       document.getElementById("btnPassAceptar").className = "btn btn-vitalicia btn-lg";
                   }
                   else {
                       document.getElementById("btnPassAceptar").className = "btn btn-vitalicia btn-lg disabled";
                   }

               }
    </script>
    </div>
</body>
</html>
