
$(document).ready(function () {
    $('body').off('click', '#btn-search').on('click', '#btn-search', Search);
    $('body').off('click', '#btn-add').on('click', '#btn-add', Add);

    user = document.getElementById('userinfo').getAttribute('data-user');
    name = document.getElementById('userinfo').getAttribute('data-display-name');
    email = document.getElementById('userinfo').getAttribute('data-email');

    function Search() {
        $('#tbl-content').html('');
        var model = new Object();
        model.CustId = $("#txt-customer-search").val() ? parseInt($("#txt-customer-search").val()) : 0;
        model.StationId = $("#txt-station-search").val() ? parseInt($("#txt-station-search").val()) : 0;
        model.PlatformId = $("#txt-platfrom-search").val() ? parseInt($("#txt-platfrom-search").val()) : 0;
        model.TypeId = $("#txt-type-search").val() ? parseInt($("#txt-type-search").val()) : 0;
        model.ScriptId = $('#txt-scriptid-search').val() ? $('#txt-scriptid-search').val() : null;
        Load(model)
    }
    function Load(model) {
        $.ajax({
            type: 'post',
            url: '/Registration/Registration_get',
            //dataType: 'json',
            cache: false,
            data: JSON.stringify(model),
            contentType: 'application/json;charset=uft-8',
            success: function (response) {
                $('#tbl-content').html(response);
            }
        })
    }
    function Add() {
        customer = parseInt($("#txt-customer").val());
        station = parseInt($("#txt-station").val());
        route = $("#txt-route").val();
        type = parseInt($("#txt-type").val());
        platform = parseInt($("#txt-platform").val());
        if (customer && station && route && type && platform) {
            var model = new Object();
            model.CustId = customer;
            model.StationId = station;
            model.RouteStep = route;
            model.TypeId = type;
            model.PlatformId = platform;
            model.Description = $('#txt-description').val() ? $('#txt-description').val() : null;
            model.CreatedBy = user
            model.CreatedName = name
            model.CreatedEmail = email

            $.ajax({
                type: 'post',
                url: '/Registration/Registration_add',
                data: JSON.stringify(model),
                contentType: 'application/json;charset=utf-8',
                success: function (response) {

                    var statusCode = response.results.statusCode;
                    var message = response.results.message;

                    if (statusCode == 200) {
                        bootbox.alert(message, function () { Load(model); });
                    }
                    else {
                        bootbox.alert(message);
                    }
                }
            })
        }
        else {
            bootbox.alert("Please input all required info");
        }
    }
})