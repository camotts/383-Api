var uri = '/api/users';
$(document).ready(function () {

    $('div[id^="div-"]').hide();
    console.log("on load");
    listUsers();

});

function formatItem(item) {
    return 'Customer ' + item.ID + ' email: ' + item.Email;
}

function formatSpecific(item) {
    return 'Customer email: ' + item.Email +
            '\n' + 'Customer Role: ' + item.Role;
}

function find() {
    


    console.log("Search?")
    var id = $('#userId').val();
    $.getJSON(uri + '/' + id)
        .done(function (data) {

            console.log(data);
            $('#div-specificUser').text(formatSpecific(data));

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
        url: uri,
    });
    ajaxHandler.done(function (result) {
        console.log("agh");
        $.each(result, function (key, item) {            
            
            // Add a list item for the product.
            $('<li>', { text: formatItem(item) }).attr("Id", "list" + item.ID).appendTo($('#div-listUser'));
            
            
            

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
    console.log("here");
    var email = $('#email').val();
    var password = $('#password').val();
    var role = $('#role').val();
    var apiKey = "123";

    

    var ajaxHandler = $.ajax({
        type: 'POST',
        url: uri,
        dataType: 'json',
        data: {
            'email': email.toString(),
            'password': password.toString(),
            'role': role,
            'APIKey':apiKey
        },
    });

    ajaxHandler.done(function (result){
        console.log(result);
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

function deleteUser(id) {
    /*var ajaxHandler = $.ajax({
        type: 'DELETE',
        url: uri,
        dataType: 'json',
        data: {
        },
    });

    ajaxHandler.done(function (result) {
        console.log(result);
        $('list').remove();
    });

    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        alert('Fail');
    });
    */
}