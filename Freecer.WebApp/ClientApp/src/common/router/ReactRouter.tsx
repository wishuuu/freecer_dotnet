import Home from "@/pages/home/Home.tsx";
import Login from "@/pages/login/Login.tsx";

export default function CreateRoutes() {
    return [
        {
            path: "/",
            element: (<Home />),
            loggedIn: false,
        },
        {
            path: "/login",
            element: (<Login />),
            loggedIn: false,
        }
    ];
}