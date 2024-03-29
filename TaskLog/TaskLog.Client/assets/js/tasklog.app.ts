﻿namespace tasklog {
    class app {
        public showSidebar(): void {
            let body = document.getElementsByTagName("body")[0];
            body.classList.add("sidebar-mobile-component");
        }

        public hideSidebar(): void {
            let body = document.getElementsByTagName("body")[0];
            body.classList.remove("sidebar-mobile-component");
        }

        public toggleSidebar(): void {
            let body = document.getElementsByTagName("body")[0];
            body.classList.toggle("sidebar-mobile-component");
        }

        public highlight(): void {
            let preList = document.getElementsByTagName("pre");
            for (var i = 0; i < preList.length; i++) {
                hljs.highlightBlock(preList[i]);
            }
        }

        public saveAsFile(filename, bytesBase64) {
            var link = document.createElement('a');
            link.download = filename;
            link.href = "data:application/octet-stream;base64," + bytesBase64;
            document.body.appendChild(link); // Needed for Firefox
            link.click();
            document.body.removeChild(link);
        }
    }

    declare var hljs: any

    declare var window: Window & { ThunderBlazor: any }

    export function Init(): void {
        const obj = {
            tasklogApp: new app()
        };

        if (window.ThunderBlazor) {
            window.ThunderBlazor = { ...window.ThunderBlazor, ...obj };
        }
        else {
            window.ThunderBlazor = { ...obj };
        }
    }
}

tasklog.Init();