import Home from "@/pages/home/Home.tsx";

export default function CreateRoutes() {
    return [
        {
            path: "/",
            element: (<Home />),
            loggedIn: false,
        },
    ];
}