$("a").on("click", function (event) {
    
    var link = $(this).attr('href');

    localStorage.setItem('lastLink', link);

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