import './App.css'
import CreateRoutes from "./common/router/ReactRouter.tsx";
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import {ThemeProvider} from "@/components/theme-provider.tsx";
import MainNavigation from "@/components/navigation/main-navigation.tsx";

const router = createBrowserRouter(CreateRoutes());

function App() {
    return (
        <ThemeProvider defaultTheme={"dark"} storageKey={"vite-ui-theme"}>
            <div className="flex flex-col min-h-screen bg-background text-foreground transition-colors">
                <MainNavigation/>
                <RouterProvider router={router}/>
            </div>
        </ThemeProvider>
    );
}

export default App;
