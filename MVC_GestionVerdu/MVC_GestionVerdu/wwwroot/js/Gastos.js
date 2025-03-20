
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
    $(".btnEditarGasto").on("click", function () {
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






function filtrarGastos() {
    let fechaInicio = document.getElementById("fechaInicio").value;
    let fechaFin = document.getElementById("fechaFin").value;
    let filas = document.querySelectorAll(".gasto-row");
    let totalGastos = 0;

    filas.forEach(fila => {
        let fecha = fila.querySelector(".fecha").textContent;
        let montoTexto = fila.querySelector(".monto").textContent.replace("$", "").replace(",", ""); // Remove the dollar sign and comma (thousand separator)
        let montoNumerico = parseFloat(montoTexto); // Convert to number directly

        let fechaFormateada = fecha.split("/").reverse().join("-"); // Converts dd/MM/yyyy to yyyy-MM-dd

        let coincideFecha = (!fechaInicio || fechaFormateada >= fechaInicio) && (!fechaFin || fechaFormateada <= fechaFin);

        if (coincideFecha) {
            fila.style.display = "";
            totalGastos += montoNumerico;
        } else {
            fila.style.display = "none";
        }
    });

    // Mostrar total de gastos filtrados
    document.getElementById("totalGastos").textContent = "$" + totalGastos.toLocaleString("es-AR", { minimumFractionDigits: 2 });
}



//function confirmarEliminacionGasto(id, url) {
//    Swal.fire({
//        title: "¿Estás seguro?",
//        text: "Esta acción no se puede deshacer",
//        icon: "warning",
//        showCancelButton: true,
//        confirmButtonColor: "#d33",
//        cancelButtonColor: "#3085d6",
//        confirmButtonText: "Sí, eliminar",
//        cancelButtonText: "Cancelar"
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                url: url,
//                type: "POST",
//                data: { id: id },
//                success: function (response) {
//                    if (response.success) {
//                        Swal.fire({
//                            icon: "success",
//                            title: "Eliminado",
//                            text: response.message,
//                            timer: 2000,
//                            showConfirmButton: false
//                        });

//                        // Eliminar la fila del gasto eliminado sin recargar la página
//                        $("#fila-" + id).fadeOut(500, function () {
//                            $(this).remove();
//                        });
//                    } else {
//                        Swal.fire("Error", response.message, "error");
//                    }
//                },
//                error: function () {
//                    Swal.fire("Error", "Ocurrió un error al eliminar.", "error");
//                }
//            });
//        }
//    });
//}


