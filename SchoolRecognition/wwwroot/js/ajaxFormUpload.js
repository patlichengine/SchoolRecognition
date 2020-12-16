$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};
/* attach a submit handler to the form */
$('form').submit(function (e) {


    e.preventDefault();

    var form = $('form');

    var formData = $('form').serializeObject();

    var fileUpload = $("#uploadedFile").get(0);
    var files = fileUpload.files;

    var _form_Data = new FormData();

    for (var key in formData) {
        _form_Data.append(key, formData[key]);
    }

    _form_Data.append("uploadedFile", files[0]);


    var _url = document.forms[0][name = "action"];

    /* get the action attribute from the <form action=""> element */
    //console.log(_form_Data);
    //console.log(form.validate().valid());

    if (form.validate().valid()) {

        $.ajax({
            url: _url,
            type: 'POST',
            processData: false,  // tell jQuery not to process the data
            contentType: false,  // tell jQuery not to set contentType
            beforeSend: function (xhr) {
                xhr.setRequestHeader("RequestVerificationToken",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: _form_Data,
        })
            .done(function (result) {
                $('.content-wrapper').load(result);
                e.preventDefault();
            });
    }

});