import './App.css'
import CreateRoutes from "./common/router/ReactRouter.tsx";
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import {ThemeProvider} from "@/components/theme-provider.tsx";
import MainNavigation from "@/components/navigation/main-navigation.tsx";
import {Toaster} from "@/components/ui/sonner.tsx";

const router = createBrowserRouter(CreateRoutes());

function App() {
    return (
        <ThemeProvider defaultTheme={"dark"} storageKey={"vite-ui-theme"}>
            <div className="flex flex-col text-foreground transition-colors mx-7">
                <MainNavigation/>
                <RouterProvider router={router}/>
                <Toaster/>
            </div>
        </ThemeProvider>
    );
}

export default App;
