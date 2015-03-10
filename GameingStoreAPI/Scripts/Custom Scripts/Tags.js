function listTags() {
    console.log("Please");
    //$('list').remove();
    $('li[id^="tagList"]').remove();
    var ajaxHandler = $.ajax({
        Type: 'GET',
        url: tagUri,
    });
    ajaxHandler.done(function (result) {
        $.each(result, function (key, item) {
            var str = item.Url;
            console.log(str);
            var split = str.split("/");
            var id = split[split.length - 1];
            console.log(id);
            // Add a list item for the product.
            $('<div>').attr('class', "panel panel-default").attr('id', "tag" + id + "depth1").attr('aria-expanded', "false").appendTo($('#tagListAccordion'));
            $('<div>').attr('class', "panel-heading").attr('role', "tab").attr('id', "tag" + id + "depth2").attr('aria-expanded', "false").appendTo($('#tag' + id + 'depth1'));
            $('<h4>').attr('class', "panel-title").attr('id', "tag" + id + "depth3").attr('aria-expanded', "false").appendTo($('#tag' + id + 'depth2'));
            $('<a>', { text: item.Name }).attr("data-toggle", "collapse").attr("data-parent", "#tagListAccordion").attr("href", "#collapseTag" + id).attr('aria-labelledby', '#tag' + id + 'depth3').attr('aria-expanded', "false").attr('id', "tag" + id + "depth4").appendTo($('#tag' + id + 'depth3'));
            $('<div>').attr('id', "collapseTag" + id).attr('class', "panel-collapse collapse in").attr('role', "tabpanel").attr('aria-labeledby', "heading" + id).attr('aria-expanded', "false").appendTo($('#tag' + id + 'depth1'));
            $('<div>').attr('class', "panel-body").attr('aria-expanded', "false").appendTo('#collapseTag' + id);
            $('<button>', { text: "Edit" }).attr('class', "float-right").attr('onclick', "showEditTag(" + id + ");").appendTo('#collapseTag' + id);
            $('<button>', { text: "Delete" }).attr('class', "float-right").attr('onclick', "deleteTag(" + id + ");").appendTo('#collapseTag' + id);

            //$('<li>', { html: formatTag(item) }).attr("Id", "tagList" + item.ID).appendTo($('#div-listTag'));
        });
    });
    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        console.log('Fail');
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
    var text = 'Games: ' + tag.Games;

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
        listTags();
        document.getElementById('tagId').value = '';
        $('#div-specificTagInfo').html("");

    });

    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        console.log('Fail');
    });


}

function showEditTag() {

    $('div[id^="div-"]').hide();
    $('#div-editTag').show();

}

function editTag() {

    var id = $('#editTagButton').attr('value');

    var name = $('#tagName').val();

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
        console.log('Fail');
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
        console.log('Fail');
    });

    $('div[id^="div-"]').hide();
    $('#div-listTag').show();
}