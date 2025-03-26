
$(function () {
    $("#btnAgregarIngreso").on("click", function () {
        //Abre modal para Agregar ingreso
        $("#modalIngresoLabel").text("Agregar Ingreso");
        $("#formIngreso").attr("action", "/DetallesVenta/AgregarVenta");

        //limpia modal en caso de haber sido usado para editar
        $("#IdDetalleVenta").val("");
        $("#Fecha").val(new Date().toLocaleDateString('sv-SE'));
        $("#Concepto").val("");
        $("#Monto").val("");

        $("#modalIngreso").modal("show");

    });

    //Abre modal para editar

    $(document).on('click', '.btnEditarIngreso', function() {

        let id = $(this).data("id");
        let fecha = $(this).data("fecha");
        let metodoPago = $(this).data("metodopagoid");
        let concepto = $(this).data("concepto");
        let monto = $(this).data("monto")


        //Abre modal para Editar ingreso
        $("#modalIngresoLabel").text("Editar Ingreso");
        $("#formIngreso").attr("action", "/DetallesVenta/Editar");

        // Llenar los campos con los datos del ingreso a editar
        $("#IdDetalleVenta").val(id);
        $("#Fecha").val(fecha);
        $("#MetodoPagoId").val(metodoPago);
        $("#Concepto").val(concepto);
        $("#Monto").val(monto);

        // Mostrar el modal
        $("#modalIngreso").modal("show");



    });

    //// Mostrar SweetAlert después de la recarga si hay un mensaje en TempData
    //var mensaje = $("#tempMensaje").val();
    //var tipoMensaje = $("#tempTipoMensaje").val();

    //if (mensaje && mensaje.trim() !== "") {
    //    Swal.fire({
    //        icon: tipoMensaje || "info", // Si no hay tipo de mensaje, usa "info" por defecto
    //        title: "¡Éxito!",
    //        text: mensaje,
    //        timer: 2000,
    //        showConfirmButton: false
    //    });
    //}


});


//function filtrarIngresos() {
//    let metodoPagoSeleccionado = document.getElementById("filtroMetodoPago").value.toLowerCase();
//    let fechaInicio = document.getElementById("fechaInicio").value;
//    let fechaFin = document.getElementById("fechaFin").value;
//    let filas = document.querySelectorAll(".ingreso-row");
//    let totalIngresos = 0;

//    filas.forEach(fila => {
//        let metodoPago = fila.querySelector(".metodo-pago").textContent.toLowerCase();
//        let fecha = fila.querySelector(".fecha").textContent;
//        let monto = parseFloat(fila.querySelector(".monto").textContent.replace("$", "").replace(/,/g, ""));



//        let coincideMetodo = metodoPagoSeleccionado === "" || metodoPago.includes(metodoPagoSeleccionado);
//        let coincideFecha = (!fechaInicio || fecha >= fechaInicio) && (!fechaFin || fecha <= fechaFin);

//        if (coincideMetodo && coincideFecha) {
//            fila.style.display = "";
//            totalIngresos += monto;
//        } else {
//            fila.style.display = "none";
//        }
//    });

//    // Mostrar total de ingresos filtrados correctamente
//    document.getElementById("totalIngresos").textContent = "$" + totalIngresos.toLocaleString("es-AR", { minimumFractionDigits: 2 });
//}


function cargarIngresos(pageNumber = 1) {
    let metodoPago = $("#filtroMetodoPago").val();
    let fechaInicio = $("#fechaInicio").val();
    let fechaFin = $("#fechaFin").val();

    console.time("Carga de ingresos");

    $.ajax({
        url: '/DetallesVenta/ListadoPaginado',
        data: { metodoPago: metodoPago, fechaInicio: fechaInicio, fechaFin: fechaFin, pageNumber: pageNumber, pageSize: 10 },
        type: 'GET',
        success: function (data) {
            $("#contenedorListadoVentas").html(data);

            let totalMonto = $("#totalMontoHidden").val();
            $("#totalIngresos").text("$" + parseFloat(totalMonto).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));

            console.timeEnd("Carga de ingresos");
        },
        error: function () {
            alert("Error al cargar los ingresos.");
        }
    });
}

// Filtrar ingresos cuando se haga clic en el botón "Filtrar"
$("#btnFiltrar").on("click", function () {
    cargarIngresos(1);
});

// Manejar la paginación al hacer clic en los enlaces de página
$(document).on("click", ".pagina", function (e) {
    e.preventDefault();
    let page = $(this).data("page");
    cargarIngresos(page);
});

// Limpiar filtros y recargar ingresos sin filtros
$("#btnLimpiarFiltro").on("click", function () {
    $("#filtroMetodoPago").val("");
    $("#fechaInicio").val("");
    $("#fechaFin").val("");
    cargarIngresos(1);
});

// Cargar ingresos al cargar la página
$(function () {
    cargarIngresos();
});
