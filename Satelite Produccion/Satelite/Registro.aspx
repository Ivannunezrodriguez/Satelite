<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Satelite.Registro" %>

<!DOCTYPE html>
<html id="top-of-site" lang="es">
	<head>
    <meta charset="utf-8">
    <title>Viveros Rio Eresma (Login)</title>
    <link rel="stylesheet" href="css/stylogin.css" type="text/css" />
    <link rel="stylesheet" href="css/estilos2.css">
    <link rel="stylesheet" href="css/frontend1.css" type="text/css" />
	<link rel="stylesheet" href="css/frontend2.css" type="text/css" />
    <link rel="stylesheet" href="css/style3.css" type="text/css" />

    <%--<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />--%>
    <style type="text/css">
        body {
            BACKGROUND-COLOR: #fff
        }
    </style>
</head>


	<body id="body">
		<form runat="server">
		<div id="top-of-site-pixel-anchor">
		</div>
		<header class="page-wrap">
			<div class="site-header" id="site-header">
				<div class="logo">

				</div>
		    </div>
			<div class="articles-and-sidebar">
				<%--<main id="post-255272">
					<article>
						</article>
					</main>--%>
			</div>
		</header>

		<video autoplay="autoplay" loop="loop" id="video_background" preload="auto" muted="muted" poster="Images/fresas.jpg">
        <source src="Images/fresas.mp4" type="video/mp4">
    </video>
    <div class="RegisterBox"  style="text-align:center;">
		<div class="row">
			<img style="height:80px; width:400px; align-content:center;" src="Images/logo.gif">
		</div>
			<div class="row">
				<div class="col-lg-4">
				</div>
				<div class="col-lg-4">
					 <div class="bs-example">
						<div class="s-wrap s-wrap-thin s-vp-lg">	
								<div class="s-vm-md">
									<label for="username" style="color:#fff;">Tus datos como usuario nuevo</label>									
									<div class="form-error"></div>
								</div>
								<div class="s-vm-md">
									<label for="username" CssClass="blanco" >Correo electrónico</label>
									<br />
									<asp:TextBox id="txtEmail" runat="server" CssClass="box-white" placeholder="Por favor introduzca su email" required=""></asp:TextBox> 
									<div class="form-error"></div>
								</div>
								<div class="s-vm-md">
									<label for="username" CssClass="blanco" >Alias con el que identificarte con el resto de usuarios</label>
									<br />
									<asp:TextBox id="TxtAlias" runat="server" CssClass="box-white" placeholder="Por favor introduzca un Alias para su usuario" required=""></asp:TextBox> 
									<div class="form-error"></div>
								</div>
								<div class="s-vm-md">
									<label for="password" CssClass="blanco">Password</label>
									<br />
									<asp:TextBox id="TextPass" runat="server" CssClass="box-white" TextMode="Password" placeholder="Por favor introduzca su password" required=""></asp:TextBox> 							
									<div class="form-error"></div>
								</div>
								<%--<input type="hidden" name="nonce" value="70422eaba3">
								<input type="hidden" name="redirect" value="">--%>
								<div class="s-vm-md">
									<label for="password" CssClass="blanco">Imagen Capcha</label>	
									<br />
									<div class="form-error"></div>
								</div>
								<div class="form-row">
									<asp:Image ID="imgCaptcha" runat="server" ImageUrl="CreateCaptcha.aspx?New=1" Height="100" Width="400" />
								</div>
								<div class="form-row">
									<asp:Label id="lblMessage" runat="server" ></asp:Label>
									<br />
									<asp:TextBox ID="txtCaptcha" runat="server"  CssClass="box-white" placeholder="Introduzca el código de la imagen Captcha" required=""></asp:TextBox>

								</div>
								<div class="form-row">
									<asp:Label id="Label1" runat="server" ></asp:Label>
									<br />
									<asp:TextBox ID="TxtCode" runat="server"  CssClass="box-white" placeholder="Introduzca el código facilitado por la Empresa" required=""></asp:TextBox>

								</div>
								<div class="s-vm-md">
									<label for="lavee"></label>	
									<div class="form-error"></div>
								</div>
								<div class="form-row" style="text-align:center;">
									<div style="text-align:right;">
										<asp:Button ID="btnSubmit" Width="200px" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-success btn-block" Text="Registrarse ahora" />
									</div>
								</div>
								<div class="form-row" style="text-align:left;">
									<a class="recover" style="color:#fff;" href="Login.aspx">¿Ya estás registrado?</a>
								</div>
							</div>
						</div>
					</div>
				</div>

		</div>
		</form>
	</body>
</html>
