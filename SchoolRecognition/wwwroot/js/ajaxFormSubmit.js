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

        var formData = $('form').serializeObject();


        var _url = document.forms[0][name = "action"];

        /* get the action attribute from the <form action=""> element */


        $.ajax({
            url: _url,
            type: 'POST',
            beforeSend: function (xhr) {
                xhr.setRequestHeader("RequestVerificationToken",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: formData,
        })
        .done(function (result) {
        $('.content-wrapper').load(result);
        e.preventDefault();
    });

    });