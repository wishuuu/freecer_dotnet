import {
    NavigationMenuContent,
    NavigationMenuItem,
    NavigationMenuTrigger
} from "@/components/ui/navigation-menu.tsx";
import LoginForm from "@/components/login-form.tsx";
import {useUser} from "@/common/hooks/useUser.ts";
import {Button} from "@/components/ui/button.tsx";
import {useAuth} from "@/common/hooks/useAuth.ts";


const NavigationItemsProvider = () => {
    const {user} = useUser();
    const {logout} = useAuth();


    if (!user) {
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
    } else {
        return (
            <>
                <NavigationMenuItem>
                    <Button variant={"outline"} onClick={() => {
                        logout().then(() => window.location.reload())
                    }}>Wyloguj</Button>
                </NavigationMenuItem>
            </>
        )
    }
}

export default NavigationItemsProvider