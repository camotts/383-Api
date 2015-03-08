window.numTags;
window.numGenres;
window.count = 1;

var gameTags = function loadGameTags() {

    var r = $.Deferred();
    var ajaxHandler = $.ajax({
        Type: 'GET',
        url: tagUri,
    });
    ajaxHandler.done(function (result) {

        window.numTags = result.length;
        
        $.each(result, function (key, item) {

            // Add a list item for the product.
            $('<li>',{ text: item.Name}).attr('class', "list-group-item").attr('style', "cursor: pointer;").appendTo($('#checkboxListOfTagsList'));


        });
    });
    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        alert('Fail');
    });

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

                $widget.prepend('<span class="state-icon ' + settings[$widget.data('state')].icon + '" id="gameList' + window.count + '"></span>');

            }

        }
        init();
        window.count += 1;

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

function findTag(id) {
    var tag;
    var ajaxHandler = $.ajax({
        Type: 'GET',
        url: tagUri + '/' + id,
        async : false
    });
    ajaxHandler.done(function (data) {
           
            tag = data;

        })
    ajaxHandler.fail(function (jqXHR, textStatus, err) {
            $('#div-specificGame').text('Error: ' + err);
    });

    return tag;
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
            // Add a list item for the product.
            $('<li>', { html: formatGame(item) }).attr("Id", "gameList" + item.ID).appendTo($('#div-listGame'));
        });
    });
    ajaxHandler.fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr)
        console.log(ajaxOptions)
        console.log(thrownError)
        alert('Fail');
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
    $('#div-searchGame').show();
    $('#div-specificGame').show();
}

function formatGame(game) {
    var text = 'Game # ' + game.ID + '\nName: ' + game.Name + '\n\tRelease Date: ' + game.ReleaseDate + '\n\tPrice: ' + game.Price;

    return text = text.replace(/\r?\n/g, '<br />');
}

function findGame() {


    var id = $('#gameId').val();
    $.getJSON(gameUri + '/' + id)
        .done(function (data) {

            $('#div-specificGameInfo').html(formatSpecificGame(data));
            $('#deleteGameButton').attr('value', data.ID.toString());
            $('#editGameButton').attr('value', data.ID.toString());

        })
        .fail(function (jqXHR, textStatus, err) {
            $('#div-specificGame').text('Error: ' + err);
        });
    $('#div-specificGameInfo').show();
    $('#div-changeGame').show();
}

function formatSpecificGame(game) {
    var text = 'Game # ' + game.ID + '\nName: ' + game.Name + '\n\tRelease Date: ' + game.ReleaseDate + '\n\tPrice: ' + game.Price;

    return text = text.replace(/\r?\n/g, '<br />');
}

function deleteGame() {
    var id = $('#deleteGameButton').attr('value');

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
        alert('Fail');
    });

    
}

function showEditGame() {

    $('div[id^="div-"]').hide();
    $('#div-editGame').show();
    $('#div-checkboxListOfTags').show();
    gameTags().done(checkControl);

}

function editGame() {

    alert(document.querySelectorAll('input[type="checkbox"]:checked').length);
    var id = $('#editGameButton').attr('value');

    var name = $('#gameName').val();
    var releasedate = $('#gameReleaseDate').val();
    var price = $('#gamePrice').val();
    var inventory = $('#gameInventoryAmmount').val();


    var tags = [];
    for (var i = 1; i <= window.numTags; i++) {
        if ($('#gameList' + i).attr('class') === 'state-icon glyphicon glyphicon-check') {
            var result = findTag(i);
            tags.push(result);
        }
    };
    var sentTags = JSON.stringify(tags);

    console.log(tags);

    var ajaxHandler = $.ajax({
        type: 'Put',
        url: gameUri + '/' + id,
        dataType: 'json',
        data: {
            'ID': id,
            'Name': name,
            'ReleaseDate': releasedate,
            'Price': price,
            'InventoryCount': inventory,
            'Tags': sentTags
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

    /*
    $('#div-listGame').html("");
    listGames();
    findGame();
    */
}

