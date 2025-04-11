<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="importFiles.aspx.cs" Inherits="Satelite.importFiles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multiple</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <script src="js/jquery.min.js"></script>
    <script>
        var counter;
        function UploadFile() {
            var files = $("#<%=file1.ClientID%>").get(0).files;
            //var theControl = document.getElementById("btActualiza");
            //theControl.style.display = "none";

            counter = 0;
            // Loop through files
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                var formdata = new FormData();
                formdata.append("file1", file);
                var ajax = new XMLHttpRequest();

                ajax.upload.addEventListener("progress", progressHandler, false);
                ajax.addEventListener("load", completeHandler, false);
                ajax.addEventListener("error", errorHandler, false);
                ajax.addEventListener("abort", abortHandler, false);
                ajax.open("POST", "FileUploadHandler.ashx");
                ajax.send(formdata);
            }
            //theControl.style.display = "";
        }
        function progressHandler(event) {
            $("#loaded_n_total").html("Subidos " + event.loaded + " bytes de " + event.total);
            var percent = (event.loaded / event.total) * 100;
            $("#progressBar").val(Math.round(percent));
            $("#status").html(Math.round(percent) + "% Subiendo... Por favor espere.");
        }
        function completeHandler(event) {
            counter++
            $("#status").html(counter + " " + event.target.responseText);
        }
        function errorHandler(event) {
            $("#status").html("Error subiendo");
        } function abortHandler(event) {
            $("#status").html("Subida abortada");
        }
    </script>
    <style>
        body {
            /*background: #f5e98d no-repeat;*/
            /*background-image: -webkit-gradient(radial, 50% 0, 150, 50% 0, 300, from(#444), to(#f5e98d));*/
        }

        h1, h2 {
            text-align: center;
            color: #FFF;
        }

            h2 a {
                color: #0184e3;
                text-decoration: none;
            }

                h2 a:hover {
                    text-decoration: underline;
                }

        .wrapper {
            width: 960px;
            margin: 0 auto;
            background-color: #FFF;
            padding: 10px;
        }

        .file-upload {
                display: inline-block;
                overflow: hidden;
                text-align: center;
                vertical-align: middle;
                font-family: Arial;
                /*border: 1px solid #124d77;*/
                background: #d5e450;
                color: #fff;
                border-radius: 6px;
                -moz-border-radius: 6px;
                cursor: pointer;
                text-shadow: #000 1px 1px 2px;
                -webkit-border-radius: 4px;
            }

            .file-upload:hover {
                    background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #0061a7), color-stop(1, #007dc1));
                    background: -moz-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                    background: -webkit-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                    background: -o-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                    background: -ms-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                    background: linear-gradient(to bottom, #0061a7 5%, #007dc1 100%);
                    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0061a7', endColorstr='#007dc1',GradientType=0);
                    background-color: #0061a7;
            }

            /* The button size */
            .file-upload {
                height: 35px;
            }

            .file-upload, .file-upload span {
                    width: 100%;
            }

            .file-upload input {
                        top: 0;
                        left: 0;
                        margin: 0;
                        font-size: 11px;
                        font-weight: bold;
                        /* Loses tab index in webkit if width is set to 0 */
                        opacity: 0;
                        filter: alpha(opacity=0);
            }

            .file-upload strong {
                        font:11px; /*normal 12px Tahoma,sans-serif;*/
                        text-align: center;
                        vertical-align: middle;
            }

            .file-upload span {
                        top: 0;
                        left: 0;
                        display: inline-block;
                        /* Adjust button text vertical alignment */
                        padding-top: 5px;
            }

    </style>
</head>
<body>
    <form id="form1" style=" height:20px; "  runat="server">
        <%--&nbsp;<h1>Subida de ficheros</h1>--%><%-- <h2><a href="http://www.yogihosting.com/multi-file-upload-with-progress-bar-in-asp-net/">Read the tutorial on YogiHosting »</a></h2>--%><%--<div class="wrapper" style="width:100%;position:absolute;">--%>
        <label class="file-upload" style="width:100%; margin-top:2px;"><span><strong>Seleccionar Ficheros</strong></span>
            <asp:FileUpload ID="file1" runat="server" AllowMultiple="true" />
            <%--<br>--%>
        </label>

        <br />
            <%--<asp:FileUpload ID="file1" runat="server" AllowMultiple="true" /><br>--%>
            <input type="button" class="btn btn-default" style="width:100%;margin-top:-5px; " value="Subir ficheros" onclick="UploadFile()" />
        <br />
            <progress id="progressBar" value="0" max="100" style="height:40px; width:100%;text-align:right;"></progress>
            <h3 id="status"></h3>
            <p id="loaded_n_total"></p>
            <%--<input type="button" class="btn btn-default" id="btActualiza" visible="false" style="width:50%;margin-top:-5px; " value="Actualizar" onserverclick="btActualiza_Click" />--%>
        <%--</div>--%>
    </form>
</body>
</html>

