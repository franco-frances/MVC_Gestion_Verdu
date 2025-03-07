function filtrarIngresos() {
    let metodoPagoSeleccionado = document.getElementById("filtroMetodoPago").value.toLowerCase();
    let fechaInicio = document.getElementById("fechaInicio").value;
    let fechaFin = document.getElementById("fechaFin").value;
    let filas = document.querySelectorAll(".producto-row");
    let totalIngresos = 0;

    filas.forEach(fila => {
        let metodoPago = fila.querySelector(".metodo-pago").textContent.toLowerCase();
        let fecha = fila.querySelector(".fecha").textContent;
        let monto = parseFloat(fila.querySelector(".monto").textContent.replace("$", "").replace(/,/g, ""));

        let fechaFormateada = fecha.split("/").reverse().join("-"); // Convierte dd/MM/yyyy a yyyy-MM-dd

        let coincideMetodo = metodoPagoSeleccionado === "" || metodoPago.includes(metodoPagoSeleccionado);
        let coincideFecha = (!fechaInicio || fechaFormateada >= fechaInicio) && (!fechaFin || fechaFormateada <= fechaFin);

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
