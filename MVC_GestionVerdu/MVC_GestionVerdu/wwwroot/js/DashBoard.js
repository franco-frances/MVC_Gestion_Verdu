document.addEventListener("DOMContentLoaded", function () {
    function manejarEnvioRapido(config) {
        const form = document.getElementById(config.formId);

        form.addEventListener("submit", function (e) {
            e.preventDefault();

            if (!$(form).valid()) return;

            const formData = new FormData(form);

            fetch(config.url, {
                method: 'POST',
                body: formData
            })
                .then(res => {
                    if (!res.ok) throw new Error("Error en el servidor.");
                    return res.json();
                })
                .then(data => {
                    if (data.exito) {
                        // Insertar fila
                        const tbody = document.querySelector(config.tbodySelector);
                        const nuevaFila = `
                                <tr>
                                    <td>${data.fecha}</td>
                                    <td>${data.concepto}</td>
                                    <td>${data.montoFormateado}</td>
                                </tr>
                                `;
                        tbody.insertAdjacentHTML("afterbegin", nuevaFila);

                        // Actualizar totales
                        if (config.totalSelector)
                            document.querySelector(config.totalSelector).textContent = data.totalFormateado;
                        if (config.balanceSelector)
                            document.querySelector(config.balanceSelector).textContent = data.balanceHoyFormateado;
                        if (config.totalSelectorCard)
                            document.querySelector(config.totalSelectorCard).textContent = data.totalFormateado





                        // Resetear formulario
                        form.reset();

                        // Mostrar toast
                        const toastEl = document.getElementById('toastConfirmacion');
                        toastEl.querySelector(".toast-body").textContent = config.toastMensaje;
                        new bootstrap.Toast(toastEl).show();
                    } else {
                        alert("Error: " + data.mensaje);
                    }
                })
                .catch(err => {
                    console.error(err);
                    alert("Ocurrió un error al enviar el formulario.");
                });
        });
    }

    // Para ingresos
    manejarEnvioRapido({
        formId: "formIngresoRapido",
        url: "/DashBoard/AgregarVentaRapida",
        tbodySelector: "#ingresosHoy tbody",
        totalSelectorCard: "#totalIngresosHoyCard",
        totalSelector: "#totalIngresosHoy",
        balanceSelector: "#totalHoy",
        toastMensaje: "Ingreso agregado correctamente."
    });

    // Para gastos
    manejarEnvioRapido({
        formId: "formGastoRapido",
        url: "/DashBoard/AgregarGastoRapido",
        tbodySelector: "#gastosHoy tbody",
        totalSelectorCard: "#totalGastosHoyCard",
        totalSelector: "#totalGastosHoy",
        balanceSelector: "#totalHoy",
        toastMensaje: "Gasto agregado correctamente."
    });
});