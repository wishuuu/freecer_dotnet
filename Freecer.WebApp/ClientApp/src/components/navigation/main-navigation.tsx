"use client"

import {
    NavigationMenu, NavigationMenuItem, NavigationMenuList,
} from "@/components/ui/navigation-menu"
import NavigationItemsProvider from "@/components/navigation/navigation-items-provider.tsx";
import {ModeToggle} from "@/components/mode-toggle.tsx";

const MainNavigation = () => {

    return (
        <>
            <header
                className="flex h-20 w-full items-center">
                <div className="container flex h-20 px-4 items-center gap-2">
                    <p className={"flex items-center font-semibold"}>
                        Freecer
                    </p>
                    <nav className="ml-auto flex items-center space-x-4">
                        <NavigationMenu>
                            <div className={"ml-auto flex items-center space-x-4"}>
                                <NavigationMenuList>
                                    <NavigationItemsProvider/>
                                    <NavigationMenuItem>
                                        <ModeToggle/>
                                    </NavigationMenuItem>
                                </NavigationMenuList>
                            </div>
                        </NavigationMenu>
                    </nav>
                </div>
            </header>
        </>
    )
}

export default MainNavigation