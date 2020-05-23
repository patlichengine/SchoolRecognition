$("a").on("click", function (event) {
    
    var link = $(this).attr('href');
    console.log(link);
    if (link) {
        if (link == '#') {

        } else {
            //$('.content-wrapper').html(defaultHtml).show();
            $('.content-wrapper').load(link);
            event.preventDefault();
        }
    }
    //return false;
});