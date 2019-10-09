namespace tasklog {
    class app {
        public showSidebar(): void {
            let body = document.getElementsByTagName("body")[0];
            body.classList.add("sidebar-mobile-component");
        }

        public hideSidebar(): void {
            let body = document.getElementsByTagName("body")[0];
            body.classList.remove("sidebar-mobile-component");
        }
    }

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