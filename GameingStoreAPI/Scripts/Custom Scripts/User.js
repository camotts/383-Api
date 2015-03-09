var userUri = '/api/users';
var gameUri = '/api/games';
var genreUri = '/api/genres';
var tagUri = '/api/tags';
var saleUri = '/api/sales';
var cartUri = '/api/carts';

$(document).ready(function () {

    $('div[id^="div-"]').hide();

    listUsers();
    searchUserBox();
});

function searchUserBox() {
    $('#globalSearchBoxButton').attr('onclick', "find();");
    $('#globalSearchBox').attr('placeholder', "Search for a User");
}

function formatItem(item) {
    return 'Email: ' + item.Email;
}

function formatSpecific(item) {
    return 'Customer Role: ' + item.Role;
}

function find() {
    
    var id = $('#globalSeachBox').val();
    $.getJSON(userUri + '/' + id)
        .done(function (data) {

            $('#div-specificUser').text(formatSpecific(data));
            $('#deleteButton').attr( 'value', data.ID.toString());
            $('#editButton').attr('value', data.ID.toString());
            $('#emailEdit').text(data.Email);
            document.getElementById("roleEdit").selectedIndex = data.Role;

        })
        .fail(function (jqXHR, textStatus, err) {
            $('#div-specificUser').text('Error: ' + err);
        });
    $('#div-deleteButton').show();
    $('#div-deleteUser').show();
    
}

function listUsers() {

    
    showListUsers();
    

    var ajaxHandler = $.ajax({
        Type: 'GET',
        url: userUri,
    });
    ajaxHandler.done(function (result) {

        $.each(result, function (key, item) {            
            // Add a list item for the product.
            $('<div>').attr('class', "panel panel-default").attr('id', "user" + item.Url.substr(item.Url.length - 1) + "depth1").attr('aria-expanded', "false").appendTo($('#userListAccordion'));
            $('<div>').attr('class', "panel-heading").attr('role', "tab").attr('id', "user" + item.Url.substr(item.Url.length - 1) + "depth2").attr('aria-expanded', "false").appendTo($('#user' + item.Url.substr(item.Url.length - 1) + 'depth1'));
            $('<h4>').attr('class', "panel-title").attr('id', "user" + item.Url.substr(item.Url.length - 1) + "depth3").attr('aria-expanded', "false").appendTo($('#user' + item.Url.substr(item.Url.length - 1) + 'depth2'));
            $('<a>', { text: item.Email }).attr("data-toggle", "collapse").attr("data-parent", "#userListAccordion").attr("href", "#collapse" + item.Url.substr(item.Url.length - 1)).attr('aria-labelledby', "heading" + item.Url.substr(item.Url.length - 1)).attr('aria-expanded', "false").attr('id', "user" + item.Url.substr(item.Url.length - 1) + "depth4").appendTo($('#user' + item.Url.substr(item.Url.length - 1) + 'depth3'));
            $('<div>').attr('id', "collapse" + item.Url.substr(item.Url.length - 1)).attr('class', "panel-collapse collapse in").attr('role', "tabpanel").attr('aria-labeledby', "heading" + item.Url.substr(item.Url.length - 1)).attr('aria-expanded', "false").appendTo($('#user' + item.Url.substr(item.Url.length - 1) + 'depth1'));
            $('<div>', { text: formatItem(item) }).attr('class', "panel-body").attr('aria-expanded', "false").appendTo('#collapse' + item.Url.substr(item.Url.length - 1));
            $('<button>', { text: "Edit" }).attr('class', "float-right").attr('onclick', "showEditUser(" + item.Url.substr(item.Url.length - 1) + ");").appendTo('#collapse' + item.Url.substr(item.Url.length - 1));
            $('<button>', { text: "Delete" }).attr('class', "float-right").attr('onclick', "deleteUser(" + item.Url.substr(item.Url.length - 1) + ");").appendTo('#collapse' + item.Url.substr(item.Url.length - 1));
            

        });
    });
    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        alert('Fail');
    });
}

function showCreateUser() {
    $('div[id^="div-"]').hide();
    $('#div-createUser').show();
}

function createUser() {

    var email = $('#email').val();
    var password = $('#password').val();
    var role = $('#role').val();
    var apiKey = "123";

    

    var ajaxHandler = $.ajax({
        type: 'POST',
        url: userUri,
        dataType: 'json',
        data: {
            'email': email.toString(),
            'password': password.toString(),
            'role': role,
            'APIKey':apiKey
        },
    });

    ajaxHandler.done(function (result){

        $('<li>', { text: formatItem(result) }).id(result.ID).appendTo($('#div-listUser'));
        deleteButton.attr("Id", "button" + result.ID).appendTo('#list' + result.ID);
    });

    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        alert('Fail');
    });

    $('div[id^="div-"]').hide();
    $('#div-listUser').show();
}

function showListUsers() {
    $('div[id^="div-"]').hide();
    $('#div-specificUser').show();
    $('#div-searchUser').show();
    $('#div-listUser').show();
}

function deleteUser() {
    var id = $('#deleteButton').attr('value');

    var ajaxHandler = $.ajax({
        type: 'DELETE',
        url: userUri+'/' + id,
        dataType: 'json',
        data: {
        },
    });

    ajaxHandler.done(function (result) {

        $('list').remove();
    });

    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        alert('Fail');
    });
    
    $('#div-listUser').html("");
    listUsers();
}

function showEditUser(id) {
    $('#editButton').attr('value', id);
    $('div[id^="div-"]').hide();
    $('#div-editUser').show();
}

function editUser() {
    var id = $('#editButton').attr('value');

    var email = $('#emailEdit').val();
    var password = $('#passwordEdit').val();

    var role = $('#roleEdit').val();
    var apiKey = "123";

    var ajaxHandler = $.ajax({
        type: 'Put',
        url: userUri + '/' + id,
        dataType: 'json',
        data: {
            'id':id,
            'email': email.toString(),
            'password': password.toString(),
            'role': role,
            'APIKey': apiKey
        },
    });

    ajaxHandler.done(function (result) {
        console.log(result);
    });

    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        alert('Fail');
    });


    $('#div-listUser').html("");
    listUsers();
    find();
}

function showLogin() {
    $('div[id^="div-"]').hide();
    $('#div-loginUser').show();
}

function loginUser() {
    var uri = '/api/APIKey';
    var email = $('#emailLogin').val();
    var password = $('#passwordLogin').val();

    var ajaxHandler = $.ajax({
        Type: 'GET',
        url: uri + '?email=' + email + '&password=' + password,
    });
    ajaxHandler.done(function (result) {

        var cookie = document.cookie;
        console.log(cookie);
        console.log("logged in?");
        sessionStorage.setItem("tokenKey", result.xcmps383authenticationid);
        sessionStorage.setItem("tokenId", result.idcookie);
        console.log(result.xcmps383authenticationid);


        var req = new XMLHttpRequest();
        console.log(req.getResponseHeader("xcmps383authenticationid"));
        console.log(req.getResponseHeader("xcmps383authenticationkey"));

        req.open('GET', document.location, false);

        req.send(null);

        var headers = req.getAllResponseHeaders();

        alert(headers);
    });
    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        alert('Fail');
    });

}