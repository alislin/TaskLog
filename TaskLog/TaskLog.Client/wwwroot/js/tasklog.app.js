var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
var tasklog;
(function (tasklog) {
    var app = /** @class */ (function () {
        function app() {
        }
        app.prototype.showSidebar = function () {
            var body = document.getElementsByTagName("body")[0];
            body.classList.add("sidebar-mobile-component");
        };
        app.prototype.hideSidebar = function () {
            var body = document.getElementsByTagName("body")[0];
            body.classList.remove("sidebar-mobile-component");
        };
        app.prototype.toggleSidebar = function () {
            var body = document.getElementsByTagName("body")[0];
            body.classList.toggle("sidebar-mobile-component");
        };
        return app;
    }());
    function Init() {
        var obj = {
            tasklogApp: new app()
        };
        if (window.ThunderBlazor) {
            window.ThunderBlazor = __assign(__assign({}, window.ThunderBlazor), obj);
        }
        else {
            window.ThunderBlazor = __assign({}, obj);
        }
    }
    tasklog.Init = Init;
})(tasklog || (tasklog = {}));
tasklog.Init();
//# sourceMappingURL=tasklog.app.js.map