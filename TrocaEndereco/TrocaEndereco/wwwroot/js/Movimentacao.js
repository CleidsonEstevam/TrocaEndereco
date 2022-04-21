

$('#chooseFile').bind('change', function () {
    debugger
        var filename = $("#chooseFile").val();
    if (/^\s*$/.test(filename)) {
        $(".file-upload").removeClass('active');
        $("#noFile").text(filename);
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
    debugger
    var formData = new FormData();
    for (var i = 0; i < this.files.length; i++) {
        formData.append('file', this.files[i]);
        $('#loader').removeClass('hidden')
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
            $('#loader').addClass('hidden')
            for (var i = 0; aaData.length > i; i++) {
                $('#corpo').append('<tr><td>' + aaData[i].enderecoOrigem + '</td><td>'
                    + aaData[i].produto + '</td><td>' + aaData[i].quantidade + '</td>'
                    + '</td><td>' + aaData[i].enderecoDestino + '</td><td>' + aaData[i].msgRetorno + '</td>');
            }
        }
    });
}

       