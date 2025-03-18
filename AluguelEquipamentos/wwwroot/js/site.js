$(document).ready(function () {
    function initDataTable(selector) {
        $(selector).DataTable({
            language: {
                "decimal": "",
                "emptyTable": "No data available in table",
                "info": "Mostrando _START_ Registro de _END_ em um total de _TOTAL_ entradas",
                "infoEmpty": "Showing 0 to 0 of 0 entries",
                "infoFiltered": "(filtered from _MAX_ total entries)",
                "infoPostFix": "",
                "thousands": ",",
                "lengthMenu": "Mostrar _MENU_ Entradas",
                "loadingRecords": "Loading...",
                "processing": "",
                "search": "Pesquisar:",
                "zeroRecords": "No matching records found",
                "paginate": {
                    "first": "Primeiro",
                    "last": "Último",
                    "next": "Próximo",
                    "previous": "Anterior"
                },
                "aria": {
                    "orderable": "Order by this column",
                    "orderableReverse": "Reverse order this column"
                }
            }
        });
    }

    initDataTable('#Aluguel');
    initDataTable('#Endereco');

    setTimeout(function () {
        $(".alert").fadeOut("slow", function () {
            $(this).alert('close');
        });
    }, 5000);
});
