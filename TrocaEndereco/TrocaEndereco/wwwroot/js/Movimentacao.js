

    $('#chooseFile').bind('change', function () {
        var filename = $("#chooseFile").val();
    if (/^\s*$/.test(filename)) {
        $(".file-upload").removeClass('active');
    $("#noFile").text("No file chosen...");
        }
    else {
        $(".file-upload").addClass('active');
    $("#noFile").text(filename.replace("C:\\fakepath\\", ""));
        }
    });

function BaixarModelo() {
    window.open("/Movimentacao/ModeloExcel");

}



$('#excel-upload').on('change', function () {
    var formData = new FormData();
    for (var i = 0; i < this.files.length; i++) {
        formData.append('file', this.files[i]);

        $.ajax({
            url: '/Movimentacao/ImportarExcel',
            type: 'post',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function () {
                Listar();
            }
        })

    }
});


function Listar() {
    debugger
    $.ajax({
        url: '/Movimentacao/Listar',
        method: "GET",
        success: function (aaData) {
            debugger
            for (var i = 0; aaData.length > i; i++) {
                $('#corpo').append('<tr><td>' + aaData[i].enderecoOrigem + '</td><td>'
                    + aaData[i].produto + '</td><td>' +
                    '</td><td class="actions col-xs-2 col-sm-2 col-md-2 col-lg-2" align="right">' +
                    '<button class="btn btn-primary btn-sm" type="button" id="' +
                    aaData[i].Id + '" onclick="Alterar(id)" >Alterar</button>' +
                    '<td/><td>' +
                    '<button class="btn btn-primary btn-sm" type="button" id="' +
                    aaData[i].Id + '" onclick="Deletar(id)" >Excluir</button>');
            }
        }
    });
}

       