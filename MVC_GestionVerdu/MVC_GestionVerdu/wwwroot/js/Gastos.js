
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
    $(".btnEditarGasto").on("click",function () {
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

    // Enviar formulario con AJAX
    $("#formGasto").on("submit", function (e) {
        e.preventDefault();
        let form = $(this);
        let url = form.attr("action");

        $.ajax({
            url: url,
            type: "POST",
            data: form.serialize(),
            success: function (response) {
                if (response.success) {
                    $("#modalAgregarGasto").modal("hide");
                    Swal.fire({
                        icon: "success",
                        title: "¡Éxito!",
                        text: response.message,
                        timer: 2000,
                        showConfirmButton: false
                    }).then(() => {
                        location.reload();
                    });
                } else {
                    Swal.fire("Error", response.message, "error");
                }
            },
            error: function () {
                Swal.fire("Error", "Ocurrió un error.", "error");
            }
        });
    });
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

