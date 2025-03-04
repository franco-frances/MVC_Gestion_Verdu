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
            // Crear formulario dinámico para enviar el POST
            let form = document.createElement("form");
            form.method = "POST";
            form.action = url; // Usar la URL pasada como argumento

            // Crear input oculto para enviar el ID
            let input = document.createElement("input");
            input.type = "hidden";
            input.name = "id";
            input.value = id;

            // Agregar input al formulario y enviarlo
            form.appendChild(input);
            document.body.appendChild(form);
            form.submit();
        }
    });
}