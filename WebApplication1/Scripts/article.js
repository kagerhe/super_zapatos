var uri = "http://localhost:56028/services/articles";


$(function () {

    GetAll();
    $('#addnewArticle').on('click', function () {
        CreateArticle();
    })
});

function GetAll() {
    // Send an AJAX request
    $('#listArticle tbody').html('');
    $.getJSON(uri)
        .done(function (data) {
            console.log(data)
            var articles = data.articles
            $.each(articles, function (key, item) {
                $('#table').append(formatItemTable(item));
            });
        })
        .fail(function (jqXHR, textStatus, err) {
            console.log(err)
        });
}


function GetAllStore(id) {
    $.getJSON("http://localhost:56028/services/stores")
        .done(function (data) {
            var store = data.stores
            $.each(store, function (key, item) {
                $('#selectStore').append(formatItemSelect(item, id))
            });
        })
        .fail(function (jqXHR, textStatus, err) {
            console.log(err)
        });
}


function formatItemSelect(item, id) {
    var option
    if (id === item.Id)
        option = "<option value=" + item.Id + "  selected='selected'>" + item.Name + " - " + item.Address + "</option>"
    else
        option = "<option value=" + item.Id + ">" + item.Name + " - " + item.Address + "</option>"
    return option
}

function formatItemTable(item) {
    var td = "<tr><td>" + item.Id
        + "</td><td>" + item.Name
        + "</td><td>" + item.Description
        + "</td><td>" + item.Price
        + "</td><td>" + item.Store.Name
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

function DeleteArticleById(id) {
    alertify.defaults.theme.ok = "btn btn-danger";
    alertify.defaults.theme.cancel = "btn btn-primary";
    alertify.confirm('Alert Title', function () {
        deleteArticle(id)
        redireccionar();
        alertify.success('Removed');
    }, function () {
        redireccionar();
        alertify.error('Declined');
    }).set({
        labels: { ok: 'Delete', cancel: 'Cancel' }
    }).setContent("You want to delete this Article")
        .setHeader("Update Store")
        .setting({
            'pinnable': true,
            'modal': true,
            'closable': false
        });
}

alertify.defaults.theme.ok = "btn btn-success";
alertify.defaults.theme.cancel = "btn btn-danger";

function UpdateArticle(id) {

    alertify.confirm('Alert Title', function () {
        editArticle(id, GetData())
        redireccionar();
        alertify.success('Accepted ');
    }, function () {
        redireccionar();
        alertify.error('Declined');
    }).set({
        labels: { ok: 'Accept', cancel: 'Cancel' }
    }).setContent($('#formStore')[0])
        .setHeader("Update Article")
        .setting({
            'pinnable': true,
            'modal': true,
            'closable': false
        });
    findStore(id)
}

function CreateArticle() {
    alertify.confirm('Alert Title', function () {
        addArticle(GetData())
        alertify.success('Accepted');
    }, function () {
        redireccionar();
        alertify.error('Declined');
    }).set({
        labels: { ok: 'Accept', cancel: 'Cancel' }
    })
        .setContent($('#formStore')[0])
        .setHeader("Create Article")
        .setting({
            'pinnable': true,
            'modal': true,
            'closable': false
        });
    GetAllStore(0)
    $('#idn').val(1)
}

function findStore(id) {
    $.ajax({
        url: uri + '/' + id,
        type: 'GET',
        dataType: "json",
        success: function (data) {
            formatItem(data.article);
        }
    });

}

function addArticle(article) {
    $.ajax({
        url: uri + '/Add',
        type: 'POST',
        crossDomain: true,
        dataType: "json",
        data: article,
        statusCode: {
            200: function (resp) {
                console.log(resp)
                alertify.success('Article added successfully');
                redireccionar();
            },
            404: function () {
                alert(404)
            },
            400: function () {
                alert(400)
            }
        }
    });
}

function editArticle(id, article) {
    $.ajax({
        url: uri + '/Edit/' + id,
        type: 'POST',
        crossDomain: true,
        contentType: 'application/json;chartset=utf-8',
        data: article,
        statusCode: {
            200: function () {
                ClearForm();
                alertify.success('Article edited successfully');
                redireccionar();
            },
            404: function () {
                alert(404)
            },
            400: function () {
                alert(400)
            }
        }
    });
}

function deleteArticle(id) {
    $.ajax({
        url: uri + '/Delete/' + id,
        type: 'POST',
        contentType: 'application/json;chartset=utf-8',
        statusCode: {
            200: function (resp) {
                ClearForm();
                alertify.error('Article removed successfully');
                redireccionar();
            },
            404: function () {
                alert(404)
            },
            400: function () {
                alert(400)
            }
        }
    });
}

function GetData() {
    alert($('#selectStore').val())
    var article = {
        Id: $('#id').val(),
        StoreId: $('#selectStore').val(),
        Store: {
            Id: $('#selectStore').val(),
            Name: "Prueba"

        },
        Name: $('#name').val(),
        Description: $('#description').val(),
        Price: $('#price').val(),
        Total_In_Shelf: $('#totalInShelf').val(),
        Total_In_Vault: $('#totalInVault').val()
    }
    return article
}

function ClearForm() {
    $('#id').val('')
    $('#selectStore').val(0)
    $('#name').val('')
    $('#description').val('')
    $('#price').val('')
    $('#totalInShelf').val('')
    $('#totalInVault').val('')
}

function formatItem(item) {
    $('#id').val(item.Idn)
    $('#name').val(item.Name)
    $('#description').val(item.Description)
    $('#price').val(item.Price)
    $('#totalInShelf').val(item.Total_In_Shelf)
    $('#totalInVault').val(item.Total_In_Vault)
    GetAllStore(item.StoreId)
}

function redireccionar() {
    location.href = window.location.pathname;
}

