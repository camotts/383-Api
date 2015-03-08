var userUri = '/api/users';
var gameUri = '/api/games';
var genreUri = '/api/genres';
var tagUri = '/api/tags';
var saleUri = '/api/sales';
var cartUri = '/api/carts';

$(document).ready(function () {

    $('div[id^="div-"]').hide();

    listUsers();

});

function formatItem(item) {
    return 'Email: ' + item.Email;
}

function formatSpecific(item) {
    return 'Customer email: ' + item.Email +
            '\n' + 'Customer Role: ' + item.Role;
}

function find() {
    
    var id = $('#userId').val();
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
            $('<li>', { text: formatItem(item) }).attr("Id", "userList" + item.ID).appendTo($('#div-listUser'));
            console.log(item);
            
            

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

function showEditUser() {

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


}