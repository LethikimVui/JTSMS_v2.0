$(document).ready(function () {
    function fncOnChangeStation() {
        stationId = $('#txt-station').val();
        $('#txt-route').empty();
        $('#txt-route').append($('<option>', {
            value: "",
            text: "--Please Select--"
        }));
        debugger
        $.ajax({
            type: 'get',
            url: '/request/DescrText',
            data: { step_ID: stationId },
            success: function (response) {
                var data = response[2]
                debugger
                $.each(response, function (index, value) {
                    $('#txt-route').append($('<option>', {
                        value: value,
                        text: value
                    }));
                })
            }
        })
    }

})