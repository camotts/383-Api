var numTags;
var numGenres;
var count = 1;

var gameTags = function loadGameTags() {
    console.log("Hit in the loadGameTags Function")
    var r = $.Deferred();
    var ajaxHandler = $.ajax({
        Type: 'GET',
        url: tagUri,
    });
    ajaxHandler.done(function (result) {

        sessionStorage.setItem("numTags", result.length);


        $.each(result, function (key, item) {
            var str = item.Url;
            var split = str.split("/");
            var id = split[split.length - 1];
            // Add a list item for the product.
            $('<li>', { text: item.Name }).attr('value', item.Name).attr('id', "tagCheckList" + id).attr('class', "list-group-item").attr('style', "cursor: pointer;").appendTo($('#checkboxListOfTagsList'));


        });
    });
    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
    });
    console.log(sessionStorage.getItem("numTags"));
    setTimeout(function () {
        r.resolve();
    }, 200);

    return r;
}

var checkControl = function checkboxControl() {


    $('.list-group.checked-list-box .list-group-item').each(function () {




        // Settings

        var $widget = $(this),

            $checkbox = $('<input type="checkbox" class="hidden"/>');


        color = ($widget.data('color') ? $widget.data('color') : "primary"),

        style = ($widget.data('style') == "button" ? "btn-" : "list-group-item-"),

        settings = {

            on: {

                icon: 'glyphicon glyphicon-check'

            },

            off: {

                icon: 'glyphicon glyphicon-unchecked'

            }

        };



        $widget.css('cursor', 'pointer')

        $widget.append($checkbox);



        // Event Handlers

        $widget.on('click', function () {

            $checkbox.prop('checked', !$checkbox.is(':checked'));

            $checkbox.triggerHandler('change');

            updateDisplay();

        });

        $checkbox.on('change', function () {

            updateDisplay();

        });





        // Actions

        function updateDisplay() {

            var isChecked = $checkbox.is(':checked');



            // Set the button's state

            $widget.data('state', (isChecked) ? "on" : "off");



            // Set the button's icon

            $widget.find('.state-icon')

                .removeClass()

                .addClass('state-icon ' + settings[$widget.data('state')].icon);



            // Update the button's color

            if (isChecked) {

                $widget.addClass(style + color + ' active');

            } else {

                $widget.removeClass(style + color + ' active');

            }

        }



        // Initialization

        function init() {



            if ($widget.data('checked') == true) {

                $checkbox.prop('checked', !$checkbox.is(':checked'));

            }



            updateDisplay();



            // Inject the icon if applicable

            if ($widget.find('.state-icon').length == 0) {

                $widget.prepend('<span class="state-icon ' + settings[$widget.data('state')].icon + '" id="gameList' + count + '"></span>');

            }

        }

        init();
        count = count + 1;

        sessionStorage.setItem("count", count);

    });



    $('#get-checked-data').on('click', function (event) {

        event.preventDefault();

        var checkedItems = {}, counter = 0;

        $("#check-list-box li.active").each(function (idx, li) {

            checkedItems[counter] = $(li).text();

            counter++;

        });

        $('#display-json').html(JSON.stringify(checkedItems, null, '\t'));

    });

}



function searchGameBox() {
    $('#globalSearchBoxButton').attr('onclick', "findGame();");
    $('#globalSearchBox').attr('placeholder', "Search for a Game");
}

function listGames() {

    //$('list').remove();
    $('li[id^="gameList"]').remove();
    var ajaxHandler = $.ajax({
        Type: 'GET',
        url: gameUri,
    });
    ajaxHandler.done(function (result) {
        $.each(result, function (key, item) {
            var str = item.Url;
            var split = str.split("/");
            var id = split[split.length - 1];
            // Add a list item for the product.
            $('<div>').attr('class', "panel panel-default").attr('id', "game" + id + "depth1").attr('aria-expanded', "false").appendTo($('#gameListAccordion'));
            $('<div>').attr('class', "panel-heading").attr('role', "tab").attr('id', "game" + id + "depth2").attr('aria-expanded', "false").appendTo($('#game' + id + 'depth1'));
            $('<h4>').attr('class', "panel-title").attr('id', "game" + id + "depth3").attr('aria-expanded', "false").appendTo($('#game' + id + 'depth2'));
            $('<a>', { text: item.Name }).attr("data-toggle", "collapse").attr("data-parent", "#gameListAccordion").attr("href", "#collapseGame" + id).attr('aria-labelledby', "#game' + id + 'depth3").attr('aria-expanded', "false").attr('id', "game" + id + "depth4").appendTo($('#game' + id + 'depth3'));
            $('<div>').attr('id', "collapseGame" + id).attr('class', "panel-collapse collapse in").attr('role', "tabpanel").attr('aria-labeledby', "heading" + id).attr('aria-expanded', "false").appendTo($('#game' + id + 'depth1'));
            $('<div>', { html: formatGame(item) }).attr('class', "panel-body").attr('aria-expanded', "false").appendTo('#collapseGame' + id);
            $('<button>', { text: "Edit" }).attr('class', "float-right").attr('onclick', "showEditGame(" + id + ");").appendTo('#collapseGame' + id);
            $('<button>', { text: "Delete" }).attr('class', "float-right").attr('onclick', "deleteGame(" + id + ");").appendTo('#collapseGame' + id);

        });
    });
    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)

    });
    var button = document.getElementById("showGameListButton");
    button.disabled = true;
    button.setAttribute('class', 'btn-danger');

    setTimeout(function () {
        button.disabled = false;
        button.setAttribute('class', 'btn-primary');
    }, 3000)
}

