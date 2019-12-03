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
        app.prototype.highlight = function () {
            var preList = document.getElementsByTagName("pre");
            for (var i = 0; i < preList.length; i++) {
                hljs.highlightBlock(preList[i]);
            }
        };
        app.prototype.saveAsFile = function (filename, bytesBase64) {
            var link = document.createElement('a');
            link.download = filename;
            link.href = "data:application/octet-stream;base64," + bytesBase64;
            document.body.appendChild(link); // Needed for Firefox
            link.click();
            document.body.removeChild(link);
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