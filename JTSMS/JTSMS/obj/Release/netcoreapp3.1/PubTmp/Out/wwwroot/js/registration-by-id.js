$(document).ready(function () {
    //$('body').off('click', '#btn-search-input').on('click', '#btn-search-input', Search_Number);
    //$('body').off('click', '#btn-select').on('click', '#btn-select', Select);
    $('body').off('click', '#btn-add').on('click', '#btn-add', AddAssy);
    $('body').off('click', '#btn-submit').on('click', '#btn-submit', Submit);
    $('body').off('click', '#btn-approve').on('click', '#btn-approve', Approve);
    $('body').off('click', '#btn-reject').on('click', '#btn-reject', Reject);



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


    GetAssyList = function () {
        var arr_assy = new Array();
        var arr = $('[class="assy"]')
        for (var i = 0; i < arr.length; i++) {
            arr_assy.push(arr[i].innerHTML)
        }
        return arr_assy;
    }
    GetEvidenceList = function (date) {
        var arr_evidence = new Array();
        var arr = $('[class="evidence"]')
        for (var i = 0; i < arr.length; i++) {
            arr_evidence.push(ChangeFileName(arr[i].innerHTML, date))
        }
        return arr_evidence;
    }
    function AddAssy() {
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
                data = response.results;
                if (!data) {
                    insertAssy(input);
                }
                else {
                    var text = "";
                    for (var i = 0; i < data.length; i++) {
                        text += data[i].type + "-" + data[i].status + "-" + data[i].regId + "\n"
                        debugger
                        //console.log(JSON.stringify(data[i]))
                    }                   
                    bootbox.confirm({
                        message: `The assy ${input} already recorded in the database.
                                    ${text}
                                   Do you want to add this Assy to the request`,
                        callback: function (result) {
                            if (result) {
                                insertAssy(input);
                                bootbox.alert('added successfully');
                            }
                            else
                                bootbox.alert(`skipped the assy ${input}`);
                        }
                    });
                }
            }
        })
    }

    function insertAssy(input) {
        let tbody = document.getElementById('tbody')
        table = tbody.innerHTML;
        let newRow = `<tr>
                    <td class="assy">${input}</td>
                    <td><button class="destroy">Delete</button> </td>
                    </tr>`
        table += newRow
        tbody.innerHTML = table;

        $('#txt-assy').val('');
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
        var assyList = GetAssyList()
        len = assyList.length;
        if ($('#frm-submit').valid()) {
            if (len) {

                var getDate = new Date();
                var date = getDate.getFullYear().toString() + (getDate.getMonth() + 1) + getDate.getDate() + getDate.getHours() + getDate.getMinutes() + getDate.getSeconds() + getDate.getMilliseconds();
                //var assyList = GetAssyList()
                var evidenceList = GetEvidenceList(date)
                var custName = $('#txt-custname').val();
                var station = $('#txt-station').val();

                var model = new Object();
                model.RegId = parseInt($(this).attr('data-regid'));
                model.TypeId = parseInt($(this).attr('data-typeid'));
                model.ScriptName = $("#txt-scriptname").val();
                model.ScriptRev = $('#txt-scriptrev').val();
                model.PCNorDevNumber = $('#txt-PCNorDevNumber').val();
                model.ChangeDetail = $('#txt-changeDetail').val();
                model.AssemblyNumber = assyList.join('|');
                model.Description = $('#txt-description').val();
                model.CreatedBy = user
                model.CreatedName = name
                model.CreatedEmail = email
                model.OriginalFileName = ChangeFileName($('#txt-original')[0].files[0].name, date)
                model.EncryptedFileName = ChangeFileName($('#txt-encrypted')[0].files[0].name, date)
                model.Evidence = evidenceList.join('|');
                //model.File = $('#txt-encrypted')[0].files[0] ///can add to the JS model but cannot transfer this to controller
                model.FileHash = $('#txt-encrypted').attr('data-hash').toUpperCase();
                $.ajax({
                    url: '/Registration/Registration_submit',
                    type: 'post',
                    data: JSON.stringify(model),
                    dataType: 'json',
                    contentType: 'application/json;charset=uft-8',
                    cache: true,
                    success: function (response) {
                        results = response.results

                        if (results.statusCode == 200) {
                            var elm_file = $('[type="file"]')
                            for (var i = 0; i < elm_file.length; i++) {
                                var type = elm_file[i].name
                                files = $(`#txt-${type}`)[0].files
                                for (var j = 0; j < assyList.length; j++) {
                                    Upload(files, type, date, custName, station, assyList[j])

                                }
                            }
                            bootbox.alert(results.message, function () { location.reload() })
                        }
                        else bootbox.alert(results.message)
                    }
                })
            }
            else
                bootbox.alert('Please input Assembly Number');
        }
    }

    function Upload(_files, _type, _date, _custName, _station, _assembly) {
        //var files = $('#txt-encrypted').get(0).files;
        var formData = new FormData();
        for (var i = 0; i < _files.length; i++) {
            formData.append('model.files', _files[i]);
        }
        formData.append('model.type', _type);
        formData.append('model.date', _date);
        formData.append('model.CustName', _custName);
        formData.append('model.Station', _station);
        formData.append('model.Assembly', _assembly.trim());

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

        var splitname = _file.split('.')
        var len = splitname.length
        var extension = splitname[len - 1]
        name = splitname.slice(0, len - 1).join('.');
        name += '_' + _date + '.' + extension;

        return name;
    }

    function Approve() {
        var model = new Object();

        var model = new Object();
        model.RegId = parseInt($(this).attr('data-regid'));
        model.ScriptId = $('#txt-scriptid').val();
        model.RouteId = parseInt($(this).attr('data-routeid'));
        model.Remark = $('#txt-remark').val();
        model.Action = "Approve";
        model.CreatedBy = user
        model.CreatedName = name
        model.CreatedEmail = email

        $.ajax({
            type: 'post',
            url: '/Registration/Registration_approve',
            data: JSON.stringify(model),
            cache: false,
            dataType: 'json',
            contentType: 'application/json;charset=utf-8',
            success: function (response) {
                var statusCode = response.results.statusCode;
                var message = response.results.message;

                if (statusCode == 200) {
                    bootbox.alert(message, function () { location.reload() });
                }
                else {
                    bootbox.alert(message);
                }
            },
            error: function (e) {
                bootbox.alert(e.responseText);
            }
        })

    }

    function Reject() {
        var model = new Object();

        var model = new Object();
        model.RegId = parseInt($(this).attr('data-regid'));
        model.ScriptId = $('#txt-scriptid').val();
        model.RouteId = parseInt($(this).attr('data-routeid'));
        model.Remark = $('#txt-remark').val();
        model.Action = "Reject";
        model.CreatedBy = user
        model.CreatedName = name
        model.CreatedEmail = email

        $.ajax({
            type: 'post',
            url: '/Registration/Registration_reject',
            data: JSON.stringify(model),
            cache: false,
            dataType: 'json',
            contentType: 'application/json;charset=utf-8',
            success: function (response) {
                var statusCode = response.results.statusCode;
                var message = response.results.message;

                if (statusCode == 200) {
                    bootbox.alert(message, function () { location.reload() });
                }
                else {
                    bootbox.alert(message);
                }
            },
            error: function (e) {
                bootbox.alert(e.responseText);
            }
        })

    }

})