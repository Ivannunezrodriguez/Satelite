<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Satelite.Login" %>

<html lang="es">
<head>
    <meta charset="utf-8">
    <title>Viveros Rioeresma (Login)</title>
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

               /** {
            padding: 0px;
            margin: 0px;
            box-sizing: border-box;
        }
        
        .container {
            width: 100%;
            height: 100vh;
            background-color: rgba(0,0,0,1);
            position: relative;
            overflow: hidden;
        }
        
       .bub {
            width: 30px;
            height: 30px;
            border: 1px solid #00FFFF;
            box-shadow: inset 1px -1px 1px #00FFFF;
            border-radius: 50%;
            background-color: rgba(255, 255, 255, .01);
            backdrop-filter: blur(1px);
            position: absolute;
            bottom: -100px;*/
            /* animation: bubble 3s ease-in infinite; */
        /*}
        
        .bub::before {
            position: absolute;
            content: "";
            background-color: #00FFFF;
            width: 6px;
            height: 6px;
            border-radius: 50%;
            top: 4px;
            right: 4px;
            box-shadow: 0px 0px 1px #00FFFF;
        }
        
        .bub.a {
            left: 10%;
            animation: bubble2 8s ease-in 1s infinite;
        }
        
        .bub.b {
            left: 20%;
            animation: bubble 10s ease-in 1.4s infinite;
        }
        
        .bub.c {
            left: 28%;
            animation: bubble 8s ease-in 3.8s infinite;
        }
        
        .bub.d {
            left: 40%;
            animation: bubble 15s ease-in .5s infinite;
        }
        
        .bub.e {
            left: 75%;
            animation: bubble2 8s ease-in 2.5s infinite;
        }
        
        .bub.f {
            left: 90%;
            animation: bubble 8s ease-in 3s infinite;
        }
        
        .bub.g {
            left: 60%;
            animation: bubble 11s ease-in 2s infinite;
        }
        
        .bub.k {
            left: 50%;
            animation: bubble 8s ease-in 2s infinite;
        }
        
        .bub.i {
            left: 65%;
            animation: bubble2 9s ease-in 2.1s infinite;
        }
        
        .bub.j {
            left: 3%;
            animation: bubble 8s ease-in 1.5s infinite;
        }
        
        .bub.h {
            left: 35%;
            animation: bubble2 12s ease-in infinite;
        }
        
        @keyframes bubble {
            0% {
                opacity: 0;
            }
            10%,
            98% {
                opacity: 1;
            }
            100% {
                transform: translate(90px, -700px);
                display: none;
            }
        }
        
        @keyframes bubble2 {
            0% {
                opacity: 0;
            }
            10%,
            98% {
                opacity: 1;
            }
            100% {
                transform: translate(-90px, -700px);
                display: none;
            }
        }*/


    </style>
</head>


