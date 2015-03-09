function listTags() {

    //$('list').remove();
    $('li[id^="tagList"]').remove();
    var ajaxHandler = $.ajax({
        Type: 'GET',
        url: tagUri,
    });
    ajaxHandler.done(function (result) {
        $.each(result, function (key, item) {
            // Add a list item for the product.
            $('<li>', { html: formatTag(item) }).attr("Id", "tagList" + item.ID).appendTo($('#div-listTag'));
        });
    });
    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        alert('Fail');
    });
    var button = document.getElementById("showTagListButton");
    button.disabled = true;
    button.setAttribute('class', 'btn-danger');

    setTimeout(function () {
        button.disabled = false;
        button.setAttribute('class', 'btn-primary');
    }, 3000)
}

function showListTags() {
    listTags();
    $('div[id^="div-"]').hide();
    $('#div-listTag').show();
    $('#div-searchTag').show();
    $('#div-specificTag').show();
}

function searchTagBox() {
    $('#globalSearchBoxButton').attr('onclick', "findTag();");
    $('#globalSearchBox').attr('placeholder', "Search for a Tag");
}

function formatTag(tag) {
    var text = 'Tag # ' + tag.ID + '\nName: ' + tag.Name;

    return text = text.replace(/\r?\n/g, '<br />');
}

function findTag() {


    var id = $('#globalSearchBox').val();
    $.getJSON(tagUri + '/' + id)
        .done(function (data) {

            $('#div-specificTagInfo').html(formatTag(data));

        })
        .fail(function (jqXHR, textStatus, err) {
            $('#div-specificTag').text('Error: ' + err);
        });
    $('#div-specificTagInfo').show();
    $('#div-changeTag').show();
}

function deleteTag() {
    var id = $('#deleteTagButton').attr('value');

    var ajaxHandler = $.ajax({
        type: 'DELETE',
        url: tagUri + '/' + id,
        dataType: 'json',
        data: {
        },
    });

    ajaxHandler.done(function (result) {

        $('li[id^="tagList' + id.toString() + '"]').remove();
        $('#div-listTags').html("");
        listGames();
        document.getElementById('tagId').value = '';
        $('#div-specificTagInfo').html("");

    });

    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        alert('Fail');
    });


}

function showEditTag() {

    $('div[id^="div-"]').hide();
    $('#div-editTag').show();

}

function editTag() {

    var id = $('#editGameButton').attr('value');

    var name = $('#gameTag').val();

    var ajaxHandler = $.ajax({
        type: 'Put',
        url: tagUri + '/' + id,
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
        alert('Fail');
    });

}

function showCreateTag() {
    $('div[id^="div-"]').hide();
    $('#div-createTag').show();
}

function createTag() {

    var name = $('#createTagName').val();
    console.log(name);



    var ajaxHandler = $.ajax({
        type: 'POST',
        url: tagUri,
        dataType: 'json',
        data: {
            'Name': name
        },
    });

    ajaxHandler.done(function (result) {

        $('<li>', { text: formatTag(result) }).id(result.ID).appendTo($('#div-listTag'));
    });

    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        alert('Fail');
    });

    $('div[id^="div-"]').hide();
    $('#div-listTag').show();
}