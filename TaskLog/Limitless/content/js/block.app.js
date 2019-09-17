var BlockUi = function () {
    var _componentBlockUi = function () {
        if (!$().block) {
            console.warn('Warning - blockui.min.js is not loaded.');
            return;
        }
    }

    return {
        init: function () {
            _componentBlockUi();
        },
        show: function (msg) {
            var block = $(this).closest('.card');
            $.blockUI({
                message: '<span class="font-weight-semibold"><i class="icon-spinner4 spinner mr-2"></i>&nbsp; ' + msg.text + '</span>',
                //timeout: 2000, //unblock after 2 seconds
                overlayCSS: {
                    backgroundColor: '#1b2024',
                    opacity: 0.8,
                    zIndex: 1200,
                    cursor: 'wait'
                },
                css: {
                    border: 0,
                    padding: 0,
                    zIndex: 1201,
                    color: '#fff',
                    '-webkit-border-radius': 3,
                    '-moz-border-radius': 3,
                    backgroundColor: 'transparent'
                }
            });
        }
    }
}();

document.addEventListener('DOMContentLoaded', function () {
    BlockUi.init();
});