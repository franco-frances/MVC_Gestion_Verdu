
$(function () {
    // Abrir modal en modo "Agregar"
    $("#btnAgregarGasto").on("click", function () {
        $("#modalTituloGasto").text("Agregar Gasto");
        $("#formGasto").attr("action", "/Gastos/AgregarGasto");

        // Limpiar campos y establecer la fecha actual
        $("#idGasto").val("");
        $("#fecha").val(new Date().toLocaleDateString('sv-SE'));
        $("#concepto").val("");
        $("#monto").val("");

        $("#modalAgregarGasto").modal("show");
    });

    // Abrir modal en modo "Editar"
    $(document).on('click', '.btnEditarGasto', function() {
        let id = $(this).data("id");
        let fecha = $(this).data("fecha");
        let concepto = $(this).data("concepto");
        let monto = $(this).data("monto");

        $("#modalTituloGasto").text("Editar Gasto");
        $("#formGasto").attr("action", "/Gastos/Editar");

        $("#idGasto").val(id);
        $("#fecha").val(fecha);
        $("#concepto").val(concepto);
        $("#monto").val(monto);

        $("#modalAgregarGasto").modal("show");
    });

    
});




function cargarGastos(pageNumber = 1) {
    let fechaInicio = $("#fechaInicio").val();
    let fechaFin = $("#fechaFin").val();
    console.time("Carga de gastos");

    $.ajax({
        url: '/Gastos/ListadoPaginado',
        data: { fechaInicio: fechaInicio, fechaFin: fechaFin, pageNumber: pageNumber, pageSize: 10 },
        type: 'GET',
        success: function (data) {
            $("#contenedorListadoGastos").html(data);

            let totalMonto = $("#totalMontoHidden").val(); // Tomamos el valor del campo oculto
            $("#totalMonto").text("$" + parseFloat(totalMonto).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
            console.timeEnd("Carga de gastos"); // Mide el tiempo hasta aquí
        },
        error: function () {
            alert("Error al cargar los datos");
        }
    });
}

$("#btnFiltrar").on("click", function () {
    cargarGastos(1);
});

$(document).on("click", ".pagina", function (e) {
    e.preventDefault();
    let page = $(this).data("page");
    cargarGastos(page);
});

$("#btnLimpiarFiltro").on("click", function () {
    // Limpiar los campos de fecha
    $("#fechaInicio").val("");
    $("#fechaFin").val("");

    // Volver a cargar la primera página sin filtro
    cargarGastos(1);
});

$(function () {
    cargarGastos();
});




