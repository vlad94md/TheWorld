// ap.js
(function start() {
    var $sidebarAndWrapper = $("#sidebar, #wrapper");

    $("#toggleMenu").on("click", function() {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        var $icon = $("#toggleMenu i.fa");

        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-chevron-left");
            $icon.addClass("fa-chevron-right");
        } else {
            $icon.removeClass("fa-chevron-right");
            $icon.addClass("fa-chevron-left");
        }
    });
})();
