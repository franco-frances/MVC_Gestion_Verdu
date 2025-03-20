
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

    $(".btnEditarIngreso").on("click", function () {

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


function filtrarIngresos() {
    let metodoPagoSeleccionado = document.getElementById("filtroMetodoPago").value.toLowerCase();
    let fechaInicio = document.getElementById("fechaInicio").value;
    let fechaFin = document.getElementById("fechaFin").value;
    let filas = document.querySelectorAll(".ingreso-row");
    let totalIngresos = 0;

    filas.forEach(fila => {
        let metodoPago = fila.querySelector(".metodo-pago").textContent.toLowerCase();
        let fecha = fila.querySelector(".fecha").textContent;
        let monto = parseFloat(fila.querySelector(".monto").textContent.replace("$", "").replace(/,/g, ""));

       

        let coincideMetodo = metodoPagoSeleccionado === "" || metodoPago.includes(metodoPagoSeleccionado);
        let coincideFecha = (!fechaInicio || fecha >= fechaInicio) && (!fechaFin || fecha <= fechaFin);

        if (coincideMetodo && coincideFecha) {
            fila.style.display = "";
            totalIngresos += monto;
        } else {
            fila.style.display = "none";
        }
    });

    // Mostrar total de ingresos filtrados correctamente
    document.getElementById("totalIngresos").textContent = "$" + totalIngresos.toLocaleString("es-AR", { minimumFractionDigits: 2 });
}
