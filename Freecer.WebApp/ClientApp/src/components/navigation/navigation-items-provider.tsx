import {
    NavigationMenuContent,
    NavigationMenuItem,
    NavigationMenuTrigger
} from "@/components/ui/navigation-menu.tsx";
import LoginForm from "@/components/login-form.tsx";
import {useUser} from "@/common/hooks/useUser.ts";
import {Button} from "@/components/ui/button.tsx";
import {useAuth} from "@/common/hooks/useAuth.ts";

const MainPageNavigationItems = () => {
    return (
        <>
            <NavigationMenuItem>
                <Button variant={"outline"}>
                    <a href={"/"}>Home</a>
                </Button>
            </NavigationMenuItem>
            <NavigationMenuItem>
                <Button variant={"outline"}>
                    <a href={"/contact"}>Contact</a>
                </Button>
            </NavigationMenuItem>
        </>
    )
}

const LoggedInNavigationItems = () => {
    const {logout} = useAuth();
    return (
        <>
            <NavigationMenuItem>
                <a href={"/profile"}>Profile</a>
            </NavigationMenuItem>
            <NavigationMenuItem>
                <Button variant={"outline"} onClick={() => {
                    logout().then(() => window.location.reload())
                }}>Wyloguj</Button>
            </NavigationMenuItem>
        </>
    )
}

const LoggedOutNavigationItems = () => {
    return (
        <>
            <NavigationMenuItem>
                <NavigationMenuTrigger
                    className={"border border-input bg-background hover:bg-accent hover:text-accent-foreground"}>
                    Login</NavigationMenuTrigger>
                <NavigationMenuContent>
                    <ul>
                        <li className="row-span-3">
                            <LoginForm/>
                        </li>
                    </ul>
                </NavigationMenuContent>
            </NavigationMenuItem>
        </>
    )
}

const NavigationItemsProvider = () => {
    const {user} = useUser();

    return (
        <>
            {
                window.location.pathname === "/" ? <MainPageNavigationItems/> : null
            }
            {
                // Logged in/out
                !user ? <LoggedOutNavigationItems/> : <LoggedInNavigationItems/>
            }
        </>
    )

}

export default NavigationItemsProvider