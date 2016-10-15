// site.js
(function () {
    $('input:radio').click(function () {
        if ($(this).is(':checked')) {

            //select the main div wrapping this radio
            $(this).parent().addClass('.active');

            //or you can do
            //$(this).closest('.label_main').addClass('.label_main_selected');
        } else {
            $(this).parent().removeClass('.active');
            //or $(this).closest('.label_main').removeClass('.label_main_selected');
        }
    });

    var $sidebarAndWrapper = $("#sidebar, #wrapper");
    var $icon = $("#sidebarToggle i.fa");

    $("#sidebarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        } else {
            $icon.removeClass("fa-angle-right");
            $icon.addClass("fa-angle-left");
        }
    });
})();