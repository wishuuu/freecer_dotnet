import {ComponentType} from "react";


interface RouteObject {
    path: string;
    component: ComponentType;
    loggedIn: boolean;
    authorization?: (roles: string[]) => boolean;
    moduleAccess?: string;
}

export default function CreateRoutes() : RouteObject[] {
    return [
        {
            path: "/",
            component: () => <div>Home</div>,
            loggedIn: false,
        },
    ];
}