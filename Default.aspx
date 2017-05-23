<%@ Page culture="es-MX" UICulture="es-MX"  Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
		<meta charset="utf-8"/>
		<title>Menú Principal</title>
   
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
		<script type="text/javascript" src="../js/jquery.min.js"></script>
		<script type="text/javascript" src="../js/bootstrap.min.js"></script>
</head>
<body>


    <!--login modal-->
		<div id="loginModal" class="modal show">
			<div class="modal-dialog">
				<div class="modal-header text-center" >
					
                    <h3 class="text-center">MÓDULO DE CONTROL DE PEDIDO DE MATERIALES</h3>
                </div>
				<div class="modal-body text-center">
					<form class="form col-md-12 center-block" id="form1" runat="server">
						<div class="form-group">
							<h4><strong></strong> </h4>
							<asp:Button ID="btnResidente" runat="server" Text="Módulo de Residentes de Obra" class="btn btn-vitalicia btn-lg btn-block" />
						</div>
						<div class="form-group">
							
                            <h4><strong></strong> </h4>
                            <asp:Button ID="btnIngenieria" runat="server" Text="Módulo de Ingeniería" class="btn btn-vitalicia btn-lg btn-block" />
						</div>
						<div class="form-group">
							
                            <h4><strong></strong> </h4>
                            <asp:Button ID="btnLogistica" runat="server" Text="Módulo de Logística" class="btn btn-vitalicia btn-lg btn-block" />
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
