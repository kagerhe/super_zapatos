var uri = "http://localhost:56028/services/stores";


$(document).ready(function () {

    GetAll();
    $('#addStore').on('click', function () {
        CreateStore();
    })
});

function GetAll() {
    // Send an AJAX request

    $('#listStore tbody').html('');
    $.getJSON(uri)
        .done(function (data) {
            var store = data.stores
            $.each(store, function (key, item) {
                $('#table').append(formatItemTable(item));
            });
        })
        .fail(function (jqXHR, textStatus, err) {
            console.log(err)
        });
}

//function GetAll() {
//    $('#listStore tbody').html('');
//    $.ajax({
//        url: uri,
//        type: 'GET',
//        contenttype: 'application/json',
//        success: function (data) {
//            alert(data);
//        },
//        beforeSend: function (xhr) {
//            xhr.setRequestHeader("Authorization",
//                "Basic " + btoa("my_user" + ":" + "my_password"));
//        }
//    });
//}
//function GetAll() {
//    // Send an AJAX request
//    $('#listStore tbody').html('');
//    $.ajax
//        ({
//            type: "GET",
//            url: uri,
//            headers: {
//                "Authorization": "basic " + btoa("my_user" + ":" + "my_password")
//            },
//            success: function () {
//                alert('Thanks for your comment!');
//            }
//        });
   
//}

function formatItemTable(item) {
    var td = "<tr><td>" + item.Id
        + "</td><td>" + item.Name
        + "</td><td>" + item.Address
        + "</td><td>" + formatButton(item.Id)
        + "</td></tr>"
    return td
}

function formatButton(valor) {
    var btnEdit = "<a href='javascript:void(0);' onclick='UpdateStore(" + valor + ")'  class='btn btn-primary btn-md'><i class='fa fa-pencil'></i> Edit</a>"
    var btnRemove = "<a href='javascript:void(0);' onclick='DeleteStoreById(" + valor + ")'  class='btn btn-danger btn-md' style='margin-left:5px; margin-rigth:0'><i class='fa fa-trash'></i> Delete</a>"
    return btnEdit + btnRemove
}

alertify.defaults.transition = "slide";

function DeleteStoreById(id) {
    alertify.defaults.theme.ok = "btn btn-danger";
    alertify.defaults.theme.cancel = "btn btn-primary";
    alertify.confirm('Alert Title', function () {
        deleteStore(id)
        redireccionar();
        alertify.success('Removed');
    }, function () {
        redireccionar();
        alertify.error('Declined');
    }).set({
        labels: { ok: 'Delete', cancel: 'Cancel' }
    }).setContent("You want to delete this store")
        .setHeader("Update Store")
        .setting({
            'pinnable': true,
            'modal': true,
            'closable': false
        });
}

alertify.defaults.theme.ok = "btn btn-success";
alertify.defaults.theme.cancel = "btn btn-danger";

function UpdateStore(id) {
    alertify.confirm('Alert Title', function () {
        editStore(id, GetData())
        redireccionar();
        alertify.success('Accepted ');
    }, function () {
        redireccionar();
        alertify.error('Declined');
    }).set({
        labels: { ok: 'Accept', cancel: 'Cancel' }
    }).setContent($('#formStore')[0])
        .setHeader("Update Store")
        .setting({
            'pinnable': true,
            'modal': true,
            'closable': false
        });
    findStore(id)
}

function CreateStore() {
    alertify.confirm('Alert Title', 'Alert Message!', function () {
        addStore(GetData())
        alertify.success('Accepted');
    }, function () {
        redireccionar();
        alertify.error('Declined');
    }).set({
        labels: { ok: 'Accept', cancel: 'Cancel' }
    })
        .setContent($('#formStore')[0])
        .setHeader("Create Store")
        .setting({
            'pinnable': true,
            'modal': true,
            'closable': false
        });
}

function findStore(id) {
    $.ajax({
        url: uri + '/' + id,
        method: "get"
    }).done(function (data) {
        formatItem(data.store);
    });

}

function addStore(store) {
    $.ajax({
        url: uri + '/Add',
        method: 'POST',
        dataType: "json",
        data: store
    }).done(function (data) {
        redireccionar();
    });
}

function editStore(id, store) {
    $.ajax({
        url: uri + '/Edit/' + id,
        type: 'POST',
        data: store
    })
        .done(function (data, textStatus, jqXHR) {
            redireccionar();
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            alert("error");
        })

        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {

            if (textStatus === 'success') {
                var jqXHR = jqXHR_errorThrown;
            } else {
                var jqXHR = data_jqXHR;
            }
            var data = jqXHR.responseJSON;

            switch (jqXHR.status) {
                case 200:
                    console.log(200);
                    break
                case 201: onsole.log(201);
                    break
                case 401: onsole.log(401);
                    break
                default:
                    console.log(data);
                    break;
            }
        });   
}

function deleteStore(id) {
    $.ajax({
        url: uri + '/Delete/' + id,
        method: "post"
    })
        .done(function (data, textStatus, jqXHR) {
            redireccionar();
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            alert("error");
        })

        .always(function (data_jqXHR, textStatus, jqXHR_errorThrown) {

            if (textStatus === 'success') {
                var jqXHR = jqXHR_errorThrown;
            } else {
                var jqXHR = data_jqXHR;
            }
            var data = jqXHR.responseJSON;

            switch (jqXHR.status) {
                case 200:
                    console.log(200);
                    break
                case 201: onsole.log(201);
                    break
                case 401: onsole.log(401);
                    break
                default:
                    console.log(data);
                    break;
            }

        });
}

function GetData() {
    var store = {
        Id: $('#id').val(),
        Name: $('#name').val(),
        Address: $('#address').val()
    }
    return store
}

function ClearForm() {
    $('#id').val('')
    $('#name').val('')
    $('#address').val('')
}

function formatItem(item) {
    $('#id').val(item.Id)
    $('#name').val(item.Name)
    $('#address').val(item.Address)
}

function redireccionar() {
    location.href = window.location.pathname;
}
