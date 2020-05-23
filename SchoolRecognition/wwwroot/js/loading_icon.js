$(document).on('invalid-form.validate', 'form', function () {
    var buttons = $('button[type="submit"]');
    setTimeout(function () {
        buttons.removeAttr('disabled');
    }, 1);
});
$(document).on('submit', 'form', function () {
    var buttons = $('button[type="submit"]');
    setTimeout(function () {
        $('#loading_icon').show();
        buttons.attr('disabled', 'disabled');
    }, 0);
});