// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('body').off('click', '#btn-test').on('click', '#btn-test', Test);

    function Test() {
        assy1 = $('#txt-assy').val().toUpperCase();
        var assy = new Array();
        var arr = $('[class="assy"]')
        debugger
        for (var i = 0; i < arr.length; i++) {
            assy.push(arr[i].innerHTML)
        }
        if (!assy.includes(assy1)) {
            alert('ok')
        }
        alert(assy.join(','));
    }

    function TestUpoadFile() {
        t = ChangeFileName();
        var elm_file = $('[type="file"]')
        for (var i = 0; i < elm_file.length; i++) {
            var formData = new FormData();
            var type = elm_file[i].name
            formData.append('model.type', type);

            var files = $(`#txt-${type}`).get(0).files;
            for (var j = 0; j < files.length; j++) {
                formData.append('model.files', files[j]);
            }
            debugger
            $.ajax({
                url: '/Registration/Upload',
                type: 'POST',
                data: formData,
                processData: false,  // tell jQuery not to process the data
                contentType: false,  // tell jQuery not to set contentType
                success: function (result) {

                },
                error: function (jqXHR) {
                },
                complete: function (jqXHR, status) {
                }
            })
        }












    }
   
})