$(document).ready(function () {
    $('.modal').modal();
    $('.datepicker').datepicker();
    $('select').formSelect();
    $('.slider').slider();

    $("#Filtrar").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#filtro tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});
