

$(document).ready(function () {
    loadSR();
});

function loadSR() {
    $(document).ready(function () {
        var options = {};
        options.url = "/RecognitionTypes/Get";
        options.type = "GET";
        options.dataType = "json";
        options.success = function (data) {
            data.forEach(function (element) {
                $("#recid").append("<option>"
                    + element.RecognitionTypeID + "</option>");
            });
        };
        options.error = function () {
            $("#msg").html("Error while calling the Web API!");
        };
        $.ajax(options);
    }
}