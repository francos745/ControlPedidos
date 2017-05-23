<%@ Page culture="es-MX" UICulture="es-MX"  Language="VB" AutoEventWireup="false" CodeFile="Ingreso.aspx.vb" Inherits="logistica_Ingreso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
		<meta charset="utf-8"/>
		<title>Página Principal</title>
         <script language="javascript" type="text/javascript">
            javascript: window.history.forward(1);
        </script>
        <link rel="shortcut icon" href="../img/clever.png" />
		<meta name="generator" content="Bootply" />
		<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=2"/>
		<link href="../css/bootstrap.min.css" rel="stylesheet"/>
        <link href="../css/Estilos.css" rel="stylesheet"/>
		<!--[if lt IE 9]>
			<script src="//html5shim.googlecode.com/svn/trunk/html5.js"></script>
		<![endif]-->
		<link href="../css/styles.css" rel="stylesheet"/>
        
        <!-- script references -->
		<script type="text/jscript" src="../js/jquery.min.js"></script>
		<script type="text/javascript" src="../js/bootstrap.min.js"></script>
</head>
<body>


    <!--login modal-->
		<div id="loginModal" class="modal show" tabindex="-1" role="dialog" aria-hidden="true">
			<div class="modal-dialog">
				<div class="modal-header text-center" >
					
					
                    <h3 class="text-center">INGRESO DE LOGÍSTICA</h3>
                    <h4 class="text-center">PEDIDO DE MATERIALES</h4>
				</div>
				<div class="modal-body text-center">
					<form class="form col-md-12 center-block" id="form1" runat="server">
						<div class="form-group">
							<h4><strong>USUARIO</strong> </h4>
							<asp:TextBox ID="txtUsuarioLog" runat="server" class="form-control input-lg text-center" placeholder="Usuario"></asp:TextBox>
						</div>
						<div class="form-group">
							
                            <h4><strong>CONTRASEÑA</strong> </h4>
                            <asp:TextBox ID="txtPassLog" runat="server" TextMode="Password" class="form-control input-lg text-center" placeholder="Contraseña" ></asp:TextBox>
						</div>
						<div class="form-group">
		                    
                            
                        </div>

                        <div class="btn-group btn-group-justified" role="group" aria-label="...">
                            <div class="btn-group" role="group">
                                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" class="btn btn-vitalicia btn-lg btn-block" />
                            </div>
  
                            <div class="btn-group" role="group">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-default btn-lg btn-block" />
                            </div>
                        </div>

                        <div runat="server" id="alert" class="">
                            <strong><asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></strong>
                        </div>
				  </form>
				</div>
				<div class="modal-footer">
					<div class="col-md-12">
					</div>	
				</div>
			</div>
            
            <div id="footer">
                <div class="container">
                   <div class="modal-header text-right" >
					<img src="../img/clever.png" class="" alt="Cinque Terre" width="5%" height="5%"/>
					
				    </div>
                </div>
            </div>
		</div>

		
</body>
</html>
