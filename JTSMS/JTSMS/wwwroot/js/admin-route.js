$(document).ready(function () {

    $('body').off('click', '#btn-search').on('click', '#btn-search', Search);
    $('body').off('click', '#btn-save').on('click', '#btn-save', Save);
    $('body').off('click', '#btn-add').on('click', '#btn-add', Add);

    user = document.getElementById('userinfo').getAttribute('data-user');
    name = document.getElementById('userinfo').getAttribute('data-display-name');
    email = document.getElementById('userinfo').getAttribute('data-email');

  
    function Search() {
        $('#tbl-content').html('');
        type = $("#txt-type-search").val();
        if (type) {
            Load(type)
        }
        else
            bootbox.alert("Please select Request Type");

    }
    function Load(_type) {

        $.ajax({
            type: 'get',
            url: '/Admin/Route_get',
            data: { id: parseInt(_type) },
            contentType: 'application/json;charset=uft-8',
            success: function (response) {
              
                $('#tbl-content').html(response);
            }
        })
    }
    $('body').on('change', '.input', Change)
    function Change() {
        $(this).css('background-color', 'red');
        //sequence = $(this).val()
        //alert(sequence);
        //var routeid = $(this).attr('data-routeid')
        //alert(routeid);

    }
    function Save() {
        var checkbox = $('input[type="checkbox"]')
        var type = $('#txt-type-search').val()
        for (var i = 0; i < checkbox.length; i++) {
            var checked = checkbox[i].checked
            var routeid = checkbox[i].attributes['data-routeid'].value
            var sequence = i + 1;
            var status = true
            status = Update(type, routeid, sequence, checked)          
        }
        if (status) {
            bootbox.alert('Saved successfully', function () { Load(type) })
        }
        else {
            bootbox.alert('not ok')
        }
    }
    function Update(_typeid, _routeid, _sequence, _isactive) {
        var model = new Object();
        model.TypeId = parseInt(_typeid);
        model.RouteId = parseInt(_routeid);
        model.Sequence = parseInt(_sequence);
        model.IsActive = _isactive ? 1 : 0;
        model.UpdatedBy = user;
        model.UpdatedName = name;
        model.UpdatedEmail = email;
        var result = true
        $.ajax({
            type: 'post',
            url: '/Admin/WorkFlow_Route_update',
            data: JSON.stringify(model),
            dataType: 'json',
            contentType: "application/json;charset-utf-8",
            success: function (response) {
                data = response.results
               
                if (data.statusCode != 200) {
                    result = false
                }
            }
        })
        return result;
    }

    function Add() {
        var checkbox = $('input[type="checkbox"]')
        var type = $('#txt-type-search').val()
        debugger
        for (var i = 0; i < checkbox.length; i++) {
            var checked = checkbox[i].checked

            var routeid = checkbox[i].attributes['data-routeid'].value
           
            var sequence = i+ 1 // $('input[type="number"]')
            var status = true
            status = A(type, routeid, sequence, checked)
            //for (var j = 0; j < sequence.length; j++) {
            //    if (sequence[j].attributes['data-routeid'].value == routeid) {
            //        value = sequence[j].value
            //        console.log(routeid, value);

            //        status = A(type, routeid, value, checked)

            //    }
            //}
        }
        if (status) {
            bootbox.alert('ok', function () { Load(type) })
        }
        else {
            bootbox.alert('not ok')
        }
    }
    function A(_typeid, _routeid, _sequence, _isactive) {
        var model = new Object();
        model.TypeId = parseInt(_typeid);
        model.RouteId = parseInt(_routeid);
        model.Sequence = parseInt(_sequence);
        model.IsActive = _isactive ? 1 : 0;
        model.UpdatedBy = user;
        model.UpdatedName = name;
        model.UpdatedEmail = email;
        var result = true
        $.ajax({
            type: 'post',
            url: '/Admin/WorkFlow_Route_add',
            data: JSON.stringify(model),
            dataType: 'json',
            contentType: "application/json;charset-utf-8",
            success: function (response) {
                data = response.results
              
                if (data.statusCode != 200) {
                    result = false
                }
            }
        })
        return result;
    }

    $('body').on('click', '.down', function () {

        
        var thisRow = $(this).closest('tr');
        activeClass = document.getElementsByClassName("active");
        for (var i = 1; i < activeClass.length; i++) {
            activeClass[i].className = activeClass[i].className.replace("active", "");
        }
        this.parentElement.parentElement.classList.add("active")
        var nextRow = thisRow.next();
        if (nextRow.length) {
            nextRow.after(thisRow);
        }
        else
            bootbox.alert("Cannot move further");
    });
    $('body').on('click', '.up', function () {
        var thisRow = $(this).closest('tr');
        activeClass = document.getElementsByClassName("active");
        for (var i = 1; i < activeClass.length; i++) {
            activeClass[i].className = activeClass[i].className.replace("active", "");
        }
        this.parentElement.parentElement.classList.add("active")
        var prevRow = thisRow.prev();
     
        isFirstRow = prevRow[0].classList.contains('first-row')
        if (prevRow.length && !isFirstRow) {
            prevRow.before(thisRow);
        }
        else
            bootbox.alert("Cannot move further");
    });
})