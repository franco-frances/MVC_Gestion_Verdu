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
