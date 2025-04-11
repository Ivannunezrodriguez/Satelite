$("#btncolumna").click(function(){


});

$("#close-modal").click(function(){
    $("#modal-1").modal("hide");

});

$("#btncolumna").click(function(){
    $(".modal-header").css("background-color","#dc3545");
    $(".modal-header").css("color","white");
    $(".modal-title").text("Grafico de pruebas");
    $("#modal-1").modal("show");
    prueba();
});

function prueba(){
    alert("prueba");
    Highcharts.chart('contenedor-modal', {
        chart:{type:'line'},
        title:{text:'Valores Mensuales'},
        xAxis:{categories:['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']},
        series:[{data:[2, 3, 4, 5, 7, 9, 6, 4, 3, 2, 1, 5]}],      
    });

};

