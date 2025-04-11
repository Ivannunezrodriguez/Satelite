//Funcion para modificar la visibilidad de la cesta de la compra
function toggleDisplay() {
    var element = document.getElementById("div_cesta");
    if (element.style.display === "none") {
        element.style.display = "flex";
    } else {
        element.style.display = "none";
    }
}

//Funcion para modificar el numero de unidades de un elemento desde la cesta de la compra
function add_uds_cesta(producto) {
    $.post(
        "../MarketplaceFunction.aspx",{
            function: "addCestaProduct",
            idProducto: producto,
            cantidad: $("#cantidad_producto_" + producto).val()
        },
        function (data) {
            //Parseamos el Json
            let obj = JSON.parse(data);

            let difPrecio = parseInt(obj.difPrecio);
            let udsCesta = obj.udsCesta;

            //modifico el contador de elementos en la cesta
            $("#cont_cesta").html(udsCesta)

            
            //recalculo del total del producto
            let unicprice = $("#precio_producto_" + producto).html()
            unicprice = parseInt(unicprice.replace("&euro;", ""));
            unicprice = unicprice + difPrecio;
            $("#precio_producto_" + producto).html(unicprice + " &euro;")

            //recalculo del total de la cesta
            let totalprice = $("#total_price").html()
            totalprice = parseInt(totalprice.replace("&euro;", ""));
            totalprice = totalprice + difPrecio;

            $("#total_price").html(totalprice + " &euro;");

        }
    );
}


//Funcion para modificar el numero de unidades de un elemento desde la cesta de la compra
function enviar_cesta() {
    if (parseInt($("#LbCantidad").html()) > 0) {
        $.post(
            "../MarketplaceFunction.aspx",
            {
                function: "EnviaRegistro_Click",
                nombre: $("#TxtName").val(),
                municipio: $("#TxtMunicipio").val(),
                provincia: $("#TxtMunicipio").val(),
                telefono: $("#TxtTelefono").val(),
                mail: $("#TxtMail").val(),
                articulos: $("#LbCantidad").html(),
                preciototal: $("#LbTotal").html()
            },
            function (data) {
                //Parseamos el Json
                let obj = JSON.parse(data);

                let ticket = obj.ticket;

                //escribimos el numero del ticket y lo mostramos
                $("#LbNumTicket").html(ticket)
                $("#LbGuarde").css("display", "")
                $("#Btnenviar2").css("display", "none")

            }
        );
    }
}