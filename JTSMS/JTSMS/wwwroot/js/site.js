// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    
    $('#upload').click(function () {
        var files = $('#fileInput').get(0).files;
        var formData = new FormData();
        formData.append('model.file', $("[name='file']")[0].files[0]);
        formData.append('model.reply', 'fff');
        formData.append('model.mid', 1);
        for (var i = 0; i < files.length; i++) {
            formData.append('model.files', files[i]);
        }     

        $.ajax({
            url: '/test/TestUpload',
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
    })


    $('#fileInput').change(function () {
        var files = $('#fileInput').get(0).files;
        
        if (!this.files.length) {
            document.getElementById('list').innerHTML = "no files selected";
        }
        else {
            document.getElementById('list').innerHTML = "";
            for (var i = 0; i < files.length; i++) {
                var dv = document.createElement('div');
                dv.className = 'input-group'

                var p = document.createElement('p');
                p.innerHTML = files[i].name

                dv.appendChild(p);
                document.getElementById('list').appendChild(dv)
            }
            //document.getElementById('fileInput').value = ""; //to clear the count of file after upload
        }
    })

    $(".destroy").click(function () {      
        $(this)[0].parentNode.parentNode.remove();
    });
})