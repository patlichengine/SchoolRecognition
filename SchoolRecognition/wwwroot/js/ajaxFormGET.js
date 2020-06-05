/* attach a submit handler to the form */
$('form').submit(function (e) {


    e.preventDefault();

    var _url = document.forms[0][name = "action"];


    $.ajax({
        url: _url,
        type: 'GET',
    })
        .done(function (result) {
            $('.content-wrapper').html(result);
            e.preventDefault();
        });

});