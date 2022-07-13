$(document).ready(function () {
    //$('body').off('click', '#btn-search-input').on('click', '#btn-search-input', Search_Number);
    //$('body').off('click', '#btn-select').on('click', '#btn-select', Select);
    $('body').off('click', '#btn-add').on('click', '#btn-add', onAddAssy);
    $('body').off('click', '#btn-submit').on('click', '#btn-submit', Submit);
    


    user = document.getElementById('userinfo').getAttribute('data-user');
    name = document.getElementById('userinfo').getAttribute('data-display-name');
    email = document.getElementById('userinfo').getAttribute('data-email');

    const tableElm = document.querySelector("table")
    tableElm.addEventListener("click", onDelelteAssy)

    function Search_Number() {
        $('#tbl-result').html('');
        _number = $('#txt-search-input').val();
        $.ajax({
            type: 'post',
            url: '/request/Search_Number',
            data: { number: _number },
            success: function (response) {
                if (response) {
                    $('#tbl-result').html(response);
                }
                else {
                    bootbox.alert("Number is not found");
                }
            }
        })
    }
    function Select() {
        _number = $(this).attr('data-number');
        _rev = $(this).attr('data-rev');
        debugger

        let tbody = document.getElementById('tbody')
        table = tbody.innerHTML;
        let newRow = `<tr><td>${_number}</td><td>${_rev}</td><td><button class="destroy">Delete</button> </td></tr>`
        table += newRow
        tbody.innerHTML = table;

        //var tr = document.createElement('tr');

        //var td_number = document.createElement('td');
        //td_number.innerHTML = _number;
        //tr.appendChild(td_number);

        //var td_rev = document.createElement('td');
        //td_rev.innerHTML = _rev;
        //tr.appendChild(td_rev);

        //let td_btn = document.createElement('td');
        //btn = document.createElement('button');
        //btn.className = 'destroy'
        //btn.innerHTML = 'Delete'
        //td_btn.appendChild(btn)
        //tr.appendChild(td_btn)  

        //document.getElementById('tbody').appendChild(tr);

        $('#tbl-result').html('');
        $('#modal-search').modal('hide');
    }


    GetAssyList = function() {
        var arr_assy = new Array();
        var arr = $('[class="assy"]')
        debugger
        for (var i = 0; i < arr.length; i++) {
            arr_assy.push(arr[i].innerHTML)
        }
        return arr_assy;
    }
    function onAddAssy() {
        input = $('#txt-assy').val().toUpperCase();
        if (input) {
            var arr_assy = GetAssyList();
            if (!arr_assy.includes(input)) {
                AddElement(input)
            }
            else {
                alert(`${input} already in the list`)
            }           
        }
        else bootbox.alert('Assembly Number is required')
    }   
    function AddElement(input) {
        $.ajax({
            url: '/Registration/CheckAssy',
            type: 'post',
            data: { assy: input },
            success: function (response) {
                debugger
                if (response.results) {
                    let tbody = document.getElementById('tbody')
                    table = tbody.innerHTML;
                    let newRow = `<tr>
                                        <td class="assy">${input}</td>
                                        <td><button class="destroy">Delete</button> </td>
                                       </tr>`
                    table += newRow
                    tbody.innerHTML = table;
                    debugger
                    $('#txt-assy').val('');
                }
                else {
                    bootbox.alert('Not able to add assy');
                }
            }
        })
    }
    function onDelelteAssy(e) {
        if (!e.target.classList.contains("destroy")) {
            return;
        }
        const btn = e.target;
        btn.closest("tr").remove();
    }
    $('#frm-submit').validate({
        rules: {
            scriptname: { required: true, },
            scriptrev: { required: true, },
            description: { required: true, },
            original: { required: true, },
            encrypted: { required: true, },
            PCNorDevNumber: { required: true, },
            changeDetail: { required: true, },
            assy: { required: true, },
        }
    })

    function Submit() {
        if ($('#frm-submit').valid()) {
            var getDate = new Date();
            var date = getDate.getFullYear().toString() + (getDate.getMonth() + 1) + getDate.getDate() + getDate.getHours() + getDate.getMinutes() + getDate.getSeconds() + getDate.getMilliseconds();
            var t = GetAssyList()
            debugger

            var model = new Object();
            model.RegId = parseInt($(this).attr('data-regid'));
            model.ScriptName = $("#txt-scriptname").val();
            model.ScriptRev = $('#txt-scriptrev').val();
            model.PCNorDevNumber = $('#txt-PCNorDevNumber').val();
            model.ChangeDetail = $('#txt-changeDetail').val();
            model.AssemblyNumber = t.join('|');
            model.Description = $('#txt-description').val();
            model.CreatedBy = user
            model.CreatedName = name
            model.CreatedEmail = email
            model.OriginalFileName = ChangeFileName($('#txt-original')[0].files[0], date)
            model.EncryptedFileName = ChangeFileName($('#txt-encrypted')[0].files[0], date)
            //model.File = $('#txt-encrypted')[0].files[0] ///can add to the JS model but cannot transfer this to controller
            model.FileHash = $('#txt-encrypted').attr('data-hash').toUpperCase();
            $.ajax({
                url: '/Registration/Registration_submit',
                type: 'post',
                data: JSON.stringify(model),
                dataType: 'json',
                contentType: 'application/json;charset=uft-8',
                success: function (response) {
                    results = response.results
                    debugger
                    if (results.statusCode == 200) {
                        var elm_file = $('[type="file"]')
                        for (var i = 0; i < elm_file.length; i++) {
                            var type = elm_file[i].name
                            files = $(`#txt-${type}`)[0].files
                            Upload(files, type, date)
                        }
                        bootbox.alert(results.message, function () { location.reload() })
                    }
                    else bootbox.alert(results.message)
                }
            })
        }

    }

    function Upload(_files, _type, _date) {
        //var files = $('#txt-encrypted').get(0).files;
        var formData = new FormData();
        for (var i = 0; i < _files.length; i++) {
            formData.append('model.files', _files[i]);
        }
        formData.append('model.type', _type);
        formData.append('model.date', _date);
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
    function ChangeFileName(_file, _date) {
        var name = "";

        var splitname = _file.name.split('.')
        var len = splitname.length
        var extension = splitname[len - 1]
        name = splitname.slice(0, len - 1).join('.');
        name += '_' + _date + '.' + extension;

        debugger
        return name;
    }
   



})