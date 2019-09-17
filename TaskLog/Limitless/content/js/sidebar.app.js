var Sidebar = function () {
    return {
        showright: function () {
            if (!$('body').hasClass('sidebar-right-visible')) {
                $('body').addClass('sidebar-right-visible sidebar-mobile-right');
            }
        },
        hideright: function () {
            if ($('body').hasClass('sidebar-right-visible')) {
                $('body').removeClass('sidebar-right-visible')
                    .removeClass('sidebar-mobile-right');
            }
        },
        hidemain: function () {
            if (!$('body').hasClass('sidebar-main-hidden')) {
                $('body').addClass('sidebar-main-hidden');
            }
        },
        showmain: function () {
            if ($('body').hasClass('sidebar-main-hidden')) {
                $('body').removeClass('sidebar-main-hidden');
            }
        },
        togglemain: function () {
            this.showmain();
            $('body').toggleClass('sidebar-xs');
        }
    }
}();