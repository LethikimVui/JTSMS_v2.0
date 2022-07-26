$(document).ready(function () {
    $('body').off('click', '#btn-search').on('click', '#btn-search', Search);

    $('body').off('click', '#btn-add').on('click', '#btn-add', Add);
    $('body').off('click', '#btn-delete').on('click', '#btn-delete', Delete);
    $('body').off('click', '#btn-search-input').on('click', '#btn-search-input', SearchName);
    $('body').off('click', '#btn-edit').on('click', '#btn-edit', Edit);
    $('body').off('click', '#btn-save').on('click', '#btn-save', Save);


    user = document.getElementById('userinfo').getAttribute('data-user');
    name = document.getElementById('userinfo').getAttribute('data-display-name');
    email = document.getElementById('userinfo').getAttribute('data-email');

    function Search() {
        $('#tbl-content').html('');
        var custId = parseInt($("#txt-customer-search").val());
        if (custId) {


            var model = new Object();

            model.CustId = custId;
            model.RoleId = parseInt($("#txt-role-search").val());
            model.Ntlogin = $('#txt-ntlogin-search').val() ? $('#txt-ntlogin-search').val() : null;
          
            Load(model)
        }
        else
            bootbox.alert('Please select Customer');
    }
    function Load(model) {
        $.ajax({
            type: 'post',
            url: '/Admin/User_Get',
            //dataType: 'json',
            data: JSON.stringify(model),
            contentType: 'application/json;charset=uft-8',
            success: function (response) {
              
                $('#tbl-content').html(response);
            }
        })
    }
    function SearchName() {
        _ntid = $('#txt-search-input').val();
        
        $.ajax({
            type: 'post',
            url: '/admin/GetDisplayNameFromSamAccountName',
            data: { samAccountName: _ntid },
            success: function (response) {
              
                if (response) {
                    $('#txt-userName').text(response);
                    $('#txt-Ntlogin').val(_ntid);
                    $('#modal-search').modal('hide');
                }
                else {
                    bootbox.alert("User is not found");
                }
            }
        })

    }

    function Add() {
        var model = new Object();
        model.Ntlogin = $('#txt-Ntlogin').val();
        model.RoleId = parseInt(document.getElementById("txt-roleId").value);
        model.PlantId = 1 // parseInt(document.getElementById("txt-wc").value);
        model.CustId = parseInt(document.getElementById("txt-custId").value);
        model.CreatedBy = user;
        model.CreatedName = name;
        model.CreatedEmail = email;
       
        $.ajax({
            type: 'post',
            url: '/admin/Access_UserRole_insert',
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var data = response.results

                if (data.statusCode == 200) {
                    bootbox.alert(data.message, function () { Load(model); })
                }
                else if (data.statusCode == 400) {
                    bootbox.alert(data.message)
                }
                else {
                    bootbox.alert("Update Error!")
                }
            }
        })
    }

    function Edit() {
        $(".error").html('');
        $(".error").removeClass("error");
        var Id = parseInt($(this).data('id'));
      
        $.ajax({
            type: 'post',
            url: '/admin/Access_UserRole_Get_By_Id',
            dataType: 'json',
            data: { Id: Id },
            success: function (response) {
                var data = response.results[0]              

                $('#txt-ntlogin').text(data.ntlogin);
                $('#txt-name').text(data.ntlogin);
                $('#txt-roleid').val(data.roleId);
                $('#txt-custid').val(data.custId);
                //$('#txt-remark').val(data.remark);
                $('#roleId').val(Id);
            }
        })
    }
    function Save() {
        var model = new Object();
        model.UserRoleId = parseInt($('#roleId').val());
        model.PlantId = 1
        model.CustId = parseInt($('#txt-custid').val());
        model.RoleId = parseInt($('#txt-roleid').val());
        model.UpdatedBy = user;
        model.UpdatedName = name;
        model.UpdatedEmail = email;
      
        $.ajax({
            type: 'post',
            url: '/admin/Access_UserRole_update',
            data: JSON.stringify(model),
            dataType: 'json',
            contentType: 'application/json,; charset=utf-8',
            success: function (response) {
                var data = response.results;
               
                if (data.statusCode == 200) {
                    bootbox.alert("Save Successfully!", function () { { Load(model); } })
                }
                else
                    bootbox.alert("Save Failed!")
            }
        })

    }

    function Delete() {
        userRoleId = $(this).attr('data-id');      
        ntlogin = $(this).attr('data-ntlogin');
        var model = new Object();
        model.UserRoleId = parseInt(userRoleId);
        model.UpdatedBy = user;
        model.UpdatedName = name;
        model.UpdatedEmail = email;
        $.ajax({
            type: 'post',
            url: '/admin/Access_UserRole_delete',
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var data = response.results;
                if (data.statusCode == 200) {
                    bootbox.alert(`${ntlogin} is deleted`, function () {
                        model.CustId = parseInt($('#txt-customer-search').val());                     
                        { Load(model); };
                    })
                }
                else {
                    bootbox.alert("Error");
                }

            }
        })
    }

})