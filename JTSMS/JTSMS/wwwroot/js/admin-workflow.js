$(document).ready(function () {
    $('body').off('click', '#btn-search').on('click', '#btn-search', Search);

   
    user = document.getElementById('userinfo').getAttribute('data-user');
    name = document.getElementById('userinfo').getAttribute('data-display-name');
    email = document.getElementById('userinfo').getAttribute('data-email');

    function Search() {
        $('#tbl-content').html('');
        var model = new Object();

        model.CustId = parseInt($("#txt-customer-search").val());
        model.RoleId = parseInt($("#txt-role-search").val());       
        model.Ntlogin = $('#txt-ntlogin-search').val() ? $('#txt-ntlogin-search').val() : null;
        
        Load(model)
        
    }
    function Load(model) {
        $.ajax({
            type: 'post',
            url: '/Admin/User_Get',
            data: JSON.stringify(model),
            contentType: 'application/json;charset=uft-8',
            success: function (response) {
               
                $('#tbl-content').html(response);
            }
        })
    }
    

})