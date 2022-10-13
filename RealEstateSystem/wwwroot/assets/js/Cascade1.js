.$(document).ready(function () {
    GetCity();
    $('#District').attr('readonly', true);

    $('#City').change(function () {
        $('#District').attr('readonly', false);
        var id = $(this).val();
        $('#District').empty();
        $('#District').append('<option> Select District  </option > ');

        $.ajax({
            url: '/Properties/District?id='+id,
            success: function (result) {
                $.each(result, function (i, data) {
                    $('#District').append('<option value=' + data.id + '>' + data.name + '</option> ');

                });
            }
        });
    });
});

function GetCity() {
    $.ajax({
        url: '/Properties/City1',
        success: function (result) {
            $.each(result, function (i, data) {
                $('#City').append('<Option value=' + data.id + '>' + data.name + '</Option > ');

            });
        }
    });
}