function showListGames() {
    listGames();
    $('div[id^="div-"]').hide();
    $('#div-listGame').show();
}

function showSpecificGame() {
    $('div[id^="div-"]').hide();
    $('#div-specificGame').show();
    $('#div-specificGameInfo').show();
}

function formatGame(game) {
    var text = '\nName: ' + game.Name + '\n\tRelease Date: ' + game.ReleaseDate + '\n\tPrice: ' + game.Price;

    return text = text.replace(/\r?\n/g, '<br />');
}

function findGame() {

    var uri;
    var id = $('#globalSearchBox').val();
    if (isNaN(id)) {
        console.log("Oh, this is a string");
        uri = gameUri + '/?name=' + id;
    } else {
        console.log("Oh, this is a number");
        uri = gameUri + '/' + id;
    }
    console.log(uri);

    $.getJSON(uri)
        .done(function (data) {
            console.log(data);
            $.each(data, function (key, item) {

                var str = item.Url;
                var split = str.split("/");
                var id = split[split.length - 1];
                $('<div>', { html: formatSpecificGame(item) }).attr('class', "").attr('id', "div-specificGame' + id + 'Info").attr('aria-expanded', "false").appendTo($('#div-specificGameInfo'));
                //$('#div-specificGameInfo').append('<div>', { html: formatSpecificGame(item) }).attr('id', 'div-specificGame' + id + 'Info');
            });

        })
        .fail(function (jqXHR, textStatus, err) {
            $('#div-specificGame').text('Error: ' + err);
        });

    showSpecificGame()
}

function formatSpecificGame(game) {
    var text = '\nName: ' + game.Name + '\n\tRelease Date: ' + game.ReleaseDate + '\n\tPrice: ' + game.Price;

    return text = text.replace(/\r?\n/g, '<br />');
}

function deleteGame(id) {
    //var id = $('#deleteGameButton').attr('value');

    var ajaxHandler = $.ajax({
        type: 'DELETE',
        url: gameUri + '/' + id,
        dataType: 'json',
        data: {
        },
    });

    ajaxHandler.done(function (result) {

        $('li[id^="gameList' + id.toString() + '"]').remove();
        $('#div-listGames').html("");
        listGames();
        document.getElementById('gameId').value = '';
        $('#div-specificGameInfo').html("");

    });

    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)

    });


}

function showEditGame(id) {
    $('#editGameButton').attr('value', id);

    $('div[id^="div-"]').hide();
    $('#div-editGame').show();
    $('#div-checkboxListOfTags').show();
    gameTags().done(checkControl);

}

function editGame() {

    console.log(document.querySelectorAll('input[type="checkbox"]:checked').length);
    var id = $('#editGameButton').attr('value');

    var name = $('#gameName').val();
    var releasedate = $('#gameReleaseDate').val();
    var price = $('#gamePrice').val();
    var inventory = $('#gameInventoryAmmount').val();

    function findTag(id) {
        var tag = { ID: "", Name: "", Games: "" };
        console.log(tagUri + '/' + id);
        var ajaxHandler = $.ajax({
            Type: 'GET',
            url: tagUri + '/' + id,
            async: false,
            timeout: 20
        });
        ajaxHandler.done(function (data) {

            tag.Name = data.Name;
            console.log(tag);
            //return data;
        })
        ajaxHandler.fail(function (jqXHR, textStatus, err) {
            console.log(jqXHR)
            console.log(textStatus)
            console.log(err)
            console.log("Failed in the Find Tag");
        });

        return tag;
    }

    var result;
    var tags = [];
    for (var i = 1; i <= count; i++) {
        if ($('#gameList' + i).attr('class') === 'state-icon glyphicon glyphicon-check') {
            console.log(i);
            result = $('#tagCheckList' + i).attr('value');
            tags.push(result);
            console.log(result);

        }
    }


    console.log(tags);
    var ajaxHandler = $.ajax({
        type: 'Put',
        url: gameUri + '/' + id,
        dataType: 'json',
        async: false,
        data: {
            'ID': id,
            'Name': name,
            'ReleaseDate': releasedate,
            'Price': price,
            'InventoryCount': inventory,
            'Tags': tags,
            // 'Genres': null,
            timeout: 5000
        },
    });

    ajaxHandler.done(function (result) {
        console.log(result);
    });

    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
    });

    /*
    $('#div-listGame').html("");
    listGames();
    findGame();
    */
}

