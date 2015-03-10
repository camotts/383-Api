﻿function listGenres() {

    //$('list').remove();
    $('li[id^="genreList"]').remove();
    var ajaxHandler = $.ajax({
        Type: 'GET',
        url: genreUri,
    });
    ajaxHandler.done(function (result) {
        $.each(result, function (key, item) {
            // Add a list item for the product.
            $('<div>').attr('class', "panel panel-default").attr('id', "genre" + item.Url.substr(item.Url.length - 1) + "depth1").attr('aria-expanded', "false").appendTo($('#genreListAccordion'));
            $('<div>').attr('class', "panel-heading").attr('role', "tab").attr('id', "genre" + item.Url.substr(item.Url.length - 1) + "depth2").attr('aria-expanded', "false").appendTo($('#genre' + item.Url.substr(item.Url.length - 1) + 'depth1'));
            $('<h4>').attr('class', "panel-title").attr('id', "genre" + item.Url.substr(item.Url.length - 1) + "depth3").attr('aria-expanded', "false").appendTo($('#genre' + item.Url.substr(item.Url.length - 1) + 'depth2'));
            $('<a>', { text: item.Name }).attr("data-toggle", "collapse").attr("data-parent", "#genreListAccordion").attr("href", "#collapse" + item.Url.substr(item.Url.length - 1)).attr('aria-labelledby', "heading" + item.Url.substr(item.Url.length - 1)).attr('aria-expanded', "false").attr('id', "genre" + item.Url.substr(item.Url.length - 1) + "depth4").appendTo($('#genre' + item.Url.substr(item.Url.length - 1) + 'depth3'));
            $('<div>').attr('id', "collapse" + item.Url.substr(item.Url.length - 1)).attr('class', "panel-collapse collapse in").attr('role', "tabpanel").attr('aria-labeledby', "heading" + item.Url.substr(item.Url.length - 1)).attr('aria-expanded', "false").appendTo($('#genre' + item.Url.substr(item.Url.length - 1) + 'depth1'));
            $('<div>', { html: formatGenre(item) }).attr('class', "panel-body").attr('aria-expanded', "false").appendTo('#collapse' + item.Url.substr(item.Url.length - 1));
            $('<button>', { text: "Edit" }).attr('class', "float-right").attr('onclick', "showEditGenre(" + item.Url.substr(item.Url.length - 1) + ");").appendTo('#collapse' + item.Url.substr(item.Url.length - 1));
            $('<button>', { text: "Delete" }).attr('class', "float-right").attr('onclick', "deleteGenre(" + item.Url.substr(item.Url.length - 1) + ");").appendTo('#collapse' + item.Url.substr(item.Url.length - 1));
            //$('<li>', { html: formatGenre(item) }).attr("Id", "genreList" + item.ID).appendTo($('#div-listGenre'));
        });
    });
    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        console.log('Fail');
    });
    var button = document.getElementById("showGenreListButton");
    button.disabled = true;
    button.setAttribute('class', 'btn-danger');

    setTimeout(function () {
        button.disabled = false;
        button.setAttribute('class', 'btn-primary');
    }, 3000)
}

function showListGenre() {
    listGenres();
    $('div[id^="div-"]').hide();
    $('#div-listGenre').show();
    $('#div-searchGenre').show();
    $('#div-specificGenre').show();
}

function searchGenreBox() {
    $('#globalSearchBoxButton').attr('onclick', "findGenre();");
    $('#globalSearchBox').attr('placeholder', "Search for a Genre");
}

function formatGenre(genre) {
    var text = 'Name: ' + genre.Name;

    return text = text.replace(/\r?\n/g, '<br />');
}

function findGenre() {


    var id = $('#globalSearchBox').val();
    $.getJSON(genreUri + '/' + id)
        .done(function (data) {

            $('#div-specificGenreInfo').html(formatGenre(data));

        })
        .fail(function (jqXHR, textStatus, err) {
            $('#div-specificGenre').text('Error: ' + err);
        });
    $('#div-specificGenreInfo').show();
    $('#div-changeGenre').show();
}

function deleteGenre() {
    var id = $('#deleteGenreButton').attr('value');

    var ajaxHandler = $.ajax({
        type: 'DELETE',
        url: genreUri + '/' + id,
        dataType: 'json',
        data: {
        },
    });

    ajaxHandler.done(function (result) {

        $('li[id^="genreList' + id.toString() + '"]').remove();
        $('#div-listGenres').html("");
        listGenres();
        document.getElementById('genreId').value = '';
        $('#div-specificGenreInfo').html("");

    });

    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        console.log('Fail');
    });


}

function showEditGenre() {

    $('div[id^="div-"]').hide();
    $('#div-editGenre').show();

}

function editGenre() {

    var id = $('#editGenreButton').attr('value');

    var name = $('#genreName').val();

    var ajaxHandler = $.ajax({
        type: 'Put',
        url: genreUri + '/' + id,
        dataType: 'json',
        data: {
            'ID': id,
            'Name': name,
        },
    });

    ajaxHandler.done(function (result) {
        console.log(result);
    });

    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        console.log('Fail');
    });

}

function showCreateGenre() {
    $('div[id^="div-"]').hide();
    $('#div-createGenre').show();
}

function createGenre() {

    var name = $('#createGenreName').val();
    console.log(name);



    var ajaxHandler = $.ajax({
        type: 'POST',
        url: genreUri,
        dataType: 'json',
        data: {
            'Name': name
        },
    });

    ajaxHandler.done(function (result) {

        $('<li>', { text: formatGenre(result) }).id(result.ID).appendTo($('#div-listGenre'));
    });

    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        console.log('Fail');
    });

    $('div[id^="div-"]').hide();
    $('#div-listGenre').show();
}