<body id="body">
     <form id="Form1"  runat="server" >
    <%--  dark-water-waves.gif--%>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <header class="HeaderMain" data-test="header-main" role="banner">
                <div class="inner">
                    <div class="s-wrap">
                        <div class="FmLogo">
                            <%--<img style="height:80px; width:200px; align-content:center; overflow:auto;" src="Images/logoTransparente.gif">--%>
							<a class="s-tr" href="http://www.rioeresma.com/">Viveros Rio Eresma</a>
						</div>
                        <label class="NavMobileHamburger" for="open-nav-main">Menu</label><nav class="Nav NavMobile NavMain">
                            <input class="toggle-target" id="open-nav-main" type="checkbox"><ul>
                                <li class="item"><a href="http://www.rioeresma.es/">Web</a></li>
                                <li class="item"><a href="Intranet.aspx">Intranet</a></li>
                                <li class="item"><a href="Plantillas.aspx">Códigos QR</a></li>
								<li class="item"></li>
								<li class="item"></li>
								<li class="item"><a href="Registro.aspx">								
								<svg width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
									<path d="M15.0726 10.4326C12.4801 10.4326 10.4 12.5451 10.4 15.1053C10.4 17.6978 12.5125 19.7779 15.0726 19.7779C17.6651 19.7779 19.7453 17.6654 19.7453 15.1053C19.744 12.544 17.664 10.4326 15.0726 10.4326Z" fill="white"></path>
									<path d="M16.0327 0.447479C7.42401 0.447479 0.447693 7.4238 0.447693 16.0324C0.447693 24.6398 7.42401 31.6161 16.0314 31.6161C24.64 31.6161 31.6163 24.6398 31.6163 16.0324C31.6163 7.4238 24.64 0.447479 16.0327 0.447479ZM23.6163 24.0324C23.3926 24.2562 23.0401 24.385 22.6563 24.385C22.2726 24.385 21.9526 24.2575 21.6963 24.0324L18.7526 21.0887L18.4651 21.2487C17.4088 21.825 16.2576 22.145 15.0724 22.145C11.1998 22.145 8.06509 19.0087 8.06509 15.1376C8.06509 11.265 11.2013 8.13023 15.0724 8.13023C18.9451 8.13023 22.0798 11.2665 22.0798 15.1376C22.0798 16.5138 21.6961 17.8251 20.9273 18.9776L20.7348 19.2976L23.6473 22.2101C23.8711 22.4664 24.0311 22.7864 24.0311 23.1376C23.9998 23.4563 23.8723 23.7763 23.616 24.0326L23.6163 24.0324Z" fill="white"></path>
									</svg></a>
								</li>
								<li class="item"><a href="#">						
									<svg width="34" height="34" viewBox="0 0 34 34" fill="none" xmlns="http://www.w3.org/2000/svg">
									<path d="M17 1.7C11.5335 1.7 6.48243 4.61655 3.74917 9.35C1.0172 14.0835 1.0172 19.9165 3.74917 24.65C6.48246 29.3835 11.5335 32.3 17 32.3C20.4918 32.3133 23.8809 31.1193 26.5917 28.9199C29.9014 26.2557 31.9559 22.3365 32.2602 18.0984C32.5657 13.8603 31.0954 9.68748 28.2003 6.57682C25.3063 3.46636 21.249 1.69986 17.0003 1.69986L17 1.7ZM27.9956 24.411C27.2651 22.1412 25.014 20.2514 21.9925 19.2447C20.7069 20.6405 18.8967 21.4361 17 21.4361C15.1021 21.4347 13.292 20.6405 12.0074 19.2447C8.98463 20.2514 6.73335 22.1413 6.00439 24.411C4.20212 21.7388 3.43579 18.5008 3.8475 15.3041C4.25921 12.1073 5.82242 9.17082 8.24234 7.04174C10.6622 4.91276 13.7752 3.7387 16.9987 3.7387C20.2221 3.7387 23.3353 4.91276 25.7564 7.04041C28.1763 9.16939 29.7395 12.1057 30.1513 15.3027C30.563 18.4995 29.7966 21.7376 27.9957 24.411L27.9956 24.411Z" fill="white"></path>
									<path d="M22.5049 13.6092C22.5049 16.9919 20.0399 19.736 17 19.736C13.9586 19.736 11.4937 16.9921 11.4937 13.6092C11.4937 10.2251 13.9587 7.48236 17 7.48236C20.0401 7.48236 22.5049 10.2249 22.5049 13.6092Z" fill="white"></path>
									</svg></a>
								</li>
                            </ul>
                        </nav>
                    </div>
                </div>
				
                <svg class="diagonal" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 10" preserveAspectRatio="none">
                    <defs>
                        <linearGradient id="linear" x1="0%" y1="0%" x2="100%" y2="0%">
                            <stop offset="0%" stop-color="#62ba5f"></stop>
                            <stop offset="100%" stop-color="#43631c"></stop>
                        </linearGradient>
                    </defs><polygon points="0,2 0,0 100,0 100,10" fill="url(#linear)"></polygon></svg>
			</header>

    <video autoplay="autoplay" loop="loop" id="video_background" preload="auto" muted="muted" poster="Images/Logo_Rio_Eresma_inv.png">
        <source src="Images/SlowMotion.mp4" type="video/mp4">
    </video>
    <div id="Dlogin" runat="server" visible="true" class="loginBox"> 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img style="height:40px; width:100px; align-content:center;" src="Images/Logo_Rio_Eresma_inv.png">
        <br />
        <%--<h2>ACCESO</h2>   fresas3.mp4--%>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">      
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnAcepta" EventName="Click" />
            </Triggers>
            <ContentTemplate>                        
                <div runat="server" id="DvPreparado" visible="false"  style=" width: 30%;z-index: 1888;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155); overflow:auto; position: fixed;" class="alert alert-grey centrado position-fixed">
                    <%--<button type="button" class="close" data-dismiss="alert">&times;</button>--%> 
                    <i id="I1" runat="server" class="fa fa-exclamation-circle fa-2x"></i>
                    <asp:Label runat="server" class="alert alert-grey alert-dismissable" ID="Lbmensaje" BorderStyle="None" border="0" Width="100%" Text=" Se eliminarán los registros actuales con una nueva consulta ¿Desea continuar?"  />
                    <div class="row" id="cuestion" visible="true" runat="server">
                        <div class="col-lg-6">
                            <asp:Button runat="server" ID="BtnAcepta" Visible="true" tooltip="Aceptar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Aceptar" OnClick="checkSi_Click"/>                                                    
                            </div>
                        <div class="col-lg-6">
                            <asp:Button runat="server" ID="BTnNoAcepta" Visible="true" tooltip="Cancelar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Cerrar" OnClick="checkNo_Click"/>                 
                        </div>
                    </div>
                        <div class="row" id="Asume" visible="false" runat="server">
                        <div class="col-lg-12">
                            <asp:Button runat="server" ID="btnasume" Visible="true" tooltip="Aceptar" BorderStyle="None" border="0" CssClass="btn btn-primary btn-block" Width="100%"  Text="Aceptar" OnClick="checkOk_Click"/>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <div id="Div1"  runat="server" >
            <asp:Label ID="lblUserID" CssClass="blanco" runat="server">User ID:</asp:Label><asp:RequiredFieldValidator ID="chkUserID" runat="server" CssClass="verde" ErrorMessage="Campo ID obligatorio" Width="132px" ControlToValidate="txtUserID" Display="Dynamic"></asp:RequiredFieldValidator><br />
            <asp:TextBox ID="txtUserID" runat="server" MaxLength="20"></asp:TextBox>
            <asp:Label ID="lblPassword" CssClass="blanco" runat="server">Password:</asp:Label><asp:RequiredFieldValidator ID="chkPassword" CssClass="verde" runat="server" ErrorMessage="Campo Password obligatorio" ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator><br />
            <asp:TextBox ID="txtPassword" CssClass="box-white" runat="server" TextMode="Password" MaxLength="25"></asp:TextBox>
            <br />
            <asp:Label ID="lbErr" CssClass="rojo" runat="server"></asp:Label>
            <br />
            <asp:Button CssClass="boton" align="center" ID="btnLogin" runat="server" Text="LOGIN" Width="173px" OnClick="btnLogin_Click" Height="42px"></asp:Button>
            <br />
        </div>
    </div>
    <div id="DloginCapcha" visible ="false" runat="server" class="loginBoxCapcha"> 
        <div id="Div2" runat="server" >
            <asp:Label ID="Label4" CssClass="naranja" Font-Size="14" runat="server">Alta de Registro del Usuario:</asp:Label><asp:Label ID="LbUserDes" CssClass="blanco" Font-Size="14" runat="server"></asp:Label><br /><br />
            <asp:Label ID="Label6" CssClass="verde" runat="server">El Administrador le otorgó una Contraseña de primer acceso para llegar hasta aquí. Ahora debe crear su nueva contraseña personal para acceder al Sistema Satelite de Viveros Rio Eresma</asp:Label><br />
            <br />
            <asp:Label ID="Label1" CssClass="blanco" runat="server">User ID:</asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="verde" ErrorMessage="Campo ID obligatorio" Width="132px" ControlToValidate="txtUserID" Display="Dynamic"></asp:RequiredFieldValidator><br />
            <asp:TextBox ID="txtUserID2" runat="server" MaxLength="20"></asp:TextBox>
            <asp:Label ID="Label2" CssClass="blanco" runat="server">Password:</asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="verde" runat="server" ErrorMessage="Campo Password obligatorio" ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator><br />
            <asp:TextBox ID="txtPassword2" CssClass="box-white" runat="server" TextMode="Password" MaxLength="25"></asp:TextBox>
            <br />
            <div class="form-row">
				<asp:Image ID="imgCaptcha" runat="server" ImageUrl="CreateCaptcha.aspx?New=1" Height="100" Width="400" />
			</div>
			<div class="form-row">
				<asp:Label id="lblMessage" runat="server" ></asp:Label>
				<br />
				<asp:TextBox ID="txtCaptcha" runat="server"  CssClass="box-white" placeholder="Introduzca el código de la imagen Captcha" required=""></asp:TextBox>

			</div>
            <br />
            <asp:Label ID="Label3" CssClass="rojo" runat="server"></asp:Label>
            <br />
            <div class="form-row">

                <div  class="col-lg-4">
                <asp:Button CssClass="boton" align="center" ID="btnLogin2" runat="server" Text="REGISTRARME" OnClick="btnLoginDes_Click" Width="300px" Height="42px"></asp:Button>
                </div>
                <div  class="col-lg-6">
                </div>
            </div>
            <br />
            <asp:Label ID="Label5" CssClass="verde" runat="server">Esta será su contraseña, la que deberá utilizar a partir de ahora</asp:Label><br />




        </div>
    </div>
  <%--  <div class="container">
        <span class="bub a "></span>
        <span class="bub b "></span>
        <span class="bub c "></span>
        <span class="bub d"></span>
        <span class="bub e"></span>
        <span class="bub f"></span>
        <span class="bub g"></span>
        <span class="bub h"></span>
        <span class="bub i"></span>
        <span class="bub j "></span>
        <span class="bub k"></span>
    </div>--%>
    </form>
</body>
</html>
