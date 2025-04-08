$(function () {
    $("#btnAgregarProducto").on("click", function () {
        //Abre modal para Agregar Producto
        $("#modalProductoLabel").text("Agregar Producto");
        $("#formProducto").attr("action", "/Dashboard/AgregarProducto");

        //limpia modal en caso de haber sido usado para editar
        $("#Id").val("");
        $("#Descripcion").val("");
        $("#CategoriaId").val("");
        $("#PrecioCajon").val("");
        $("#PesoCajon").val("");
        $("#MargenGanancia").val("");
        $("#PrecioCosto").val("");
        $("#PrecioFinal").val("");

       
        $("#modalProducto").modal("show");

    });


    //Abre modal para editar

    $(".btnEditarProducto").on("click", function () {

        let id = $(this).data("id");
        let descripcion = $(this).data("descripcion");
        let categoriaid = $(this).data("categoriaid");
        let precio = $(this).data("precio");
        let peso = $(this).data("peso");
        let margen = $(this).data("margen");
        let preciocosto = $(this).data("preciocosto");
        let preciofinal = $(this).data("preciofinal");




        //Abre modal para Editar ingreso
        $("#modalProductoLabel").text("Editar Producto");
        $("#formProducto").attr("action", "/Dashboard/Editar");

        // Llenar los campos con los datos del ingreso a editar
        $("#IdProducto").val(id);
        $("#Descripcion").val(descripcion);
        $("#CategoriaId").val(categoriaid);
        $("#PrecioCajon").val(precio);
        $("#PesoCajon").val(peso);
        $("#MargenGanancia").val(margen);
        $("#PrecioCosto").val(preciocosto);
        $("#PrecioFinal").val(preciofinal);


      
        // Mostrar el modal
        $("#modalProducto").modal("show");


    });


 


});