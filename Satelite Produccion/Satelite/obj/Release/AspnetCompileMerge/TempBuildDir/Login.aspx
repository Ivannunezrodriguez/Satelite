<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QRCode_Demo.Login" %>

<html lang="es">
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
    <%--  dark-water-waves.gif--%>


            <header class="HeaderMain" data-test="header-main" role="banner">
                <div class="inner">
                    <div class="s-wrap">
                        <div class="FmLogo">
                            <%--<img style="height:80px; width:200px; align-content:center; overflow:auto;" src="Images/logoTransparente.gif">--%>
							<a class="s-tr" href="http://www.rioeresma.es/">Viveros Rio Eresma</a>
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




    <video autoplay="autoplay" loop="loop" id="video_background" preload="auto" muted="muted" poster="Images/fresas.png">
        <source src="Images/SlowMotion.mp4" type="video/mp4">
    </video>
    <div class="loginBox">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img style="height:80px; width:200px; align-content:center;" src="Images/logo.png">
        <br />
        <%--<h2>ACCESO</h2>   fresas3.mp4--%>
        <form id="Form1" runat="server">
            <asp:Label ID="lblUserID" CssClass="blanco" runat="server">User ID:</asp:Label><asp:RequiredFieldValidator ID="chkUserID" runat="server" CssClass="verde" ErrorMessage="Campo ID obligatorio" Width="132px" ControlToValidate="txtUserID" Display="Dynamic"></asp:RequiredFieldValidator><br />
            <asp:TextBox ID="txtUserID" runat="server" MaxLength="20"></asp:TextBox>
            <asp:Label ID="lblPassword" CssClass="blanco" runat="server">Password:</asp:Label><asp:RequiredFieldValidator ID="chkPassword" CssClass="verde" runat="server" ErrorMessage="Campo Password obligatorio" ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator><br />
            <asp:TextBox ID="txtPassword" CssClass="box-white" runat="server" TextMode="Password" MaxLength="25"></asp:TextBox>
            <br />
            <asp:Label ID="lbErr" CssClass="rojo" runat="server"></asp:Label>
            <br />
            <asp:Button CssClass="boton" align="center" ID="btnLogin" runat="server" Text="LOGIN" Width="173px" OnClick="btnLogin_Click" Height="42px"></asp:Button>
            <br />
        </form>
    </div>
</body>
</html>
