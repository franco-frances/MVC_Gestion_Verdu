document.addEventListener('DOMContentLoaded', function () {
    // Comprueba si estamos en la vista de agregar producto
    if (document.getElementById('PrecioCajon')) {
        const precioCajonInput = document.getElementById('PrecioCajon');
        const pesoCajonInput = document.getElementById('PesoCajon');
        const margenGananciaInput = document.getElementById('MargenGanancia');
        const precioCostoInput = document.getElementById('PrecioCosto');
        const precioFinalInput = document.getElementById('PrecioFinal');

        // Calcula los precios cuando se cambia el Precio del Cajón o el Margen de Ganancia
        function calcularPrecios() {
            const precioCajon = parseFloat(precioCajonInput.value) || 0;
            const pesoCajon = parseFloat(pesoCajonInput.value) || 0; // Asegúrate de obtener el valor del input
            const margenGanancia = parseFloat(margenGananciaInput.value) || 0;

            // Calcular Precio Costo por Kg
            const precioCostoPorKg = pesoCajon > 0 ? (precioCajon / pesoCajon) : 0;
            precioCostoInput.value = precioCostoPorKg.toFixed(2); // Establecer el precio costo en el input

            // Calcular Precio Final
            const precioFinal = precioCostoPorKg * (1 + (margenGanancia / 100));
            precioFinalInput.value = precioFinal.toFixed(2);
        }

        // Escuchar cambios en los campos
        precioCajonInput.addEventListener('input', calcularPrecios);
        pesoCajonInput.addEventListener('input', calcularPrecios); // Agregar listener para peso del cajón
        margenGananciaInput.addEventListener('input', calcularPrecios);
    }
});


function confirmarEliminacion(id, url) {
    Swal.fire({
        title: "¿Estás seguro?",
        text: "Esta acción no se puede deshacer",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "POST",
                data: { id: id },
                success: function (response) {
                    if (response.success) {
                        Swal.fire({
                            icon: "success",
                            title: "Eliminado",
                            text: response.message,
                            timer: 2000,
                            showConfirmButton: false
                        });

                        // Eliminar la fila del gasto eliminado sin recargar la página
                        $("#fila-" + id).fadeOut(500, function () {
                            $(this).remove();
                        });
                    } else {
                        Swal.fire("Error", response.message, "error");
                    }
                },
                error: function () {
                    Swal.fire("Error", "Ocurrió un error al eliminar.", "error");
                }
            });
        }
    });
}

function filtrarProductos() {
    let buscador = document.getElementById("buscador").value.toLowerCase();
    let categoriaSeleccionada = document.getElementById("filtroCategoria").value;
    let filas = document.querySelectorAll(".producto-row");

    filas.forEach(fila => {
        let nombre = fila.querySelector(".nombre").textContent.toLowerCase();
        let categoria = fila.getAttribute("data-categoria");

        let coincideNombre = nombre.includes(buscador);
        let coincideCategoria = categoriaSeleccionada === "" || categoria === categoriaSeleccionada;

        if (coincideNombre && coincideCategoria) {
            fila.style.display = "";
        } else {
            fila.style.display = "none";
        }
    });
}
