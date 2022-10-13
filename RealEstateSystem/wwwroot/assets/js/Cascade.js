$(document).ready(function () {
    GetGovernorate();
    $('#City').attr('readonly', true);
    $('#District').attr('readonly', true);

    $('#Governorate').change(function () {
        $('#City').attr('readonly', false);
        var id = $(this).val();
        $('#City').empty();
        $('#City').append('<option> Select City  </option > ');

        $.ajax({
            url: '/Properties/City?id='+id,
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#City').append('<option value=' + data.id + '>' + data.name + '</option> ');

                });
            }
        });
    });
});

$('#City').change(function () {
    $('#District').attr('readonly', false);
    var id = $(this).val();
    $('#District').empty();
    $('#District').append('<option> Select District </option> ');

    $.ajax({
        url: '/Properties/District?id=' + id,
        success: function (result) {
            $.each(result, function (i, data) {
                $('#District').append('<option value=' + data.id + '>' + data.name + '</option> ');

            });
        }
    });
});

function GetGovernorate() {
    $.ajax({
        url: '/Properties/Governorate',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#Governorate').append('<Option value=' + data.id + '>' + data.name + '</Option > ');

            });
        }
    });
}